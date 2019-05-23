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
        public Player[] Players { get; set; }

        public PlayerService()
        {
            Players = new Player[2];
        }

        public void AddPlayer(Player newPlayer)
        {
            Players[1] = newPlayer;
        }

        public void UpdatePlayer(Player editedPlayer)
        {
            Players[editedPlayer.Index] = editedPlayer;
        }
    }
}
