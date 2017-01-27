using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Othello
{
    enum state
    {
        black,
        white,
        empty,
        isAbleToPlay
    }

    class Tile
    {
        public state state;

        public Tile()
        {
            state = state.empty;       
        }

  
    }
}
