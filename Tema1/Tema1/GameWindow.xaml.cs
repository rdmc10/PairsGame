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
using System.Windows.Shapes;

namespace Tema1
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {

        private int NumberOfRows { get; set; }
        private int NumberOfCols { get; set; }
        private ushort LevelNumber { get; set; }
        private string Username { get; set; }
        private string PathToImg { get; set; }
        public GameWindow(string username, string pathToImg)
        {
            Username = username;
            InitializeComponent();
            LabelUsername.Content = Username;
            PathToImg = pathToImg;
            LevelNumber = 1;
            NumberOfRows = 5;
            NumberOfCols = 5;
            UserImageHolder.Source = new BitmapImage(new Uri(PathToImg, UriKind.Absolute));
        }
        private void NewGameClick(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < NumberOfRows; i++)
            {
                matrixGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                matrixGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }
            for (int row = 0; row < NumberOfRows; row++)
            {
                for (int col = 0; col < NumberOfCols; col++)
                {
                    Button button = new Button();
                    button.Content = new Image
                    {
                        Source = new BitmapImage(new Uri(@"Images/black.jpg", UriKind.Relative)) ,
                        VerticalAlignment = VerticalAlignment.Center,
                        Stretch = Stretch.Fill,
                        Height = 256,
                        Width = 256
                    };
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, col);
                    matrixGrid.Children.Add(button);
                }
            }
        }

        private void RandomAssingPhoto()
        {

        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {

        }
        private void ExitClick(object sender, RoutedEventArgs e)
        {

        }
        private void OptionStandard(object sender, RoutedEventArgs e)
        {

        }
        private void SaveGameClick(object sender, RoutedEventArgs e)
        {

        }
        private void StatisticsClick(object sender, RoutedEventArgs e)
        {

        }

        private void OptionCustom(object sender, RoutedEventArgs e)
        {

        }

        private void OpenGameClick(object sender, RoutedEventArgs e)
        {

        }

    }
}
