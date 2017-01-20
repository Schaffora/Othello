using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Othello
{

    class Tile
    {
        public bool taken;
        public bool isPlayable;
        public bool isWhite;

        public Tile()
        {
            taken = false;       
            isWhite = true;
            isPlayable = true;
        }

  
    }
}
