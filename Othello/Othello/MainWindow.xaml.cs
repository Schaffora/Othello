using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Othello
{
    // Utiliser les mots clé suivant : Black - White - Tile - Move
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool isWhiteTurn;
        public int whitePlayerActualTime;
        public int blackPlayerActualTime;
        public int whitePlayerTotalTime;
        public int blackPlayerTotalTime;
        private Othello.Game game;
        private SolidColorBrush black = new SolidColorBrush(Colors.Black);
        private SolidColorBrush white = new SolidColorBrush(Colors.White);
        private Button[,] buttons;

        public MainWindow()
        {
            InitializeComponent();

            isWhiteTurn = false;
            whitePlayerActualTime = 0;
            whitePlayerTotalTime = 0;
            blackPlayerActualTime = 0;
            blackPlayerTotalTime = 0;

            game = new Othello.Game();

            buttons = new Button[Game.BOARDSIZE, Game.BOARDSIZE];
            for (int i = 0; i < Game.BOARDSIZE; i++)
            {
                for (int j = 0; j < Game.BOARDSIZE; j++)
                {
                   buttons[i, j] = new Button();
                    buttons[i, j].Background= new SolidColorBrush(Colors.ForestGreen);
                    Grid.SetRow(buttons[i, j], j);
                   Grid.SetColumn(buttons[i, j], i);
                   buttons[i, j].Click += this.case_Click;
                   Board.Children.Add(buttons[i, j]);
                }
            }
            refreshBoard();
        }
        private void case_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int column = Grid.GetColumn(b);
            int row= Grid.GetRow(b);
            if (game.playMove(column, row, isWhiteTurn))
            {
                if (isWhiteTurn == true)
                {
                    b.Background = white ;
                }
                else
                {
                    b.Background = black;
                }
                isWhiteTurn = !isWhiteTurn;
            }
            refreshBoard();
        }
        private void refreshBoard()
        {
            //TODO afficher les case jouables par chaque user
            for (int i = 0; i < Game.BOARDSIZE; i++)
            {
                for (int j = 0; j < Game.BOARDSIZE; j++)
                {
                    if(game.tiles[i, j].state==state.white)
                    {
                        buttons[i, j].Background = white;
                    }
                    else if(game.tiles[i, j].state == state.black)
                    {
                        buttons[i, j].Background = black;
                    }                  
                }
            }
        }

    }
}
