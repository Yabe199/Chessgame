using Players.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chessgame.Lib.Entities
{

    public class Pawn
    {
        #region properties
        public bool turn { get; set; }
        public Colour pawnColour { get; set; }
        public PawnType pawnType { get; set; }
        //public PawnMove Moveset { get; set; }

        #endregion

        #region Constructor
        public Pawn(int pawnTypeParam, string kleur)
        {
            turn = false;

            //set colour of pawn
            if (kleur == "Black")
            {
                pawnColour = Colour.Black;
            }
            else if (kleur == "White")
            {
                pawnColour = Colour.White;
            }
            else
            {
                MessageBox.Show("you only have black and white pawns in chess...");
            }

            //set pawnType
            if (pawnTypeParam == 0)
            {
                pawnType = PawnType.Pawn;
            }
            else if (pawnTypeParam == 1)
            {
                pawnType = PawnType.King;
            }
            else if (pawnTypeParam == 2)
            {
                pawnType = PawnType.Queen;
            }
            else if (pawnTypeParam == 3)
            {
                pawnType = PawnType.Horse;
            }
            else if (pawnTypeParam == 4)
            {
                pawnType = PawnType.Tower;
            }
            else if (pawnTypeParam == 1)
            {
                pawnType = PawnType.Bishop;
            }


            //switch (pawnType)
            //{
            //    case type.Pawn:
            //        Moveset = PawnMove.Pawn;
            //        break;
            //    case type.King:
            //        Moveset = PawnMove.King;
            //        break;
            //    case type.Queen:
            //        Moveset = PawnMove.Queen;
            //        break;
            //    case type.Horse:
            //        Moveset = PawnMove.Horse;
            //        break;
            //    case type.Tower:
            //        Moveset = PawnMove.Tower;
            //        break;
            //    case type.Bishop:
            //        Moveset = PawnMove.Bishop;
            //        break;
            //    default:
            //        break;
            //}
            #endregion


        }
    }
}


