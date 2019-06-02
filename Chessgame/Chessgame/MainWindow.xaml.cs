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
using Chessgame.Lib.Entities;
using Chessgame.Lib.Services;


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
        Pawn selectedPawn;
        bool pawnSelected = false;
        string currentMoveDescription = string.Empty;
        int[] newPosition = new int[2];
        int[] oldPosition = new int[2];


        Pawn testpawn1 = new Pawn(1, "White");
        Pawn testpawn2 = new Pawn(1, "Black");


        private string ManageCurrentMoveDescription(string playerName, Label moveOrigin, Label moveDestination, Pawn thisPawn)
        {
            string description = string.Empty;
            
            if (moveOrigin == null && moveDestination == null)
            {
                description = $"{playerName} is up.";
            }
            else if (moveOrigin != null && moveDestination == null)
            {
                description = $"{playerName} moves {thisPawn.pawnType.ToString()} from {moveOrigin.Name} to ...";
            }
            else
            {
                description = $"{playerName} moves {thisPawn.pawnType.ToString()} from {moveOrigin.Name} to {moveDestination.Name}.";
            }

            return description;
        }

        private string scoreString(int playerScore)
        {
            string scoreInfo;

            scoreInfo = $"Score: {playerScore}";

            return scoreInfo;
        }

        private bool CheckSelectedField(colour PlayerColour, Label selectedLabel)
        {
            bool selection = false;

            if (selectedLabel.Content != null)
            {
                if (selectedLabel.Content.GetType() == typeof(Pawn))
                {
                    Pawn selectedPawn = (Pawn)selectedLabel.Content;
                    if (selectedPawn.pawnColour == PlayerColour)
                    {
                        selection = true;
                    }
                }
            }

            return selection;
        }

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
                        Foreground = Brushes.Gray
                    };

                    if (y == 1)
                    {
                        label.Content = "Pawn White";
                    }
                    else if (label.Name == "A1" || label.Name == "H1")
                    {
                        label.Content = "Rook White";
                    }
                    else if (label.Name == "B1" || label.Name == "G1")
                    {
                        label.Content = "Knight White";
                    }
                    else if (label.Name == "C1" || label.Name == "F1")
                    {
                        label.Content = "Bishop White";
                    }
                    else if (label.Name == "D1")
                    {
                        label.Content = "Queen White";
                    }
                    else if (label.Name == "E1")
                    {
                        label.Content = testpawn1;
                    }
                    else if (y == 6)
                    {
                        label.Content = "Pawn Black";
                    }
                    else if (label.Name == "A8" || label.Name == "H8")
                    {
                        label.Content = "Rook Black";
                    }
                    else if (label.Name == "B8" || label.Name == "G8")
                    {
                        label.Content = "Knight Black";
                    }
                    else if (label.Name == "C8" || label.Name == "F8")
                    {
                        label.Content = "Bishop Black";
                    }
                    else if (label.Name == "D8")
                    {
                        label.Content = "Queen Black";
                    }
                    else if (label.Name == "E8")
                    {
                        label.Content = testpawn2;
                    }


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
                    Grid.SetRow(labels[x, y], labels.GetLength(1) - y - 1);
                }
            }
        }

        void ResetGridSelections()
        {
            grdChessboard.IsEnabled = true;

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

        #region Gameplay

        void ExecuteMove()
        {
            foreach (Control child in grdChessboard.Children)
            {
                Label currentLabel = (Label)child;
                int[] currentPosition = { Grid.GetColumn(child), Grid.GetRow(child) };

                if (oldPosition[0] == currentPosition[0] && oldPosition[1] == currentPosition[1])
                {
                    currentLabel.Content = null;
                }

                if (newPosition[0] == currentPosition[0] && newPosition[1] == currentPosition[1])
                {
                    currentLabel.Content = selectedPawn;
                }
            }
        }

        void ResetValues()
        {
            oldPosition = new int[2];
            newPosition = new int[2];
            labelToMove = null;
            labelToMove = null;
            pawnSelected = false;
            selectedPawn = null;
        }

        private void ChangeActivePlayer()
        {
            if (activePlayer.Index == 0)
            {
                activePlayer = chessPlayers.Players[1];
            }
            else
            {
                activePlayer = chessPlayers.Players[0];
            }
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

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Player playerOne, playerTwo;
            string playerOneName = txtPlayerOne.Text.Trim(),
                   playerTwoName = txtPlayerTwo.Text.Trim();

            playerOne = new Player(playerOneName, 0, 0);
            playerTwo = new Player(playerTwoName, 1, 0);
            activePlayer = playerOne;
            currentMoveDescription = ManageCurrentMoveDescription(activePlayer.Name, labelToMove, null, null);

            chessPlayers.AddPlayer(playerOne);
            chessPlayers.AddPlayer(playerTwo);

            lblPlayerOne.Content = playerOne.Name;
            lblPlayerTwo.Content = playerTwo.Name;
            lblScorePlayerOne.Content = scoreString(playerOne.Score);
            lblScorePlayerTwo.Content = scoreString(playerTwo.Score);
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
                pawnSelected = CheckSelectedField(activePlayer.Color, label);

                if (pawnSelected)
                {
                    selectedPawn = (Pawn)label.Content;
                    labelToMove = label;
                    oldPosition[0] = Grid.GetColumn(label);
                    oldPosition[1] = Grid.GetRow(label);
                    label.BorderBrush = Brushes.Red;
                    label.BorderThickness = new Thickness(3);
                    lblCurrentMove.Content = ManageCurrentMoveDescription(activePlayer.Name, labelToMove, null, selectedPawn);
                }
            }
            else
            {
                newPosition[0] = Grid.GetColumn(label);
                newPosition[1] = Grid.GetRow(label);
                label.BorderBrush = Brushes.Green;
                label.BorderThickness = new Thickness(3);
                lblCurrentMove.Content = ManageCurrentMoveDescription(activePlayer.Name, labelToMove, label, selectedPawn);
                grdChessboard.IsEnabled = false;
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            ExecuteMove();
            ResetGridSelections();
            ResetValues();
            ChangeActivePlayer();

            lblCurrentMove.Content = ManageCurrentMoveDescription(activePlayer.Name, null, null, null);
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetGridSelections();
            ResetValues();

            lblCurrentMove.Content = ManageCurrentMoveDescription(activePlayer.Name, null, null, null);
        }
    }

}
