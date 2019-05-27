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

namespace Chessgame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
            SwitchToGrid(grdChessGame, grdStartUp);
        }

        private void btnChangePosition_Click(object sender, RoutedEventArgs e)
        {
            int Column = int.Parse(txtColumn.Text),
                Row = int.Parse(txtRow.Text);
            Grid.SetColumn(testtest, Column -1);
            Grid.SetRow(testtest, Row -1);
        }

        private void SetPawns()
        {

            string PlayerOneColour = "white";
            string PlayerTwoColour = "black";

            //player one pawns
            Pawns player1Pawn1 = new Pawns(0, PlayerOneColour);
            Pawns player1pawn2 = new Pawns(0, PlayerOneColour);
            Pawns player1pawn3 = new Pawns(0, PlayerOneColour);
            Pawns player1pawn4 = new Pawns(0, PlayerOneColour);
            Pawns player1pawn5 = new Pawns(0, PlayerOneColour);
            Pawns player1pawn6 = new Pawns(0, PlayerOneColour);
            Pawns player1pawn7 = new Pawns(0, PlayerOneColour);
            Pawns player1pawn8 = new Pawns(0, PlayerOneColour);

            Pawns player1King = new Pawns(2, PlayerOneColour);
            Pawns player1Queen = new Pawns(3, PlayerOneColour);
                  
            //player two pawns

            Pawns player2pawn1 = new Pawns(0, PlayerTwoColour);
            Pawns player2pawn2 = new Pawns(0, PlayerTwoColour);
            Pawns player2pawn3 = new Pawns(0, PlayerTwoColour);
            Pawns player2pawn4 = new Pawns(0, PlayerTwoColour);
            Pawns player2pawn5 = new Pawns(0, PlayerTwoColour);
            Pawns player2pawn6 = new Pawns(0, PlayerTwoColour);
            Pawns player2pawn7 = new Pawns(0, PlayerTwoColour);
            Pawns player2pawn8 = new Pawns(0, PlayerTwoColour);
                  



        }
    }
}
