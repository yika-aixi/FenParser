using FenParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FenParser
{
    /// <summary>
    /// Determines board state from a given FEN string.
    /// https://www.xqbase.com/protocol/cchess_fen.htm
    /// </summary>
    public class FenParser
    {
        #region Fields

        /// <summary>
        /// The unparsed FEN string.
        /// </summary>
        private string FEN { get; set; }

        /// <summary>
        /// A FEN substring representing the position of the white and black pieces on the board.
        /// 红色 -- 棋盘布局
        /// </summary>
        private string PiecePlacementString { get; set; }

        /// <summary>
        /// A FEN substring representing the active player.
        /// 绿色 -- 移动方
        /// </summary>
        private string ActiveColorString { get; set; }

        /// <summary>
        /// A FEN substring representing the availability of castling for each player.
        /// 深紫色区域 -- 可选,中国象棋输入"-"
        /// </summary>
        private string CastlingAvailabilityString { get; set; }

        /// <summary>
        /// A FEN substring representing the square which is currently available for "en passant" capture ('-' if a square is not available).  
        /// 紫红色区域 -- 可选,中国象棋输入"-"
        /// </summary>
        private string EnPassantSquareString { get; set; }

        /// <summary>
        /// A FEN substring representing the number of half moves since the last pawn advance or piece capture (used to determine stalemate).
        /// 蓝色区域 -- 半回合数
        /// </summary>
        private string HalfmoveClockString { get; set; }

        /// <summary>
        /// A FEN substring representing the game turn (incremented after Black moves).
        /// 棕色区域 -- 回合数
        /// </summary>
        private string FullmoveNumberString { get; set; }

        public BoardStateData BoardStateData { get; set; }

        #endregion

        #region Methods

        public void ParseFenSubstrings(string[] fenSubstrings)
        {
            PiecePlacementString = fenSubstrings[0];
            ActiveColorString = fenSubstrings[1];
            CastlingAvailabilityString = fenSubstrings[2];
            EnPassantSquareString = fenSubstrings[3];
            HalfmoveClockString = fenSubstrings[4];
            FullmoveNumberString = fenSubstrings[5];
        }

        #endregion

        #region Constructors
        public FenParser()
        {
            BoardStateData = new BoardStateData();
        }

        public FenParser(string fen, ChessType chessType = ChessType.ChessGame)
        {
            FEN = fen;
            ParseFenSubstrings(FEN.Split(' '));
            if (chessType == ChessType.ChinaChess)
            {
                BoardStateData = new BoardStateData(10,9,PiecePlacementString, ActiveColorString, CastlingAvailabilityString,
                EnPassantSquareString, HalfmoveClockString, FullmoveNumberString);
            }
            else
            {
                BoardStateData = new BoardStateData(PiecePlacementString, ActiveColorString, CastlingAvailabilityString,
                EnPassantSquareString, HalfmoveClockString, FullmoveNumberString);
            }
            
        }
        #endregion
    }

    public enum ChessType
    {
        /// <summary>
        /// 国际象棋
        /// </summary>
        ChessGame,
        /// <summary>
        /// 中国象棋
        /// </summary>
        ChinaChess
    }
}
