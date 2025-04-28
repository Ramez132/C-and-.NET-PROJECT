using System;
using System.Collections.Generic;
using System.Linq;
namespace GameEngine
{
    public class Piece
    {
        public int Row {  get; set; }
        public int Column { get; set; }
        public ePieceType Type {  get; set; }
        public eTypeOfPlayer Player { get; private set; }
        public char PieceRepresentation { get; set; }

        public Piece(ePieceType i_Type, eTypeOfPlayer i_Player, int i_row, int i_column)
        {
            Type = i_Type;
            Player = i_Player;
            Row = i_row;
            Column = i_column;
            pieceReresentaionInitialization();
        }

        private void pieceReresentaionInitialization()
        {
            if (Player == eTypeOfPlayer.Player1)
            {
                if (Type == ePieceType.King)
                {
                    PieceRepresentation = 'K';
                }

                else
                {
                    PieceRepresentation = 'X';
                }
            }

            else if (Player == eTypeOfPlayer.Player2 || Player == eTypeOfPlayer.Cpu)
            {
                if (Type == ePieceType.King)
                {
                    PieceRepresentation = 'U';
                }

                else
                {
                    PieceRepresentation = 'O';
                }
            }

            else
            {
                PieceRepresentation = ' ';
            }
        }

        public void MakePieceEmpty()
        {
            Type = ePieceType.Empty;
            Player = eTypeOfPlayer.None;
            PieceRepresentation = ' ';
        }
    }
}
