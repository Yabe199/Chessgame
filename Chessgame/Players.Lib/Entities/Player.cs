using Pawns.Lib.Entities;
using Pawns.Lib.Services;
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
        public List<Pawn> PlayerPawns { get; set; }
        public List<Pawn> TakenPawns { get; set; }

        public Player(string name, int index, int score)
        {
            PawnService pawnService = new PawnService(); 
            
            Name = name;
            Score = score;
            Index = index;
            PlayerPawns = pawnService.Pawns;
            TakenPawns = new List<Pawn>();
        }
    }
}
