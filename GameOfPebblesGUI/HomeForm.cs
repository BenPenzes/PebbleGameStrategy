using GameUI;
using Newtonsoft.Json;
using StrategyAndStatisticsLibrary;
using GameOfPebblesLibrary;

namespace GameOfPebblesGUI
{
    public partial class HomeForm : Form
    {
        private GameTemplate? SelectedGameTemplate { get; set; }
        private string? SelectedGameStr { get; set; }
        private string[]? GameFileNames { get; set; }
        public HomeForm()
        {
            InitializeComponent();
            UpdateGamesListBox();
        }
        public GameTemplate LoadGameTemplate()
        {
            string gameTemplateFilePath;
            string gameTemplateJsonString;
            if (this.GamesListBox.SelectedItem != null) 
                this.SelectedGameStr = this.GamesListBox.SelectedItem.ToString();
            gameTemplateFilePath = Paths.GameTemplatesFolderPath + SelectedGameStr + ".json";
            gameTemplateJsonString = File.ReadAllText(gameTemplateFilePath);
            GameTemplate loadedGameTemplate = JsonConvert.DeserializeObject<GameTemplate>(gameTemplateJsonString);
            return loadedGameTemplate;
        }
        private void NewGameButton_Click(object sender, EventArgs e)
        {
            CreateGameForm createGameForm = new(this);   
            createGameForm.Show();
            this.Hide();
        }
        private void DeleteGameButton_Click(object sender, EventArgs e)
        {
            int selectedGameIndex = this.GamesListBox.SelectedIndex;
            if (selectedGameIndex == -1)
            {
                MessageBox.Show("Please select a game to delete!");
                return;
            }
            string filePath = Paths.GameTemplatesFolderPath + (string)this.GamesListBox.SelectedItem + ".json";
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    this.GamesListBox.Items.RemoveAt(selectedGameIndex);
                    this.GamesListBox.SelectedIndex = -1;
                    MessageBox.Show("Game Succesfuly deleted!");
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
            }
        }
        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (this.GamesListBox.SelectedItem != null)
            {
                this.SelectedGameTemplate = LoadGameTemplate();
                PlayGameForm playGameForm = new(this, this.SelectedGameTemplate, 30); // last parameter is the turn timer (s)
                if (!playGameForm.IsDisposed)
                {
                    playGameForm.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Choose a game first to play it!");
            }
        }
        private void PrintStrategyTableButtton_Click(object sender, EventArgs e)
        {
            if(this.GamesListBox.SelectedItem != null)
            {
                GameModel loadedGame;
                this.SelectedGameTemplate = LoadGameTemplate();
                loadedGame = new GameModel(this.SelectedGameTemplate.GameParameters, "");
                string strategyTableFileName = (string)this.GamesListBox.SelectedItem + "_StrategyTable";
                TablePrinter tablePrinter = new(loadedGame.GameEngine, Paths.GameStrategyTablesFolderPath, strategyTableFileName, ";");
                tablePrinter.PrintExcel();
                MessageBox.Show("Startegy table succefuly printed!");
            } 
            else
            {
                MessageBox.Show("Choose a game first to print the strategy table!");
            }
        }
        private void SaveStatisticsButton_Click(object sender, EventArgs e)
        {
            if(GamesListBox.SelectedItem != null)
            {
                string str = "";
                GameModel loadedGame;
                this.SelectedGameTemplate = LoadGameTemplate();
                loadedGame = new GameModel(this.SelectedGameTemplate.GameParameters, "");
                str += this.SelectedGameTemplate;
                str += new GameStatistics(loadedGame.GameEngine, false).ToString();
                string gameStatisticFileName = (string)this.GamesListBox.SelectedItem + "_Statistics.txt";
                string filePath = Paths.GameStatisticsFolderPath + gameStatisticFileName;
                using (StreamWriter writer = new(filePath))
                {
                    writer.WriteLine(str);
                }
                MessageBox.Show("Game statistics successfully printed!");
            } 
            else
            {
                MessageBox.Show("Choose a game first to print the game statistics!");
            }
        }
        public void UpdateGamesListBox()
        {
            this.GamesListBox.Items.Clear();
            this.GameFileNames = Directory.GetFiles(Paths.GameTemplatesFolderPath, "*.json");
            foreach (var fileName in this.GameFileNames)
            {
                this.GamesListBox.Items.Add(Path.GetFileName(fileName)[..^5]); // [..^5] trims the .json extension from the name
            }
        }
        private void UpdateGamePreviewPanel()
        {
            // declaring game preview elements
            string pileSizesStr = "Piles (";
            Label titleOfTheGameLabel = new();
            Label pilesLabel = new();
            Label movesetLabel = new();
            ComboBox? customMovesPlayer1ComboBox = null;
            ComboBox? customMovesPlayer2ComboBox = null;
            Label whatVsWhatLabel = new();
            Label lastMoveWinsLabel = new();
            Label extraTerminalStatesLabel = new();
            int x, y;
            // centering the elements horizontally
            titleOfTheGameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            titleOfTheGameLabel.TextAlign = ContentAlignment.MiddleCenter;
            pilesLabel.Anchor = titleOfTheGameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            pilesLabel.TextAlign = ContentAlignment.MiddleCenter;
            movesetLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            movesetLabel.TextAlign = ContentAlignment.MiddleCenter;
            whatVsWhatLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            whatVsWhatLabel.TextAlign = ContentAlignment.MiddleCenter;
            lastMoveWinsLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            lastMoveWinsLabel.TextAlign = ContentAlignment.MiddleCenter;
            extraTerminalStatesLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            extraTerminalStatesLabel.TextAlign = ContentAlignment.MiddleCenter;
            // font sizes and styles
            titleOfTheGameLabel.Font = new Font(titleOfTheGameLabel.Font.FontFamily, 16, FontStyle.Bold);
            titleOfTheGameLabel.Height = 40;
            pilesLabel.Font = new Font(titleOfTheGameLabel.Font.FontFamily, 14);
            pilesLabel.Height = 40;
            movesetLabel.Font = new Font(titleOfTheGameLabel.Font.FontFamily, 12);
            movesetLabel.Height = 40;
            whatVsWhatLabel.Font = new Font(titleOfTheGameLabel.Font.FontFamily, 12);
            whatVsWhatLabel.Height = 40;
            lastMoveWinsLabel.Font = new Font(titleOfTheGameLabel.Font.FontFamily, 12);
            lastMoveWinsLabel.Height = 40;
            extraTerminalStatesLabel.Font = new Font(titleOfTheGameLabel.Font.FontFamily, 12);
            extraTerminalStatesLabel.Height = 40;
            // setting the sizes of the labels
            titleOfTheGameLabel.Width = this.GamePreviewPanel.Width - 2;
            pilesLabel.Width = this.GamePreviewPanel.Width - 2;
            movesetLabel.Width = this.GamePreviewPanel.Width - 2;
            whatVsWhatLabel.Width = this.GamePreviewPanel.Width - 2;
            lastMoveWinsLabel.Width = this.GamePreviewPanel.Width - 2;
            extraTerminalStatesLabel.Width = this.GamePreviewPanel.Width - 2;
            this.SelectedGameTemplate = LoadGameTemplate();
            titleOfTheGameLabel.Text = this.SelectedGameTemplate.GameTitle;
            for (int dimensionIdx = 0; dimensionIdx < this.SelectedGameTemplate.GameParameters.Dimensions.Count - 1; dimensionIdx++)
            {
                pileSizesStr += this.SelectedGameTemplate.GameParameters.Dimensions[dimensionIdx] + ", ";
            }
            pileSizesStr = pileSizesStr[..^2];
            pileSizesStr += ")";
            pilesLabel.Text = pileSizesStr;
            if(this.SelectedGameTemplate.GameParameters.MoveSetLabel == "nim")
                movesetLabel.Text = "Nim";
            else
            {
                movesetLabel.Text = "Custom moves";
                customMovesPlayer1ComboBox = new();
                for (int moveIdx = 0; 
                    moveIdx < this.SelectedGameTemplate.GameParameters.MovesPerPlayer; 
                    moveIdx++)
                {
                    customMovesPlayer1ComboBox.Items.Add(this.SelectedGameTemplate.GameParameters.Moves[moveIdx]);
                }
                if (!this.SelectedGameTemplate.GameParameters.Symetric)
                {
                    customMovesPlayer2ComboBox = new();
                    for (int moveIdx = this.SelectedGameTemplate.GameParameters.MovesPerPlayer; 
                        moveIdx < 2 * this.SelectedGameTemplate.GameParameters.MovesPerPlayer; 
                        moveIdx++)
                    {
                        customMovesPlayer2ComboBox.Items.Add(this.SelectedGameTemplate.GameParameters.Moves[moveIdx]);
                    }
                }
            }
            if (this.SelectedGameTemplate.GameParameters.PlayerList[0] is PlayerAI || this.SelectedGameTemplate.GameParameters.PlayerList[1] is PlayerAI)
                whatVsWhatLabel.Text = "PvE";
            else
                whatVsWhatLabel.Text = "PvP";
            if (this.SelectedGameTemplate.GameParameters.LastMoveWins)
                lastMoveWinsLabel.Text = "Last Move Wins!";
            else
                lastMoveWinsLabel.Text = "Last Move Loses!";
            if (this.SelectedGameTemplate.GameParameters.TerminalStates[0].Count > 1 || this.SelectedGameTemplate.GameParameters.TerminalStates[1].Count > 1)
                lastMoveWinsLabel.Text = "Extra Terminal States!";
            else
                lastMoveWinsLabel.Text = "No Extra Terminal States!";
            // Add the labels to the panel
            this.GamePreviewPanel.Controls.Clear();
            this.GamePreviewPanel.Controls.Add(titleOfTheGameLabel);
            this.GamePreviewPanel.Controls.Add(pilesLabel);
            x = this.GamePreviewPanel.Controls[this.GamePreviewPanel.Controls.Count - 1].Location.X;
            y = this.GamePreviewPanel.Controls[this.GamePreviewPanel.Controls.Count - 1].Bottom + 10;
            pilesLabel.Location = new Point(x, y);
            this.GamePreviewPanel.Controls.Add(movesetLabel);
            x = this.GamePreviewPanel.Controls[this.GamePreviewPanel.Controls.Count - 1].Location.X;
            y = this.GamePreviewPanel.Controls[this.GamePreviewPanel.Controls.Count - 1].Bottom + 10;
            movesetLabel.Location = new Point(x, y);
            if (customMovesPlayer1ComboBox != null) // checks if game is custom or nim
            {
                customMovesPlayer1ComboBox.SelectedIndex = 0;
                customMovesPlayer1ComboBox.BackColor = Color.FloralWhite;
                this.GamePreviewPanel.Controls.Add(customMovesPlayer1ComboBox);
                x = this.GamePreviewPanel.Controls[this.GamePreviewPanel.Controls.Count - 2].Location.X;
                y = this.GamePreviewPanel.Controls[this.GamePreviewPanel.Controls.Count - 2].Bottom + 10;
                customMovesPlayer1ComboBox.Location = new Point(x, y);
                if(customMovesPlayer2ComboBox != null) // asymetric movesets
                {
                    customMovesPlayer2ComboBox.SelectedIndex = 0;
                    customMovesPlayer2ComboBox.BackColor = Color.FloralWhite;
                    customMovesPlayer1ComboBox.Width = this.GamePreviewPanel.Width / 2 - 1;
                    customMovesPlayer2ComboBox.Width = this.GamePreviewPanel.Width / 2 - 1;
                    this.GamePreviewPanel.Controls.Add(customMovesPlayer2ComboBox);
                    x = this.GamePreviewPanel.Controls[this.GamePreviewPanel.Controls.Count - 2].Right + 2;
                    y = this.GamePreviewPanel.Controls[this.GamePreviewPanel.Controls.Count - 2].Location.Y;
                    customMovesPlayer2ComboBox.Location = new Point(x, y);
                }
                else // symetric movesets
                {
                    customMovesPlayer1ComboBox.Width = GamePreviewPanel.Width - 2;
                }
            }
            this.GamePreviewPanel.Controls.Add(whatVsWhatLabel);
            x = 0;
            y = this.GamePreviewPanel.Controls[this.GamePreviewPanel.Controls.Count - 2].Bottom + 10;
            whatVsWhatLabel.Location = new Point(x, y);
            this.GamePreviewPanel.Controls.Add(lastMoveWinsLabel);
            y = this.GamePreviewPanel.Controls[this.GamePreviewPanel.Controls.Count - 2].Bottom + 10;
            lastMoveWinsLabel.Location = new Point(x, y);
            this.GamePreviewPanel.Controls.Add(extraTerminalStatesLabel);
            y = this.GamePreviewPanel.Controls[this.GamePreviewPanel.Controls.Count - 2].Bottom + 10;
            extraTerminalStatesLabel.Location = new Point(x, y);
        }
        private void HomeForm_Load(object sender, EventArgs e)
        {
            UpdateGamesListBox();
        }
        private void GamesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGamePreviewPanel();
        }
    }
}
