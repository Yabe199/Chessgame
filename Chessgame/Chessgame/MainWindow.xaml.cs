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
        
        private void WdwChessgame_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            grdChessGame.Visibility = Visibility.Hidden;
            grdStartUp.HorizontalAlignment = HorizontalAlignment.Center;
            grdStartUp.VerticalAlignment = VerticalAlignment.Center;
            grdStartUp.Margin = new Thickness(0);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            grdStartup.Visibility = Visibility.Hidden;
            grdChessGame.Visibility = Visibility.Visible;
            grdChessGame.HorizontalAlignment = HorizontalAlignment.Center;
            grdChessGame.VerticalAlignment = VerticalAlignment.Center;
            grdChessGame.Margin = new Thickness(0);
        }
    }
}
