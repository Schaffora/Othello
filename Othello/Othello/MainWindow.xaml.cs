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
        public static int BOARDSIZE= 8;
        public bool isWhiteTurn;

        public int whitePlayerActualTime;
        public int blackPlayerActualTime;
        public int whitePlayerTotalTime;
        public int blackPlayerTotalTime;

        private Othello.Game game;

        private SolidColorBrush black = new SolidColorBrush(Colors.Black);
        private SolidColorBrush white = new SolidColorBrush(Colors.White);
        private SolidColorBrush lgtGreen = new SolidColorBrush(Colors.LightGreen);

        private Label score1;
        private Label score2;

        private Label time1;
        private Label time2;

        private Button[,] buttons;

        public MainWindow()
        {
            InitializeComponent();

            isWhiteTurn = false;
            whitePlayerActualTime = 0;
            whitePlayerTotalTime = 0;
            blackPlayerActualTime = 0;
            blackPlayerTotalTime = 0;

            game = new Othello.Game(isWhiteTurn, BOARDSIZE);

            ButtonInit();
            LabelInit();
            refreshBoard();
        }
       
        private void case_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int column = Grid.GetColumn(b);
            int line= Grid.GetRow(b);
            if (game.playMove(column, line, isWhiteTurn))
                isWhiteTurn = !isWhiteTurn;
            refreshBoard();
        }
        private void refreshBoard()
        {
            for (int i = 0; i < BOARDSIZE; i++)
            {
                for (int j = 0; j < BOARDSIZE; j++)
                {
                    if(game.tiles[i, j].state==state.white)
                        buttons[i, j].Background = white;
                    else if(game.tiles[i, j].state == state.black)
                        buttons[i, j].Background = black;
                    else if (game.tiles[i, j].state == state.isAbleToPlay)
                        buttons[i, j].Background = lgtGreen;
                    else if (game.tiles[i, j].state == state.empty)
                        buttons[i, j].Background = new SolidColorBrush(Colors.ForestGreen);
                }
            }

            int scr1 = game.getBlackScore();
            score1.Content = "Actual score: " + scr1.ToString();
            int scr2 = game.getWhiteScore();
            score2.Content = "Actual score: " + scr2.ToString();

        }
        public void ButtonInit()
        {
            buttons = new Button[BOARDSIZE, BOARDSIZE];
            for (int i = 0; i < BOARDSIZE; i++)
            {
                for (int j = 0; j < BOARDSIZE; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Background = new SolidColorBrush(Colors.ForestGreen);
                    Grid.SetRow(buttons[i, j], j);
                    Grid.SetColumn(buttons[i, j], i);
                    buttons[i, j].Click += this.case_Click;
                    Board.Children.Add(buttons[i, j]);
                }
            }
        }
        public void LabelInit()
        {
            Label title = new Label();
            title.Content = "Our Perfect Othello";
            title.VerticalAlignment = VerticalAlignment.Center;
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.FontWeight = FontWeights.Bold;
            title.FontSize = 30;
            Grid.SetRow(title, 0);
            Grid.SetColumn(title, 1);
            MainGrid.Children.Add(title);

            Label player1 = new Label();
            Label player2 = new Label();

            player1.Content = "Player 1";
            player2.Content = "Player 2";
            player2.HorizontalAlignment = HorizontalAlignment.Right;

            Player1.Children.Add(player1);
            Player2.Children.Add(player2);


            score1 = new Label();
            score2 = new Label();

            score1.Content = "Actual score: ";
            score2.Content = "Actual score: ";
            score2.HorizontalAlignment = HorizontalAlignment.Right;

            Grid.SetRow(score1, 1);
            Grid.SetColumn(score1, 0);
            Player1.Children.Add(score1);

            Grid.SetRow(score2, 1);
            Grid.SetColumn(score2, 0);
            Player2.Children.Add(score2);

            time1 = new Label();
            time2 = new Label();

            time1.Content = "Played Time: ";
            time2.Content = "Played Time: ";
            time2.HorizontalAlignment = HorizontalAlignment.Right;

            Grid.SetRow(time1, 2);
            Grid.SetColumn(time1, 0);
            Player1.Children.Add(time1);

            Grid.SetRow(time2, 2);
            Grid.SetColumn(time2, 0);
            Player2.Children.Add(time2);
        }

    }
}
