using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FloodControlCh2
{
    class GamePiece
    {
        public static string[] PieceTypes =
        {
                "Left,Right",
                "Top,Bottom",
                "Left,Top",
                "Top,Right",
                "Right,Bottom",
                "Bottom,Left",
                "Empty"
        };

        public const int PIECE_HEIGHT = 40;
        public const int PIECE_WIDTH = 40;

        public const int MAX_PLAYABLE_PIECE_INDEX = 5;
        public const int EMPTY_PIECE_INDEX = 6;

        private const int TEXTURE_OFFSET_X = 1;
        private const int TEXTURE_OFFSET_Y = 1;
        private const int TEXTURE_PADDING_X = 1;
        private const int TEXTURE_PADDING_Y = 1;

        private string pieceType="";
        private string pieceSuffix = "";


        public string PieceType
        {
            get { return pieceType; }
        }

        public string PieceSuffix
        {
            get { return pieceSuffix; }
        }

    }
}
