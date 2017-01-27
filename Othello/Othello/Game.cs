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
        public static int BOARDSIZE = 8;
        public Tile [,] tiles = new Tile[BOARDSIZE, BOARDSIZE];
        private List<Tuple<int, int>> moveList;

        public Game()
        {
            moveList = new List<Tuple<int, int>>();
            for (int i=0;i< BOARDSIZE; i++)
            {
                for(int j=0;j< BOARDSIZE; j++)
                {
                    tiles[i, j] = new Tile();
                }
            }

            tiles[4, 4].state = state.white;
            tiles[3, 3].state = state.white;
            tiles[4, 3].state = state.black;
            tiles[3, 4].state = state.black;
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
                    if (moveList.Contains(pos))
                    {
                        tiles[column, line].state = state.white;
                        flipPieces(column, line,state.black);
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
                        flipPieces(column, line,state.white);
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

            for (int i = 0; i < BOARDSIZE; i++)
            {
                for (int j = 0; j < BOARDSIZE; j++)
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

        public void flipPieces(int posX, int posY,state s)
        {
            bool opponent;
            state player1 = s;
            state player2;
            bool flipLeft = false;
            int x;
            int y;
            if (player1 == state.black)
            {
                player2 = state.white;
            }
            else
            {
                player2 = state.black;
            }
            x = posX + 1;
            opponent = false;
            while (x < BOARDSIZE)
            {
                if (tiles[x, posY].state == player1)
                {
                    x += 1;
                    opponent = true;
                }
                else if (tiles[x, posY].state == player2)
                {
                    if (opponent)
                        flipLeft = true;
                    break;
                }
                else
                {
                    break;
                }
            }
            int tempx = posX;
            int tempy = posY;
            if (flipLeft)
            {
                tempx = posX + 1;
                while (tiles[tempx, posY].state == player1)
                {
                    tiles[tempx, posY].state = player2;
                    tempx += 1;
                }
            }

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
