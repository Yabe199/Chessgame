using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Players.Lib.Entities
{
    public class Board
    {
        public Grid ChessBoard { get; set; }
        public Label[,] BoardSquares { get; set; }

        public Board()
        {
            ChessBoard = CreateGrid();
            BoardSquares = new Label[8, 8];
        }

        private Grid CreateGrid()
        {
            Grid grid = new Grid();
            grid.Name = "grdChessboard";
            grid.Width = 800;
            grid.Height = 800;
            grid.Margin = new Thickness(0);
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;

            for (int i = 0; i < 8; i++)
            {
                string columnName = ((char)(65 + i)).ToString();
                ColumnDefinition colDef = new ColumnDefinition();
                colDef.Name = columnName;
                grid.ColumnDefinitions.Add(colDef);
            }

            for (int i = 0; i < 8; i++)
            {
                string rowName = (i+1).ToString();
                RowDefinition rowDef = new RowDefinition();
                rowDef.Name = rowName;
                grid.RowDefinitions.Add(rowDef);
            }
            
            return grid;
        }
    }
}
