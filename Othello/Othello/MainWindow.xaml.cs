using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
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

        private Label time1;
        private Label time2;

        private Button[,] buttons;
        
        public event PropertyChangedEventHandler PropertyChanged;
        private String whiteScore;
        private String blackScore;

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
            updateScores();
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


            time1 = new Label();
            time2 = new Label();

            time1.Content = "Actual score: ";
            time2.Content = "Actual score: ";
            time2.HorizontalAlignment = HorizontalAlignment.Right;

            Grid.SetRow(time1, 2);
            Grid.SetColumn(time1, 0);
            Player1.Children.Add(time1);

            Grid.SetRow(time2, 2);
            Grid.SetColumn(time2, 0);
            Player2.Children.Add(time2);
        }
        private void updateScores()
        {
            updateBlackScore = "Actual score: " + game.getBlackScore().ToString();
            updateWhiteScore = "Actual score: " + game.getWhiteScore().ToString();
        }

        public String updateBlackScore
        {
            get { return blackScore; }
            set
            {
                blackScore = value;
                RaisePropertyChanged("updateBlackScore");
            }
        }
        public String updateWhiteScore
        {
            get { return whiteScore; }
            set
            {
                whiteScore = value;
                RaisePropertyChanged("updateWhiteScore");
            }
        }
        
        private void RaisePropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
