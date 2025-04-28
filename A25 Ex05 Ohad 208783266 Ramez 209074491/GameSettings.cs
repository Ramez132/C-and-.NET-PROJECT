using System.Xml;
using GameEngine;
namespace A25_Ex05_Ohad_208783266_Ramez_209074491
{
    public partial class GameSettings : Form
    {
        public readonly Game r_Game = new Game();
        private bool IsPlayerNameValid {  get; set; }
        private bool IsOpponentNameValid { get; set; } = true;
        private int SelectedBoardSize { get; set; } = 0;

        public GameSettings()
        {
            InitializeComponent();
            this.ActiveControl = nameLabel;
            this.Click += gameSettings_Click;
            r_Game.InitializeParticipant(eTypeOfPlayer.Cpu, opponentNameBox.Text);
        }

        private bool checkNameInput(string i_Name)
        {
            return !(string.IsNullOrWhiteSpace(i_Name) || i_Name.Length > 20 || i_Name.Contains(" "));
        }

        private void opponentCheckBox_Click(object sender, EventArgs e)
        {
            if (opponentCheckBox.Checked)
            {
                opponentNameBox.Enabled = true;
                opponentNameBox.Text = string.Empty;
                r_Game.Opponent = eOpponent.Player;
                opponentNameBox.Leave += opponentNameBox_Leave;
            }

            else
            {
                opponentNameBox.Enabled = false;
                r_Game.InitializeParticipant(eTypeOfPlayer.Cpu, "Computer");
                opponentNameBox.Text = "Computer";
                IsOpponentNameValid = true;
                opponentNameErrorLabel.Visible = false;
                opponentNameBox.Leave -= opponentNameBox_Leave;
            }

            updateStartButton();
        }

        private void gameInitiazation(Game r_Game, int i_IndexSelected)
        {
            r_Game.InitializeBoard(i_IndexSelected);
        }

        private void nameTextBox_Leave(object sender, EventArgs e)
        {
            IsPlayerNameValid = checkNameInput(nameTextBox.Text);
            
            if (!IsPlayerNameValid)
            {
                nameErrorLabel.Visible = true;
            }

            else
            {
                r_Game.InitializeParticipant(eTypeOfPlayer.Player1, nameTextBox.Text);
                nameErrorLabel.Visible = false;
            }

            updateStartButton();
        }

        private void boardSizeRadioButton_Click(object sender, EventArgs e)
        {
            if (boardSizeSix.Checked)
            {
                SelectedBoardSize = 6;
            }

            else if (boardSizeEight.Checked)
            {
                SelectedBoardSize = 8;
            }

            else if (boardSizeTen.Checked)
            {
                SelectedBoardSize = 10;
            }

            updateStartButton();
        }

        private void updateStartButton()
        {
            startButton.Enabled = IsPlayerNameValid && IsOpponentNameValid && (SelectedBoardSize != 0);
        }

        private void opponentNameBox_Leave(object sender, EventArgs e)
        {
            IsOpponentNameValid = checkNameInput(opponentNameBox.Text);

            if (!IsOpponentNameValid)
            {
                opponentNameErrorLabel.Visible = true;
            }

            else
            {
                r_Game.InitializeParticipant(eTypeOfPlayer.Player2, opponentNameBox.Text);
                opponentNameErrorLabel.Visible = false;
            }

            updateStartButton();
        }

        private void gameSettings_Click(object sender, EventArgs e)
        {
            IsOpponentNameValid = checkNameInput(opponentNameBox.Text);
            IsPlayerNameValid = checkNameInput(nameTextBox.Text);
            updateStartButton();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            gameInitiazation(r_Game, SelectedBoardSize);
            GameForm gameForm = new GameForm(r_Game);
            this.Hide();
            
            if (gameForm.ShowDialog() == DialogResult.Cancel)
            {
                Application.Exit();
            }
        }
    }
}
