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


        //----------------------------
        // Game Board management

        //starting at given location, examine rows above for non empty piece
        //copy piece down and repeat for row above until all rows have been
        //moved
        public void FillFromAbove(int x, int y)
        {
            int rowLookup = y - 1;

            while (rowLookup >= 0)
            {
                if (GetSquare(x, rowLookup) != "Empty")
                {
                    SetSquare(x, y, GetSquare(x, rowLookup));
                    SetSquare(x, rowLookup, "Empty");
                    rowLookup = -1;
                }
                rowLookup--;
            }
        }


        // GenerateNewPieces
        // can be at start of game or for completion of a path
        public void GenerateNewPieces(bool dropSquares)
        {
            if (dropSquares) // successful path
            {
                for (int x = 0; x < GAME_BOARD_WIDTH; x++)
                {
                    for (int y = GAME_BOARD_HEIGHT - 1; y >= 0;y-- )
                    {
                        if (GetSquare(x, y) == "Empty")
                        {
                            FillFromAbove(x, y);
                        }
                    }
                }
            }
            else //start of game
            {
                for (int y=0; y < GAME_BOARD_HEIGHT;y++)
                    for(int x=0; x < GAME_BOARD_WIDTH; x++)
                        if (GetSquare(x, y) == "Empty")
                        {
                            RandomPiece(x, y);
                        }
            }
        } // GenerateNewSquares


        //------------------------
        // Water manipulation
        public void ResetWater()
        {
            for (int y = 0; y < GAME_BOARD_HEIGHT; y++)
                for (int x = 0; x < GAME_BOARD_WIDTH; x++)
                    boardSquares[x, y].RemoveSuffix("W");
        }

        public void FillPiece(int x, int y)
        {
            boardSquares[x, y].AddSuffix("W");
        }

        public void PropogateWater(int x, int y, string fromDirection)
        {
            // check to see if we are still on the board
            if ((y >= 0) && (y < GAME_BOARD_HEIGHT) &&
                (x >= 0) && (x < GAME_BOARD_WIDTH))
            {
                //check to see if the path should continue
                if (boardSquares[x, y].HasConnector(fromDirection) &&
                    !boardSquares[x, y].PieceSuffix.Contains("W"))
                {
                    // fill the square, add to water tracker and propogate the path
                    FillPiece(x, y);
                    waterTracker.Add(new Vector2(x, y));
                    foreach (string end in boardSquares[x, y].GetOtherEnds(fromDirection))
                    {
                        switch (end)
                        {
                            case "Left":
                                PropogateWater(x - 1, y, "Right");
                                break;
                            case "Right":
                                PropogateWater(x + 1, y, "Left");
                                break;
                            case "Top":
                                PropogateWater(x, y-1, "Bottom");
                                break;
                            case "Bottom":
                                PropogateWater(x, y + 1, "Top");
                                break;
                        }//switch
                    }//for each (not really necessary to block this one
                } // if path continues
            } // if on board
        } // propogate water


        public List<Vector2> GetWaterChain(int y) {
            waterTracker.Clear();
            PropogateWater(0, y, "Left");
            return waterTracker;
        }

    } //class GameBoard
} // namespace
