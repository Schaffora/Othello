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

        public MainWindow()
        {
            InitializeComponent();

            isWhiteTurn = true;
            whitePlayerActualTime = 0;
            whitePlayerTotalTime = 0;
            blackPlayerActualTime = 0;
            blackPlayerTotalTime = 0;

            game = new Othello.Game();

            List<Button> buttons = new List<Button>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                   buttons.Add(new Button());
                   Grid.SetRow(buttons[buttons.Count-1], i);
                   Grid.SetColumn(buttons[buttons.Count - 1], j);
                   buttons[buttons.Count - 1].Click += this.case_Click;
                   Board.Children.Add(buttons[buttons.Count - 1]);
                }
            }
        }
        private void case_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int column = Grid.GetColumn(b);
            int row= Grid.GetRow(b);
            if(isWhiteTurn==true)
            {
                b.Background = new SolidColorBrush(Colors.Black);
            }
            else
            {
                b.Background = new SolidColorBrush(Colors.White);
            }
            isWhiteTurn = !isWhiteTurn;

        }

    }
}
