using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Players.Lib.Entities;
using Players.Lib.Services;

namespace Chessgame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PlayerService playerService;
        Label[,] BoardSquares;
        
        #region ChessboardGrid

        private void CreateChessboard()
        {
            Grid Chessgrid = CreateGrid();
            BoardSquares = CreateLabels();
            AddLabelsToGrid(BoardSquares, Chessgrid);
            grdChessGame.Children.Add(Chessgrid);
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
                string rowName = "row" + (i + 1).ToString();
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

                    label.Drop += Pawn_Drop;
                    label.MouseMove += Pawn_MouseMove;

                    Labels[x, y] = label;
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

        Label previousLabel;

        public void Pawn_Drop(object sender, DragEventArgs e)
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

        public void Pawn_MouseMove(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;

            previousLabel = label;

            if (label != null && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(label, label.Content.ToString(), DragDropEffects.Move);
            }
        }

        #endregion  

        public MainWindow()
        {
            InitializeComponent();
            playerService = new PlayerService();
        }

        void SwitchToGrid(Grid gridToShow, Grid gridToHide)
        {
            gridToHide.Visibility = Visibility.Hidden;
            gridToShow.Visibility = Visibility.Visible;
        }

        void CenterGrid(Grid gridToCenter)
        {
            gridToCenter.HorizontalAlignment = HorizontalAlignment.Center;
            gridToCenter.VerticalAlignment = VerticalAlignment.Center;
            gridToCenter.Margin = new Thickness(0);
        }

        private void WdwChessgame_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            CenterGrid(grdStartUp);
            CenterGrid(grdChessGame);
            SwitchToGrid(grdStartUp, grdChessGame);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Player playerOne, playerTwo;
            string PlayerOneName = txtPlayerOne.Text,
                   PlyerTwoName = txtPlayerTwo.Text;

            playerOne = new Player(PlayerOneName, 0, 0);
            playerTwo = new Player(PlyerTwoName, 1, 0);

            playerService.AddPlayer(playerOne);
            playerService.AddPlayer(playerTwo);

            lblPlayerOne.Content = playerOne.Name;
            lblPlayerTwo.Content = playerTwo.Name;

            SwitchToGrid(grdChessGame, grdStartUp);
            CreateChessboard();
        }
    }
}
