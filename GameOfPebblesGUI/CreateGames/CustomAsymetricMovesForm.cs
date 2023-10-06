using GameOfPebblesLibrary;
using GameUI;

namespace GameOfPebblesGUI
{
    public partial class CustomAsymetricMovesForm : Form
    {
        private CreateGameForm ParentCreateGameForm { get; set; }
        public CustomAsymetricMovesForm(CreateGameForm parentCreateGameForm, List<int> pileSizes, List<Move> previouslyAddedMoves)
        {
            InitializeComponent();
            this.ParentCreateGameForm = parentCreateGameForm;
            this.ParentCreateGameForm.Enabled = false;
            string pileSizesStr; 
            NumericUpDown pileSizeNumericUpDown;
            string moveStr;
            int x, y;
            // PileSizesLabel
            pileSizesStr = "(";
            foreach (var pileSize in pileSizes)
            {
                pileSizesStr += pileSize.ToString() + ", ";
            }
            pileSizesStr = pileSizesStr[..^2];
            pileSizesStr += ")";
            this.PileNumbersLabel.Text = pileSizesStr;
            this.PileNumbersLabel.Location = new Point((this.ClientSize.Width - this.PileNumbersLabel.Width) / 2, 
                 this.PileNumbersLabel.Location.Y);
            // Adding previously added moves to list
            //      for Player #1
            for (int moveIdx = 0; moveIdx < previouslyAddedMoves.Count/2; moveIdx++)
            {
                moveStr = "(";
                foreach (var modifier in previouslyAddedMoves[moveIdx].Modifiers)
                {
                    if (modifier > 0)
                        moveStr += "+";
                    moveStr += modifier.ToString() + ", ";
                }
                moveStr = moveStr[..^2];
                moveStr += ")";
                this.MovesPlayer1ListBox.Items.Add(moveStr);
            }
            //      for Player #2
            for (int moveIdx = previouslyAddedMoves.Count / 2; moveIdx < previouslyAddedMoves.Count; moveIdx++)
            {
                moveStr = "(";
                foreach (var modifier in previouslyAddedMoves[moveIdx].Modifiers)
                {
                    if (modifier > 0)
                        moveStr += "+";
                    moveStr += modifier.ToString() + ", ";
                }
                moveStr = moveStr[..^2];
                moveStr += ")";
                this.MovesPlayer2ListBox.Items.Add(moveStr);
            }
            // NumericUpDowns for Modifiers of Player #1
            // Set the min-max value of the first up down
            this.Modifier1Player1NumericUpDown.Minimum = pileSizes[0] * (-1);
            this.Modifier1Player1NumericUpDown.Maximum = pileSizes[0];
            // Add the Additional upDowns
            for (int pileSizeIdx = 1; pileSizeIdx < pileSizes.Count; pileSizeIdx++)
            {
                pileSizeNumericUpDown = new NumericUpDown();
                pileSizeNumericUpDown.Width = 40;
                x = this.ModifiersPlayer1Panel.Controls[this.ModifiersPlayer1Panel.Controls.Count - 1].Right + 10;
                y = this.ModifiersPlayer1Panel.Controls[0].Top; 
                pileSizeNumericUpDown.Location = new Point(x, y);
                pileSizeNumericUpDown.Minimum = pileSizes[pileSizeIdx] * (-1);
                pileSizeNumericUpDown.Maximum = pileSizes[pileSizeIdx];
                this.ModifiersPlayer1Panel.Controls.Add(pileSizeNumericUpDown);
            }
            this.ModifiersPlayer1Panel.Width = this.ModifiersPlayer1Panel.Controls[this.ModifiersPlayer1Panel.Controls.Count - 1].Right + 10;
            this.ModifiersPlayer1Panel.Location = new Point(this.MovesPlayer1ListBox.Location.X + (this.MovesPlayer1ListBox.Width - this.ModifiersPlayer1Panel.Width) / 2,
                this.ModifiersPlayer1Panel.Location.Y);
            // NumericUpDowns for Modifiers of Player #2
            // Set the min-max value of the first up down
            this.Modifier1Player2NumericUpDown.Minimum = pileSizes[0] * (-1);
            this.Modifier1Player2NumericUpDown.Maximum = pileSizes[0];
            // Add the Additional upDowns
            for (int pileSizeIdx = 1; pileSizeIdx < pileSizes.Count; pileSizeIdx++)
            {
                pileSizeNumericUpDown = new NumericUpDown();
                pileSizeNumericUpDown.Width = 40;
                x = this.ModifiersPlayer2Panel.Controls[this.ModifiersPlayer2Panel.Controls.Count - 1].Right + 10;
                y = this.ModifiersPlayer2Panel.Controls[0].Top;
                pileSizeNumericUpDown.Location = new Point(x, y);
                pileSizeNumericUpDown.Minimum = pileSizes[pileSizeIdx] * (-1);
                pileSizeNumericUpDown.Maximum = pileSizes[pileSizeIdx];
                this.ModifiersPlayer2Panel.Controls.Add(pileSizeNumericUpDown);
            }
            this.ModifiersPlayer2Panel.Width = this.ModifiersPlayer2Panel.Controls[this.ModifiersPlayer2Panel.Controls.Count - 1].Right + 10;
            this.ModifiersPlayer2Panel.Location = new Point(this.MovesPlayer2ListBox.Location.X + (this.MovesPlayer2ListBox.Width - ModifiersPlayer2Panel.Width) / 2,
                this.ModifiersPlayer2Panel.Location.Y);
        }
        private bool ValidateForm()
        {
            bool result = true;
            if (this.MovesPlayer1ListBox.Items.Count == 0 && this.MovesPlayer2ListBox.Items.Count == 0)
            {
                MessageBox.Show("No moves provided for either player!");
                result = false;
                return result;
            }
            if (this.MovesPlayer1ListBox.Items.Count != this.MovesPlayer2ListBox.Items.Count)
            {
                MessageBox.Show("Both players have to have the same amount of moves!");
                result = false;
                return result;
            }
            return result;
        }
        private bool ValidatePlayer1Move(string newMoveStr)
        {
            bool result = true;
            int modifierIdx;
            for(modifierIdx = 0; modifierIdx < this.ModifiersPlayer1Panel.Controls.Count; modifierIdx++)
            {
                NumericUpDown modifierNumericUpDown = (NumericUpDown)this.ModifiersPlayer1Panel.Controls[modifierIdx];
                if ((int)modifierNumericUpDown.Value != 0)
                    break;
            }
            if(modifierIdx == this.ModifiersPlayer1Panel.Controls.Count)
            {
                MessageBox.Show("At least one modifier has to be non-zero!");
                result = false;
                return result;
            }
            foreach (var move in this.MovesPlayer1ListBox.Items)
            {
                string moveStr = (string)move;
                if (move.Equals(newMoveStr))
                {
                    MessageBox.Show("Move already added for Player #1!");
                    result = false;
                    return result;
                }
            }
            return result;
        }
        private bool ValidatePlayer2Move(string newMoveStr)
        {
            bool result = true;
            int modifierIdx;
            for (modifierIdx = 0; modifierIdx < this.ModifiersPlayer2Panel.Controls.Count; modifierIdx++)
            {
                NumericUpDown modifierNumericUpDown = (NumericUpDown)this.ModifiersPlayer2Panel.Controls[modifierIdx];
                if ((int)modifierNumericUpDown.Value != 0)
                    break;
            }
            if (modifierIdx == this.ModifiersPlayer2Panel.Controls.Count)
            {
                MessageBox.Show("At least one modifier has to be non-zero!");
                result = false;
                return result;
            }
            foreach (var move in this.MovesPlayer2ListBox.Items)
            {
                string moveStr = (string)move;
                if (moveStr.Equals(newMoveStr))
                {
                    MessageBox.Show("Move already added for Player #2!");
                    result = false;
                    return result;
                }
            }
            return result;
        }
        private void AddMovePlayer1Button_Click(object sender, EventArgs e)
        {
            int moidifierValue;
            string moveStr = "(";
            if(ValidatePlayer1Move(moveStr))
            {
                foreach (var control in this.ModifiersPlayer1Panel.Controls)
                {
                    NumericUpDown modifierNumericUpDown = (NumericUpDown)control;
                    moidifierValue = (int)modifierNumericUpDown.Value;
                    if (moidifierValue > 0)
                        moveStr += "+";
                    moveStr += moidifierValue.ToString() + ", ";
                }
                moveStr = moveStr[..^2];
                moveStr += ")";
                if (ValidatePlayer1Move(moveStr))
                    this.MovesPlayer1ListBox.Items.Add(moveStr);
            }
        }
        private void DeleteMovePlayer1Button_Click(object sender, EventArgs e)
        {
            int selectedMoveIndex = this.MovesPlayer1ListBox.SelectedIndex;
            if (selectedMoveIndex == -1)
            {
                MessageBox.Show("Please select a move of Player #1 to delete!");
                return;
            }
            this.MovesPlayer1ListBox.Items.RemoveAt(selectedMoveIndex);
        }
        private void AddMovePlayer2Button_Click(object sender, EventArgs e)
        {
            int moidifierValue;
            string moveStr = "(";
            if (ValidatePlayer2Move(moveStr))
            {
                foreach (var control in this.ModifiersPlayer2Panel.Controls)
                {
                    NumericUpDown modifierNumericUpDown = (NumericUpDown)control;
                    moidifierValue = (int)modifierNumericUpDown.Value;
                    if (moidifierValue > 0)
                        moveStr += "+";
                    moveStr += moidifierValue.ToString() + ", ";
                }
                moveStr = moveStr[..^2];
                moveStr += ")";
                if (ValidatePlayer2Move(moveStr))
                    this.MovesPlayer2ListBox.Items.Add(moveStr);
            }
        }
        private void DeleteMovePlayer2Button_Click(object sender, EventArgs e)
        {
            int selectedMoveIndex = this.MovesPlayer2ListBox.SelectedIndex;
            if (selectedMoveIndex == -1)
            {
                MessageBox.Show("Please select a move of Player #2 to delete!");
                return;
            }
            this.MovesPlayer2ListBox.Items.RemoveAt(selectedMoveIndex);
        }
        private List<Move> SortMoves(List<Move> playerMoves)
        {
            List<Move> sortedMoves = new();
            List<int> decreasingMoves = new();
            List<int> complexMoves = new();
            List<int> increasingMoves = new();
            List<int> sortedModifiers;
            int modifierIdx;
            for (int moveIdx = 0; moveIdx < playerMoves.Count; moveIdx++)
            {
                sortedModifiers = new(playerMoves[moveIdx].Modifiers);
                sortedModifiers.Sort();
                if (sortedModifiers[0] >= 0)
                {
                    increasingMoves.Add(moveIdx);
                }
                else
                {
                    modifierIdx = 0;
                    while (modifierIdx < playerMoves[moveIdx].Modifiers.Count && playerMoves[moveIdx].Modifiers[modifierIdx] <= 0)
                        modifierIdx++;
                    if (modifierIdx != playerMoves[moveIdx].Modifiers.Count)
                    {
                        complexMoves.Add(moveIdx);
                    }
                    else
                    {
                        decreasingMoves.Add(moveIdx);
                    }
                }
            }
            foreach (int moveIdx in decreasingMoves)
            {
                sortedMoves.Add(playerMoves[moveIdx]);
            }
            foreach (int moveIdx in complexMoves)
            {
                sortedMoves.Add(playerMoves[moveIdx]);
            }
            foreach (int moveIdx in increasingMoves)
            {
                sortedMoves.Add(playerMoves[moveIdx]);
            }
            return sortedMoves;
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                List<Move> moves = new();
                List<Move> player1Moves = new();
                List<Move> sortedPlayer1Moves = new();
                List<Move> player2Moves = new();
                List<Move> sortedPlayer2Moves = new();
                List<int> moveModifiers;
                int newModifier;
                foreach (var move in this.MovesPlayer1ListBox.Items)
                {
                    string moveStr = new((string)move);
                    moveStr = moveStr[1..]; // minus (
                    moveStr = moveStr[..^1]; // minus )
                    moveModifiers = new();
                    foreach (var modifierStr in moveStr.Split(", "))
                    {
                        if(int.TryParse(modifierStr, out newModifier))
                        {
                            moveModifiers.Add(newModifier);
                        }
                    }
                    player1Moves.Add(new(null, moveModifiers));
                }
                sortedPlayer1Moves = SortMoves(player1Moves);
                foreach (var move in this.MovesPlayer2ListBox.Items)
                {
                    string moveStr = new((string)move);
                    moveStr = moveStr[1..]; // minus (
                    moveStr = moveStr[..^1]; // minus )
                    moveModifiers = new();
                    foreach (var modifierStr in moveStr.Split(", "))
                    {
                        if (int.TryParse(modifierStr, out newModifier))
                        {
                            moveModifiers.Add(newModifier);
                        }
                    }
                    player2Moves.Add(new(null, moveModifiers));
                }
                sortedPlayer2Moves = SortMoves(player2Moves);
                foreach (var move in sortedPlayer1Moves)
                {
                    moves.Add(move);
                }
                foreach (var move in sortedPlayer2Moves)
                {
                    moves.Add(move);
                }
                player1Moves = null;
                player2Moves = null;
                this.ParentCreateGameForm.SetAsymetricMoves(moves);
                this.ParentCreateGameForm.Enabled = true;
                this.Close();
            }
        }
        private void CustomAsymetricMovesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ParentCreateGameForm.Enabled = true;
        }
    }
}
