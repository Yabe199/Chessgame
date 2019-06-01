using Chessgame.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Chessgame.Lib.Services
{
    public class PawnService
    {
        public List<Pawn> Pawns { get; set; }

        public PawnService(colour color)
        {
            Pawns = CreateSet();
        }

        private List<Pawn> CreateSet()
        {
            List<Pawn> currentPawnSet = new List<Pawn>();

            return currentPawnSet;
        }
    }
}
