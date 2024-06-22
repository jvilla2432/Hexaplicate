using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate
{
    internal struct Recipe
    {
        public int ticks;
        public string name;
        public string description;
        public (int resource, int cost, bool type)[] inputs;
        public (int resource, int cost, bool type)[] outputs;
    }
}
