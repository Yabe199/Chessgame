using Chessgame.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessgame.Lib.Services
{
    public class PlayerService
    {
        // TODO public List<Player> Players { get; set; }
        public Player[] Players { get; set; }

        public PlayerService()
        {
            Players = new Player[2];
        }

        public void AddPlayer(Player newPlayer)
        {
            Players[newPlayer.Index] = newPlayer;
        }

        public void UpdatePlayer(Player editedPlayer)
        {
            Players[editedPlayer.Index] = editedPlayer;
        }
    }
}
