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
using Microsoft.Win32;
using System.IO;


namespace Othello
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        /* Static boardsize */
        public static int BOARDSIZE= 8;

        /* Choose if black or white start */
        public bool isWhiteTurn;

        /* Time values */
        public int whitePlayerActualTime;
        public int blackPlayerActualTime;
        public int whitePlayerTotalTime;
        public int blackPlayerTotalTime;

        /* Othello game class (using IPlayable interface) */
        private Othello.Game game;

        /* Static color values for the graphics */
        private static SolidColorBrush BLACK = new SolidColorBrush(Colors.Black);
        private static SolidColorBrush WHITE = new SolidColorBrush(Colors.White);
        private static SolidColorBrush LGTGREEN = new SolidColorBrush(Colors.LightGreen);

        /* Time Labels */
        private Label time1;
        private Label time2;

        /* Button tab */
        private Button[,] buttons;
        
        /* Databinding */
        public event PropertyChangedEventHandler PropertyChanged;

        /* Scores */
        private String whiteScore;
        private String blackScore;
        bool win = false;

        /* Main point of the application */
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
       
        /* Function that handle the click event of all buttons*/
        private void case_Click(object sender, EventArgs e)
        {
            if(win==false)
            {
                Button b = (Button)sender;
                int column = Grid.GetColumn(b);
                int line = Grid.GetRow(b);
                if (game.playMove(column, line, isWhiteTurn))
                    isWhiteTurn = !isWhiteTurn;
                else
                {
                    if(game.updatePlayables(!isWhiteTurn) == false)
                        MessageBox.Show("Player can't play, so he skip his turn");
                    game.updatePlayables(isWhiteTurn);
                }
                if(game.getScore(state.white)==0)
                {
                    MessageBox.Show("Player 2 win ");
                    win = true;
                }
                if (game.getScore(state.black) == 0)
                {
                    MessageBox.Show("Player 1 win ");
                    win = true;
                }
                if (game.getScore(state.white)+ game.getScore(state.black)==64)
                {
                    win = true;
                    if(game.getScore(state.white)>game.getScore(state.black))
                        MessageBox.Show("Player 1 win ");
                    else if (game.getScore(state.white) < game.getScore(state.black))
                        MessageBox.Show("Player 2 win ");
                    else
                        MessageBox.Show("It's a tile ");
                }
                refreshBoard();
            }
        }

        /* Graphic refresh of the board */
        private void refreshBoard()
        {
            updateScores();
            for (int i = 0; i < BOARDSIZE; i++)
            {
                for (int j = 0; j < BOARDSIZE; j++)
                {
                    if(game.tiles[i, j].state==state.white)
                        buttons[i, j].Background = WHITE;
                    else if(game.tiles[i, j].state == state.black)
                        buttons[i, j].Background = BLACK;
                    else if (game.tiles[i, j].state == state.isAbleToPlay)
                        buttons[i, j].Background = LGTGREEN;
                    else if (game.tiles[i, j].state == state.empty)
                        buttons[i, j].Background = new SolidColorBrush(Colors.ForestGreen);
                }
            }

        }

        /* Button initialisation (creation) */
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

        /* Label initialisation (creation) */
        public void LabelInit()
        {
            Label title = new Label();
            title.Content = "Our Perfect Othello";
            title.VerticalAlignment = VerticalAlignment.Center;
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.FontWeight = FontWeights.Bold;
            title.FontSize = 30;
            title.FontFamily = new FontFamily("magneto");
            title.Foreground = new SolidColorBrush(Colors.Blue);
            Grid.SetRow(title, 0);
            Grid.SetColumn(title, 1);
            MainGrid.Children.Add(title);

            Label player1 = new Label();
            Label player2 = new Label();

            player1.Content = "Player 1";
            player1.FontSize = 18;
            player1.Foreground= new SolidColorBrush(Colors.Blue);
            player1.FontStyle = FontStyles.Oblique;
            player2.Content = "Player 2";
            player2.FontSize = 18;
            player2.Foreground = new SolidColorBrush(Colors.Blue);
            player2.FontStyle = FontStyles.Oblique;
            player2.HorizontalAlignment = HorizontalAlignment.Right;

            Player1.Children.Add(player1);
            Player2.Children.Add(player2);


            time1 = new Label();
            time2 = new Label();

            time1.Content = "Actual time: ";
            time2.Content = "Actual time: ";
            time2.HorizontalAlignment = HorizontalAlignment.Right;

            Grid.SetRow(time1, 2);
            Grid.SetColumn(time1, 0);
            Player1.Children.Add(time1);

            Grid.SetRow(time2, 2);
            Grid.SetColumn(time2, 0);
            Player2.Children.Add(time2);
        }

        /* Databinding for score */
        private void updateScores()
        {
            updateBlackScore = "Actual score: " + game.getBlackScore().ToString();
            updateWhiteScore = "Actual score: " + game.getWhiteScore().ToString();
        }

        /* Databinding black score */
        public String updateBlackScore
        {
            get { return blackScore; }
            set
            {
                blackScore = value;
                RaisePropertyChanged("updateBlackScore");
            }
        }

        /* Databinding white score */
        public String updateWhiteScore
        {
            get { return whiteScore; }
            set
            {
                whiteScore = value;
                RaisePropertyChanged("updateWhiteScore");
            }
        }
        
        /* On property changed */
        private void RaisePropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /*Menu handling, on new game click */
        private void mnuNew_Click(object sender, RoutedEventArgs e)
        {
            game.resetGame();
            refreshBoard();
            isWhiteTurn = false;
        }

        /*Menu handling, on close game click */
        private void mnuClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /*Menu handling, on Save game click */
        private void mnuSave_Click(object sender, RoutedEventArgs e)
        {
            game.saveGame(isWhiteTurn);
        }

        /*Menu handling, on Open game click */
        private void mnuOpen_Click(object sender, RoutedEventArgs e)
        {
            string datas = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                datas = File.ReadAllText(openFileDialog.FileName);
                Char delimiter = ';';
                String[] stateDatas = datas.Split(delimiter);
                if (stateDatas[BOARDSIZE * BOARDSIZE] == "f")
                    isWhiteTurn = false;
                else
                    isWhiteTurn = true;
                int cpt = 0;
                for (int i = 0; i < BOARDSIZE; i++)
                {
                    for (int j = 0; j < BOARDSIZE; j++)
                    {
                        game.openGame(stateDatas[cpt], i, j);
                        cpt++;
                    }
                }
            }
            refreshBoard();
        }
    }
}
