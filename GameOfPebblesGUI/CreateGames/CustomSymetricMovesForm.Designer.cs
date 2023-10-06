namespace GameOfPebblesGUI
{
    partial class CustomSymetricMovesForm
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
            this.MovesListBox = new System.Windows.Forms.ListBox();
            this.MovesLabel = new System.Windows.Forms.Label();
            this.DeleteMoveButton = new System.Windows.Forms.Button();
            this.AddMoveButton = new System.Windows.Forms.Button();
            this.Modifier1NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.SaveButton = new System.Windows.Forms.Button();
            this.PilesLabel = new System.Windows.Forms.Label();
            this.PileNumbersLabel = new System.Windows.Forms.Label();
            this.ModifiersPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Modifier1NumericUpDown)).BeginInit();
            this.ModifiersPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MovesListBox
            // 
            this.MovesListBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.MovesListBox.BackColor = System.Drawing.Color.FloralWhite;
            this.MovesListBox.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MovesListBox.FormattingEnabled = true;
            this.MovesListBox.ItemHeight = 31;
            this.MovesListBox.Location = new System.Drawing.Point(72, 181);
            this.MovesListBox.Name = "MovesListBox";
            this.MovesListBox.Size = new System.Drawing.Size(384, 221);
            this.MovesListBox.Sorted = true;
            this.MovesListBox.TabIndex = 8;
            // 
            // MovesLabel
            // 
            this.MovesLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.MovesLabel.AutoSize = true;
            this.MovesLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MovesLabel.Location = new System.Drawing.Point(208, 132);
            this.MovesLabel.Name = "MovesLabel";
            this.MovesLabel.Size = new System.Drawing.Size(119, 46);
            this.MovesLabel.TabIndex = 6;
            this.MovesLabel.Text = "Moves";
            // 
            // DeleteMoveButton
            // 
            this.DeleteMoveButton.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DeleteMoveButton.Location = new System.Drawing.Point(266, 468);
            this.DeleteMoveButton.Name = "DeleteMoveButton";
            this.DeleteMoveButton.Size = new System.Drawing.Size(190, 52);
            this.DeleteMoveButton.TabIndex = 14;
            this.DeleteMoveButton.Text = "Delete Move";
            this.DeleteMoveButton.UseVisualStyleBackColor = true;
            this.DeleteMoveButton.Click += new System.EventHandler(this.DeleteMoveButton_Click);
            // 
            // AddMoveButton
            // 
            this.AddMoveButton.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AddMoveButton.Location = new System.Drawing.Point(72, 468);
            this.AddMoveButton.Name = "AddMoveButton";
            this.AddMoveButton.Size = new System.Drawing.Size(188, 52);
            this.AddMoveButton.TabIndex = 13;
            this.AddMoveButton.Text = "Add Move";
            this.AddMoveButton.UseVisualStyleBackColor = true;
            this.AddMoveButton.Click += new System.EventHandler(this.AddMoveButton_Click);
            // 
            // Modifier1NumericUpDown
            // 
            this.Modifier1NumericUpDown.BackColor = System.Drawing.Color.FloralWhite;
            this.Modifier1NumericUpDown.Location = new System.Drawing.Point(13, 12);
            this.Modifier1NumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Modifier1NumericUpDown.Name = "Modifier1NumericUpDown";
            this.Modifier1NumericUpDown.Size = new System.Drawing.Size(40, 27);
            this.Modifier1NumericUpDown.TabIndex = 21;
            // 
            // SaveButton
            // 
            this.SaveButton.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SaveButton.Location = new System.Drawing.Point(163, 526);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(188, 52);
            this.SaveButton.TabIndex = 29;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // PilesLabel
            // 
            this.PilesLabel.AutoSize = true;
            this.PilesLabel.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PilesLabel.Location = new System.Drawing.Point(199, 9);
            this.PilesLabel.Name = "PilesLabel";
            this.PilesLabel.Size = new System.Drawing.Size(137, 72);
            this.PilesLabel.TabIndex = 30;
            this.PilesLabel.Text = "Piles";
            // 
            // PileNumbersLabel
            // 
            this.PileNumbersLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PileNumbersLabel.AutoSize = true;
            this.PileNumbersLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PileNumbersLabel.Location = new System.Drawing.Point(239, 78);
            this.PileNumbersLabel.Name = "PileNumbersLabel";
            this.PileNumbersLabel.Size = new System.Drawing.Size(47, 54);
            this.PileNumbersLabel.TabIndex = 31;
            this.PileNumbersLabel.Text = "()";
            // 
            // ModifiersPanel
            // 
            this.ModifiersPanel.Controls.Add(this.Modifier1NumericUpDown);
            this.ModifiersPanel.Location = new System.Drawing.Point(227, 408);
            this.ModifiersPanel.Name = "ModifiersPanel";
            this.ModifiersPanel.Size = new System.Drawing.Size(79, 54);
            this.ModifiersPanel.TabIndex = 32;
            // 
            // CustomSymetricMovesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(530, 603);
            this.Controls.Add(this.ModifiersPanel);
            this.Controls.Add(this.PileNumbersLabel);
            this.Controls.Add(this.PilesLabel);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.DeleteMoveButton);
            this.Controls.Add(this.AddMoveButton);
            this.Controls.Add(this.MovesListBox);
            this.Controls.Add(this.MovesLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CustomSymetricMovesForm";
            this.Text = "CustomMoves";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CustomSymetricMovesForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.Modifier1NumericUpDown)).EndInit();
            this.ModifiersPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox MovesListBox;
        private Label MovesLabel;
        private Button DeleteMoveButton;
        private Button AddMoveButton;
        private NumericUpDown Modifier1NumericUpDown;
        private Button SaveButton;
        private Label PilesLabel;
        private Label PileNumbersLabel;
        private Panel ModifiersPanel;
    }
}