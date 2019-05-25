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
            BoardSquares = CreateLabels();
            AddLabelsToGrid(BoardSquares, ChessBoard);
        }

        private Grid CreateGrid()
        {
            Grid grid = new Grid();
            grid.Name = "grdChessboard";
            grid.Width = 600;
            grid.Height = 600;
            grid.Margin = new Thickness(0);
            grid.HorizontalAlignment = HorizontalAlignment.Center;
            grid.VerticalAlignment = VerticalAlignment.Center;

            for (int i = 0; i < 8; i++)
            {
                string columnName = "column" + ((char)(65 + i)).ToString();
                ColumnDefinition colDef = new ColumnDefinition();
                colDef.Name = columnName;
                grid.ColumnDefinitions.Add(colDef);
            }

            for (int i = 0; i < 8; i++)
            {
                string rowName = "row" + (i+1).ToString();
                RowDefinition rowDef = new RowDefinition();
                rowDef.Name = rowName;
                grid.RowDefinitions.Add(rowDef);
            }
            
            return grid;
        }

        private Label[,] CreateLabels()
        {
            Label[,] Labels = new Label[8, 8];

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    string labelName = ((char)(65 + x)).ToString() + (y + 1);

                    Label label = new Label();
                    label.Name = labelName;
                    label.Width = 75;
                    label.Height = 75;
                    label.Margin = new Thickness(0);
                    label.Content = labelName;
                    label.HorizontalContentAlignment = HorizontalAlignment.Center;
                    label.VerticalContentAlignment = VerticalAlignment.Center;
                    label.AllowDrop = true;

                    Labels[x,y] = label;
                }
            }

            return Labels;
        }

        private void AddLabelsToGrid(Label[,] labels, Grid grid)
        {
            for (int x = 0; x < labels.GetLength(0); x++)
            {
                for (int y = 0; y < labels.GetLength(1); y++)
                {
                    grid.Children.Add(labels[x, y]);
                    Grid.SetColumn(labels[x, y], x);
                    Grid.SetRow(labels[x, y], y);
                }
            }
        }

    }
}
