namespace GameEngine
{
    public class Game
    {
        public Board Board { get; set; }
        public Participant MainPlayer { get; set; } = new Participant();
        public eOpponent Opponent { get; set; }
        public string CurrentTurn { get; set; }
        public string OpponentName { get; set; }
        public string[] GamePieces { get; } = ["X", "O"];
        public int PlayerScore { get; set; } = 0;
        public int OpponentScore { get; set; } = 0;
        private Piece LastUsedPiece { get; set; }
        private Move LastMoveMade { get; set; }

        private bool IsItPlayerFirstTurn { get; set; } = true;
        private bool IsItOpponentFirstTurn { get; set; } = true;
        public string Winner { get; set; } = string.Empty;
        private readonly Random r_Random = new Random();

        public void InitializeParticipant(eTypeOfPlayer i_PlayerType, string i_Name)
        {
            if (i_PlayerType == eTypeOfPlayer.Player1)
            {
                MainPlayer.PlayerName = i_Name;
                MainPlayer.PlayerType = eTypeOfPlayer.Player1;
            }

            else
            {
                OpponentName = i_Name;
                Opponent = i_PlayerType == eTypeOfPlayer.Player2 ? eOpponent.Player : eOpponent.Computer;
            }
        }

        public void InitializeGameSettings()
        {
            InitializeBoard(Board.SizeOfBoard);
            UpdateScore();
            CurrentTurn = MainPlayer.PlayerName;
        }

        public string MoveAttempt(int i_SourceRow, int i_SourceColumn, int i_TargetRow, int i_TargetColumn)
        {
            Move move = new Move((i_SourceRow, i_SourceColumn), (i_TargetRow, i_TargetColumn));
            eTypeOfPlayer currentPlayer = GetCurrentPlayerTurn() == MainPlayer.PlayerName ? eTypeOfPlayer.Player1 : GetOpponentPlayerType();
            return isMoveValid(move, currentPlayer).Item2;
        }

        public void InitializeBoard(int i_size)
        {
            Board = new Board(i_size, Opponent);
            CurrentTurn = MainPlayer.PlayerName;
        }

        public void SwitchTurn()
        {
            if (!isThereCaptureMove(LastUsedPiece) || !LastMoveMade.IsThereCapture)
            {
                if (CurrentTurn == MainPlayer.PlayerName)
                {
                    CurrentTurn = OpponentName;
                }

                else
                {
                    CurrentTurn = MainPlayer.PlayerName;
                }
            }
        }

        public string GetCurrentPlayerTurn()
        {
            return CurrentTurn;
        }

        public bool IsItComputerTurn()
        {
            return (Opponent == eOpponent.Computer) && (CurrentTurn == "Computer");
        }

        public eTypeOfPlayer GetOpponentPlayerType()
        {
            if (Opponent == eOpponent.Player)
            {
                return eTypeOfPlayer.Player2;
            }

            return eTypeOfPlayer.Cpu;
        }

        public bool IsOpponentComputer()
        {
            return Opponent == eOpponent.Computer;
        }

        public bool IsPieceBelongToPlayer(int i_Row, int i_Col)
        {
            eTypeOfPlayer typeOfCurrentPlayer;

            if (CurrentTurn == MainPlayer.PlayerName)
            {
                typeOfCurrentPlayer = eTypeOfPlayer.Player1;
            }

            else
            {
                typeOfCurrentPlayer = Opponent == eOpponent.Player ? eTypeOfPlayer.Player2 : eTypeOfPlayer.Cpu;
            }

            return typeOfCurrentPlayer == Board.Pieces[i_Row, i_Col].Player;
        }

        public string GetPieceRepresentation(int i_Row, int i_Col)
        {
            return Board.Pieces[i_Row, i_Col].PieceRepresentation.ToString();
        }

        public (string, int) GetPlayerNameAndScore(eTypeOfPlayer i_PlayerType)
        {
            return i_PlayerType == eTypeOfPlayer.Player1 ? (MainPlayer.PlayerName, PlayerScore) : (OpponentName, OpponentScore);
        }

        public int HowManyPiecesLeft(eTypeOfPlayer i_Player)
        {
            int piecesLeft = 0;

            for (int i = 0; i < Board.SizeOfBoard; i++)
            {
                for (int j = 0; j < Board.SizeOfBoard; j++)
                {
                    Piece piece = Board.Pieces[i, j];

                    if (piece.Type != ePieceType.Empty && piece.Player == i_Player)
                    {
                        if (piece.Type == ePieceType.King)
                        {
                            piecesLeft += 4;
                        }

                        else
                        {
                            piecesLeft++;
                        }
                    }
                }
            }

            return piecesLeft;
        }

        public void UpdateScore()
        {
            PlayerScore += HowManyPiecesLeft(MainPlayer.PlayerType);
            OpponentScore += HowManyPiecesLeft(GetOpponentPlayerType());
        }

        public (List<Move>, List<Move>) GetAvailableMoves(eTypeOfPlayer i_Player)
        {
            List<Move> availableMoves = new List<Move>();
            List<Move> captureMoves = new List<Move>();

            for (int row = 0; row < Board.SizeOfBoard; row++)
            {
                for (int column = 0; column < Board.SizeOfBoard; column++)
                {
                    Piece currentPiece = Board.Pieces[row, column];

                    if (currentPiece.Type != ePieceType.Empty && currentPiece.Player == i_Player)
                    {
                        List<Move> pieceMoves = GetMovesForPiece(currentPiece);

                        foreach (Move move in pieceMoves)
                        {
                            if (move.IsThereCapture)
                            {
                                captureMoves.Add(move);
                            }

                            else
                            {
                                availableMoves.Add(move);
                            }
                        }
                    }
                }
            }

            return (availableMoves, captureMoves);
        }

        public void EraseDuplicatedInLists(List<Move> i_List)
        {
            List<Move> listOfDuplicated = new List<Move>();
            int appearancesInList = 0;

            for (int i = 0; i < i_List.Count; i++)
            {
                for (int j = i; j < i_List.Count; j++)
                {
                    if (i_List[i].Source == i_List[j].Source && i_List[i].Target == i_List[j].Target && i != j)
                    {
                        listOfDuplicated.Add(i_List[i]);
                    }
                }
            }

            foreach (Move move in listOfDuplicated)
            {
                i_List.Remove(move);
            }
        }

        public List<Move> GetMovesForPiece(Piece i_Piece)
        {
            int[] rowDirectionOptions = [];
            List<Move> o_availableMoves = new List<Move>();
            Piece piece = Board.Pieces[i_Piece.Row, i_Piece.Column];

            if (piece.Type == ePieceType.Empty)
            {
                return o_availableMoves;
            }

            if (piece.Player == eTypeOfPlayer.Player1)
            {
                if (piece.Type == ePieceType.King)
                {
                    rowDirectionOptions = [-1, 1];
                }

                else
                {
                    rowDirectionOptions = [-1];
                }
            }

            else 
            {
                if (piece.Type == ePieceType.King)
                {
                    rowDirectionOptions = [-1, 1];
                }

                else
                {
                    rowDirectionOptions = [1];
                }
            }

            FillListOfAvailableMoves(o_availableMoves, i_Piece, rowDirectionOptions, piece);
            EraseDuplicatedInLists(o_availableMoves);
            return o_availableMoves;
        }

        private void FillListOfAvailableMoves(List<Move> i_AvailableMoves, Piece i_Piece, int[] rowDirectionOptions, Piece i_CurrentPiece)
        {
            int[] columnsOffset = { -1, 1 };

            foreach (int columnOffset in columnsOffset)
            {
                foreach (int movementDirection in rowDirectionOptions)
                {
                    int targetedRow = i_Piece.Row + movementDirection;
                    int targetedColumn = i_Piece.Column + columnOffset;

                    if (Board.IsMoveInBoarders(targetedRow, targetedColumn) && Board.Pieces[targetedRow, targetedColumn].Type == ePieceType.Empty)
                    {
                        i_AvailableMoves.Add(new Move((i_Piece.Row, i_Piece.Column), (targetedRow, targetedColumn)));
                    }

                    foreach (int colOffset in columnsOffset)
                    {
                        int afterCaptureRow = targetedRow + movementDirection;
                        int afterCaptureColumn = targetedColumn + columnOffset;

                        if (IsCapturedMovePossible(targetedRow, targetedColumn, afterCaptureRow, afterCaptureColumn, i_CurrentPiece))
                        {
                            i_AvailableMoves.Add(new Move((i_Piece.Row, i_Piece.Column), (afterCaptureRow, afterCaptureColumn), true, (targetedRow, targetedColumn)));
                        }
                    }
                }
            }
        }

        public bool IsCapturedMovePossible(int i_OpponentRow, int i_OpponentColumn, int i_AfterCaptureRow, int i_AfterCaptureColumn, Piece i_Piece)
        {
            return Board.IsMoveInBoarders(i_OpponentRow, i_OpponentColumn) && Board.IsMoveInBoarders(i_AfterCaptureRow, i_AfterCaptureColumn)
                            && Board.Pieces[i_OpponentRow, i_OpponentColumn].Type != ePieceType.Empty && Board.Pieces[i_OpponentRow, i_OpponentColumn].Player
                            != i_Piece.Player && Board.Pieces[i_AfterCaptureRow, i_AfterCaptureColumn].Type == ePieceType.Empty;
        }

        public bool IsMoveContainsInList(Move i_Move, List<Move> i_AvailableMoves)
        {
            foreach (Move move in i_AvailableMoves)
            {
                if (move.Source.m_Row == i_Move.Source.m_Row && move.Source.m_Column == i_Move.Source.m_Column && move.CapturedSpot.m_capturedRow == i_Move.Target.m_Row
                    && move.CapturedSpot.m_capturedColumn == i_Move.Target.m_Column)
                {
                    i_Move.CapturedSpot = (move.CapturedSpot.m_capturedRow, move.CapturedSpot.m_capturedColumn);
                    return true;
                }
            }

            return false;
        }

        public string CheckVictoryOrDraw()
        {
            string resultOfGame = "";

            if (GetAvailableMoves(MainPlayer.PlayerType).Item1.Count == 0 && GetAvailableMoves(MainPlayer.PlayerType).Item2.Count == 0 &&
                GetAvailableMoves(GetOpponentPlayerType()).Item1.Count == 0 && GetAvailableMoves(GetOpponentPlayerType()).Item2.Count == 0)
            {
                resultOfGame = "draw";
            }

            else if ((GetAvailableMoves(MainPlayer.PlayerType).Item1.Count == 0 && GetAvailableMoves(MainPlayer.PlayerType).Item2.Count == 0) &&
                (GetAvailableMoves(GetOpponentPlayerType()).Item1.Count != 0 || GetAvailableMoves(GetOpponentPlayerType()).Item2.Count != 0))
            {
                Winner = OpponentName;
                resultOfGame = "winner";
            }

            else if ((GetAvailableMoves(MainPlayer.PlayerType).Item1.Count != 0 || GetAvailableMoves(MainPlayer.PlayerType).Item2.Count != 0) &&
                (GetAvailableMoves(GetOpponentPlayerType()).Item1.Count == 0 && GetAvailableMoves(GetOpponentPlayerType()).Item2.Count == 0))
            {
                Winner = MainPlayer.PlayerName;
                resultOfGame = "winner";
            }

            return resultOfGame;
        }

        private Move computerMove()
        {
            (List<Move> availableComputerMoves, List<Move> availableComputerCaptureMoves) = GetAvailableMoves(eTypeOfPlayer.Cpu);
            int moveSelectionIndexInList;
            Move selectedMove;

            if (availableComputerCaptureMoves.Count > 0)
            {
                moveSelectionIndexInList = r_Random.Next(availableComputerCaptureMoves.Count);
                selectedMove = availableComputerCaptureMoves[moveSelectionIndexInList];
            }

            else
            {
                moveSelectionIndexInList = r_Random.Next(availableComputerMoves.Count);
                selectedMove = availableComputerMoves[moveSelectionIndexInList];
            }

            return selectedMove;
        }

        public void ApplyComputerMove()
        {
            Move moveSelection = computerMove();

            if (moveSelection.IsThereCapture)
            {
                keepMakingCaptureMoves(moveSelection);
            }

            else
            {
                makeAMove(moveSelection, Board.Pieces[moveSelection.Source.m_Row, moveSelection.Source.m_Column]);
                LastUsedPiece = Board.Pieces[moveSelection.Target.m_Row, moveSelection.Target.m_Column];
                LastMoveMade = moveSelection;
                makePieceKing(LastUsedPiece);
            }
        }

        private void keepMakingCaptureMoves(Move i_MoveSelection)
        {
            while (true)
            {
                MakeACaptureMove(i_MoveSelection, Board.Pieces[i_MoveSelection.Source.m_Row, i_MoveSelection.Source.m_Column]);
                LastUsedPiece = Board.Pieces[i_MoveSelection.Target.m_Row, i_MoveSelection.Target.m_Column];
                LastMoveMade = i_MoveSelection;
                makePieceKing(LastUsedPiece);
                List<Move> additionalCaptureMoves = GetMovesForPiece(LastUsedPiece).FindAll(moves => moves.IsThereCapture);

                if (additionalCaptureMoves.Count == 0)
                {
                    break;
                }

                i_MoveSelection = applyAdittionalCaptureMovesForComputer(additionalCaptureMoves);
            }
        }

        private Move applyAdittionalCaptureMovesForComputer(List<Move> i_AdditionalCaptureMoves)
        {
            int moveSelectionIndexInList;
            Move selectedMove;
            moveSelectionIndexInList = r_Random.Next(i_AdditionalCaptureMoves.Count);
            selectedMove = i_AdditionalCaptureMoves[moveSelectionIndexInList];
            return selectedMove;
        }

        private bool isIndexesIsBoarders(int i_SourceRow, int i_SourceColumn, int i_TargetRow, int i_TargetColumn)
        {
            return !Board.IsMoveInBoarders(i_SourceRow, i_SourceColumn) || !Board.IsMoveInBoarders(i_TargetRow, i_TargetColumn);
        }

        private bool isPieceTypeSimilar(Piece i_Piece, eTypeOfPlayer i_Player)
        {
            return i_Piece.Type == ePieceType.Empty || i_Piece.Player != i_Player;
        }

        private bool isTargetPieceWithSimilarPiece(Move i_Move, eTypeOfPlayer i_Player)
        {
            return Board.Pieces[i_Move.Target.m_Row, i_Move.Target.m_Column].Player == i_Player;
        }

        private (Move, string) checkDirectionOfMovement(Move i_Move, Piece piece, int rowDifference, int columnDifference)
        {
            if (piece.Type == ePieceType.Regular)
            {
                int rowMovementDirection = piece.Player == eTypeOfPlayer.Player1 ? -1 : 1;

                if (rowDifference != rowMovementDirection || Math.Abs(columnDifference) != 1)
                {
                    return (i_Move, "Invalid move, regular pieces can only move diagonally forward");
                }
            }

            else if (piece.Type == ePieceType.King)
            {
                if (Math.Abs(rowDifference) != 1 && Math.Abs(columnDifference) != 1)
                {
                    return (i_Move, "Invalid move, king pieces can only move diagonally forward/backward");
                }
            }

            return (i_Move, "");
        }

        private (Move, string) isTargetMoveTakenOrOutOfBounds(Move i_Move, Piece i_Piece, int i_RowDifference, int i_ColumnDifference)
        {
            if (Board.Pieces[i_Move.Target.m_Row, i_Move.Target.m_Column].Player != i_Piece.Player &&
                Board.Pieces[i_Move.Target.m_Row, i_Move.Target.m_Column].Type != ePieceType.Empty)
            {
                i_Move.CapturedSpot = (i_Move.Target.m_Row, i_Move.Target.m_Column);
                i_Move.IsThereCapture = true;
                int capturedRow = i_Move.CapturedSpot.m_capturedRow;
                int capturedCol = i_Move.CapturedSpot.m_capturedColumn;

                if (!Board.IsMoveInBoarders(capturedRow + i_RowDifference, capturedCol + i_ColumnDifference))
                {
                    return (i_Move, "Invalid move, you're out of the board borders");
                }

                else if (Board.Pieces[capturedRow + i_RowDifference, capturedCol + i_ColumnDifference].Type != ePieceType.Empty)
                {
                    return (i_Move, "Invalid move, spot after captured spot is taken");
                }
            }

            return (i_Move, "");
        }

        private bool isAdditionalCaptureMoveValid(Move i_Move)
        {
            return !((i_Move.Source != (LastUsedPiece.Row, LastUsedPiece.Column)));
        }

        private void itIsNotTheFirstTurn()
        {
            if (CurrentTurn == MainPlayer.PlayerName)
            {
                IsItPlayerFirstTurn = false;
            }

            else
            {
                IsItOpponentFirstTurn = false;
            }
        }
        private (Move, string) isMoveValid(Move i_Move, eTypeOfPlayer i_Player)
        {
            if ((!IsItPlayerFirstTurn || !IsItOpponentFirstTurn) && LastMoveMade.IsThereCapture)
            {
                itIsNotTheFirstTurn();
                if (!isAdditionalCaptureMoveValid(i_Move))
                {
                    return (i_Move, "Invalid move! You have an additional capture move available");
                }
            }

            int sourceRow = i_Move.Source.m_Row;
            int sourceColumn = i_Move.Source.m_Column;
            int targetRow = i_Move.Target.m_Row;
            int targetColumn = i_Move.Target.m_Column;

            if (isIndexesIsBoarders(sourceRow, sourceColumn, targetRow, targetColumn))
            {
                return (i_Move, "Invalid move, you're out of the board borders");
            }

            Piece piece = Board.Pieces[i_Move.Source.m_Row, i_Move.Source.m_Column];

            if (isPieceTypeSimilar(piece, i_Player))
            {
                return (i_Move, "Invalid move, this is not your piece to play");
            }

            if (isTargetPieceWithSimilarPiece(i_Move, i_Player))
            {
                return (i_Move, "Invalid move, the target spot has a similar piece");
            }

            int rowDifference = targetRow - sourceRow;
            int columnDifference = i_Move.Target.m_Column - i_Move.Source.m_Column;

            if (checkDirectionOfMovement(i_Move, piece, rowDifference, columnDifference) != (i_Move, ""))
            {
                return checkDirectionOfMovement(i_Move, piece, rowDifference, columnDifference);
            }

            if (isTargetMoveTakenOrOutOfBounds(i_Move, piece, rowDifference, columnDifference) != (i_Move, ""))
            {
                return isTargetMoveTakenOrOutOfBounds(i_Move, piece, rowDifference, columnDifference);
            }

            eTypeOfPlayer currentPlayer = Board.Pieces[i_Move.Source.m_Row, i_Move.Source.m_Column].Player;
            (List<Move> availableMoves, List<Move> availableCaptureMoves) = GetAvailableMoves(currentPlayer);
            
            if (!i_Move.IsThereCapture && GetAvailableMoves(currentPlayer).Item2.Count > 0)
            {
                return (i_Move, "Invalid move, there are other capture moved to make");
            }
            
            piece = Board.Pieces[i_Move.Source.m_Row, i_Move.Source.m_Column];

            if (IsMoveContainsInList(i_Move, availableCaptureMoves))
            {
                MakeACaptureMove(i_Move, piece);
            }

            else
            {
                makeAMove(i_Move, piece);
            }
           
            makePieceKing(Board.Pieces[i_Move.Target.m_Row, i_Move.Target.m_Column]);
            LastUsedPiece = Board.Pieces[i_Move.Target.m_Row, i_Move.Target.m_Column];
            LastMoveMade = i_Move;
            return (i_Move,  "");
        }

        private bool isThereCaptureMove(Piece i_CurrentPiece)
        {
            List<Move> additionalMoves = GetMovesForPiece(i_CurrentPiece).FindAll(moves => moves.IsThereCapture);
            bool isThereCapture;

            if (additionalMoves.Count > 0)
            {
                isThereCapture = true;
            }

            else
            {
                isThereCapture = false;
            }

            return isThereCapture;
        }
        
        private void makePieceKing(Piece i_Piece)
        {
            if (isPositionInTheEndOfTheBoard(i_Piece))
            {
                i_Piece.Type = ePieceType.King;
                i_Piece.PieceRepresentation = i_Piece.Player == eTypeOfPlayer.Player1 ? 'K' : 'U';
            }
        }

        private bool isPositionInTheEndOfTheBoard(Piece i_Piece)
        {
            return i_Piece.Type == ePieceType.Regular && ((i_Piece.Player == eTypeOfPlayer.Player1
                && i_Piece.Row == 0) || (i_Piece.Player != eTypeOfPlayer.Player1
                && i_Piece.Row == Board.SizeOfBoard - 1));
        }
        private void makeAMove(Move i_Move, Piece i_Piece)
        {
            Piece copyOfPiece = new Piece(i_Piece.Type, i_Piece.Player, i_Move.Target.m_Row, i_Move.Target.m_Column);
            Board.Pieces[i_Move.Source.m_Row, i_Move.Source.m_Column].MakePieceEmpty();
            Board.Pieces[i_Move.Target.m_Row, i_Move.Target.m_Column] = copyOfPiece;
        }

        public void MakeACaptureMove(Move i_Move, Piece i_Piece)
        {
            int capturedRow = i_Move.CapturedSpot.m_capturedRow;
            int capturedColumn = i_Move.CapturedSpot.m_capturedColumn;
            int movementDirectionRow = capturedRow - i_Move.Source.m_Row;
            int movementDirectionColumn = capturedColumn - i_Move.Source.m_Column;
            int afterCaptureRow = capturedRow + movementDirectionRow;
            int afterCaptureColumn = capturedColumn + movementDirectionColumn;
            Piece copyOfPiece = new Piece(i_Piece.Type, i_Piece.Player, afterCaptureRow, afterCaptureColumn);
            Board.Pieces[i_Move.Source.m_Row, i_Move.Source.m_Column].MakePieceEmpty();
            Board.Pieces[i_Move.Target.m_Row, i_Move.Target.m_Column].MakePieceEmpty();
            Board.Pieces[afterCaptureRow, afterCaptureColumn] = copyOfPiece;
            Board.Pieces[capturedRow, capturedColumn].MakePieceEmpty();
            i_Move.Target = (afterCaptureRow, afterCaptureColumn);
        }
    }
}
