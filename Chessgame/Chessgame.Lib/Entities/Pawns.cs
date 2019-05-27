using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chessgame.Lib.Entities
{

    public enum type
    {
        Pawn, King, Queen, Horse, Tower, Bishop
    }
    public enum colour
    {
        Black, White
    }
    public class Pawns
    {
        #region properties
        public bool turn { get; set; }
        public colour pawnColour { get; set; }
        public type pawnType { get; set; }
        //public PawnMove Moveset { get; set; }

        #endregion

        #region Constructor
        public Pawns(int pawnTypeParam, string Colour)
        {
            turn = false;

            //set colour of pawn
            if (Colour == "Black")
            {
                pawnColour = colour.Black;
            }
            else if (Colour == "White")
            {
                pawnColour = colour.White;
            }
            else
            {
                MessageBox.Show("you only have black and white pawns in chess...");
            }

            //set pawnType
            if (pawnTypeParam == 0)
            {
                pawnType = type.Pawn;
            }
            else if (pawnTypeParam == 1)
            {
                pawnType = type.King;
            }
            else if (pawnTypeParam == 2)
            {
                pawnType = type.Queen;
            }
            else if (pawnTypeParam == 3)
            {
                pawnType = type.Horse;
            }
            else if (pawnTypeParam == 4)
            {
                pawnType = type.Tower;
            }
            else if (pawnTypeParam == 1)
            {
                pawnType = type.Bishop;
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

