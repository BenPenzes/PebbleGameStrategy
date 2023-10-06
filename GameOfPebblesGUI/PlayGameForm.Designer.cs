using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameUI
{
    partial class PlayGameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PreviousMovesLabel = new System.Windows.Forms.Label();
            this.MoveButton = new System.Windows.Forms.Button();
            this.MovesComboBox = new System.Windows.Forms.ComboBox();
            this.PreviousMovesListBox = new System.Windows.Forms.ListBox();
            this.OpponentsMovesComboBox = new System.Windows.Forms.ComboBox();
            this.OpponentMovesLabel = new System.Windows.Forms.Label();
            this.PilesPanel = new System.Windows.Forms.Panel();
            this.YourMovesLabel = new System.Windows.Forms.Label();
            this.TurnTimer = new System.Windows.Forms.Timer(this.components);
            this.RemainingTimeLabel = new System.Windows.Forms.Label();
            this.TurnTimerLabel = new System.Windows.Forms.Label();
            this.WhoMovesLabel = new System.Windows.Forms.Label();
            this.SecondsLabel = new System.Windows.Forms.Label();
            this.TimerInfoPanel = new System.Windows.Forms.Panel();
            this.TimerInfoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PreviousMovesLabel
            // 
            this.PreviousMovesLabel.AutoEllipsis = true;
            this.PreviousMovesLabel.AutoSize = true;
            this.PreviousMovesLabel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.PreviousMovesLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PreviousMovesLabel.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PreviousMovesLabel.Location = new System.Drawing.Point(872, 99);
            this.PreviousMovesLabel.Name = "PreviousMovesLabel";
            this.PreviousMovesLabel.Size = new System.Drawing.Size(249, 47);
            this.PreviousMovesLabel.TabIndex = 0;
            this.PreviousMovesLabel.Text = "Previous Moves";
            // 
            // MoveButton
            // 
            this.MoveButton.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MoveButton.Location = new System.Drawing.Point(311, 520);
            this.MoveButton.Name = "MoveButton";
            this.MoveButton.Size = new System.Drawing.Size(205, 67);
            this.MoveButton.TabIndex = 1;
            this.MoveButton.Text = "MOVE";
            this.MoveButton.UseVisualStyleBackColor = true;
            this.MoveButton.Click += new System.EventHandler(this.MoveButton_Click);
            // 
            // MovesComboBox
            // 
            this.MovesComboBox.BackColor = System.Drawing.Color.FloralWhite;
            this.MovesComboBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MovesComboBox.FormattingEnabled = true;
            this.MovesComboBox.Location = new System.Drawing.Point(229, 463);
            this.MovesComboBox.Name = "MovesComboBox";
            this.MovesComboBox.Size = new System.Drawing.Size(371, 36);
            this.MovesComboBox.TabIndex = 10;
            // 
            // PreviousMovesListBox
            // 
            this.PreviousMovesListBox.FormattingEnabled = true;
            this.PreviousMovesListBox.ItemHeight = 28;
            this.PreviousMovesListBox.Location = new System.Drawing.Point(795, 164);
            this.PreviousMovesListBox.Name = "PreviousMovesListBox";
            this.PreviousMovesListBox.Size = new System.Drawing.Size(433, 256);
            this.PreviousMovesListBox.TabIndex = 14;
            // 
            // OpponentsMovesComboBox
            // 
            this.OpponentsMovesComboBox.BackColor = System.Drawing.Color.FloralWhite;
            this.OpponentsMovesComboBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OpponentsMovesComboBox.FormattingEnabled = true;
            this.OpponentsMovesComboBox.Location = new System.Drawing.Point(833, 463);
            this.OpponentsMovesComboBox.Name = "OpponentsMovesComboBox";
            this.OpponentsMovesComboBox.Size = new System.Drawing.Size(371, 36);
            this.OpponentsMovesComboBox.TabIndex = 29;
            // 
            // OpponentMovesLabel
            // 
            this.OpponentMovesLabel.AutoSize = true;
            this.OpponentMovesLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.OpponentMovesLabel.Location = new System.Drawing.Point(883, 423);
            this.OpponentMovesLabel.Name = "OpponentMovesLabel";
            this.OpponentMovesLabel.Size = new System.Drawing.Size(238, 37);
            this.OpponentMovesLabel.TabIndex = 30;
            this.OpponentMovesLabel.Text = "Opponent Moves";
            // 
            // PilesPanel
            // 
            this.PilesPanel.Location = new System.Drawing.Point(83, 165);
            this.PilesPanel.Name = "PilesPanel";
            this.PilesPanel.Size = new System.Drawing.Size(678, 256);
            this.PilesPanel.TabIndex = 31;
            // 
            // YourMovesLabel
            // 
            this.YourMovesLabel.AutoSize = true;
            this.YourMovesLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.YourMovesLabel.Location = new System.Drawing.Point(333, 423);
            this.YourMovesLabel.Name = "YourMovesLabel";
            this.YourMovesLabel.Size = new System.Drawing.Size(167, 37);
            this.YourMovesLabel.TabIndex = 32;
            this.YourMovesLabel.Text = "Your Moves";
            // 
            // TurnTimer
            // 
            this.TurnTimer.Interval = 1000;
            // 
            // RemainingTimeLabel
            // 
            this.RemainingTimeLabel.AutoSize = true;
            this.RemainingTimeLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RemainingTimeLabel.Location = new System.Drawing.Point(214, 6);
            this.RemainingTimeLabel.Name = "RemainingTimeLabel";
            this.RemainingTimeLabel.Size = new System.Drawing.Size(38, 46);
            this.RemainingTimeLabel.TabIndex = 33;
            this.RemainingTimeLabel.Text = "0";
            // 
            // TurnTimerLabel
            // 
            this.TurnTimerLabel.AutoSize = true;
            this.TurnTimerLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TurnTimerLabel.Location = new System.Drawing.Point(19, 6);
            this.TurnTimerLabel.Name = "TurnTimerLabel";
            this.TurnTimerLabel.Size = new System.Drawing.Size(189, 46);
            this.TurnTimerLabel.TabIndex = 34;
            this.TurnTimerLabel.Text = "Turn Timer:";
            // 
            // WhoMovesLabel
            // 
            this.WhoMovesLabel.AutoSize = true;
            this.WhoMovesLabel.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.WhoMovesLabel.Location = new System.Drawing.Point(424, 9);
            this.WhoMovesLabel.Name = "WhoMovesLabel";
            this.WhoMovesLabel.Size = new System.Drawing.Size(66, 72);
            this.WhoMovesLabel.TabIndex = 35;
            this.WhoMovesLabel.Text = "...";
            // 
            // SecondsLabel
            // 
            this.SecondsLabel.AutoSize = true;
            this.SecondsLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SecondsLabel.Location = new System.Drawing.Point(264, 6);
            this.SecondsLabel.Name = "SecondsLabel";
            this.SecondsLabel.Size = new System.Drawing.Size(141, 46);
            this.SecondsLabel.TabIndex = 36;
            this.SecondsLabel.Text = "seconds";
            // 
            // TimerInfoPanel
            // 
            this.TimerInfoPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.TimerInfoPanel.Controls.Add(this.SecondsLabel);
            this.TimerInfoPanel.Controls.Add(this.TurnTimerLabel);
            this.TimerInfoPanel.Controls.Add(this.RemainingTimeLabel);
            this.TimerInfoPanel.Location = new System.Drawing.Point(210, 93);
            this.TimerInfoPanel.Name = "TimerInfoPanel";
            this.TimerInfoPanel.Size = new System.Drawing.Size(423, 64);
            this.TimerInfoPanel.TabIndex = 37;
            // 
            // PlayGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1251, 599);
            this.Controls.Add(this.TimerInfoPanel);
            this.Controls.Add(this.WhoMovesLabel);
            this.Controls.Add(this.YourMovesLabel);
            this.Controls.Add(this.PilesPanel);
            this.Controls.Add(this.OpponentMovesLabel);
            this.Controls.Add(this.OpponentsMovesComboBox);
            this.Controls.Add(this.PreviousMovesListBox);
            this.Controls.Add(this.MovesComboBox);
            this.Controls.Add(this.MoveButton);
            this.Controls.Add(this.PreviousMovesLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PlayGameForm";
            this.Text = "GameModel of Pebbles";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PlayGameForm_FormClosed);
            this.TimerInfoPanel.ResumeLayout(false);
            this.TimerInfoPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label PreviousMovesLabel;
        private Button MoveButton;
        private ComboBox MovesComboBox;
        private ListBox PreviousMovesListBox;
        private ComboBox OpponentsMovesComboBox;
        private Label OpponentMovesLabel;
        private Panel PilesPanel;
        private Label YourMovesLabel;
        private System.Windows.Forms.Timer TurnTimer;
        private Label RemainingTimeLabel;
        private Label TurnTimerLabel;
        private Label WhoMovesLabel;
        private Label SecondsLabel;
        private Panel TimerInfoPanel;
    }
}

