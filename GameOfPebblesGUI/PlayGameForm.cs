using GameOfPebblesGUI;
using GameOfPebblesLibrary;
using Newtonsoft.Json;
using static GameOfPebblesLibrary.GameTemplate;

namespace GameUI
{
    public partial class PlayGameForm : Form
    {
        private HomeForm ParentHomeForm { get; set; }
        private GameTemplate GameTemplate { get; set; }
        private GameModel GameModel { get; set; }
        private int TurnTime { get; set; }
        private int RemainingTime { get; set; }
        private List<Tuple<int, int>> MoveHistory { get; set; }
        private List<int> PlayerResults { get; set; }
        private bool PvE { get; set; }
        public PlayGameForm(HomeForm parentHomeForm, GameTemplate gameTemplate, int turnTime)
        {
            InitializeComponent();
            this.GameTemplate = gameTemplate;
            this.GameModel = new GameModel(GameTemplate.GameParameters, "");
            this.ParentHomeForm = parentHomeForm;
            this.MoveHistory = new();
            this.PlayerResults = new();
            this.PvE = false;
            int x, y;
            int pileCount = this.GameModel.Dimensions.Values.Count - 1;
            // PILES
            for (int dimensionIdx = 0; dimensionIdx < pileCount; dimensionIdx++)
            {
                Panel newPilePanel = new();
                Label newPileLabel = new();
                Label newPileSizeLabel = new();
                newPileLabel.Font = new Font(newPileLabel.Font.FontFamily, 16, FontStyle.Bold);
                newPileLabel.Height = 40;
                newPileLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                newPileLabel.TextAlign = ContentAlignment.MiddleCenter;                
                newPileSizeLabel.Font = new Font(newPileLabel.Font.FontFamily, 12);
                newPileSizeLabel.Height = 30;
                newPileSizeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                newPileSizeLabel.TextAlign = ContentAlignment.MiddleCenter;
                newPileLabel.Text = "Pile " + (dimensionIdx + 1);
                newPilePanel.Controls.Add(newPileLabel);
                newPileSizeLabel.Text = GameModel.Dimensions.Values[dimensionIdx].ToString();
                x = newPilePanel.Controls[newPilePanel.Controls.Count - 1].Location.X;
                y = newPilePanel.Controls[newPilePanel.Controls.Count - 1].Bottom + 10;
                newPileSizeLabel.Location = new(x, y);
                newPilePanel.Controls.Add(newPileSizeLabel);
                newPilePanel.Width = newPilePanel.Controls[newPilePanel.Controls.Count - 1].Right + 10;
                newPilePanel.Height = newPilePanel.Controls[newPilePanel.Controls.Count - 1].Bottom + 10;
                PilesPanel.Controls.Add(newPilePanel);
            }
            InitializePositionsOfPileLabels(pileCount);
            UpdatePlayerMovesComboBoxes();
            // TIMER
            this.RemainingTimeLabel.Text = turnTime.ToString();
            this.TurnTime = turnTime;
            this.RemainingTime = turnTime;
            this.TurnTimer.Enabled = true;
            this.TurnTimer.Interval = 1000;
            this.TurnTimer.Tick += TurnTimer_Tick;
            if (this.GameModel.PlayerList[0] is PlayerAI)
            {
                this.PvE = true;
                MessageBox.Show("AI moves first!");
                AIExecuteMove(0);
                this.GameModel.UpdateGameModelInformation();
                UpdateGUI();
                if (this.GameModel.CheckGameOver())
                {
                    CleanUpGame();
                    return;
                }
                this.WhoMovesLabel.Text = "Player moves!";
            }
            else
            {
                this.WhoMovesLabel.Text = "Player#1 moves!";
            }
            if (this.GameModel.PlayerList[1] is PlayerAI)
            {
                this.PvE = true;
            }
            if (this.GameModel.CheckGameOver())
            {
                MessageBox.Show("The game immediately ended because there are no valid starting moves!");
                this.Close();
                this.ParentHomeForm.Show();
                return;
            }
        }
        private void InitializePositionsOfPileLabels(int pileCount)
        {
            int x, y;
            x = 10;
            y = 10;
            this.PilesPanel.Controls[0].Location = new(x, y);
            switch (pileCount)
            {
                case 2:
                    x = this.PilesPanel.Controls[0].Right + 10;
                    y = this.PilesPanel.Controls[0].Location.Y;
                    this.PilesPanel.Controls[1].Location = new(x, y);
                    break;
                case 3:
                    x = this.PilesPanel.Controls[0].Right + 10;
                    y = this.PilesPanel.Controls[0].Location.Y;
                    this.PilesPanel.Controls[1].Location = new(x, y);
                    x = this.PilesPanel.Controls[1].Right + 10;
                    y = this.PilesPanel.Controls[1].Location.Y;
                    this.PilesPanel.Controls[2].Location = new(x, y);
                    break;
                case 4:
                    x = this.PilesPanel.Controls[0].Right + 10;
                    y = this.PilesPanel.Controls[0].Location.Y;
                    this.PilesPanel.Controls[1].Location = new(x, y);
                    x = this.PilesPanel.Controls[0].Location.X;
                    y = this.PilesPanel.Controls[0].Bottom + 10;
                    this.PilesPanel.Controls[2].Location = new(x, y);
                    x = this.PilesPanel.Controls[2].Right + 10;
                    y = this.PilesPanel.Controls[2].Location.Y;
                    this.PilesPanel.Controls[3].Location = new(x, y);
                    break;
                case 5:
                    x = this.PilesPanel.Controls[0].Right + 10;
                    y = this.PilesPanel.Controls[0].Location.Y;
                    this.PilesPanel.Controls[1].Location = new(x, y);
                    x = this.PilesPanel.Controls[1].Right + 10;
                    y = this.PilesPanel.Controls[1].Location.Y;
                    this.PilesPanel.Controls[2].Location = new(x, y);
                    x = this.PilesPanel.Controls[0].Right + 1;
                    y = this.PilesPanel.Controls[0].Bottom + 10;
                    this.PilesPanel.Controls[3].Location = new(x, y);
                    x = this.PilesPanel.Controls[3].Right + 10;
                    y = this.PilesPanel.Controls[3].Location.Y;
                    this.PilesPanel.Controls[4].Location = new(x, y);
                    break;
                case 6:
                    x = this.PilesPanel.Controls[0].Right + 10;
                    y = this.PilesPanel.Controls[0].Location.Y;
                    this.PilesPanel.Controls[1].Location = new(x, y);
                    x = this.PilesPanel.Controls[1].Right + 10;
                    y = this.PilesPanel.Controls[1].Location.Y;
                    this.PilesPanel.Controls[2].Location = new(x, y);
                    x = this.PilesPanel.Controls[0].Location.X;
                    y = this.PilesPanel.Controls[0].Bottom + 10;
                    this.PilesPanel.Controls[3].Location = new(x, y);
                    x = this.PilesPanel.Controls[3].Right + 10;
                    y = this.PilesPanel.Controls[3].Location.Y;
                    this.PilesPanel.Controls[4].Location = new(x, y);
                    x = this.PilesPanel.Controls[4].Right + 10;
                    y = this.PilesPanel.Controls[4].Location.Y;
                    this.PilesPanel.Controls[5].Location = new(x, y);
                    break;
                case 7:
                    x = this.PilesPanel.Controls[0].Right + 10;
                    y = this.PilesPanel.Controls[0].Location.Y;
                    this.PilesPanel.Controls[1].Location = new(x, y);
                    x = this.PilesPanel.Controls[1].Right + 10;
                    y = this.PilesPanel.Controls[1].Location.Y;
                    this.PilesPanel.Controls[2].Location = new(x, y);
                    x = this.PilesPanel.Controls[2].Right + 10;
                    y = this.PilesPanel.Controls[2].Location.Y;
                    this.PilesPanel.Controls[3].Location = new(x, y);
                    x = this.PilesPanel.Controls[0].Right + 1;
                    y = this.PilesPanel.Controls[0].Bottom + 10;
                    this.PilesPanel.Controls[4].Location = new(x, y);
                    x = this.PilesPanel.Controls[4].Right + 10;
                    y = this.PilesPanel.Controls[4].Location.Y;
                    this.PilesPanel.Controls[5].Location = new(x, y);
                    x = this.PilesPanel.Controls[5].Right + 10;
                    y = this.PilesPanel.Controls[5].Location.Y;
                    this.PilesPanel.Controls[6].Location = new(x, y);
                    break;
                case 8:
                    x = this.PilesPanel.Controls[0].Right + 10;
                    y = this.PilesPanel.Controls[0].Location.Y;
                    this.PilesPanel.Controls[1].Location = new(x, y);
                    x = this.PilesPanel.Controls[1].Right + 10;
                    y = this.PilesPanel.Controls[1].Location.Y;
                    this.PilesPanel.Controls[2].Location = new(x, y);
                    x = this.PilesPanel.Controls[2].Right + 10;
                    y = this.PilesPanel.Controls[2].Location.Y;
                    this.PilesPanel.Controls[3].Location = new(x, y);
                    x = this.PilesPanel.Controls[0].Location.X;
                    y = this.PilesPanel.Controls[0].Bottom + 10;
                    this.PilesPanel.Controls[4].Location = new(x, y);
                    x = this.PilesPanel.Controls[4].Right + 10;
                    y = this.PilesPanel.Controls[4].Location.Y;
                    this.PilesPanel.Controls[5].Location = new(x, y);
                    x = this.PilesPanel.Controls[5].Right + 10;
                    y = this.PilesPanel.Controls[5].Location.Y;
                    this.PilesPanel.Controls[6].Location = new(x, y);
                    x = this.PilesPanel.Controls[6].Right + 10;
                    y = this.PilesPanel.Controls[6].Location.Y;
                    this.PilesPanel.Controls[7].Location = new(x, y);
                    break;
                default:
                    break;
            }
        }
        private void TurnTimer_Tick(object sender, EventArgs e)
        {
            this.RemainingTime--; // subtract 1 from the remaining time
            this.RemainingTimeLabel.Text = this.RemainingTime.ToString(); // update the label text
            if (this.RemainingTime == 0)
            {
                this.TurnTimer.Stop();
                PlayerExecuteMove();
                this.TurnTimer.Start(); // Start the timer again
            }
        }
        private void ResetTurnTimer()
        {
            this.RemainingTime = this.TurnTime;  // Reset the timer
            this.RemainingTimeLabel.Text = this.RemainingTime.ToString(); // reset the label text
        }
        private void MoveButton_Click(object sender, EventArgs e)
        {
            PlayerExecuteMove();
        }
        private void PlayerExecuteMove()
        {
            int selectedMoveIdx = GetCurrentPlayerMoveIdx();
            if (selectedMoveIdx == -1)
            {
                MessageBox.Show("Select a move first!");
                return;
            }
            try
            {
                List<Move> validMoves = this.GameModel.GetValidMovesOfPlayer(GameModel.CurrentPlayerIdx);
                Move selectedMove = validMoves[selectedMoveIdx];
                this.Enabled = false; // disables GUI while move is executing
                this.GameModel.PlayerList[this.GameModel.CurrentPlayerIdx].Move(selectedMove);
                int thinkingTime = this.TurnTime - this.RemainingTime;
                UpdateMoveHistory(selectedMove, thinkingTime);
                this.GameModel.UpdateGameModelInformation();
                UpdateGUI();
                if (this.GameModel.CheckGameOver())
                {
                    CleanUpGame();
                    return;
                }
                if (this.PvE)
                {
                    ResetTurnTimer();
                    this.Enabled = true;
                    this.WhoMovesLabel.Text = "AI moves!";
                    this.Enabled = false;
                    AIExecuteMove(this.GameModel.CurrentPlayerIdx);
                    this.GameModel.UpdateGameModelInformation();
                    UpdateGUI();
                    if (this.GameModel.CheckGameOver())
                    {
                        CleanUpGame();
                        return;
                    }
                    this.Enabled = true;
                    this.WhoMovesLabel.Text = "Player moves!";
                }
                else
                {
                    if(this.WhoMovesLabel.Text == "Player#1 moves!")
                        this.WhoMovesLabel.Text = "Player#2 moves!";
                    else
                        this.WhoMovesLabel.Text = "Player#1 moves!";
                }
                this.Enabled = true;
                ResetTurnTimer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
        public void CleanUpGame()
        {
            this.TurnTimer.Enabled = false;
            SetPlayerResults();
            FinishGame();
        }
        private int GetCurrentPlayerMoveIdx()
        {
            int actulaMoveIdx = -1;
            if (this.MovesComboBox.SelectedIndex != -1)
            {
                return this.MovesComboBox.SelectedIndex;
            }
            return actulaMoveIdx;
        }
        private void UpdateMoveHistory(Move selectedMove, int thinkingTime)
        {
            this.MoveHistory.Add(new(this.GameModel.Moves.IndexOf(selectedMove), thinkingTime)); 
        }
        private int AIExecuteMove(int idxOfPlayerAI)
        {
            int thinkingTime = 0;
            Move selectedMove;
            if (this.GameModel.PlayerList[idxOfPlayerAI] is PlayerAI aiPlayer)
            {
                selectedMove = aiPlayer.GetMove(this.GameModel.GetValidMovesOfPlayer(idxOfPlayerAI), out thinkingTime);
                aiPlayer.Move(selectedMove);
                UpdateMoveHistory(selectedMove, thinkingTime);
            }
            return thinkingTime;
        }
        private void SetPlayerResults() // who won and lost
        {
            if (this.GameModel.GameStateIsTerminal())
            { // the game ended becasue we stepped on an EXPLICIT terminal state
                if (this.GameModel.GameEngine.GameStates[this.GameModel.CurrentGameStateIndex].MyState == GameState.State.WINNNING)
                {
                    MessageBox.Show($"Game ended by stepping on an EXPLICIT terminal state! Winner Player #{this.GameModel.CurrentPlayerIdx + 1}");
                    this.PlayerResults.Add(this.GameModel.CurrentPlayerIdx);
                    this.PlayerResults.Add(this.GameModel.GetNextPlayerIdx());
                }
                else
                {
                    MessageBox.Show($"Game ended by stepping on an EXPLICIT terminal state! Winner Player #{GameModel.GetNextPlayerIdx() + 1}");
                    this.PlayerResults.Add(this.GameModel.GetNextPlayerIdx());
                    this.PlayerResults.Add(this.GameModel.CurrentPlayerIdx);
                }
                return;
            }
            if (this.GameModel.GetValidMovesOfPlayer(this.GameModel.CurrentPlayerIdx).Count == 0)
            { // the game ended because we stepped on an IMPLICIT terminal state (zero vector)
                if (this.GameModel.LastMoveWins)
                {
                    MessageBox.Show($"Game ended by stepping on an IMPLICIT terminal state! Winner Player #{this.GameModel.GetNextPlayerIdx() + 1}");
                    this.PlayerResults.Add(this.GameModel.GetNextPlayerIdx());
                    this.PlayerResults.Add(this.GameModel.CurrentPlayerIdx);
                }
                else
                {
                    MessageBox.Show($"Game ended by stepping on an IMPLICIT terminal state! Winner Player #{this.GameModel.CurrentPlayerIdx + 1}");
                    this.PlayerResults.Add(this.GameModel.CurrentPlayerIdx);
                    this.PlayerResults.Add(this.GameModel.GetNextPlayerIdx());
                }
                return;
            }
            if (this.GameModel.VisitsPerState[this.GameModel.CurrentGameStateIndex] == this.GameModel.MaxVisitsPerState)
            { // the game ended because we went in circles a bunch -> result = DRAW
                this.PlayerResults.Add(0);
                this.PlayerResults.Add(0);
                MessageBox.Show($"Game is a draw!");
                return;
            }
        }
        private void FinishGame()
        {
            SaveFinishedGame();
            this.Close();
            this.ParentHomeForm.Show();
        }
        private void SaveFinishedGame()
        {
            PlayedGame playedGame = new(this.MoveHistory, this.PlayerResults);
            if (playedGame != null && this.GameTemplate.PlayedGames != null)
            {
                this.GameTemplate.PlayedGames.Add(playedGame);
                string templateToSaveJson = JsonConvert.SerializeObject(this.GameTemplate);
                File.WriteAllText(Paths.GameTemplatesFolderPath + this.GameTemplate.GameTitle + @".json", templateToSaveJson);
                MessageBox.Show("Game finished! Game history succesfully updated!");
            }
            else
            {
                MessageBox.Show("Something went wrong updating the game history!");
            }
        }
        private void UpdateGUI()
        {
            UpdatePileSizes();
            UpdatePreviousMovesListBox();
            UpdatePlayerMovesComboBoxes();
        }
        private void UpdatePileSizes()
        {
            for (int dimensionIdx = 0; dimensionIdx < this.PilesPanel.Controls.Count; dimensionIdx++)
            {
                this.PilesPanel.Controls[dimensionIdx].Controls[1].Text = this.GameModel.CurrentGameDimensions.Values[dimensionIdx].ToString();
            }
        }
        private void UpdatePlayerMovesComboBoxes()
        {
            int currentPlayerID = this.GameModel.CurrentPlayerIdx;
            int nextPlayerID = this.GameModel.GetNextPlayerIdx();
            List<Move> validMoves = this.GameModel.GetValidMovesOfPlayer(currentPlayerID);
            List<Move> opponentMoves = this.GameModel.GetAllMovesOfPlayer(nextPlayerID);
            this.MovesComboBox.Items.Clear();
            foreach (var move in validMoves)
                this.MovesComboBox.Items.Add(move);
            if(this.MovesComboBox.Items.Count > 0)
                this.MovesComboBox.SelectedIndex = 0;
            this.OpponentsMovesComboBox.Items.Clear();
            foreach (var move in opponentMoves)
                this.OpponentsMovesComboBox.Items.Add(move);
        }
        private void UpdatePreviousMovesListBox()
        {
            string currentTurn = string.Format("{0,3:0000}", this.MoveHistory.Count);
            string moveStr = "Turn #" + currentTurn + " -  ";
            if (PvE)
            {
                if (this.GameModel.PlayerList[this.GameModel.GetNextPlayerIdx()] is PlayerHuman)
                {
                    moveStr += "Player's move: ";
                    moveStr += GameModel.Moves[this.MoveHistory[this.MoveHistory.Count - 1].Item1];
                    this.PreviousMovesListBox.Items.Add(moveStr);
                }
                else
                {
                    moveStr += "AI's move: ";
                    moveStr += this.GameModel.Moves[this.MoveHistory[this.MoveHistory.Count - 1].Item1];
                    this.PreviousMovesListBox.Items.Add(moveStr);
                }
            }
            else
            {
                moveStr += "Player#" + (this.GameModel.CurrentPlayerIdx == 0 ? 2 : 1) + "'s move: ";
                moveStr += this.GameModel.Moves[this.MoveHistory[this.MoveHistory.Count - 1].Item1];
                this.PreviousMovesListBox.Items.Add(moveStr);
            }
        }
        private void PlayGameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ParentHomeForm.Show();
        }
    }
}
