using Chessgame.Lib.Entities;
using Pawns.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Players.Lib.Entities
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int Index { get; set; }
        public colour Color { get; set; }
        public List<Pawn> PlayerPawns { get; set; }
        public List<Pawn> TakenPawns { get; set; }

        public Player(string name, int index, int score)
        {
            PawnService pawnService = new PawnService(AssignColor(index)); 
            
            Name = name;
            Color = AssignColor(index);
            Index = index;
            PlayerPawns = pawnService.Pawns;
            TakenPawns = new List<Pawn>();
            Score = score;
        }

        private colour AssignColor(int index)
        {
            colour playerColor;

            if (Index == 0)
            {
                playerColor = colour.Black;
            }
            else
            {
                playerColor = colour.White;
            }

            return playerColor;
        }
    }
}
