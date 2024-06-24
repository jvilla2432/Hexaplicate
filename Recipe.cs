using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexaplicate
{
    // Type : 0 is resource, 1 is essence
    internal readonly struct Recipe
    {
        public Recipe(int ticks, string name, string description,
            (int resource, int cost, bool type)[] inputs, (int resource, int cost, bool type)[] outputs)
        {
            Ticks = ticks;
            Name = name;
            Description = description;
            Inputs = inputs;
            Outputs = outputs;
        }
        public int Ticks { get; init;  }
        public string Name { get; init; }
        public string Description { get; init; }
        public (int resource, int cost, bool type)[] Inputs { get; init; }
        public (int resource, int cost, bool type)[] Outputs { get; init; }
    }
}
