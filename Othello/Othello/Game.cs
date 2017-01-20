using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OthelloConsole;
namespace Othello
{

    
    class Game : IPlayable { 
   
        public Tile [,] tiles = new Tile[8,8];
        private List<Tuple<int, int>> moveList;

        public Game()
        {
            moveList = new List<Tuple<int, int>>();
            for (int i=0;i<8;i++)
            {
                for(int j=0;j<8;j++)
                {
                    tiles[i, j] = new Tile();
                }
            }

            tiles[4, 4].state = state.white;
            tiles[3, 3].state = state.white;
            tiles[4, 3].state = state.black;
            tiles[3, 4].state = state.black;

            tiles[4, 4].isPlayable = false;
            tiles[3, 3].isPlayable = false;
            tiles[4, 3].isPlayable = false;
            tiles[3, 4].isPlayable = false;
        }

        public bool isPlayable(int column, int line, bool isWhite)
        {
            return true;
        }


        public bool playMove(int column, int line, bool isWhite)
        {
            if (tiles[column, line].state == state.empty)
            {
                if(isWhite == true)
                {
                    tiles[column, line].state = state.white;
                    return true;
                }
                else
                {
                    tiles[column, line].state = state.black;
                    return true;
                }                
            }
            else
            {
                return false;
            }
        }

        public void listMove()
        {
            moveList.Clear();
        }

        public Tuple<char, int> getNextMove(int[,] game, int level, bool whiteTurn)
        {
            Tuple<char, int> tuple = new Tuple<char, int>('a',0);
            return tuple;
        }

        public int getWhiteScore()
        {
            return 0;
        }

        public int getBlackScore()
        {
            return 0;
        }
    }
}
