using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Othello
{
    /* State enum for a tile */
    enum state
    {
        black,
        white,
        empty,
        isAbleToPlay
    }
    /* Tile class */
    class Tile
    {
        public state state;
        public Tile()
        {
            /* Initial state is empty */
            state = state.empty;       
        }
    }
}
