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
        PlayerService chessPlayers;
        Player activePlayer;
        Label labelToMove = null;
        bool pawnSelected = false;
        string currentMoveDescription = string.Empty;

        #region ChessboardGrid

        private void CreateChessboard()
        {
            Label[,] BoardSquares = CreateLabels();
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
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Content = labelName
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

        void ResetGridSelections()
        {
            foreach (Control child in grdChessboard.Children)
            {
                child.Margin = new Thickness(0);
                child.BorderBrush = null;
                lblCurrentMove.Content = string.Empty;
            }
        }

        #endregion

        #region ScreenControl

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

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            chessPlayers = new PlayerService();
        }
        
        private void WdwChessgame_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            CenterGrid(grdStartUp);
            CenterGrid(grdChessGame);
            SwitchToGrid(grdStartUp, grdChessGame);
        }

        string ManageCurrentMoveDescription(string playerName, Label moveOrigin, Label moveDestination)
        {
            string description = string.Empty;

            if (moveOrigin == null && moveDestination == null)
            {
                description = $"{playerName} is aan de beurt.";
            }
            else if (moveOrigin != null && moveDestination == null)
            {
                description = $"{playerName} zet {moveOrigin.Content.ToString()} van {moveOrigin.Name} naar ...";
            }
            else
            {
                description = $"{playerName} zet {moveOrigin.Content.ToString()} van {moveOrigin.Name} naar {moveDestination.Name}.";
            }

            return description;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Player playerOne, playerTwo;
            string playerOneName = txtPlayerOne.Text.Trim(),
                   playerTwoName = txtPlayerTwo.Text.Trim();

            playerOne = new Player(playerOneName, 0, 0);
            playerTwo = new Player(playerTwoName, 1, 0);
            activePlayer = playerOne;
            currentMoveDescription = ManageCurrentMoveDescription(activePlayer.Name, labelToMove, null);

            chessPlayers.AddPlayer(playerOne);
            chessPlayers.AddPlayer(playerTwo);

            lblPlayerOne.Content = playerOne.Name;
            lblPlayerTwo.Content = playerTwo.Name;
            lblCurrentMove.Content = currentMoveDescription;

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

        public void Pawn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Label label = sender as Label;

            if (pawnSelected == false)
            {
                pawnSelected = true;
                labelToMove = label;
                label.BorderBrush = Brushes.Red;
                label.BorderThickness = new Thickness(3);
                lblCurrentMove.Content = ManageCurrentMoveDescription(activePlayer.Name, labelToMove, null);
            }
            else
            {
                label.BorderBrush = Brushes.Green;
                label.BorderThickness = new Thickness(3);
                lblCurrentMove.Content = ManageCurrentMoveDescription(activePlayer.Name, labelToMove, label);
                grdChessboard.IsEnabled = false;
            }

        //    if (selectedLabel == null)
        //    {

        //    }
        //    else
        //    {
        //        int[] cord = { Grid.GetColumn(selectedLabel), Grid.GetRow(selectedLabel), Grid.GetColumn(label), Grid.GetRow(label) };
        //        Grid.SetColumn(label, cord[0]);
        //        Grid.SetRow(label, cord[1]);
        //        Grid.SetColumn(selectedLabel, cord[2]);
        //        Grid.SetRow(selectedLabel, cord[3]);
        //        label.BorderBrush = null;
        //        label.BorderThickness = new Thickness(0);
        //        lblPlayerOne.Focus();
        //        selectedLabel = null;
        //    }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            labelToMove = null;
            pawnSelected = false;
            
            ResetGridSelections();
            grdChessboard.IsEnabled = true;
            lblCurrentMove.Content = ManageCurrentMoveDescription(activePlayer.Name, null, null);
        }
    }

}
