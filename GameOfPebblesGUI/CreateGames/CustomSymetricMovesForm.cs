using GameOfPebblesLibrary;
using GameUI;

namespace GameOfPebblesGUI
{
    public partial class CustomSymetricMovesForm : Form
    {
        private CreateGameForm ParentCreateGameForm { get; set; }
        public CustomSymetricMovesForm(CreateGameForm parentCreateGameForm, List<int> pileSizes, List<Move> currentMoves)
        {
            InitializeComponent();
            this.ParentCreateGameForm = parentCreateGameForm;
            this.ParentCreateGameForm.Enabled = false;
            NumericUpDown pileSizeNumericDropdown;
            string pileSizesStr; 
            string moveStr;
            // PileSizesLabel
            pileSizesStr = "(";
            foreach (var pileSize in pileSizes)
            {
                pileSizesStr += pileSize.ToString() + ", ";
            }
            pileSizesStr = pileSizesStr[..^2];
            pileSizesStr += ")";
            this.PileNumbersLabel.Text = pileSizesStr;
            PileNumbersLabel.Location = new Point((this.ClientSize.Width - PileNumbersLabel.Width) / 2, PileNumbersLabel.Location.Y);
            // MoveListBox
            for (int moveIdx = 0; moveIdx < currentMoves.Count/2; moveIdx++)
            {
                moveStr = "(";
                foreach (var modifier in currentMoves[moveIdx].Modifiers)
                {
                    if (modifier > 0)
                        moveStr += "+";
                    moveStr += modifier.ToString() + ", ";
                }
                moveStr = moveStr[..^2];
                moveStr += ")";
                MovesListBox.Items.Add(moveStr);
            }
            // NumericUpDowns for Modifiers
            // Set the min-max value of the first up down
            Modifier1NumericUpDown.Minimum = pileSizes[0] * (-1);
            Modifier1NumericUpDown.Maximum = pileSizes[0];
            // Add the Additional upDowns
            for (int pileSizeIdx = 1; pileSizeIdx < pileSizes.Count; pileSizeIdx++)
            {
                pileSizeNumericDropdown = new NumericUpDown();
                pileSizeNumericDropdown.Width = 40;
                int x = ModifiersPanel.Controls[ModifiersPanel.Controls.Count - 1].Right + 10;
                int y = ModifiersPanel.Controls[0].Top;
                pileSizeNumericDropdown.Location = new Point(x, y);
                pileSizeNumericDropdown.Minimum = pileSizes[pileSizeIdx] * (-1);
                pileSizeNumericDropdown.Maximum = pileSizes[pileSizeIdx];
                ModifiersPanel.Controls.Add(pileSizeNumericDropdown);
            }
            ModifiersPanel.Width = ModifiersPanel.Controls[ModifiersPanel.Controls.Count - 1].Right + 10;
            ModifiersPanel.Location = new Point(MovesListBox.Location.X + (MovesListBox.Width - ModifiersPanel.Width) / 2,
                ModifiersPanel.Location.Y);
        }
        private bool ValidateForm()
        {
            bool result = true;
            if(MovesListBox.Items.Count == 0)
            {
                MessageBox.Show("No moves added!");
                result = false;
            }
            return result;
        }
        private bool ValidateMove(string newMoveStr)
        {
            bool result = true;
            int modifierIdx;
            for (modifierIdx = 0; modifierIdx < ModifiersPanel.Controls.Count; modifierIdx++)
            {
                NumericUpDown modifierNumericUpDown = (NumericUpDown)ModifiersPanel.Controls[modifierIdx];
                if ((int)modifierNumericUpDown.Value != 0)
                    break;
            }
            if (modifierIdx == ModifiersPanel.Controls.Count)
            {
                MessageBox.Show("At least one modifier has to be non-zero!");
                result = false;
                return result;
            }
            foreach (var move in MovesListBox.Items)
            {
                string moveStr = (string)move;
                if (moveStr.Equals(newMoveStr))
                {
                    MessageBox.Show("Move already added!");
                    result = false;
                    return result;
                }
            }
            return result;
        }
        private void AddMoveButton_Click(object sender, EventArgs e)
        {
            int moidifierValue;
            string moveStr = "(";
            if (ValidateMove(moveStr))
            {
                foreach (var control in ModifiersPanel.Controls)
                {
                    NumericUpDown modifierNumericUpDown = (NumericUpDown)control;
                    moidifierValue = (int)modifierNumericUpDown.Value;
                    if (moidifierValue > 0)
                        moveStr += "+";
                    moveStr += moidifierValue.ToString() + ", ";
                }
                moveStr = moveStr[..^2];
                moveStr += ")";
                if(ValidateMove(moveStr)) 
                    MovesListBox.Items.Add(moveStr);
            }
        }
        private void DeleteMoveButton_Click(object sender, EventArgs e)
        {
            int selectedMoveIndex = MovesListBox.SelectedIndex;
            if (selectedMoveIndex == -1)
            {
                MessageBox.Show("Please select a move to delete!");
                return;
            }
            MovesListBox.Items.RemoveAt(selectedMoveIndex);
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
                List<Move> sortedPlayer1Moves;
                List<Move> player2Moves = new();
                List<Move> sortedPlayer2Moves;
                List<int> moveModifiers;
                int newModifier;
                foreach (var move in MovesListBox.Items)
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
                    player1Moves.Add(new(null, moveModifiers));
                }
                sortedPlayer1Moves = SortMoves(player1Moves);
                foreach (var move in MovesListBox.Items)
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
                ParentCreateGameForm.SetSymetricMoves(moves);
                ParentCreateGameForm.Enabled = true;
                this.Close();
            }
        }
        private void CustomSymetricMovesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ParentCreateGameForm.Enabled = true;
        }
    }
}
