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

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            grdStartup.Visibility = Visibility.Hidden;
            grdGameBoard.Visibility = Visibility.Visible;
        }

        private void WdwChessgame_Loaded(object sender, RoutedEventArgs e)
        {
            grdGameBoard.Visibility = Visibility.Hidden;
 
        }
    }
}
