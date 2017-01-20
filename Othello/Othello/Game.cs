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
            listMove(state.black);

        }

        public bool isPlayable(int column, int line, bool isWhite)
        {
            return true;
        }


        public bool playMove(int column, int line, bool isWhite)
        {
            Tuple<int, int> pos = new Tuple<int, int>(column, line);
            if (tiles[column, line].state == state.empty)
            {
                if (isWhite == true)
                {
                    //tiles[column, line].state = state.white;
                    //return true;
                    if (moveList.Contains(pos))
                    {
                        tiles[column, line].state = state.white;
                        listMove(state.black);
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    if (moveList.Contains(pos))
                    {
                        tiles[column, line].state = state.black;
                        listMove(state.white);
                        return true;
                    }
                    else
                        return false;
                }                
            }
            else
            {
                return false;
            }
        }
        //TODO lister tout les coups permis pour un joueur
        public void listMove(state s)
        {
            List<Tuple<int, int>> taken = new List<Tuple<int, int>>();
            moveList.Clear();
            //TODO check les cases au dessus, au dessous, à gauche et à droite

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (tiles[i, j].state != state.empty)
                    {
                        taken.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            
            for(int i=0;i<taken.Count;i++)
            {
                bool opponent;
                state player1 = s;
                state player2;
                if (player1 == state.black)
                {
                    player2 = state.white;
                }
                else
                {
                    player2 = state.black;
                }                
                int posX = taken[i].Item1;
                int posY = taken[i].Item2;
                int x;
                int y;

                //Left
                x = posX - 1;
                opponent = false;
                while (x >= 0)
                {
                    if (tiles[x, posY].state == player2)
                    {
                        x -= 1;
                        opponent = true;
                    }
                    else if (tiles[x, posY].state == player1)
                    {
                        break;
                    }
                    else
                    {
                        if (opponent)
                            moveList.Add(new Tuple<int, int>(x, posY));
                        break;
                    }
                }
            }
            //TODO check les diagonales
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
