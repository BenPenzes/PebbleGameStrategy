using GameUI;
using GameOfPebblesLibrary;

namespace GameOfPebblesGUI
{
    public partial class TerminalStatesAsymetricForm : Form
    {
        private List<int> PileSizes { get; set; }
        private CreateGameForm ParentCreateGameForm { get; set; }
        public TerminalStatesAsymetricForm(CreateGameForm parentCreateGameForm, List<int> pileSizes, List<List<Tuple<Dimensions, string>>> currentExtraTerminalStates)
        {
            InitializeComponent();
            this.ParentCreateGameForm = parentCreateGameForm;
            this.ParentCreateGameForm.Enabled = false;
            this.PileSizes  = pileSizes;
            NumericUpDown pileSizeNumericDropdown;
            string pileSizesStr;
            string terminalStateStr;
            // Previously added extra terminal states
            //      for Player#1
            foreach (var stateOutcomePair in currentExtraTerminalStates[0])
            {
                terminalStateStr = "(";
                foreach (var pileSize in stateOutcomePair.Item1.Values)
                {
                    terminalStateStr += pileSize + ", ";
                }
                terminalStateStr = terminalStateStr[..^2];
                terminalStateStr += ") - [";
                terminalStateStr += stateOutcomePair.Item2;
                terminalStateStr += "]";
                this.TerminalStatesPlayer1ListBox.Items.Add(terminalStateStr);
            }
            //      for Player#2
            foreach (var stateOutcomePair in currentExtraTerminalStates[1])
            {
                terminalStateStr = "(";
                foreach (var pileSize in stateOutcomePair.Item1.Values)
                {
                    terminalStateStr += pileSize + ", ";
                }
                terminalStateStr = terminalStateStr[..^2];
                terminalStateStr += ") - [";
                terminalStateStr += stateOutcomePair.Item2;
                terminalStateStr += "]";
                this.TerminalStatesPlayer2ListBox.Items.Add(terminalStateStr);
            }
            // PileSizesLabel
            pileSizesStr = "(";
            foreach (var pileSize in pileSizes)
            {
                pileSizesStr += pileSize.ToString() + ", ";
            }
            pileSizesStr = pileSizesStr[..^2];
            pileSizesStr += ")";
            this.PileNumbersLabel.Text = pileSizesStr;
            this.PileNumbersLabel.Location = new Point((this.ClientSize.Width - this.PileNumbersLabel.Width) / 2, this.PileNumbersLabel.Location.Y);
            // NumericUpDowns - Player #1
            //      Set the min-max value of the first up down
            this.Pile1SizePlayer1NumericUpDown.Minimum = 0;
            this.Pile1SizePlayer1NumericUpDown.Maximum = pileSizes[0];
            //      Add the Additional upDowns
            for (int pileSizeIdx = 1; pileSizeIdx < pileSizes.Count; pileSizeIdx++)
            {
                pileSizeNumericDropdown = new NumericUpDown();
                pileSizeNumericDropdown.Width = 40;
                int x = this.PileSizesPlayer1Panel.Controls[PileSizesPlayer1Panel.Controls.Count - 1].Right + 10;
                int y = this.PileSizesPlayer1Panel.Controls[0].Top;
                pileSizeNumericDropdown.Location = new Point(x, y);
                pileSizeNumericDropdown.Minimum = 0;
                pileSizeNumericDropdown.Maximum = pileSizes[pileSizeIdx];
                this.PileSizesPlayer1Panel.Controls.Add(pileSizeNumericDropdown);
            }
            this.PileSizesPlayer1Panel.Width = this.PileSizesPlayer1Panel.Controls[this.PileSizesPlayer1Panel.Controls.Count - 1].Right + 10;
            this.PileSizesPlayer1Panel.Location = new Point(this.TerminalStatesPlayer1ListBox.Location.X + (this.TerminalStatesPlayer1ListBox.Width - this.PileSizesPlayer1Panel.Width) / 2
                , PileSizesPlayer1Panel.Location.Y);
            // NumericUpDowns - Player #2
            //      Set the min-max value of the first up down
            this.Pile1SizePlayer2NumericUpDown.Minimum = 0;
            this.Pile1SizePlayer2NumericUpDown.Maximum = pileSizes[0];
            //      Add the Additional upDowns
            for (int pileSizeIdx = 1; pileSizeIdx < pileSizes.Count; pileSizeIdx++)
            {
                pileSizeNumericDropdown = new NumericUpDown();
                pileSizeNumericDropdown.Width = 40;
                int x = this.PileSizesPlayer2Panel.Controls[this.PileSizesPlayer2Panel.Controls.Count - 1].Right + 10;
                int y = this.PileSizesPlayer2Panel.Controls[0].Top;
                pileSizeNumericDropdown.Location = new Point(x, y);
                pileSizeNumericDropdown.Minimum = 0;
                pileSizeNumericDropdown.Maximum = pileSizes[pileSizeIdx];
                this.PileSizesPlayer2Panel.Controls.Add(pileSizeNumericDropdown);
            }
            PileSizesPlayer2Panel.Width = PileSizesPlayer2Panel.Controls[PileSizesPlayer2Panel.Controls.Count - 1].Right + 10;
            PileSizesPlayer2Panel.Location = new Point(this.TerminalStatesPlayer2ListBox.Location.X + (this.TerminalStatesPlayer2ListBox.Width - PileSizesPlayer2Panel.Width) / 2
                , PileSizesPlayer2Panel.Location.Y);
            // Default Radio Button Values
            this.LosingStatePlayer1RadioButton.Checked = true;
            this.LosingStatePlayer2RadioButton.Checked = true;
        }

        private bool ValidateForm()
        {
            bool result = true;
            if(this.TerminalStatesPlayer1ListBox.Items.Count == 0 && this.TerminalStatesPlayer2ListBox.Items.Count == 0)
            {
                MessageBox.Show("No Extra terminal states added!");
                result = false;
            }
            return result;
        }
        private bool ValidatePlayer1TerminalState(string newStateOutcomePairStr)
        {
            bool result = true;
            int pileSizeIdx;
            for (pileSizeIdx = 0; pileSizeIdx < this.PileSizesPlayer1Panel.Controls.Count; pileSizeIdx++)
            {
                NumericUpDown pileSizeNumericUpDown = (NumericUpDown)this.PileSizesPlayer1Panel.Controls[pileSizeIdx];
                if ((int)pileSizeNumericUpDown.Value != 0)
                    break;
            }
            if (pileSizeIdx == this.PileSizesPlayer1Panel.Controls.Count)
            {
                MessageBox.Show("Zero-vector already a terminal state!");
                result = false;
                return result;
            }
            foreach (var stateOutcomePair in this.TerminalStatesPlayer1ListBox.Items)
            {
                string stateOutcomePairStr = (string)stateOutcomePair;
                if (stateOutcomePairStr.Split(" - ")[0].Equals(newStateOutcomePairStr.Split(" - ")[0]))
                {
                    MessageBox.Show("Extra terminal state already added for Player#1!");
                    result = false;
                    return result;
                }
            }
            return result;
        }
        private bool ValidatePlayer2TerminalState(string newStateOutcomePairStr)
        {
            bool result = true;
            int pileSizeIdx;
            for (pileSizeIdx = 0; pileSizeIdx < this.PileSizesPlayer2Panel.Controls.Count; pileSizeIdx++)
            {
                NumericUpDown pileSizeNumericUpDown = (NumericUpDown)this.PileSizesPlayer2Panel.Controls[pileSizeIdx];
                if ((int)pileSizeNumericUpDown.Value != 0)
                    break;
            }
            if(pileSizeIdx == this.PileSizesPlayer2Panel.Controls.Count)
            {
                MessageBox.Show("Zero-vector already a terminal state!");
                result = false;
                return result;
            }
            foreach (var stateOutcomePair in this.TerminalStatesPlayer2ListBox.Items)
            {
                string stateOutcomePairStr = (string)stateOutcomePair;
                if (stateOutcomePairStr.Split(" - ")[0].Equals(newStateOutcomePairStr.Split(" - ")[0]))
                {
                    MessageBox.Show("Extra terminal state already added for Player#2!");
                    result = false;
                    return result;
                }
            }
            return result;
        }
        private void AddTerminalStatePlayer1Button_Click(object sender, EventArgs e)
        {
            string stateOutcomePairStr = "(";
            if(ValidatePlayer1TerminalState(stateOutcomePairStr))
            {
                foreach (var control in this.PileSizesPlayer1Panel.Controls)
                {
                    NumericUpDown pileSizeNumericUpDown = (NumericUpDown)control;
                    int pileSize = (int)pileSizeNumericUpDown.Value;
                    stateOutcomePairStr += pileSize.ToString() + ", ";
                }
                stateOutcomePairStr = stateOutcomePairStr[..^2];
                stateOutcomePairStr += ")";
                if(this.WinningStatePlayer1RadioButton.Checked == true) 
                    stateOutcomePairStr += " - [Y]";
                else
                    stateOutcomePairStr += " - [N]";
                if(ValidatePlayer1TerminalState(stateOutcomePairStr))
                    this.TerminalStatesPlayer1ListBox.Items.Add(stateOutcomePairStr);
            }
        }
        private void DeleteTerminalStatePlayer1Button_Click(object sender, EventArgs e)
        {
            int selectedTerminalStateIndex = this.TerminalStatesPlayer1ListBox.SelectedIndex;
            if (selectedTerminalStateIndex == -1)
            {
                MessageBox.Show("Please select a terminal state of Player#1 to delete!");
                return;
            }
            this.TerminalStatesPlayer1ListBox.Items.RemoveAt(selectedTerminalStateIndex);
        }
        private void AddTerminalStatePlayer2Button_Click(object sender, EventArgs e)
        {
            string stateOutcomePairStr = "(";
            if (ValidatePlayer2TerminalState(stateOutcomePairStr))
            {
                foreach (var control in this.PileSizesPlayer2Panel.Controls)
                {
                    NumericUpDown pileSizeNumericUpDown = (NumericUpDown)control;
                    int pileSize = (int)pileSizeNumericUpDown.Value;
                    stateOutcomePairStr += pileSize.ToString() + ", ";
                }
                stateOutcomePairStr = stateOutcomePairStr[..^2];
                stateOutcomePairStr += ")";
                if (this.WinningStatePlayer2RadioButton.Checked == true)
                    stateOutcomePairStr += " - [Y]";
                else
                    stateOutcomePairStr += " - [N]";
                if (ValidatePlayer2TerminalState(stateOutcomePairStr))
                    this.TerminalStatesPlayer2ListBox.Items.Add(stateOutcomePairStr);
            }
        }
        private void DeleteTerminalStatePlayer2Button_Click(object sender, EventArgs e)
        {
            int selectedTerminalStateIndex = this.TerminalStatesPlayer2ListBox.SelectedIndex;
            if (selectedTerminalStateIndex == -1)
            {
                MessageBox.Show("Please select a terminal state of Player#2 to delete!");
                return;
            }
            this.TerminalStatesPlayer2ListBox.Items.RemoveAt(selectedTerminalStateIndex);
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                List<List<Tuple<Dimensions, string>>> extraTerminalStates = new();
                List<int> pileSizes;
                string dimensionsStr;
                string outcomeStr;
                // Player#1
                extraTerminalStates.Add(new());
                foreach (var stateOutcome in this.TerminalStatesPlayer1ListBox.Items)
                {
                    dimensionsStr = stateOutcome.ToString().Split(" - ")[0];
                    outcomeStr = stateOutcome.ToString().Split(" - ")[1];
                    dimensionsStr = dimensionsStr[1..]; // minus (
                    dimensionsStr = dimensionsStr[..^1]; // minus )
                    pileSizes = new();
                    foreach (var pileSizeStr in dimensionsStr.Split(", "))
                    {
                        pileSizes.Add(int.Parse(pileSizeStr));
                    }
                    pileSizes.Add(0);
                    extraTerminalStates[0].Add(new(new(null, pileSizes), outcomeStr[1].ToString()));
                }
                // Player#2
                extraTerminalStates.Add(new());
                foreach (var stateOutcome in this.TerminalStatesPlayer2ListBox.Items)
                {
                    dimensionsStr = stateOutcome.ToString().Split(" - ")[0];
                    outcomeStr = stateOutcome.ToString().Split(" - ")[1];
                    dimensionsStr = dimensionsStr[1..]; // minus (
                    dimensionsStr = dimensionsStr[..^1]; // minus )
                    pileSizes = new();
                    foreach (var pileSizeStr in dimensionsStr.Split(", "))
                    {
                        pileSizes.Add(int.Parse(pileSizeStr));
                    }
                    pileSizes.Add(1);
                    extraTerminalStates[1].Add(new(new(null, pileSizes), outcomeStr[1].ToString()));
                }
                this.ParentCreateGameForm.SetExtraTerimnalStates(extraTerminalStates);
                this.ParentCreateGameForm.Enabled = true;
                this.Close();
            } 
        }
        private void TerminalStatesAsymetricForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ParentCreateGameForm.Enabled = true;
        }
    }
}
