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
        Label labelToMoveTo = null;
        Pawn selectedPawn;
        bool pawnSelected = false;
        string currentMoveDescription = string.Empty;
        int[] newPosition = new int[2];
        int[] oldPosition = new int[2];


        Pawn testpawnWhite0 = new Pawn(0, "White");
        Pawn testpawnWhite1 = new Pawn(1, "White");
        Pawn testpawnWhite2 = new Pawn(2, "White");
        Pawn testpawnWhite3 = new Pawn(3, "White");
        Pawn testpawnWhite4 = new Pawn(4, "White");
        Pawn testpawnWhite5 = new Pawn(5, "White");
        Pawn testpawnBlack0 = new Pawn(0, "Black");
        Pawn testpawnBlack1 = new Pawn(1, "Black");
        Pawn testpawnBlack2 = new Pawn(2, "Black");
        Pawn testpawnBlack3 = new Pawn(3, "Black");
        Pawn testpawnBlack4 = new Pawn(4, "Black");
        Pawn testpawnBlack5 = new Pawn(5, "Black");


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

        private bool CheckNewPosition()
        {
            bool permitted = false;

            if (labelToMoveTo.Content == null || selectedPawn.pawnColour != activePlayer.Color || labelToMoveTo.Content.GetType() == typeof(Pawn))
            {
                permitted = true;
            }

            return permitted;
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
                        Foreground = Brushes.Aqua

                    };

                    if (y == 1)
                    {
                        label.Content = testpawnWhite0;
                    }
                    else if (label.Name == "A1" || label.Name == "H1")
                    {
                        label.Content = testpawnWhite4;
                    }
                    else if (label.Name == "B1" || label.Name == "G1")
                    {
                        label.Content = testpawnWhite3;
                    }
                    else if (label.Name == "C1" || label.Name == "F1")
                    {
                        label.Content = testpawnBlack5;
                    }
                    else if (label.Name == "D1")
                    {
                        label.Content = testpawnWhite2;
                    }
                    else if (label.Name == "E1")
                    {
                        label.Content = testpawnWhite1;
                    }
                    else if (y == 6)
                    {
                        label.Content = testpawnBlack0;
                    }
                    else if (label.Name == "A8" || label.Name == "H8")
                    {
                        label.Content = testpawnBlack4;
                    }
                    else if (label.Name == "B8" || label.Name == "G8")
                    {
                        label.Content = testpawnBlack3;
                    }
                    else if (label.Name == "C8" || label.Name == "F8")
                    {
                        label.Content = testpawnBlack5;
                    }
                    else if (label.Name == "D8")
                    {
                        label.Content = testpawnBlack2;
                    }
                    else if (label.Name == "E8")
                    {
                        label.Content = testpawnBlack1;
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

        private void SetPawns()
        {

            int PlayerOneColour = (int)Colour.White;
            int PlayerTwoColour = (int)Colour.Black;

            //player one pieces
            Pawn player1Pawn1 = new Pawn(0, PlayerOneColour);
            Pawn player1Pawn2 = new Pawn(0, PlayerOneColour);
            Pawn player1Pawn3 = new Pawn(0, PlayerOneColour);
            Pawn player1Pawn4 = new Pawn(0, PlayerOneColour);
            Pawn player1Pawn5 = new Pawn(0, PlayerOneColour);
            Pawn player1Pawn6 = new Pawn(0, PlayerOneColour);
            Pawn player1Pawn7 = new Pawn(0, PlayerOneColour);
            Pawn player1Pawn8 = new Pawn(0, PlayerOneColour);

            Pawn player1King = new Pawn(1, PlayerOneColour);
            Pawn player1Queen = new Pawn(2, PlayerOneColour);
            Pawn player1Horse1 = new Pawn(3, PlayerOneColour);
            Pawn player1Horse2 = new Pawn(3, PlayerOneColour);
            Pawn player1Tower1 = new Pawn(4, PlayerOneColour);
            Pawn player1Tower2 = new Pawn(4, PlayerOneColour);
            Pawn player1Bishop1 = new Pawn(5, PlayerOneColour);
            Pawn player1Bishop2 = new Pawn(5, PlayerOneColour);

            BoardSquares[0, 1].Content = player1Pawn1.ToString();
            BoardSquares[1, 1].Content = player1Pawn2.ToString();
            BoardSquares[2, 1].Content = player1Pawn3.ToString();
            BoardSquares[3, 1].Content = player1Pawn4.ToString();
            BoardSquares[4, 1].Content = player1Pawn5.ToString();
            BoardSquares[5, 1].Content = player1Pawn6.ToString();
            BoardSquares[6, 1].Content = player1Pawn7.ToString();
            BoardSquares[7, 1].Content = player1Pawn8.ToString();

            BoardSquares[0, 0].Content = player1Tower1.ToString();
            BoardSquares[1, 0].Content = player1Horse1.ToString();
            BoardSquares[2, 0].Content = player1Bishop1.ToString();
            BoardSquares[3, 0].Content = player1King.ToString();
            BoardSquares[4, 0].Content = player1Queen.ToString();
            BoardSquares[5, 0].Content = player1Horse2.ToString();
            BoardSquares[6, 0].Content = player1Bishop2.ToString();
            BoardSquares[7, 0].Content = player1Tower2.ToString();

            //player two pieces
            Pawn player2Pawn1 = new Pawn(0, PlayerTwoColour);
            Pawn player2Pawn2 = new Pawn(0, PlayerTwoColour);
            Pawn player2Pawn3 = new Pawn(0, PlayerTwoColour);
            Pawn player2Pawn4 = new Pawn(0, PlayerTwoColour);
            Pawn player2Pawn5 = new Pawn(0, PlayerTwoColour);
            Pawn player2Pawn6 = new Pawn(0, PlayerTwoColour);
            Pawn player2Pawn7 = new Pawn(0, PlayerTwoColour);
            Pawn player2Pawn8 = new Pawn(0, PlayerTwoColour);

            Pawn player2King = new Pawn(1, PlayerTwoColour);
            Pawn player2Queen = new Pawn(2, PlayerTwoColour);
            Pawn player2Horse1 = new Pawn(3, PlayerTwoColour);
            Pawn player2Horse2 = new Pawn(3, PlayerTwoColour);
            Pawn player2Tower1 = new Pawn(4, PlayerTwoColour);
            Pawn player2Tower2 = new Pawn(4, PlayerTwoColour);
            Pawn player2Bishop1 = new Pawn(5, PlayerTwoColour);
            Pawn player2Bishop2 = new Pawn(5, PlayerTwoColour);

            BoardSquares[0, 6].Content = player2Pawn1.ToString();
            BoardSquares[1, 6].Content = player2Pawn2.ToString();
            BoardSquares[2, 6].Content = player2Pawn3.ToString();
            BoardSquares[3, 6].Content = player2Pawn4.ToString();
            BoardSquares[4, 6].Content = player2Pawn5.ToString();
            BoardSquares[5, 6].Content = player2Pawn6.ToString();
            BoardSquares[6, 6].Content = player2Pawn7.ToString();
            BoardSquares[7, 6].Content = player2Pawn8.ToString();

            BoardSquares[0, 7].Content = player2Tower1.ToString();
            BoardSquares[1, 7].Content = player2Horse1.ToString();
            BoardSquares[2, 7].Content = player2Bishop1.ToString();
            BoardSquares[3, 7].Content = player2King.ToString();
            BoardSquares[4, 7].Content = player2Queen.ToString();
            BoardSquares[5, 7].Content = player2Horse2.ToString();
            BoardSquares[6, 7].Content = player2Bishop2.ToString();
            BoardSquares[7, 7].Content = player2Tower2.ToString();

        }
        #endregion  


        public MainWindow()
        {
            InitializeComponent();
            playerService = new PlayerService();
        }

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

        private void TakePawn()
        {
            if (labelToMoveTo.Content.GetType() == typeof(Pawn))
            {
                Pawn aPawn = (Pawn)labelToMoveTo.Content;
                Label stolenPawn = new Label
                {
                    Content = aPawn
                };

                if (activePlayer.Index == 0)
                {
                    chessPlayers.Players[0].TakenPawns.Add(aPawn);
                    grdPlayerOne.Children.Add(stolenPawn);
                }
                else
                {
                    chessPlayers.Players[1].TakenPawns.Add(aPawn);
                    grdPlayerTwo.Children.Add(stolenPawn);
                }
            }
        }

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
            SetPawns();
        }

        private void BtnBackToMenu_Click_1(object sender, RoutedEventArgs e)
        {

            SwitchToGrid(grdStartUp, grdChessGame);

        }

        private void Pawn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
                labelToMoveTo = label;
                newPosition[0] = Grid.GetColumn(label);
                newPosition[1] = Grid.GetRow(label);
                label.BorderBrush = Brushes.Green;
                label.BorderThickness = new Thickness(3);
                lblCurrentMove.Content = ManageCurrentMoveDescription(activePlayer.Name, labelToMove, label, selectedPawn);
                grdChessboard.IsEnabled = false;
            }
        }

        private void VerifyMove()
        {

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool canMoveHere = CheckNewPosition();

            VerifyMove();
            if (canMoveHere)
            {
                if (labelToMoveTo.Content != null)
                {
                    TakePawn();
                }
                ExecuteMove();
                ResetGridSelections();
                ResetValues();
                ChangeActivePlayer();

                lblCurrentMove.Content = ManageCurrentMoveDescription(activePlayer.Name, null, null, null);
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetGridSelections();
            ResetValues();

            lblCurrentMove.Content = ManageCurrentMoveDescription(activePlayer.Name, null, null, null);
        }
    }

}
