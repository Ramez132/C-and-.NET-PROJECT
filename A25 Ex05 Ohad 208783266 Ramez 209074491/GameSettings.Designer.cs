namespace A25_Ex05_Ohad_208783266_Ramez_209074491
{
    partial class GameSettings
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
            Label playersLabel;
            welcomeLabel = new Label();
            playerNameLabel = new Label();
            nameLabel = new Label();
            nameTextBox = new TextBox();
            opponentLabel = new Label();
            boardSizeLabel = new Label();
            startButton = new Button();
            opponentNameBox = new TextBox();
            nameErrorLabel = new Label();
            opponentNameErrorLabel = new Label();
            boardSizeSix = new RadioButton();
            boardSizeEight = new RadioButton();
            boardSizeTen = new RadioButton();
            opponentCheckBox = new CheckBox();
            playersLabel = new Label();
            SuspendLayout();
            // 
            // playersLabel
            // 
            playersLabel.AutoSize = true;
            playersLabel.Font = new Font("Arial", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            playersLabel.Location = new Point(50, 125);
            playersLabel.Name = "playersLabel";
            playersLabel.Size = new Size(113, 32);
            playersLabel.TabIndex = 19;
            playersLabel.Text = "Players:";
            // 
            // welcomeLabel
            // 
            welcomeLabel.Location = new Point(117, 42);
            welcomeLabel.Name = "welcomeLabel";
            welcomeLabel.Size = new Size(100, 23);
            welcomeLabel.TabIndex = 0;
            // 
            // playerNameLabel
            // 
            playerNameLabel.Location = new Point(0, 0);
            playerNameLabel.Name = "playerNameLabel";
            playerNameLabel.Size = new Size(100, 23);
            playerNameLabel.TabIndex = 1;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Font = new Font("Arial", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            nameLabel.ForeColor = Color.Black;
            nameLabel.Location = new Point(66, 175);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(107, 28);
            nameLabel.TabIndex = 5;
            nameLabel.Text = "Player 1:";
            // 
            // nameTextBox
            // 
            nameTextBox.Font = new Font("Arial", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            nameTextBox.Location = new Point(246, 175);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(200, 30);
            nameTextBox.TabIndex = 6;
            nameTextBox.Leave += nameTextBox_Leave;
            // 
            // opponentLabel
            // 
            opponentLabel.AutoSize = true;
            opponentLabel.Font = new Font("Arial", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            opponentLabel.Location = new Point(110, 228);
            opponentLabel.Name = "opponentLabel";
            opponentLabel.Size = new Size(107, 28);
            opponentLabel.TabIndex = 7;
            opponentLabel.Text = "Player 2:";
            // 
            // boardSizeLabel
            // 
            boardSizeLabel.AutoSize = true;
            boardSizeLabel.Font = new Font("Arial", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            boardSizeLabel.Location = new Point(50, 65);
            boardSizeLabel.Name = "boardSizeLabel";
            boardSizeLabel.Size = new Size(238, 28);
            boardSizeLabel.TabIndex = 8;
            boardSizeLabel.Text = "Board Size (6, 8, 10):";
            // 
            // startButton
            // 
            startButton.BackColor = Color.Transparent;
            startButton.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            startButton.Location = new Point(50, 301);
            startButton.Name = "startButton";
            startButton.Size = new Size(112, 34);
            startButton.TabIndex = 9;
            startButton.Text = "Done";
            startButton.UseVisualStyleBackColor = false;
            startButton.Click += startButton_Click;
            // 
            // opponentNameBox
            // 
            opponentNameBox.Enabled = false;
            opponentNameBox.Font = new Font("Arial Narrow", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            opponentNameBox.Location = new Point(246, 228);
            opponentNameBox.Name = "opponentNameBox";
            opponentNameBox.Size = new Size(200, 30);
            opponentNameBox.TabIndex = 13;
            opponentNameBox.Text = "Computer";
            // 
            // nameErrorLabel
            // 
            nameErrorLabel.AutoSize = true;
            nameErrorLabel.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nameErrorLabel.ForeColor = Color.Red;
            nameErrorLabel.Location = new Point(476, 180);
            nameErrorLabel.Name = "nameErrorLabel";
            nameErrorLabel.Size = new Size(501, 21);
            nameErrorLabel.TabIndex = 14;
            nameErrorLabel.Text = "Name should be between 1 and 20 characters with no spaces";
            nameErrorLabel.Visible = false;
            // 
            // opponentNameErrorLabel
            // 
            opponentNameErrorLabel.AutoSize = true;
            opponentNameErrorLabel.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            opponentNameErrorLabel.ForeColor = Color.Red;
            opponentNameErrorLabel.Location = new Point(476, 235);
            opponentNameErrorLabel.Name = "opponentNameErrorLabel";
            opponentNameErrorLabel.Size = new Size(501, 21);
            opponentNameErrorLabel.TabIndex = 15;
            opponentNameErrorLabel.Text = "Name should be between 1 and 20 characters with no spaces";
            opponentNameErrorLabel.Visible = false;
            // 
            // boardSizeSix
            // 
            boardSizeSix.AutoSize = true;
            boardSizeSix.Location = new Point(294, 65);
            boardSizeSix.Name = "boardSizeSix";
            boardSizeSix.Size = new Size(68, 29);
            boardSizeSix.TabIndex = 16;
            boardSizeSix.TabStop = true;
            boardSizeSix.Text = "6X6";
            boardSizeSix.UseVisualStyleBackColor = true;
            boardSizeSix.CheckedChanged += boardSizeRadioButton_Click;
            // 
            // boardSizeEight
            // 
            boardSizeEight.AutoSize = true;
            boardSizeEight.Location = new Point(378, 65);
            boardSizeEight.Name = "boardSizeEight";
            boardSizeEight.Size = new Size(68, 29);
            boardSizeEight.TabIndex = 17;
            boardSizeEight.TabStop = true;
            boardSizeEight.Text = "8X8";
            boardSizeEight.UseVisualStyleBackColor = true;
            boardSizeEight.CheckedChanged += boardSizeRadioButton_Click;
            // 
            // boardSizeTen
            // 
            boardSizeTen.AutoSize = true;
            boardSizeTen.Location = new Point(467, 65);
            boardSizeTen.Name = "boardSizeTen";
            boardSizeTen.Size = new Size(88, 29);
            boardSizeTen.TabIndex = 18;
            boardSizeTen.TabStop = true;
            boardSizeTen.Text = "10X10";
            boardSizeTen.UseVisualStyleBackColor = true;
            boardSizeTen.CheckedChanged += boardSizeRadioButton_Click;
            // 
            // opponentCheckBox
            // 
            opponentCheckBox.AutoSize = true;
            opponentCheckBox.Location = new Point(66, 233);
            opponentCheckBox.Name = "opponentCheckBox";
            opponentCheckBox.Size = new Size(22, 21);
            opponentCheckBox.TabIndex = 20;
            opponentCheckBox.UseVisualStyleBackColor = true;
            opponentCheckBox.CheckedChanged += opponentCheckBox_Click;
            // 
            // GameSettings
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            ClientSize = new Size(989, 344);
            Controls.Add(opponentCheckBox);
            Controls.Add(playersLabel);
            Controls.Add(boardSizeTen);
            Controls.Add(boardSizeEight);
            Controls.Add(boardSizeSix);
            Controls.Add(opponentNameErrorLabel);
            Controls.Add(nameErrorLabel);
            Controls.Add(opponentNameBox);
            Controls.Add(startButton);
            Controls.Add(boardSizeLabel);
            Controls.Add(opponentLabel);
            Controls.Add(nameTextBox);
            Controls.Add(nameLabel);
            Controls.Add(welcomeLabel);
            Controls.Add(playerNameLabel);
            Name = "GameSettings";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Damka";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label welcomeLabel;
        private Label playerNameLabel;
        private Label nameLabel;
        private TextBox nameTextBox;
        private Label opponentLabel;
        private Label boardSizeLabel;
        private Button startButton;
        private TextBox opponentNameBox;
        private Label nameErrorLabel;
        private Label opponentNameErrorLabel;
        private RadioButton boardSizeSix;
        private RadioButton boardSizeEight;
        private RadioButton boardSizeTen;
        private CheckBox opponentCheckBox;
    }
}
