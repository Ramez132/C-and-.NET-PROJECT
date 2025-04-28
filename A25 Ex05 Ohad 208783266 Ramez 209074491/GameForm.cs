using GameEngine;

namespace A25_Ex05_Ohad_208783266_Ramez_209074491
{
    public partial class GameForm : Form
    {
        public readonly Game r_Game;
        private Button[,] m_ButtonGrid;
        private Button m_SelectedButton;

        public GameForm(Game i_Game)
        {
            r_Game = i_Game;
            InitializeComponent();
            adjustFormSize();
            generateBoard();
            initializePlayersNamesAndScores();
        }

        private void adjustFormSize()
        {
            int baseGap = 300;
            int width = baseGap + (r_Game.Board.SizeOfBoard * 60) + 30;
            int height = baseGap + (r_Game.Board.SizeOfBoard * 60);
            this.Size = new System.Drawing.Size(width, height);
        }

        private void generateBoard()
        {
            m_ButtonGrid = new Button[r_Game.Board.SizeOfBoard, r_Game.Board.SizeOfBoard];
            int gridTileSize = 60;
            boardPanel.Width = r_Game.Board.SizeOfBoard * gridTileSize;
            boardPanel.Height = r_Game.Board.SizeOfBoard * gridTileSize;

            for (int i = 0; i < r_Game.Board.SizeOfBoard; i++)
            {
                for (int j = 0; j < r_Game.Board.SizeOfBoard; j++)
                {
                    Button gridTile = new Button
                    {
                        Width = gridTileSize,
                        Height = gridTileSize,
                        Location = new Point(j * gridTileSize, i * gridTileSize),
                        BackColor = (i + j) % 2 == 1 ? Color.White : Color.SlateGray,
                        Enabled = (i + j) % 2 == 1 ? true : false,
                        FlatStyle = FlatStyle.Flat,
                        Tag = new Point(i, j),
                        Text = r_Game.GetPieceRepresentation(i, j),
                        Font = new Font("Arial", 14, FontStyle.Bold)
                    };

                    m_ButtonGrid[i, j] = gridTile;
                    gridTile.Click += boardPiece_Click;
                    boardPanel.Controls.Add(gridTile);
                }
            }
        }

        private void initializePlayersNamesAndScores()
        {
            (string name, int score) playerNameAndScore = r_Game.GetPlayerNameAndScore(eTypeOfPlayer.Player1);
            (string name, int score) opponentNameAndScore = r_Game.GetPlayerNameAndScore(r_Game.GetOpponentPlayerType());
            playerNameLabel.Text = playerNameAndScore.name + ":";
            playerScoreLabel.Text = playerNameAndScore.score.ToString();
            opponentNameLabel.Text = opponentNameAndScore.name + ":";
            opponentScoreLabel.Text = opponentNameAndScore.score.ToString();
            playerNameLabel.Location = new Point(boardPanel.Location.X + 25, boardPanel.Location.Y - 60);
            playerScoreLabel.Location = new System.Drawing.Point(playerNameLabel.Right + 5, playerNameLabel.Top);
            opponentNameLabel.Location = new Point(boardPanel.Location.X + 350 - (opponentNameLabel.Text.Length * 5), boardPanel.Location.Y - 60);
            opponentScoreLabel.Location = new System.Drawing.Point(opponentNameLabel.Right + 5, opponentNameLabel.Top);
            markCurrentPlayer();
        }

        private void makeOpponentClickable()
        {
            opponentNameLabel.Cursor = Cursors.Hand;
            opponentNameLabel.MouseEnter += (s, e) => opponentNameLabel.ForeColor = Color.Blue;
            opponentNameLabel.MouseLeave += (s, e) => opponentNameLabel.ForeColor = Color.Black;
            opponentNameLabel.Click += opponentLabel_Click;
        }

        private void markCurrentPlayer()
        {
            string playerNameWithoutLastChar = playerNameLabel.Text.Remove(playerNameLabel.Text.Length - 1);
            string opponentNameWithoutLastChar = opponentNameLabel.Text.Remove(opponentNameLabel.Text.Length - 1);

            if (r_Game.GetCurrentPlayerTurn() == playerNameWithoutLastChar)
            {
                playerNameLabel.BackColor = Color.LightBlue;
                playerScoreLabel.BackColor = Color.LightBlue;
                opponentNameLabel.BackColor = Color.Transparent;
                opponentScoreLabel.BackColor = Color.Transparent;
            }

            else
            {
                opponentNameLabel.BackColor = Color.LightBlue;
                opponentScoreLabel.BackColor = Color.LightBlue;
                playerNameLabel.BackColor = Color.Transparent;
                playerScoreLabel.BackColor = Color.Transparent;
            }
        }

        private void boardPiece_Click(object sender, EventArgs e)
        {
            if (!r_Game.IsItComputerTurn())
            {
                Button clickedGridTile = sender as Button;
                onBoardPieceClick(clickedGridTile);
            }
        }

        private void markClickedButton(Point i_TargetPosition, Button i_ClickedGridTile)
        {
            if (r_Game.IsPieceBelongToPlayer(i_TargetPosition.X, i_TargetPosition.Y))
            {
                m_SelectedButton = i_ClickedGridTile;
                i_ClickedGridTile.BackColor = Color.LightBlue;
            }
        }

        private void onBoardPieceClick(Button i_ClickedGridTile)
        {
            Point targetPosition = (Point)i_ClickedGridTile.Tag;

            if (m_SelectedButton == null)
            {
                markClickedButton(targetPosition, i_ClickedGridTile);
            }

            else
            {
                if (i_ClickedGridTile == m_SelectedButton)
                {
                    i_ClickedGridTile.BackColor = Color.White;
                    m_SelectedButton = null;
                }

                else if (i_ClickedGridTile.Text == m_SelectedButton.Text)
                {
                    undoColorChangeForButton(i_ClickedGridTile);
                }

                else
                {
                    checkIfMoveIsValid(targetPosition, i_ClickedGridTile);
                }
            }
        }

        private void undoColorChangeForButton(Button i_ClickedGridTile)
        {
            m_SelectedButton.BackColor = Color.Transparent;
            m_SelectedButton = i_ClickedGridTile;
            m_SelectedButton.BackColor = Color.LightBlue;
        }

        private void checkIfMoveIsValid(Point i_TargetPosition, Button i_ClickedGridTile)
        {
            Point sourcePosition = (Point)m_SelectedButton.Tag;
            string message = r_Game.MoveAttempt(sourcePosition.X, sourcePosition.Y, i_TargetPosition.X, i_TargetPosition.Y);

            if (string.IsNullOrEmpty(message))
            {
                applyValidMoveMade();
            }

            else
            {
                MessageBox.Show(message, "Illegal Move", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                i_ClickedGridTile.BackColor = Color.White;
            }

            m_SelectedButton = null;
        }

        private void applyValidMoveMade()
        {
            updateBoard();
            m_SelectedButton = null;
            checkGameStatus();
            r_Game.SwitchTurn();

            if (r_Game.IsItComputerTurn())
            {
                makeOpponentClickable();
            }

            markCurrentPlayer();
        }

        private void opponentLabel_Click(object sender, EventArgs e)
        {
            handleComputerMove();
        }

        private void handleComputerMove()
        {
            Task.Delay(500).Wait();
            r_Game.ApplyComputerMove();
            updateBoard();
            checkGameStatus();
            r_Game.SwitchTurn();
            undoOpponentLabelFeatures();
            markCurrentPlayer();
        }

        private void undoOpponentLabelFeatures()
        {
            opponentNameLabel.Cursor = Cursors.Default;
            opponentNameLabel.MouseEnter -= (s, e) => opponentNameLabel.ForeColor = Color.Blue;
            opponentNameLabel.MouseLeave -= (s, e) => opponentNameLabel.ForeColor = Color.Black;
            opponentNameLabel.Click -= opponentLabel_Click;
        }

        private void updateBoard()
        {
            for (int i = 0; i < r_Game.Board.SizeOfBoard; i++)
            {
                for (int j = 0; j < r_Game.Board.SizeOfBoard; j++)
                {
                    m_ButtonGrid[i, j].Text = r_Game.GetPieceRepresentation(i, j);
                    m_ButtonGrid[i, j].BackColor = (i + j) % 2 == 1 ? Color.White : Color.SlateGray;
                }
            }
        }

        private void checkGameStatus()
        {
            string gameStatus = r_Game.CheckVictoryOrDraw();

            if (gameStatus == "winner")
            {
                r_Game.UpdateScore();
                string winnerMessage = $"{r_Game.Winner} Won! Another Round?";
                DialogResult result = MessageBox.Show(winnerMessage, "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    restartGame();
                }

                else
                {
                    Application.Exit();
                }
            }

            else if (gameStatus == "draw")
            {
                r_Game.UpdateScore();
                DialogResult result = MessageBox.Show("Tie! Another Round?", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    restartGame();
                }

                else
                {
                    Application.Exit();
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            DialogResult selection = MessageBox.Show("The round has finished. Do you wish to quit it and start a new round?", 
                "Quit Game", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (selection == DialogResult.Cancel)
            {
                e.Cancel = true;
            }

            else if (selection == DialogResult.Yes)
            {
                e.Cancel = true;
                r_Game.UpdateScore();
                restartGame();
            }

            r_Game.UpdateScore();
        }

        private void restartGame()
        {
            initializePlayersNamesAndScores();
            boardPanel.Controls.Clear();
            r_Game.InitializeGameSettings();
            markCurrentPlayer();
            generateBoard();
        }
    }
}
