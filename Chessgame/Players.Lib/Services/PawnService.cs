using Pawns.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawns.Lib.Services
{
    public class PawnsService
    {
        public List<Pawn> Pawns { get; set; }

        public PawnsService()
        {
            Pawns = new List<Pawn>();
        }
    }
}
