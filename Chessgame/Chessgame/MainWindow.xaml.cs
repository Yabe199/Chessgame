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

        void CreateChessBoard()
        {
            int totalIndex = 0;

            for (int rowIndex = 0; rowIndex < 8; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < 8; columnIndex++)
                {
                    string rectangleName;
                    Rectangle rectangle = new Rectangle();

                    rectangleName = $"{(char)(65 + columnIndex)}{rowIndex + 1}";
                    totalIndex++;

                    rectangle.Name = rectangleName;
                    rectangle.AllowDrop = true;
                    if ((columnIndex + rowIndex)%2 == 0)
                    {
                        rectangle.Fill = Brushes.Black;
                    }
                    else
                    {
                        rectangle.Fill = Brushes.LightGray;
                    }

                    grdChessBoard.Children.Add(rectangle);
                    Grid.SetColumn(rectangle, columnIndex);
                    Grid.SetRow(rectangle, rowIndex);
                    rectangle.Drop += BoardPosition_Drop;
                    rectangle.MouseMove += BoardPosition_MouseMove;
                }
            }
        }

        private void BoardPosition_Drop(object sender, DragEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;

            if (rectangle != null)
            {
                if (e.Data.GetDataPresent(DataFormats.StringFormat))
                {
                    string dataString = (string)e.Data.GetData(DataFormats.StringFormat);
                    BrushConverter converter = new BrushConverter();

                    if (converter.IsValid(dataString))
                    {
                        Brush newFill = (Brush)converter.ConvertFromString(dataString);
                        rectangle.Fill = newFill;
                    }
                }
            }
        }

        private void BoardPosition_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;

            if (rectangle != null && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(rectangle,
                                     rectangle.Fill.ToString(),
                                     DragDropEffects.Move);
            }
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
            CreateChessBoard();
        }
    }
}
