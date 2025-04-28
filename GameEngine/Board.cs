namespace GameEngine
{
    public class Board
    {
        public int SizeOfBoard { get; set; }
        public Piece[,] Pieces { get; set; }
        public eOpponent Opponent { get; set; }

        public Board(int i_BoardSize, eOpponent i_Opponent)
        {
            
            SizeOfBoard = i_BoardSize;
            Opponent = i_Opponent;
            Pieces = new Piece[SizeOfBoard, SizeOfBoard];
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            for (int row = 0; row < SizeOfBoard; row++)
            {
                for (int column = 0; column < SizeOfBoard; column++)
                {
                    if ((row + column) % 2 != 0)
                    {
                        if (row < ((SizeOfBoard / 2) - 1))
                        {
                            if (Opponent == eOpponent.Player)
                            {
                                Pieces[row, column] = new Piece(ePieceType.Regular, eTypeOfPlayer.Player2, row, column);

                            }

                            else
                            {
                                Pieces[row, column] = new Piece(ePieceType.Regular, eTypeOfPlayer.Cpu, row, column);
                            }
                        }

                        else if (row > ((SizeOfBoard / 2)))
                        {
                            Pieces[row, column] = new Piece(ePieceType.Regular, eTypeOfPlayer.Player1, row, column);
                        }

                        else
                        {
                            Pieces[row, column] = new Piece(ePieceType.Empty, eTypeOfPlayer.None, row, column);
                        }
                    }

                    else
                    {
                        Pieces[row, column] = new Piece(ePieceType.Empty, eTypeOfPlayer.None, row, column);
                    }
                }
            }
        }

        public void ResetBorad()
        {
            InitializeBoard();
        }

        public bool IsMoveInBoarders(int i_Row, int i_Col)
        {
            return i_Row >= 0 && i_Row < SizeOfBoard && i_Col >= 0 && i_Col < SizeOfBoard;
        }

        
    }
}
