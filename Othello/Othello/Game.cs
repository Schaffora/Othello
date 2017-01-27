using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OthelloConsole;
using System.Windows;

namespace Othello
{
    
    class Game : IPlayable {
        public int boardsize;
        public Tile[,] tiles;
        private List<Tuple<int, int>> flips;

        public Game(bool isWhiteTurn, int size)
        {
            flips= new List<Tuple<int, int>>();
            boardsize = size;
            tiles = new Tile[boardsize, boardsize];

            for (int i=0;i< boardsize; i++)
            {
                for(int j=0;j< boardsize; j++)
                {
                    tiles[i, j] = new Tile();
                }
            }
            tiles[4, 4].state = state.white;
            tiles[3, 3].state = state.white;
            tiles[4, 3].state = state.black;
            tiles[3, 4].state = state.black;

            updatePlayables(isWhiteTurn);
        }

        public bool isPlayable(int column, int line, bool isWhite)
        {
            bool tilePlayabe = false;
            if(isWhite)
                tilePlayabe=playable(column, line, state.white);
            else
                tilePlayabe=playable(column, line, state.black);
            return tilePlayabe;
        }
        public bool playable(int c, int l, state s)
        {
            bool found = false;
            state otherState;
            if (s == state.white)
                otherState = state.black;
            else
                otherState = state.white;

            int[,] direction = { { 1, 0 }, { 0, 1 }, { 0, -1 }, { -1, 0 }, { 1, 1 }, { 1, -1 }, { -1, -1 }, { -1, 1 } };

            for(int i=0;i<8; i++)
            {
                if (found == true)
                {
                    break;
                }
                int sl = l + direction[i, 0];
                int sc = c + direction[i, 1];
        
                bool got = false;
                while (sl >=0 && sl<=boardsize-1 && sc>=0 && sc<=boardsize-1)
                {
                    if(tiles[sl,sc].state==state.empty || tiles[sl, sc].state==state.isAbleToPlay)
                    {
                        got = false;
                        break;
                    }
                    if (tiles[sl, sc].state == otherState)
                    {
                        got = true;
                    }
                    if (tiles[sl, sc].state == s && got == false)
                    {
                        got = false;
                        break;
                    }
                    if (tiles[sl, sc].state == s && got == true)
                    {
                        found = true;
                        break;
                    }
                    sl = sl + direction[i, 0];
                    sc = sc + direction[i, 1];
                }
            }
            return found;
        }


        public bool playMove(int column, int line, bool isWhite)
        {
            if (tiles[line, column].state == state.isAbleToPlay)
            {
                if (isWhite == true)
                {
                        tiles[line, column].state = state.white;
                        flipPieces(column, line,state.white);
                        updatePlayables(false);
                        return true;
                }
                else
                {
                        tiles[line, column].state = state.black;
                        flipPieces(column, line,state.black);
                        updatePlayables(true);
                        return true;
                }                
            }
            else
            {
                return false;
            }
        }
        public void clearPlayables()
        {
            for (int i = 0; i < boardsize; i++)
            {
                for (int j = 0; j < boardsize; j++)
                {
                    if (tiles[i, j].state == state.isAbleToPlay)
                    {
                        tiles[i, j].state = state.empty;
                    }
                }
            }
        }
        public void updatePlayables(bool isWhiteTurn)
        {
            clearPlayables();
            
            for (int i = 0; i < boardsize; i++)
            {
                for (int j = 0; j < boardsize; j++)
                {
                    if(isPlayable(i,j, isWhiteTurn))
                    {
                        tiles[j, i].state = state.isAbleToPlay;
                    }
                }
            }
        }

        public void flipPieces(int c, int l, state s)
        {
            flips.Clear();
            state otherState;
            if (s == state.white)
                otherState = state.black;
            else
                otherState = state.white;

            int[,] direction = { { 1, 0 }, { 0, 1 }, { 0, -1 }, { -1, 0 }, { 1, 1 }, { 1, -1 }, { -1, -1 }, { -1, 1 } };

            for (int i = 0; i < 8; i++)
            {
                int sl = l + direction[i, 0];
                int sc = c + direction[i, 1];
                bool got = false;
                while (sl >= 0 && sl <= boardsize - 1 && sc >= 0 && sc <= boardsize - 1)
                {
                    if (tiles[sl, sc].state == state.empty || tiles[sl, sc].state == state.isAbleToPlay)
                    {
                        got = false;
                        break;
                    }
                    if (tiles[sl, sc].state == otherState)
                    {
                        got = true;
                    }
                    if (tiles[sl, sc].state == s && got == false)
                    {
                        got = false;
                        break;
                    }
                    if (tiles[sl, sc].state == s && got == true)
                    {
                        flips.Add(new Tuple<int, int>(sl, sc));
                        break;
                    }
                    sl = sl + direction[i, 0];
                    sc = sc + direction[i, 1];
                }
            }
            foreach (Tuple<int, int> item in flips)
            {
                int lVal= item.Item1;
                int cVal = item.Item2;
                tiles[lVal, cVal].state = s;
            }
        }

        public Tuple<char, int> getNextMove(int[,] game, int level, bool whiteTurn)
        {
            Tuple<char, int> tuple = new Tuple<char, int>('a',0);
            return tuple;
        }

        public int getWhiteScore()
        {
            int score = 0;
            for (int i = 0; i < boardsize; i++)
            {
                for (int j = 0; j < boardsize; j++)
                {
                    if(tiles[i, j].state==state.white)
                    {
                        score += 1;
                    }
                }
            }
            return score;
        }

        public int getBlackScore()
        {
            int score = 0;
            for (int i = 0; i < boardsize; i++)
            {
                for (int j = 0; j < boardsize; j++)
                {
                    if (tiles[i, j].state == state.black)
                    {
                        score += 1;
                    }
                }
            }
            return score;
        }
    }
}
