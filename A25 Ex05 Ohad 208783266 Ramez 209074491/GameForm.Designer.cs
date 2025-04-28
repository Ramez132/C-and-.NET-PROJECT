namespace A25_Ex05_Ohad_208783266_Ramez_209074491
{
    partial class GameForm
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
            boardPanel = new Panel();
            playerNameLabel = new Label();
            playerScoreLabel = new Label();
            opponentNameLabel = new Label();
            opponentScoreLabel = new Label();
            SuspendLayout();
            // 
            // boardPanel
            // 
            boardPanel.BorderStyle = BorderStyle.FixedSingle;
            boardPanel.Location = new Point(163, 109);
            boardPanel.Name = "boardPanel";
            boardPanel.Size = new Size(400, 400);
            boardPanel.TabIndex = 0;
            // 
            // playerNameLabel
            // 
            playerNameLabel.AutoSize = true;
            playerNameLabel.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            playerNameLabel.Location = new Point(110, 40);
            playerNameLabel.Margin = new Padding(3, 0, 5, 0);
            playerNameLabel.Name = "playerNameLabel";
            playerNameLabel.Size = new Size(118, 29);
            playerNameLabel.TabIndex = 1;
            playerNameLabel.Text = "playerName";
            // 
            // playerScoreLabel
            // 
            playerScoreLabel.AutoSize = true;
            playerScoreLabel.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            playerScoreLabel.Location = new Point(258, 40);
            playerScoreLabel.Name = "playerScoreLabel";
            playerScoreLabel.Size = new Size(65, 29);
            playerScoreLabel.TabIndex = 2;
            playerScoreLabel.Text = "label1";
            // 
            // opponentNameLabel
            // 
            opponentNameLabel.AutoSize = true;
            opponentNameLabel.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            opponentNameLabel.Location = new Point(441, 40);
            opponentNameLabel.Margin = new Padding(3, 0, 5, 0);
            opponentNameLabel.Name = "opponentNameLabel";
            opponentNameLabel.Size = new Size(65, 29);
            opponentNameLabel.TabIndex = 3;
            opponentNameLabel.Text = "label1";
            // 
            // opponentScoreLabel
            // 
            opponentScoreLabel.AutoSize = true;
            opponentScoreLabel.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            opponentScoreLabel.Location = new Point(548, 38);
            opponentScoreLabel.Margin = new Padding(5, 0, 3, 0);
            opponentScoreLabel.Name = "opponentScoreLabel";
            opponentScoreLabel.Size = new Size(65, 29);
            opponentScoreLabel.TabIndex = 4;
            opponentScoreLabel.Text = "label1";
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 583);
            Controls.Add(opponentScoreLabel);
            Controls.Add(opponentNameLabel);
            Controls.Add(playerScoreLabel);
            Controls.Add(playerNameLabel);
            Controls.Add(boardPanel);
            Name = "GameForm";
            Text = "BoardForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel boardPanel;
        private Label playerNameLabel;
        private Label playerScoreLabel;
        private Label opponentNameLabel;
        private Label opponentScoreLabel;
    }
}