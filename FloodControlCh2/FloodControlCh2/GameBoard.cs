using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FloodControlCh2
{
    class GameBoard
    {
        Random rand = new Random();

        public const int GAME_BOARD_HEIGHT = 10;
        public const int GAME_BOARD_WIDTH = 8;

        private GamePiece[,] boardSquares = new GamePiece[GAME_BOARD_WIDTH, GAME_BOARD_HEIGHT];

        private List<Vector2> waterTracker = new List<Vector2>();

        //-----------------
        // Constructors and initialization

        public GameBoard()
        {
            ClearBoard();
        }

        public void ClearBoard()
        {
            for (int x = 0; x < GAME_BOARD_WIDTH; x++)
                for (int y = 0; y < GAME_BOARD_HEIGHT; y++)
                    boardSquares[x, y] = new GamePiece("Empty");
        }


        //------------------------
        // Game Board to Game Piece handoffs

        public void RotatePiece(int x, int y, bool clockwise)
        {
            boardSquares[x, y].RotatePiece(clockwise);
        }

        public Rectangle GetSourceRect(int x, int y)
        {
            return boardSquares[x, y].GetSourceRect();
        }

        public string GetSquare(int x, int y)
        {
            return boardSquares[x, y].PieceType;
        }

        public void SetSquare(int x, int y, string pieceName)
        {
            boardSquares[x, y].SetPiece(pieceName);
        }

        public bool HasConnector(int x, int y, string direction)
        {
            return boardSquares[x, y].HasConnector(direction);
        }

        public void RandomPiece(int x, int y)
        {
            boardSquares[x, y].SetPiece(
                GamePiece.PieceTypes[rand.Next(0, GamePiece.MAX_PLAYABLE_PIECE_INDEX + 1)]);
        }




    }
}
