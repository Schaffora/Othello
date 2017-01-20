using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OthelloConsole;
namespace Othello
{
    
    class Game : IPlayable { 
   
        Tile [,] tiles = new Tile[8,8];
        public Game()
        {   
            for(int i=0;i<8;i++)
            {
                for(j=0;j<8;j++)
                {
                    tiles[i, j].Add(new Othello.Tile());
                }
            }
            tiles[4, 4].taken = true;
            tiles[3, 3].taken = true;
            tiles[4, 3].taken = true;
            tiles[3, 4].taken = true;

            tiles[4, 4].isWhite = true;
            tiles[3, 3].isWhite = true;
            tiles[4, 3].isWhite = false;
            tiles[3, 4].isWhite = false;

            tiles[4, 4].isPlayable = false;
            tiles[3, 3].isPlayable = false;
            tiles[4, 3].isPlayable = false;
            tiles[3, 4].isPlayable = false;
        }

        bool IPlayable.isPlayable(int column, int line, bool isWhite)
        {
            return true;
        }

        bool IPlayable.playMove(int column, int line, bool isWhite)
        {
            return true;
        }

        Tuple<char, int> IPlayable.getNextMove(int[,] game, int level, bool whiteTurn)
        {
            Tuple<char, int> tuple = new Tuple<char, int>('a',0);
            return tuple;
        }

        int IPlayable.getWhiteScore()
        {
            return 0;
        }

        int IPlayable.getBlackScore()
        {
            return 0;
        }
    }
}
