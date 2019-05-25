﻿using System;
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
        BoardService ChessBoard;

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
            ChessBoard = new BoardService();
            grdChessGame.Children.Add(ChessBoard.Chessboard.ChessBoard);
        }
    }
}
