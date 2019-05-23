using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players.Lib.Entities
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int Index { get; set; }

        public Player(string name, int index, int score)
        {
            Name = name;
            Score = score;
            Index = index;
        }
    }
}
