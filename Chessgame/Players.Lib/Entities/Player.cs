using Chessgame.Lib.Entities;
using Chessgame.Lib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Chessgame.Lib.Entities
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int Index { get; set; }
        public colour Color
        {
            get
            {
                colour playerColor;

                if (Index == 0)
                {
                    playerColor = colour.White;
                }
                else
                {
                    playerColor = colour.Black;
                }

                return playerColor;
            } 
        }
        public List<Pawn> PlayerPawns { get; set; }
        public List<Pawn> TakenPawns { get; set; }

        public Player(string name, int index, int score)
        {
            
            Name = name;
            Index = index;
            TakenPawns = new List<Pawn>();
            Score = score;
            PawnService pawnService = new PawnService(Color);
            PlayerPawns = pawnService.Pawns;
        }
    }
}
