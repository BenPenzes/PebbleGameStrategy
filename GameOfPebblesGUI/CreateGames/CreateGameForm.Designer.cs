
namespace GameUI
{
    partial class CreateGameForm
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
            this.SizeOfThePileLabel = new System.Windows.Forms.Label();
            this.SizeOfThePileUpDown = new System.Windows.Forms.NumericUpDown();
            this.CreateGameButton = new System.Windows.Forms.Button();
            this.AI_IntelligenceGroupBox = new System.Windows.Forms.GroupBox();
            this.HighIntelligenceRadioButton = new System.Windows.Forms.RadioButton();
            this.MidIntelligenceRadioButton = new System.Windows.Forms.RadioButton();
            this.LowIntelligenceRadioButton = new System.Windows.Forms.RadioButton();
            this.MovesetComboBox = new System.Windows.Forms.ComboBox();
            this.SymetricMovesComboBox = new System.Windows.Forms.ComboBox();
            this.PvPOrPvEGroupBox = new System.Windows.Forms.GroupBox();
            this.PvERadioButton = new System.Windows.Forms.RadioButton();
            this.PvPRadioButton = new System.Windows.Forms.RadioButton();
            this.WhoGoesFirstComboBox = new System.Windows.Forms.ComboBox();
            this.LastMoveWinsGroupBox = new System.Windows.Forms.GroupBox();
            this.NoLastMoveWinsRadioButton = new System.Windows.Forms.RadioButton();
            this.YesLastMoveWinsRadioButton = new System.Windows.Forms.RadioButton();
            this.ExtraTerminalStatesGroupBox = new System.Windows.Forms.GroupBox();
            this.NoExtraTerminalStatesRadioButton = new System.Windows.Forms.RadioButton();
            this.YesExtraTerminalStatesRadioButton = new System.Windows.Forms.RadioButton();
            this.TerminalStatesButton = new System.Windows.Forms.Button();
            this.TableFillingAlgorithmComboBox = new System.Windows.Forms.ComboBox();
            this.PileSizesPanel = new System.Windows.Forms.Panel();
            this.RemovePileButton = new System.Windows.Forms.Button();
            this.AddPileButton = new System.Windows.Forms.Button();
            this.WhoGoesFirstLabel = new System.Windows.Forms.Label();
            this.MovesetLabel = new System.Windows.Forms.Label();
            this.SymetricMovesLabel = new System.Windows.Forms.Label();
            this.NameOfTheGameTextBox = new System.Windows.Forms.TextBox();
            this.TableFillingAlgorithmLabel = new System.Windows.Forms.Label();
            this.MovesButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SizeOfThePileUpDown)).BeginInit();
            this.AI_IntelligenceGroupBox.SuspendLayout();
            this.PvPOrPvEGroupBox.SuspendLayout();
            this.LastMoveWinsGroupBox.SuspendLayout();
            this.ExtraTerminalStatesGroupBox.SuspendLayout();
            this.PileSizesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SizeOfThePileLabel
            // 
            this.SizeOfThePileLabel.AutoSize = true;
            this.SizeOfThePileLabel.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SizeOfThePileLabel.Location = new System.Drawing.Point(298, 9);
            this.SizeOfThePileLabel.Name = "SizeOfThePileLabel";
            this.SizeOfThePileLabel.Size = new System.Drawing.Size(137, 72);
            this.SizeOfThePileLabel.TabIndex = 1;
            this.SizeOfThePileLabel.Text = "Piles";
            // 
            // SizeOfThePileUpDown
            // 
            this.SizeOfThePileUpDown.BackColor = System.Drawing.Color.White;
            this.SizeOfThePileUpDown.Location = new System.Drawing.Point(16, 28);
            this.SizeOfThePileUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.SizeOfThePileUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SizeOfThePileUpDown.Name = "SizeOfThePileUpDown";
            this.SizeOfThePileUpDown.Size = new System.Drawing.Size(70, 34);
            this.SizeOfThePileUpDown.TabIndex = 4;
            this.SizeOfThePileUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // CreateGameButton
            // 
            this.CreateGameButton.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CreateGameButton.Location = new System.Drawing.Point(455, 718);
            this.CreateGameButton.Name = "CreateGameButton";
            this.CreateGameButton.Size = new System.Drawing.Size(187, 78);
            this.CreateGameButton.TabIndex = 9;
            this.CreateGameButton.Text = "Create";
            this.CreateGameButton.UseVisualStyleBackColor = true;
            this.CreateGameButton.Click += new System.EventHandler(this.CreateGameButton_Click);
            // 
            // AI_IntelligenceGroupBox
            // 
            this.AI_IntelligenceGroupBox.Controls.Add(this.HighIntelligenceRadioButton);
            this.AI_IntelligenceGroupBox.Controls.Add(this.MidIntelligenceRadioButton);
            this.AI_IntelligenceGroupBox.Controls.Add(this.LowIntelligenceRadioButton);
            this.AI_IntelligenceGroupBox.Location = new System.Drawing.Point(161, 379);
            this.AI_IntelligenceGroupBox.Name = "AI_IntelligenceGroupBox";
            this.AI_IntelligenceGroupBox.Size = new System.Drawing.Size(408, 101);
            this.AI_IntelligenceGroupBox.TabIndex = 8;
            this.AI_IntelligenceGroupBox.TabStop = false;
            this.AI_IntelligenceGroupBox.Text = "Choose the AI\'s Intelligence";
            this.AI_IntelligenceGroupBox.Visible = false;
            // 
            // HighIntelligenceRadioButton
            // 
            this.HighIntelligenceRadioButton.AutoSize = true;
            this.HighIntelligenceRadioButton.Location = new System.Drawing.Point(277, 45);
            this.HighIntelligenceRadioButton.Name = "HighIntelligenceRadioButton";
            this.HighIntelligenceRadioButton.Size = new System.Drawing.Size(75, 32);
            this.HighIntelligenceRadioButton.TabIndex = 8;
            this.HighIntelligenceRadioButton.TabStop = true;
            this.HighIntelligenceRadioButton.Text = "High";
            this.HighIntelligenceRadioButton.UseVisualStyleBackColor = true;
            // 
            // MidIntelligenceRadioButton
            // 
            this.MidIntelligenceRadioButton.AutoSize = true;
            this.MidIntelligenceRadioButton.Location = new System.Drawing.Point(159, 45);
            this.MidIntelligenceRadioButton.Name = "MidIntelligenceRadioButton";
            this.MidIntelligenceRadioButton.Size = new System.Drawing.Size(68, 32);
            this.MidIntelligenceRadioButton.TabIndex = 7;
            this.MidIntelligenceRadioButton.TabStop = true;
            this.MidIntelligenceRadioButton.Text = "Mid";
            this.MidIntelligenceRadioButton.UseVisualStyleBackColor = true;
            // 
            // LowIntelligenceRadioButton
            // 
            this.LowIntelligenceRadioButton.AutoSize = true;
            this.LowIntelligenceRadioButton.Location = new System.Drawing.Point(43, 45);
            this.LowIntelligenceRadioButton.Name = "LowIntelligenceRadioButton";
            this.LowIntelligenceRadioButton.Size = new System.Drawing.Size(68, 32);
            this.LowIntelligenceRadioButton.TabIndex = 6;
            this.LowIntelligenceRadioButton.TabStop = true;
            this.LowIntelligenceRadioButton.Text = "Low";
            this.LowIntelligenceRadioButton.UseVisualStyleBackColor = true;
            // 
            // MovesetComboBox
            // 
            this.MovesetComboBox.BackColor = System.Drawing.Color.FloralWhite;
            this.MovesetComboBox.FormattingEnabled = true;
            this.MovesetComboBox.Location = new System.Drawing.Point(158, 201);
            this.MovesetComboBox.Name = "MovesetComboBox";
            this.MovesetComboBox.Size = new System.Drawing.Size(191, 36);
            this.MovesetComboBox.TabIndex = 8;
            this.MovesetComboBox.SelectedIndexChanged += new System.EventHandler(this.MovesetComboBox_SelectedIndexChanged);
            // 
            // SymetricMovesComboBox
            // 
            this.SymetricMovesComboBox.BackColor = System.Drawing.Color.FloralWhite;
            this.SymetricMovesComboBox.FormattingEnabled = true;
            this.SymetricMovesComboBox.Location = new System.Drawing.Point(363, 201);
            this.SymetricMovesComboBox.Name = "SymetricMovesComboBox";
            this.SymetricMovesComboBox.Size = new System.Drawing.Size(206, 36);
            this.SymetricMovesComboBox.TabIndex = 21;
            this.SymetricMovesComboBox.Visible = false;
            // 
            // PvPOrPvEGroupBox
            // 
            this.PvPOrPvEGroupBox.Controls.Add(this.PvERadioButton);
            this.PvPOrPvEGroupBox.Controls.Add(this.PvPRadioButton);
            this.PvPOrPvEGroupBox.Location = new System.Drawing.Point(161, 286);
            this.PvPOrPvEGroupBox.Name = "PvPOrPvEGroupBox";
            this.PvPOrPvEGroupBox.Size = new System.Drawing.Size(178, 87);
            this.PvPOrPvEGroupBox.TabIndex = 9;
            this.PvPOrPvEGroupBox.TabStop = false;
            this.PvPOrPvEGroupBox.Text = "PvP or PvE";
            // 
            // PvERadioButton
            // 
            this.PvERadioButton.AutoSize = true;
            this.PvERadioButton.Location = new System.Drawing.Point(95, 33);
            this.PvERadioButton.Name = "PvERadioButton";
            this.PvERadioButton.Size = new System.Drawing.Size(64, 32);
            this.PvERadioButton.TabIndex = 7;
            this.PvERadioButton.TabStop = true;
            this.PvERadioButton.Text = "PvE";
            this.PvERadioButton.UseVisualStyleBackColor = true;
            this.PvERadioButton.CheckedChanged += new System.EventHandler(this.PvERadioButton_CheckedChanged);
            // 
            // PvPRadioButton
            // 
            this.PvPRadioButton.AutoSize = true;
            this.PvPRadioButton.Location = new System.Drawing.Point(19, 33);
            this.PvPRadioButton.Name = "PvPRadioButton";
            this.PvPRadioButton.Size = new System.Drawing.Size(65, 32);
            this.PvPRadioButton.TabIndex = 6;
            this.PvPRadioButton.TabStop = true;
            this.PvPRadioButton.Text = "PvP";
            this.PvPRadioButton.UseVisualStyleBackColor = true;
            // 
            // WhoGoesFirstComboBox
            // 
            this.WhoGoesFirstComboBox.BackColor = System.Drawing.Color.FloralWhite;
            this.WhoGoesFirstComboBox.FormattingEnabled = true;
            this.WhoGoesFirstComboBox.Location = new System.Drawing.Point(353, 337);
            this.WhoGoesFirstComboBox.Name = "WhoGoesFirstComboBox";
            this.WhoGoesFirstComboBox.Size = new System.Drawing.Size(216, 36);
            this.WhoGoesFirstComboBox.TabIndex = 22;
            this.WhoGoesFirstComboBox.Visible = false;
            // 
            // LastMoveWinsGroupBox
            // 
            this.LastMoveWinsGroupBox.Controls.Add(this.NoLastMoveWinsRadioButton);
            this.LastMoveWinsGroupBox.Controls.Add(this.YesLastMoveWinsRadioButton);
            this.LastMoveWinsGroupBox.Location = new System.Drawing.Point(161, 486);
            this.LastMoveWinsGroupBox.Name = "LastMoveWinsGroupBox";
            this.LastMoveWinsGroupBox.Size = new System.Drawing.Size(191, 101);
            this.LastMoveWinsGroupBox.TabIndex = 10;
            this.LastMoveWinsGroupBox.TabStop = false;
            this.LastMoveWinsGroupBox.Text = "Last Move Wins";
            // 
            // NoLastMoveWinsRadioButton
            // 
            this.NoLastMoveWinsRadioButton.AutoSize = true;
            this.NoLastMoveWinsRadioButton.Location = new System.Drawing.Point(96, 45);
            this.NoLastMoveWinsRadioButton.Name = "NoLastMoveWinsRadioButton";
            this.NoLastMoveWinsRadioButton.Size = new System.Drawing.Size(60, 32);
            this.NoLastMoveWinsRadioButton.TabIndex = 7;
            this.NoLastMoveWinsRadioButton.TabStop = true;
            this.NoLastMoveWinsRadioButton.Text = "No";
            this.NoLastMoveWinsRadioButton.UseVisualStyleBackColor = true;
            // 
            // YesLastMoveWinsRadioButton
            // 
            this.YesLastMoveWinsRadioButton.AutoSize = true;
            this.YesLastMoveWinsRadioButton.Location = new System.Drawing.Point(16, 45);
            this.YesLastMoveWinsRadioButton.Name = "YesLastMoveWinsRadioButton";
            this.YesLastMoveWinsRadioButton.Size = new System.Drawing.Size(60, 32);
            this.YesLastMoveWinsRadioButton.TabIndex = 6;
            this.YesLastMoveWinsRadioButton.TabStop = true;
            this.YesLastMoveWinsRadioButton.Text = "Yes";
            this.YesLastMoveWinsRadioButton.UseVisualStyleBackColor = true;
            // 
            // ExtraTerminalStatesGroupBox
            // 
            this.ExtraTerminalStatesGroupBox.Controls.Add(this.NoExtraTerminalStatesRadioButton);
            this.ExtraTerminalStatesGroupBox.Controls.Add(this.YesExtraTerminalStatesRadioButton);
            this.ExtraTerminalStatesGroupBox.Location = new System.Drawing.Point(357, 486);
            this.ExtraTerminalStatesGroupBox.Name = "ExtraTerminalStatesGroupBox";
            this.ExtraTerminalStatesGroupBox.Size = new System.Drawing.Size(212, 101);
            this.ExtraTerminalStatesGroupBox.TabIndex = 11;
            this.ExtraTerminalStatesGroupBox.TabStop = false;
            this.ExtraTerminalStatesGroupBox.Text = "Extra Terminal States";
            // 
            // NoExtraTerminalStatesRadioButton
            // 
            this.NoExtraTerminalStatesRadioButton.AutoSize = true;
            this.NoExtraTerminalStatesRadioButton.Location = new System.Drawing.Point(96, 47);
            this.NoExtraTerminalStatesRadioButton.Name = "NoExtraTerminalStatesRadioButton";
            this.NoExtraTerminalStatesRadioButton.Size = new System.Drawing.Size(60, 32);
            this.NoExtraTerminalStatesRadioButton.TabIndex = 7;
            this.NoExtraTerminalStatesRadioButton.TabStop = true;
            this.NoExtraTerminalStatesRadioButton.Text = "No";
            this.NoExtraTerminalStatesRadioButton.UseVisualStyleBackColor = true;
            // 
            // YesExtraTerminalStatesRadioButton
            // 
            this.YesExtraTerminalStatesRadioButton.AutoSize = true;
            this.YesExtraTerminalStatesRadioButton.Location = new System.Drawing.Point(18, 47);
            this.YesExtraTerminalStatesRadioButton.Name = "YesExtraTerminalStatesRadioButton";
            this.YesExtraTerminalStatesRadioButton.Size = new System.Drawing.Size(60, 32);
            this.YesExtraTerminalStatesRadioButton.TabIndex = 6;
            this.YesExtraTerminalStatesRadioButton.TabStop = true;
            this.YesExtraTerminalStatesRadioButton.Text = "Yes";
            this.YesExtraTerminalStatesRadioButton.UseVisualStyleBackColor = true;
            this.YesExtraTerminalStatesRadioButton.CheckedChanged += new System.EventHandler(this.YesExtraTerminalStatesRadioButton_CheckedChanged);
            // 
            // TerminalStatesButton
            // 
            this.TerminalStatesButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TerminalStatesButton.Location = new System.Drawing.Point(256, 593);
            this.TerminalStatesButton.Name = "TerminalStatesButton";
            this.TerminalStatesButton.Size = new System.Drawing.Size(196, 43);
            this.TerminalStatesButton.TabIndex = 23;
            this.TerminalStatesButton.Text = "Terminal States";
            this.TerminalStatesButton.UseVisualStyleBackColor = true;
            this.TerminalStatesButton.Visible = false;
            this.TerminalStatesButton.Click += new System.EventHandler(this.TerminalStatesButton_Click);
            // 
            // TableFillingAlgorithmComboBox
            // 
            this.TableFillingAlgorithmComboBox.FormattingEnabled = true;
            this.TableFillingAlgorithmComboBox.Location = new System.Drawing.Point(161, 676);
            this.TableFillingAlgorithmComboBox.Name = "TableFillingAlgorithmComboBox";
            this.TableFillingAlgorithmComboBox.Size = new System.Drawing.Size(408, 36);
            this.TableFillingAlgorithmComboBox.TabIndex = 25;
            // 
            // PileSizesPanel
            // 
            this.PileSizesPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PileSizesPanel.Controls.Add(this.SizeOfThePileUpDown);
            this.PileSizesPanel.Location = new System.Drawing.Point(309, 84);
            this.PileSizesPanel.Name = "PileSizesPanel";
            this.PileSizesPanel.Size = new System.Drawing.Size(102, 74);
            this.PileSizesPanel.TabIndex = 26;
            // 
            // RemovePileButton
            // 
            this.RemovePileButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RemovePileButton.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RemovePileButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.RemovePileButton.Location = new System.Drawing.Point(235, 91);
            this.RemovePileButton.Margin = new System.Windows.Forms.Padding(0);
            this.RemovePileButton.Name = "RemovePileButton";
            this.RemovePileButton.Size = new System.Drawing.Size(66, 61);
            this.RemovePileButton.TabIndex = 28;
            this.RemovePileButton.Text = "-";
            this.RemovePileButton.UseVisualStyleBackColor = true;
            this.RemovePileButton.Visible = false;
            this.RemovePileButton.Click += new System.EventHandler(this.RemovePileButton_Click);
            // 
            // AddPileButton
            // 
            this.AddPileButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.AddPileButton.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AddPileButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.AddPileButton.Location = new System.Drawing.Point(417, 94);
            this.AddPileButton.Margin = new System.Windows.Forms.Padding(0);
            this.AddPileButton.Name = "AddPileButton";
            this.AddPileButton.Size = new System.Drawing.Size(66, 58);
            this.AddPileButton.TabIndex = 27;
            this.AddPileButton.Text = "+";
            this.AddPileButton.UseVisualStyleBackColor = true;
            this.AddPileButton.Click += new System.EventHandler(this.AddPileButton_Click);
            // 
            // WhoGoesFirstLabel
            // 
            this.WhoGoesFirstLabel.AutoSize = true;
            this.WhoGoesFirstLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.WhoGoesFirstLabel.Location = new System.Drawing.Point(363, 297);
            this.WhoGoesFirstLabel.Name = "WhoGoesFirstLabel";
            this.WhoGoesFirstLabel.Size = new System.Drawing.Size(196, 37);
            this.WhoGoesFirstLabel.TabIndex = 29;
            this.WhoGoesFirstLabel.Text = "Who Goes First";
            this.WhoGoesFirstLabel.Visible = false;
            // 
            // MovesetLabel
            // 
            this.MovesetLabel.AutoSize = true;
            this.MovesetLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MovesetLabel.Location = new System.Drawing.Point(196, 161);
            this.MovesetLabel.Name = "MovesetLabel";
            this.MovesetLabel.Size = new System.Drawing.Size(118, 37);
            this.MovesetLabel.TabIndex = 30;
            this.MovesetLabel.Text = "Moveset";
            // 
            // SymetricMovesLabel
            // 
            this.SymetricMovesLabel.AutoSize = true;
            this.SymetricMovesLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SymetricMovesLabel.Location = new System.Drawing.Point(417, 161);
            this.SymetricMovesLabel.Name = "SymetricMovesLabel";
            this.SymetricMovesLabel.Size = new System.Drawing.Size(111, 37);
            this.SymetricMovesLabel.TabIndex = 31;
            this.SymetricMovesLabel.Text = "Symetry";
            this.SymetricMovesLabel.Visible = false;
            // 
            // NameOfTheGameTextBox
            // 
            this.NameOfTheGameTextBox.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NameOfTheGameTextBox.Location = new System.Drawing.Point(96, 721);
            this.NameOfTheGameTextBox.Name = "NameOfTheGameTextBox";
            this.NameOfTheGameTextBox.Size = new System.Drawing.Size(352, 61);
            this.NameOfTheGameTextBox.TabIndex = 24;
            // 
            // TableFillingAlgorithmLabel
            // 
            this.TableFillingAlgorithmLabel.AutoSize = true;
            this.TableFillingAlgorithmLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TableFillingAlgorithmLabel.Location = new System.Drawing.Point(233, 641);
            this.TableFillingAlgorithmLabel.Name = "TableFillingAlgorithmLabel";
            this.TableFillingAlgorithmLabel.Size = new System.Drawing.Size(253, 32);
            this.TableFillingAlgorithmLabel.TabIndex = 32;
            this.TableFillingAlgorithmLabel.Text = "Table Filling Algorithm";
            // 
            // MovesButton
            // 
            this.MovesButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MovesButton.Location = new System.Drawing.Point(257, 243);
            this.MovesButton.Name = "MovesButton";
            this.MovesButton.Size = new System.Drawing.Size(196, 43);
            this.MovesButton.TabIndex = 33;
            this.MovesButton.Text = "Moves";
            this.MovesButton.UseVisualStyleBackColor = true;
            this.MovesButton.Visible = false;
            this.MovesButton.Click += new System.EventHandler(this.MovesButton_Click);
            // 
            // CreateGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(738, 811);
            this.Controls.Add(this.MovesButton);
            this.Controls.Add(this.RemovePileButton);
            this.Controls.Add(this.TableFillingAlgorithmLabel);
            this.Controls.Add(this.AddPileButton);
            this.Controls.Add(this.SymetricMovesLabel);
            this.Controls.Add(this.MovesetLabel);
            this.Controls.Add(this.WhoGoesFirstLabel);
            this.Controls.Add(this.PileSizesPanel);
            this.Controls.Add(this.TableFillingAlgorithmComboBox);
            this.Controls.Add(this.NameOfTheGameTextBox);
            this.Controls.Add(this.TerminalStatesButton);
            this.Controls.Add(this.ExtraTerminalStatesGroupBox);
            this.Controls.Add(this.LastMoveWinsGroupBox);
            this.Controls.Add(this.WhoGoesFirstComboBox);
            this.Controls.Add(this.PvPOrPvEGroupBox);
            this.Controls.Add(this.SymetricMovesComboBox);
            this.Controls.Add(this.MovesetComboBox);
            this.Controls.Add(this.AI_IntelligenceGroupBox);
            this.Controls.Add(this.CreateGameButton);
            this.Controls.Add(this.SizeOfThePileLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CreateGameForm";
            this.Text = "CreateGameForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateGameForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.SizeOfThePileUpDown)).EndInit();
            this.AI_IntelligenceGroupBox.ResumeLayout(false);
            this.AI_IntelligenceGroupBox.PerformLayout();
            this.PvPOrPvEGroupBox.ResumeLayout(false);
            this.PvPOrPvEGroupBox.PerformLayout();
            this.LastMoveWinsGroupBox.ResumeLayout(false);
            this.LastMoveWinsGroupBox.PerformLayout();
            this.ExtraTerminalStatesGroupBox.ResumeLayout(false);
            this.ExtraTerminalStatesGroupBox.PerformLayout();
            this.PileSizesPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label SizeOfThePileLabel;
        private System.Windows.Forms.NumericUpDown SizeOfThePileUpDown;
        private System.Windows.Forms.Button CreateGameButton;
        private System.Windows.Forms.GroupBox AI_IntelligenceGroupBox;
        private System.Windows.Forms.RadioButton HighIntelligenceRadioButton;
        private System.Windows.Forms.RadioButton MidIntelligenceRadioButton;
        private System.Windows.Forms.RadioButton LowIntelligenceRadioButton;
        private ComboBox MovesetComboBox;
        private ComboBox SymetricMovesComboBox;
        private GroupBox PvPOrPvEGroupBox;
        private RadioButton PvERadioButton;
        private RadioButton PvPRadioButton;
        private ComboBox WhoGoesFirstComboBox;
        private GroupBox LastMoveWinsGroupBox;
        private RadioButton NoLastMoveWinsRadioButton;
        private RadioButton YesLastMoveWinsRadioButton;
        private GroupBox ExtraTerminalStatesGroupBox;
        private RadioButton NoExtraTerminalStatesRadioButton;
        private RadioButton YesExtraTerminalStatesRadioButton;
        private Button TerminalStatesButton;
        private ComboBox TableFillingAlgorithmComboBox;
        private Panel PileSizesPanel;
        private Button AddPileButton;
        private Button RemovePileButton;
        private Label WhoGoesFirstLabel;
        private Label MovesetLabel;
        private Label SymetricMovesLabel;
        private TextBox NameOfTheGameTextBox;
        private Label TableFillingAlgorithmLabel;
        private Button MovesButton;
    }
}