using GameOfPebblesGUI;
using GameOfPebblesLibrary;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace GameUI
{
    public partial class CreateGameForm : Form
    {
        private const int MAX_NUMBER_OF_PILES = 5;
        private readonly int[] MAX_PILE_SIZES = {1000, 100, 50, 10, 5, 3, 2, 2};
        private HomeForm ParentHomeForm { get; set; }
        private List<Move> SymetricMoveList { get; set; }
        private List<Move> AsymetricMoveList { get; set; }
        private List<List<Tuple<Dimensions, string>>> ExtraTerminalStates { get; set; }
        private GameParameters gameParameters { get; set; }
        public CreateGameForm(HomeForm parentHomeForm)
        {
            InitializeComponent();
            // Parent form
            this.ParentHomeForm = parentHomeForm;
            // Symetric and Asymetric moves
            SymetricMoveList = new();
            AsymetricMoveList = new();
            ExtraTerminalStates = new()
            {
                new(),
                new()
            };
            // Moveset
            this.MovesetComboBox.Items.Add("Custom");
            this.MovesetComboBox.Items.Add("Nim");
            this.MovesetComboBox.SelectedIndex = 0;
            // Symetric/Asymteric Combo Box
            this.SymetricMovesComboBox.Items.Add("Symetric");
            this.SymetricMovesComboBox.Items.Add("Asymetric");
            this.SymetricMovesComboBox.SelectedIndex = 0;
            // Who Goes First Combo Box
            this.WhoGoesFirstComboBox.Items.Add("Player");
            this.WhoGoesFirstComboBox.Items.Add("AI");
            this.WhoGoesFirstComboBox.SelectedIndex = 0;
            // Table Filling Algorithms Combo Box
            this.TableFillingAlgorithmComboBox.Items.Add("Tabulation Filler");
            this.TableFillingAlgorithmComboBox.Items.Add("Extended Tabulation Filler");
            this.TableFillingAlgorithmComboBox.Items.Add("Forward Filler");
            this.TableFillingAlgorithmComboBox.Items.Add("Extended Forward Filler - Check All States");
            this.TableFillingAlgorithmComboBox.Items.Add("Extended Forward Filler - Check Border States");
            // Default Radio Button Options
            this.PvPRadioButton.Checked = true;
            this.LowIntelligenceRadioButton.Checked = true;
            this.YesLastMoveWinsRadioButton.Checked = true;
            this.NoExtraTerminalStatesRadioButton.Checked = true;
        }
        private bool ValidateGameName()
        {
            bool result = true;
            List<string> GameFileNames = new();
            foreach(string file in Directory.GetFiles(Paths.GameTemplatesFolderPath, "*.json"))
            {
                GameFileNames.Add(Path.GetFileName(file));
            }
            string[] specialCharacters = {"/", "\\", ":", "*", "?", "\"", "<", ">", "|"};
            string gameNameStr = this.NameOfTheGameTextBox.Text;
            if (string.IsNullOrEmpty(gameNameStr))
            {
                MessageBox.Show("No name povided for the game!");
                result = false;
                return result;
            }
            foreach (string c in specialCharacters)
            {
                if (Regex.IsMatch(gameNameStr, Regex.Escape(c)))
                {
                    MessageBox.Show($"The filename cannot contain special characters like '{c}'");
                    result = false;
                    return result;
                }
            }
            if (gameNameStr.Length > 50)
            {
                MessageBox.Show("The game name cannot be more than 50 characters!");
                result = false;
                return result;
            }
            if(GameFileNames.Contains(gameNameStr + ".json")) 
            {
                MessageBox.Show("The game with this name already exists! Choose a different name!");
                result = false;
                return result;
            }
            return result;
        }
        private bool ValidateForm()
        {
            bool result = true;
            if (this.MovesetComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("No moveset selected! Choose a moveset first!");
                result = false;
                return result;
            }
            if ((string)this.MovesetComboBox.SelectedItem == "Custom" &&
                ((string)this.SymetricMovesComboBox.SelectedItem == "Symetric" && this.SymetricMoveList.Count == 0 ||
                    (string)this.SymetricMovesComboBox.SelectedItem == "Asymetric" && this.AsymetricMoveList.Count == 0))
            {
                MessageBox.Show("No moves provided! Click on the Moves button to add moves to the moveset!");
                result = false;
                return result;
            }
            if (this.YesExtraTerminalStatesRadioButton.Checked == true && this.ExtraTerminalStates.Count == 0) 
            {
                MessageBox.Show("No terminal states provided! Click on the Terminal States button to add extra terminal states!");
                result = false;
                return result;
            }
            if (this.TableFillingAlgorithmComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("No table filler algorithm selected! Choose a table filler algorithm first!");
                result = false;
                return result;
            }
            if(!ValidateGameName())
            {
                result = false;
                return result;
            }
            return result;
        }
        public void SetSymetricMoves(List<Move> moves)
        {
            this.SymetricMoveList = moves;
        }
        public void SetAsymetricMoves(List<Move> moves)
        {
            this.AsymetricMoveList = moves;
        }
        public void SetExtraTerimnalStates(List<List<Tuple<Dimensions, string>>> termminalStates)
        {
            this.ExtraTerminalStates = termminalStates;
        }
        private void CreateGame()
        {
            if (ValidateForm())
            {
                GameModel game;
                List<int> dimensions = new();
                Dimensions gameDimensions;
                List<IPlayer> players = new ();
                string moveSetLabel;
                List<Move> moves = new();
                bool symetric = true;
                bool lastMoveWins = false;
                List<List<Tuple<int, string>>> terminalStates = new();
                int statesPerPlayer = 1;
                // DIMENSIONS
                foreach (var control in this.PileSizesPanel.Controls)
                {
                    NumericUpDown pileSizeNumericUpDown = (NumericUpDown)control;
                    dimensions.Add((int)pileSizeNumericUpDown.Value);
                }
                dimensions.Add(1);
                gameDimensions = new(null, dimensions);
                // PLAYERS
                if (this.PvPRadioButton.Checked == true)
                {
                    players.Add(new PlayerHuman(0, null));
                    players.Add(new PlayerHuman(1, null));
                }
                else
                {
                    PlayerAI.Intelligence intelligence = PlayerAI.Intelligence.LOW;
                    if (this.LowIntelligenceRadioButton.Checked == true) 
                    {
                        intelligence = PlayerAI.Intelligence.LOW;
                    }
                    if (this.MidIntelligenceRadioButton.Checked == true)
                    {
                        intelligence = PlayerAI.Intelligence.MID;
                    }
                    if (this.HighIntelligenceRadioButton.Checked == true)
                    {
                        intelligence = PlayerAI.Intelligence.HIGH;
                    }
                    if ((string)this.WhoGoesFirstComboBox.SelectedItem == "Player")
                    {
                        players.Add(new PlayerHuman(0, null));
                        players.Add(new PlayerAI(1, null, intelligence));
                    }
                    else
                    {
                        players.Add(new PlayerAI(0, null, intelligence));
                        players.Add(new PlayerHuman(1, null));
                    }
                }
                // Moveset Label
                string moveDebugStr = "";
                switch ((string)this.MovesetComboBox.SelectedItem)
                {
                    case "Custom":
                        moveSetLabel = "custom";
                        if((string)this.SymetricMovesComboBox.SelectedItem == "Symetric")
                        {
                            foreach (var move in this.SymetricMoveList)
                            {
                                move.Modifiers.Add(1);
                                moveDebugStr += move + "\n";
                            }
                            moves = this.SymetricMoveList;
                        }
                        else
                        {
                            foreach (var move in this.AsymetricMoveList)
                            {
                                move.Modifiers.Add(1);
                                moveDebugStr += move + "\n";
                            }
                            symetric = false;
                            moves = this.AsymetricMoveList;
                        }
                        break;
                    case "Nim":
                        moveSetLabel = "nim";
                        break;
                    default:
                        moveSetLabel = "custom";
                        if ((string)this.SymetricMovesComboBox.SelectedItem == "Symetric")
                        {
                            moves = this.SymetricMoveList;
                        }
                        else
                        {
                            symetric = false;
                            moves = this.AsymetricMoveList;
                        }
                        break;
                }
                // Last Move Wins and Terminal States
                for (int dimensionIdx = 0; dimensionIdx < dimensions.Count - 1; dimensionIdx++)
                {
                    statesPerPlayer *= (dimensions[dimensionIdx] + 1);
                }
                terminalStates.Add(new());
                terminalStates.Add(new());
                if (this.YesLastMoveWinsRadioButton.Checked == true)
                {
                    lastMoveWins = true;
                    terminalStates[0].Add(new(0, "N"));
                    terminalStates[1].Add(new(statesPerPlayer, "N"));
                }
                else
                {
                    terminalStates[0].Add(new(0, "Y"));
                    terminalStates[1].Add(new(statesPerPlayer, "Y"));
                }
                if (this.YesExtraTerminalStatesRadioButton.Checked == true)
                {
                    for (int playerIdx = 0; playerIdx < this.ExtraTerminalStates.Count; playerIdx++)
                    {
                        foreach (var stateOutcomePair in this.ExtraTerminalStates[playerIdx])
                        {
                            int stateIdx = gameDimensions.MapDimensionsToIndex(stateOutcomePair.Item1);
                            terminalStates[playerIdx].Add(new(stateIdx, stateOutcomePair.Item2));
                        }
                    }
                }
                // Constructing the game's parameters
                GameParameters gameParameters = new(
                   dimensions,
                   players,
                   moveSetLabel,
                   moves,
                   symetric,
                   lastMoveWins,
                   terminalStates,
                   null
                   );
                // SAVING THE GAMESTATES
                switch ((string)this.TableFillingAlgorithmComboBox.SelectedItem)
                {
                    case "Tabulation Filler":
                        game = new(gameParameters, "tabulation");
                        break;
                    case "Extended Tabulation Filler":
                        game = new(gameParameters, "etabulation");
                        break;
                    case "Forward Filler":
                        game = new(gameParameters, "forward");
                        break;
                    case "Extended Forward Filler - Check All States":
                        game = new(gameParameters, "eforwardall");
                        break;
                    case "Extended Forward Filler - Check Border States":
                        game = new(gameParameters, "eforwardborder");
                        break;
                    default:
                        game = new(gameParameters, "tabulation");
                        break;
                }
                if (((string)TableFillingAlgorithmComboBox.SelectedItem == "Forward Filler" ||
                    (string)TableFillingAlgorithmComboBox.SelectedItem == "Tabulation Filler") &&
                    game.GameEngine.AtLeastOneUndecidedState())
                {
                    MessageBox.Show("Warning! " +
                        "This tablefiller algorithm may not have filled the table properly!" +
                        "Choose another one of the extended algorithms to make sure the table is filled!");
                }
                gameParameters.GameStates = game.GameEngine.GameStates;
                try
                {
                    string titleOfTheGame = this.NameOfTheGameTextBox.Text;
                    GameTemplate templateToSave = new(titleOfTheGame, gameParameters, null);
                    string templateToSaveJson = JsonConvert.SerializeObject(templateToSave);
                    File.WriteAllText(Paths.GameTemplatesFolderPath + titleOfTheGame + @".json", templateToSaveJson);
                    MessageBox.Show("New game succesfuly created!");
                    this.ParentHomeForm.UpdateGamesListBox();
                    this.ParentHomeForm.Show();
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong when saving the game!");
                }
            }
        }
        private void AddNewPileNumericUpDown()
        {
            NumericUpDown newPileSizeNumericDropdown = new();
            newPileSizeNumericDropdown.Width = 60;
            int x = this.PileSizesPanel.Controls.Count > 0 ?
                this.PileSizesPanel.Controls[PileSizesPanel.Controls.Count - 1].Right + 10 : 0;
            int y = this.PileSizesPanel.Controls.Count > 0 ?
                this.PileSizesPanel.Controls[PileSizesPanel.Controls.Count - 1].Top : 15;
            newPileSizeNumericDropdown.Location = new Point(x, y);
            this.PileSizesPanel.Controls.Add(newPileSizeNumericDropdown);
            newPileSizeNumericDropdown.Minimum = 1; 
        }
        private void UpdateMaximumPileSizes()
        {
            int numOfPiles = this.PileSizesPanel.Controls.Count - 1;
            int newMaximum = this.MAX_PILE_SIZES[numOfPiles];
            foreach (var control in PileSizesPanel.Controls)
            {
                NumericUpDown pileSizeNumericUpDown = (NumericUpDown)control;
                pileSizeNumericUpDown.Maximum = newMaximum;
                if (pileSizeNumericUpDown.Value > newMaximum)
                    pileSizeNumericUpDown.Value = newMaximum;
            }
        }
        private void UpdatePileSizePanelSizeAndPosition()
        {            
            this.PileSizesPanel.Width = this.PileSizesPanel.Controls[this.PileSizesPanel.Controls.Count - 1].Right;
            this.PileSizesPanel.Height = this.PileSizesPanel.Controls[this.PileSizesPanel.Controls.Count - 1].Bottom;
            Point newPostion = new((this.ClientSize.Width - this.PileSizesPanel.Width) / 2, this.PileSizesPanel.Location.Y);
            this.PileSizesPanel.Location = newPostion;
        }
        private void SetLocationOfMinusPlusPileSizeButtons()
        {
            this.AddPileButton.Location = new Point(
                this.PileSizesPanel.Location.X + this.PileSizesPanel.Width + 10,
                this.AddPileButton.Location.Y
                );
            this.RemovePileButton.Location = new Point(
                this.PileSizesPanel.Location.X - (this.RemovePileButton.Width + 10),
                this.AddPileButton.Location.Y
                );
        }
        private void RemovePileNumericUpDown()
        {
            int numOfPileSizeNumericUpdowns = this.PileSizesPanel.Controls.Count;
            if (numOfPileSizeNumericUpdowns >= 2)
            {
                this.PileSizesPanel.Controls.RemoveAt(numOfPileSizeNumericUpdowns - 1);
            }         
        }
        private void CreateGameButton_Click(object sender, EventArgs e)
        {
            CreateGame();
        }
        private void HideAndShowMinusPlusPileSizeButtons()
        {
            int numOfPileSizeNumericDropdowns = this.PileSizesPanel.Controls.Count;
            switch (numOfPileSizeNumericDropdowns)
            {
                case 1:
                    this.RemovePileButton.Visible = false; // Hide the minus button if there is exactly one pile size numeric updown
                    break;
                case 2:
                    this.RemovePileButton.Visible = true; // Show the minus button if there is at least one pile size numeric updown
                    break;
                case MAX_NUMBER_OF_PILES - 1:
                    this.AddPileButton.Visible = true; // Show the plus button if there are less than the maximum number of numeric updown
                    break;
                case MAX_NUMBER_OF_PILES:
                    this.AddPileButton.Visible = false; // Hide the plus button if there are exactly the maximum number of numeric updown
                    break;
                default:
                    break;
            }   
        }
        private void AddPileButton_Click(object sender, EventArgs e)
        {
            AddNewPileNumericUpDown();
            HideAndShowMinusPlusPileSizeButtons();
            UpdatePileSizePanelSizeAndPosition(); // Updates the size of the panel to fit all controls
            SetLocationOfMinusPlusPileSizeButtons();
            UpdateMaximumPileSizes();
            this.SymetricMoveList = new();
            this.AsymetricMoveList = new();
            this.ExtraTerminalStates = new()
            {
                new(),
                new()
            };
        }
        private void RemovePileButton_Click(object sender, EventArgs e)
        {
            RemovePileNumericUpDown();
            HideAndShowMinusPlusPileSizeButtons();
            UpdatePileSizePanelSizeAndPosition(); // Updates the size of the panel to fit all controls
            SetLocationOfMinusPlusPileSizeButtons();
            UpdateMaximumPileSizes();
            this.SymetricMoveList = new();
            this.AsymetricMoveList = new();
            this.ExtraTerminalStates = new()
            {
                new(),
                new()
            };
        }
        private void MovesetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.MovesetComboBox.SelectedItem != null && (string)this.MovesetComboBox.SelectedItem == "Custom")
            {
                this.SymetricMovesLabel.Visible = true;
                this.SymetricMovesComboBox.Visible = true;
                this.MovesButton.Visible = true;
            } 
            else
            {
                this.SymetricMovesLabel.Visible = false;
                this.SymetricMovesComboBox.Visible = false;
                this.MovesButton.Visible = false;    
            }
        }
        private void PvERadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(this.PvERadioButton.Checked == true)
            {
                this.WhoGoesFirstLabel.Visible = true;
                this.WhoGoesFirstComboBox.Visible = true;
                this.AI_IntelligenceGroupBox.Visible = true;
            }
            else
            {
                this.WhoGoesFirstLabel.Visible = false;
                this.WhoGoesFirstComboBox.Visible = false;
                this.AI_IntelligenceGroupBox.Visible = false;
            }
        }
        private void YesExtraTerminalStatesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.YesExtraTerminalStatesRadioButton.Checked == true)
                this.TerminalStatesButton.Visible = true;
            else
                this.TerminalStatesButton.Visible = false;
        }
        private List<int> GetPileSizesFromNumericUpDowns()
        {
            List<int> pileSizes = new();
            foreach (var control in this.PileSizesPanel.Controls)
            {
                NumericUpDown pileSizeNumericUpDown = (NumericUpDown)control;
                pileSizes.Add((int)pileSizeNumericUpDown.Value);
            }
            return pileSizes;
        }
        private void MovesButton_Click(object sender, EventArgs e)
        {
            List<int> pileSizes = GetPileSizesFromNumericUpDowns();
            if ((string)this.SymetricMovesComboBox.SelectedItem == "Symetric")
            {
                CustomSymetricMovesForm customSymetricMovesForm = new(this, pileSizes, this.SymetricMoveList);
                customSymetricMovesForm.Show();
            } 
            else
            {
                CustomAsymetricMovesForm customAsymetricMovesForm = new(this, pileSizes, this.AsymetricMoveList);
                customAsymetricMovesForm.Show();
            }
        }
        private void TerminalStatesButton_Click(object sender, EventArgs e)
        {
            List<int> pileSizes = GetPileSizesFromNumericUpDowns();
            TerminalStatesAsymetricForm terminalStatesAsymetricForm = new(this, pileSizes, this.ExtraTerminalStates);
            terminalStatesAsymetricForm.Show();
        }
        private void CreateGameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ParentHomeForm.Show();
        }
    }
}
