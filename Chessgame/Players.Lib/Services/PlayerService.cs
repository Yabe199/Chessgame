using Players.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players.Lib.Services
{
    public class PlayerService
    {
        public List<Player> Players { get; set; }

        public PlayerService()
        {
            Players = new List<Player>();
        }
    }
}
