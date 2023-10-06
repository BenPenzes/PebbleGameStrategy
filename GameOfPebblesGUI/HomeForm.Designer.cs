namespace GameOfPebblesGUI
{
    partial class HomeForm
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
            this.SaveStatisticsButton = new System.Windows.Forms.Button();
            this.PrintStrategyTableButtton = new System.Windows.Forms.Button();
            this.NewGameButton = new System.Windows.Forms.Button();
            this.GamesListBox = new System.Windows.Forms.ListBox();
            this.PlayButton = new System.Windows.Forms.Button();
            this.DeleteGameButton = new System.Windows.Forms.Button();
            this.GamePreviewPanel = new System.Windows.Forms.Panel();
            this.ChooseAGameLabel = new System.Windows.Forms.Label();
            this.GamesLabel = new System.Windows.Forms.Label();
            this.GamePreviewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SaveStatisticsButton
            // 
            this.SaveStatisticsButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.SaveStatisticsButton.Location = new System.Drawing.Point(734, 401);
            this.SaveStatisticsButton.Name = "SaveStatisticsButton";
            this.SaveStatisticsButton.Size = new System.Drawing.Size(107, 75);
            this.SaveStatisticsButton.TabIndex = 0;
            this.SaveStatisticsButton.Text = "Save Statistics";
            this.SaveStatisticsButton.UseVisualStyleBackColor = true;
            this.SaveStatisticsButton.Click += new System.EventHandler(this.SaveStatisticsButton_Click);
            // 
            // PrintStrategyTableButtton
            // 
            this.PrintStrategyTableButtton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PrintStrategyTableButtton.Location = new System.Drawing.Point(622, 401);
            this.PrintStrategyTableButtton.Name = "PrintStrategyTableButtton";
            this.PrintStrategyTableButtton.Size = new System.Drawing.Size(106, 75);
            this.PrintStrategyTableButtton.TabIndex = 1;
            this.PrintStrategyTableButtton.Text = "Print Strategy Table";
            this.PrintStrategyTableButtton.UseVisualStyleBackColor = true;
            this.PrintStrategyTableButtton.Click += new System.EventHandler(this.PrintStrategyTableButtton_Click);
            // 
            // NewGameButton
            // 
            this.NewGameButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.NewGameButton.Location = new System.Drawing.Point(41, 401);
            this.NewGameButton.Name = "NewGameButton";
            this.NewGameButton.Size = new System.Drawing.Size(146, 75);
            this.NewGameButton.TabIndex = 2;
            this.NewGameButton.Text = "New Game";
            this.NewGameButton.UseVisualStyleBackColor = true;
            this.NewGameButton.Click += new System.EventHandler(this.NewGameButton_Click);
            // 
            // GamesListBox
            // 
            this.GamesListBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GamesListBox.FormattingEnabled = true;
            this.GamesListBox.ItemHeight = 28;
            this.GamesListBox.Location = new System.Drawing.Point(41, 80);
            this.GamesListBox.Name = "GamesListBox";
            this.GamesListBox.Size = new System.Drawing.Size(396, 284);
            this.GamesListBox.TabIndex = 4;
            this.GamesListBox.SelectedIndexChanged += new System.EventHandler(this.GamesListBox_SelectedIndexChanged);
            // 
            // PlayButton
            // 
            this.PlayButton.BackColor = System.Drawing.SystemColors.Menu;
            this.PlayButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PlayButton.Location = new System.Drawing.Point(509, 401);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(107, 75);
            this.PlayButton.TabIndex = 6;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = false;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // DeleteGameButton
            // 
            this.DeleteGameButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DeleteGameButton.Location = new System.Drawing.Point(290, 401);
            this.DeleteGameButton.Name = "DeleteGameButton";
            this.DeleteGameButton.Size = new System.Drawing.Size(147, 75);
            this.DeleteGameButton.TabIndex = 7;
            this.DeleteGameButton.Text = "Delete Game";
            this.DeleteGameButton.UseVisualStyleBackColor = true;
            this.DeleteGameButton.Click += new System.EventHandler(this.DeleteGameButton_Click);
            // 
            // GamePreviewPanel
            // 
            this.GamePreviewPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.GamePreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GamePreviewPanel.Controls.Add(this.ChooseAGameLabel);
            this.GamePreviewPanel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.GamePreviewPanel.Location = new System.Drawing.Point(459, 37);
            this.GamePreviewPanel.Name = "GamePreviewPanel";
            this.GamePreviewPanel.Size = new System.Drawing.Size(436, 347);
            this.GamePreviewPanel.TabIndex = 8;
            // 
            // ChooseAGameLabel
            // 
            this.ChooseAGameLabel.AutoSize = true;
            this.ChooseAGameLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.ChooseAGameLabel.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.ChooseAGameLabel.Location = new System.Drawing.Point(67, 167);
            this.ChooseAGameLabel.Name = "ChooseAGameLabel";
            this.ChooseAGameLabel.Size = new System.Drawing.Size(352, 37);
            this.ChooseAGameLabel.TabIndex = 0;
            this.ChooseAGameLabel.Text = "Choose a game from the list!";
            // 
            // GamesLabel
            // 
            this.GamesLabel.AutoSize = true;
            this.GamesLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GamesLabel.Location = new System.Drawing.Point(41, 31);
            this.GamesLabel.Name = "GamesLabel";
            this.GamesLabel.Size = new System.Drawing.Size(121, 46);
            this.GamesLabel.TabIndex = 9;
            this.GamesLabel.Text = "Games";
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(928, 533);
            this.Controls.Add(this.GamesLabel);
            this.Controls.Add(this.GamePreviewPanel);
            this.Controls.Add(this.DeleteGameButton);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.GamesListBox);
            this.Controls.Add(this.NewGameButton);
            this.Controls.Add(this.PrintStrategyTableButtton);
            this.Controls.Add(this.SaveStatisticsButton);
            this.Name = "HomeForm";
            this.Text = "HomeForm";
            this.Load += new System.EventHandler(this.HomeForm_Load);
            this.GamePreviewPanel.ResumeLayout(false);
            this.GamePreviewPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button SaveStatisticsButton;
        private Button PrintStrategyTableButtton;
        private Button NewGameButton;
        private ListBox GamesListBox;
        private Button PlayButton;
        private Button DeleteGameButton;
        private Panel GamePreviewPanel;
        private Label GamesLabel;
        private Label ChooseAGameLabel;
    }
}