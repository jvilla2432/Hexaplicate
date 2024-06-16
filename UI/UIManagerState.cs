using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Hexaplicate
{
    internal interface UIManagerState
    {
        public void handle_input(MouseState state); 
    }
}
