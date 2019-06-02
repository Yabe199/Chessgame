using Players.Lib.Entities;
using Players.Lib.Services;
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
        public String avatar { get; set; }
        public Point location { get; set; }
        //public PawnMove moveset { get; set; }

        #endregion

        #region Constructor
        public Pawn(int pawnTypeParam, string kleur)
        {
            turn = false;
            avatar = AppDomain.CurrentDomain.BaseDirectory + "/images/";
            //location = new Point(0, 0);
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
                if (kleur == "Black")
                {
                    avatar = "Pawnblack" ;
                }
                avatar += "Pawnwhite";
                //moveset = PawnMove.Pawn;
            }
            else if (pawnTypeParam == 1)
            {
                pawnType = PawnType.King;
                if (kleur == "Black")
                {
                    avatar += "Kingblack";
                }
                avatar += "Kingwhite";
                //moveset = PawnMove.King;
            }
            else if (pawnTypeParam == 2)
            {
                pawnType = PawnType.Queen;
                if (kleur == "Black")
                {
                    avatar += "Queenblack";
                }
                avatar += "Queenwhite";
                //moveset = PawnMove.Queen;
            }
            else if (pawnTypeParam == 3)
            {
                pawnType = PawnType.Horse;
                if (kleur == "Black")
                {
                    avatar += "Horseblack";
                }
                avatar = "Horsewhite";
                //moveset = PawnMove.Horse;
            }
            else if (pawnTypeParam == 4)
            {
                pawnType = PawnType.Tower;
                if (kleur == "Black")
                {
                    avatar += "Towerblack";
                }
                avatar = "Towerwhite";
                //moveset = PawnMove.Tower;
            }
            else if (pawnTypeParam == 1)
            {
                pawnType = PawnType.Bishop;
                if (kleur == "Black")
                {
                    avatar += "Bishopblack";
                }
                avatar += "Bishopwhite";
                //moveset = PawnMove.Bishop;
            }

            #endregion


        }
    }
}


