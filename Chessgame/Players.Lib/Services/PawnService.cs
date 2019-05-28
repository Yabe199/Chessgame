using Chessgame.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawns.Lib.Services
{
    public class PawnService
    {
        public List<Pawn> Pawns { get; set; }

        public PawnService()
        {
            Pawns = new List<Pawn>();
        }
    }
}
