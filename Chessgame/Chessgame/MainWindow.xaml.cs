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
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Foreground = Brushes.Aqua
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
            SetPawns();
        }

        private void BtnBackToMenu_Click_1(object sender, RoutedEventArgs e)
        {
            SwitchToGrid(grdStartUp, grdChessGame);
        }
    }

}
