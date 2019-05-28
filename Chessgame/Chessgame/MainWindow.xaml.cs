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
using Chessgame.Lib.Entities;


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
            BoardSquares = CreateLabels();
            AddLabelsToGrid(BoardSquares);
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
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center
                    };

                    label.MouseLeftButtonDown += Pawn_MouseLeftButtonDown;

                    Labels[x, y] = label;
                }
            }

            return Labels;
        }

        private void AddLabelsToGrid(Label[,] labels)
        {
            for (int x = 0; x < labels.GetLength(0); x++)
            {
                for (int y = 0; y < labels.GetLength(1); y++)
                {
                    grdChessboard.Children.Add(labels[x, y]);
                    Grid.SetColumn(labels[x, y], x);
                    Grid.SetRow(labels[x, y], y);
                }
            }
        }

        Label selectedLabel;
        public void Pawn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Label label = sender as Label;

            if (selectedLabel == null)
            {
                selectedLabel = label;
                label.BorderBrush = Brushes.Red;
                label.BorderThickness = new Thickness(3);
                lblCurrentMove.Content = $"Move {label.Name.ToString()} from {label.Name.ToString()}";

            }
            else
            {
                int[] cord = { Grid.GetColumn(selectedLabel), Grid.GetRow(selectedLabel), Grid.GetColumn(label), Grid.GetRow(label) };
                Grid.SetColumn(label, cord[0]);
                Grid.SetRow(label, cord[1]);
                Grid.SetColumn(selectedLabel, cord[2]);
                Grid.SetRow(selectedLabel, cord[3]);
                label.BorderBrush = null;
                label.BorderThickness = new Thickness(0);
                lblPlayerOne.Focus();
                selectedLabel = null;
                lblCurrentMove.Content += $" to {label.Name.ToString()}";
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

        private void SetPawns()
        {

            string PlayerOneColour = "white";
            string PlayerTwoColour = "black";

            //player one pawns
            Pawn player1Pawn1 = new Pawn(0, PlayerOneColour);
            Pawn player1pawn2 = new Pawn(0, PlayerOneColour);
            Pawn player1pawn3 = new Pawn(0, PlayerOneColour);
            Pawn player1pawn4 = new Pawn(0, PlayerOneColour);
            Pawn player1pawn5 = new Pawn(0, PlayerOneColour);
            Pawn player1pawn6 = new Pawn(0, PlayerOneColour);
            Pawn player1pawn7 = new Pawn(0, PlayerOneColour);
            Pawn player1pawn8 = new Pawn(0, PlayerOneColour);

            Pawn player1King = new Pawn(2, PlayerOneColour);
            Pawn player1Queen = new Pawn(3, PlayerOneColour);

            //player two pawns

            Pawn player2pawn1 = new Pawn(0, PlayerTwoColour);
            Pawn player2pawn2 = new Pawn(0, PlayerTwoColour);
            Pawn player2pawn3 = new Pawn(0, PlayerTwoColour);
            Pawn player2pawn4 = new Pawn(0, PlayerTwoColour);
            Pawn player2pawn5 = new Pawn(0, PlayerTwoColour);
            Pawn player2pawn6 = new Pawn(0, PlayerTwoColour);
            Pawn player2pawn7 = new Pawn(0, PlayerTwoColour);
            Pawn player2pawn8 = new Pawn(0, PlayerTwoColour);
                  



        }
    }

}
