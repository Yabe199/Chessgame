using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            Grid grid = new Grid
            {
                Name = "grdChessboard",
                Width = 600,
                Height = 600,
                Margin = new Thickness(0),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

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

                    Label label = new Label
                    {
                        Name = labelName,
                        Width = 75,
                        Height = 75,
                        Margin = new Thickness(0),
                        Content = labelName,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        AllowDrop = true
                    };
                    
                    label.Drop += BoardPosition_Drop;
                    label.MouseMove += BoardPosition_MouseMove;

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

        #region Eventhandlers

        Label previousLabel;

        private void BoardPosition_Drop(object sender, DragEventArgs e)
        {
            Label label = sender as Label;

            previousLabel.Content = label.Content;

            if (label != null)
            {
                if (e.Data.GetDataPresent(DataFormats.StringFormat))
                {
                    string dataString = (string)e.Data.GetData(DataFormats.StringFormat);
                    label.Content = dataString;

                }
            }
        }

        private void BoardPosition_MouseMove(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;

            previousLabel = label;

            if (label != null && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(label, label.Content.ToString(), DragDropEffects.Move);
            }
        }

        #endregion

    }
}
