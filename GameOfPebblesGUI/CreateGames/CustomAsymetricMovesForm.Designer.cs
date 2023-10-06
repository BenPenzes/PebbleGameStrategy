namespace GameOfPebblesGUI
{
    partial class CustomAsymetricMovesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PileNumbersLabel = new System.Windows.Forms.Label();
            this.PilesLabel = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.Modifier1Player1NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.DeleteMovePlayer1Button = new System.Windows.Forms.Button();
            this.AddMovePlayer1Button = new System.Windows.Forms.Button();
            this.MovesForPlayer1Label = new System.Windows.Forms.Label();
            this.MovesPlayer1ListBox = new System.Windows.Forms.ListBox();
            this.MovesPlayer2ListBox = new System.Windows.Forms.ListBox();
            this.Modifier1Player2NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.DeleteMovePlayer2Button = new System.Windows.Forms.Button();
            this.AddMovePlayer2Button = new System.Windows.Forms.Button();
            this.MovesForPlayer2Label = new System.Windows.Forms.Label();
            this.ModifiersPlayer1Panel = new System.Windows.Forms.Panel();
            this.ModifiersPlayer2Panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Modifier1Player1NumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Modifier1Player2NumericUpDown)).BeginInit();
            this.ModifiersPlayer1Panel.SuspendLayout();
            this.ModifiersPlayer2Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PileNumbersLabel
            // 
            this.PileNumbersLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PileNumbersLabel.AutoSize = true;
            this.PileNumbersLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PileNumbersLabel.Location = new System.Drawing.Point(436, 72);
            this.PileNumbersLabel.Name = "PileNumbersLabel";
            this.PileNumbersLabel.Size = new System.Drawing.Size(40, 46);
            this.PileNumbersLabel.TabIndex = 45;
            this.PileNumbersLabel.Text = "()";
            // 
            // PilesLabel
            // 
            this.PilesLabel.AutoSize = true;
            this.PilesLabel.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PilesLabel.Location = new System.Drawing.Point(394, 9);
            this.PilesLabel.Name = "PilesLabel";
            this.PilesLabel.Size = new System.Drawing.Size(137, 72);
            this.PilesLabel.TabIndex = 44;
            this.PilesLabel.Text = "Piles";
            // 
            // SaveButton
            // 
            this.SaveButton.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SaveButton.Location = new System.Drawing.Point(367, 521);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(188, 52);
            this.SaveButton.TabIndex = 43;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // Modifier1Player1NumericUpDown
            // 
            this.Modifier1Player1NumericUpDown.BackColor = System.Drawing.Color.FloralWhite;
            this.Modifier1Player1NumericUpDown.Location = new System.Drawing.Point(19, 13);
            this.Modifier1Player1NumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Modifier1Player1NumericUpDown.Name = "Modifier1Player1NumericUpDown";
            this.Modifier1Player1NumericUpDown.Size = new System.Drawing.Size(40, 27);
            this.Modifier1Player1NumericUpDown.TabIndex = 35;
            // 
            // DeleteMovePlayer1Button
            // 
            this.DeleteMovePlayer1Button.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DeleteMovePlayer1Button.Location = new System.Drawing.Point(250, 463);
            this.DeleteMovePlayer1Button.Name = "DeleteMovePlayer1Button";
            this.DeleteMovePlayer1Button.Size = new System.Drawing.Size(190, 52);
            this.DeleteMovePlayer1Button.TabIndex = 34;
            this.DeleteMovePlayer1Button.Text = "Delete Move";
            this.DeleteMovePlayer1Button.UseVisualStyleBackColor = true;
            this.DeleteMovePlayer1Button.Click += new System.EventHandler(this.DeleteMovePlayer1Button_Click);
            // 
            // AddMovePlayer1Button
            // 
            this.AddMovePlayer1Button.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AddMovePlayer1Button.Location = new System.Drawing.Point(56, 463);
            this.AddMovePlayer1Button.Name = "AddMovePlayer1Button";
            this.AddMovePlayer1Button.Size = new System.Drawing.Size(188, 52);
            this.AddMovePlayer1Button.TabIndex = 33;
            this.AddMovePlayer1Button.Text = "Add Move";
            this.AddMovePlayer1Button.UseVisualStyleBackColor = true;
            this.AddMovePlayer1Button.Click += new System.EventHandler(this.AddMovePlayer1Button_Click);
            // 
            // MovesForPlayer1Label
            // 
            this.MovesForPlayer1Label.AutoSize = true;
            this.MovesForPlayer1Label.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MovesForPlayer1Label.Location = new System.Drawing.Point(115, 123);
            this.MovesForPlayer1Label.Name = "MovesForPlayer1Label";
            this.MovesForPlayer1Label.Size = new System.Drawing.Size(274, 37);
            this.MovesForPlayer1Label.TabIndex = 32;
            this.MovesForPlayer1Label.Text = "Moves for Player #1";
            // 
            // MovesPlayer1ListBox
            // 
            this.MovesPlayer1ListBox.BackColor = System.Drawing.Color.FloralWhite;
            this.MovesPlayer1ListBox.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MovesPlayer1ListBox.FormattingEnabled = true;
            this.MovesPlayer1ListBox.ItemHeight = 31;
            this.MovesPlayer1ListBox.Location = new System.Drawing.Point(96, 163);
            this.MovesPlayer1ListBox.Name = "MovesPlayer1ListBox";
            this.MovesPlayer1ListBox.Size = new System.Drawing.Size(308, 221);
            this.MovesPlayer1ListBox.Sorted = true;
            this.MovesPlayer1ListBox.TabIndex = 46;
            // 
            // MovesPlayer2ListBox
            // 
            this.MovesPlayer2ListBox.BackColor = System.Drawing.Color.FloralWhite;
            this.MovesPlayer2ListBox.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MovesPlayer2ListBox.FormattingEnabled = true;
            this.MovesPlayer2ListBox.ItemHeight = 31;
            this.MovesPlayer2ListBox.Location = new System.Drawing.Point(520, 163);
            this.MovesPlayer2ListBox.Name = "MovesPlayer2ListBox";
            this.MovesPlayer2ListBox.Size = new System.Drawing.Size(317, 221);
            this.MovesPlayer2ListBox.Sorted = true;
            this.MovesPlayer2ListBox.TabIndex = 58;
            // 
            // Modifier1Player2NumericUpDown
            // 
            this.Modifier1Player2NumericUpDown.BackColor = System.Drawing.Color.FloralWhite;
            this.Modifier1Player2NumericUpDown.Location = new System.Drawing.Point(15, 13);
            this.Modifier1Player2NumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Modifier1Player2NumericUpDown.Name = "Modifier1Player2NumericUpDown";
            this.Modifier1Player2NumericUpDown.Size = new System.Drawing.Size(40, 27);
            this.Modifier1Player2NumericUpDown.TabIndex = 50;
            // 
            // DeleteMovePlayer2Button
            // 
            this.DeleteMovePlayer2Button.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DeleteMovePlayer2Button.Location = new System.Drawing.Point(678, 463);
            this.DeleteMovePlayer2Button.Name = "DeleteMovePlayer2Button";
            this.DeleteMovePlayer2Button.Size = new System.Drawing.Size(190, 52);
            this.DeleteMovePlayer2Button.TabIndex = 49;
            this.DeleteMovePlayer2Button.Text = "Delete Move";
            this.DeleteMovePlayer2Button.UseVisualStyleBackColor = true;
            this.DeleteMovePlayer2Button.Click += new System.EventHandler(this.DeleteMovePlayer2Button_Click);
            // 
            // AddMovePlayer2Button
            // 
            this.AddMovePlayer2Button.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AddMovePlayer2Button.Location = new System.Drawing.Point(484, 463);
            this.AddMovePlayer2Button.Name = "AddMovePlayer2Button";
            this.AddMovePlayer2Button.Size = new System.Drawing.Size(188, 52);
            this.AddMovePlayer2Button.TabIndex = 48;
            this.AddMovePlayer2Button.Text = "Add Move";
            this.AddMovePlayer2Button.UseVisualStyleBackColor = true;
            this.AddMovePlayer2Button.Click += new System.EventHandler(this.AddMovePlayer2Button_Click);
            // 
            // MovesForPlayer2Label
            // 
            this.MovesForPlayer2Label.AutoSize = true;
            this.MovesForPlayer2Label.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MovesForPlayer2Label.Location = new System.Drawing.Point(535, 123);
            this.MovesForPlayer2Label.Name = "MovesForPlayer2Label";
            this.MovesForPlayer2Label.Size = new System.Drawing.Size(274, 37);
            this.MovesForPlayer2Label.TabIndex = 47;
            this.MovesForPlayer2Label.Text = "Moves for Player #2";
            // 
            // ModifiersPlayer1Panel
            // 
            this.ModifiersPlayer1Panel.Controls.Add(this.Modifier1Player1NumericUpDown);
            this.ModifiersPlayer1Panel.Location = new System.Drawing.Point(207, 393);
            this.ModifiersPlayer1Panel.Name = "ModifiersPlayer1Panel";
            this.ModifiersPlayer1Panel.Size = new System.Drawing.Size(91, 52);
            this.ModifiersPlayer1Panel.TabIndex = 59;
            // 
            // ModifiersPlayer2Panel
            // 
            this.ModifiersPlayer2Panel.Controls.Add(this.Modifier1Player2NumericUpDown);
            this.ModifiersPlayer2Panel.Location = new System.Drawing.Point(640, 393);
            this.ModifiersPlayer2Panel.Name = "ModifiersPlayer2Panel";
            this.ModifiersPlayer2Panel.Size = new System.Drawing.Size(75, 52);
            this.ModifiersPlayer2Panel.TabIndex = 60;
            // 
            // CustomAsymetricMovesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(919, 599);
            this.Controls.Add(this.ModifiersPlayer2Panel);
            this.Controls.Add(this.ModifiersPlayer1Panel);
            this.Controls.Add(this.MovesPlayer2ListBox);
            this.Controls.Add(this.DeleteMovePlayer2Button);
            this.Controls.Add(this.AddMovePlayer2Button);
            this.Controls.Add(this.MovesForPlayer2Label);
            this.Controls.Add(this.MovesPlayer1ListBox);
            this.Controls.Add(this.PileNumbersLabel);
            this.Controls.Add(this.PilesLabel);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.DeleteMovePlayer1Button);
            this.Controls.Add(this.AddMovePlayer1Button);
            this.Controls.Add(this.MovesForPlayer1Label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CustomAsymetricMovesForm";
            this.Text = "CustomAsymetricMoves";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CustomAsymetricMovesForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.Modifier1Player1NumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Modifier1Player2NumericUpDown)).EndInit();
            this.ModifiersPlayer1Panel.ResumeLayout(false);
            this.ModifiersPlayer2Panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label PileNumbersLabel;
        private Label PilesLabel;
        private Button SaveButton;
        private NumericUpDown Modifier1Player1NumericUpDown;
        private Button DeleteMovePlayer1Button;
        private Button AddMovePlayer1Button;
        private Label MovesForPlayer1Label;
        private ListBox MovesPlayer1ListBox;
        private ListBox MovesPlayer2ListBox;
        private NumericUpDown Modifier1Player2NumericUpDown;
        private Button DeleteMovePlayer2Button;
        private Button AddMovePlayer2Button;
        private Label MovesForPlayer2Label;
        private Panel ModifiersPlayer1Panel;
        private Panel ModifiersPlayer2Panel;
    }
}