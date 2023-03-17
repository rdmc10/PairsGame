using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        private Dictionary<Tuple<ushort,ushort>,string> assignedPhoto { get; set; }
        private List<ushort> allPhotos { get; set; }
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
            assignedPhoto = new Dictionary<Tuple<ushort,ushort>,string>();
            allPhotos= new List<ushort>();
            for(ushort i = 1; i < 10; i++)
            {
                allPhotos.Add(i);
            }
            for(ushort row = 0; row < NumberOfRows; row++)
            {
                for(ushort col = 0; col < NumberOfCols; col++)
                {
                    Tuple <ushort,ushort> pos = new Tuple<ushort,ushort>(row, col);
                    assignedPhoto[pos] = "0";
                }
            }
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
                        Source = new BitmapImage(new Uri(@"Images/0.jpg", UriKind.Relative)) ,
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
            RandomAssingPhoto();
        }

        private void RandomAssingPhoto()
        {
            Random rnd = new Random();
            foreach(Button btn in matrixGrid.Children)
            {
                int randomIndex = rnd.Next(1, allPhotos.Count);
                ushort randomKey = allPhotos[randomIndex];

                ushort row = (ushort)Grid.GetRow(btn);
                ushort col = (ushort)Grid.GetColumn(btn);
                Tuple<ushort, ushort> pos = new Tuple<ushort, ushort>(row, col);
                List<Tuple<ushort,ushort>> falseKeys = assignedPhoto.Where(pair => pair.Value == 0.ToString()).Select(pair => pair.Key).ToList();
                if(assignedPhoto[pos] == "0")
                {
                    assignedPhoto[pos] = randomKey.ToString();
                    //TODO: Assign another unsigned button the same key;
                    //List<Tuple<ushort,ushort>> falseKeys = assignedPhoto.Where(pair => pair.Value == 0).Select(pair => pair.Key).ToList();
                    int rndNextIndex = rnd.Next(0, falseKeys.Count);
                    Console.WriteLine(rndNextIndex);
                    if(falseKeys.Count > 0)
                    {
                        Tuple<ushort,ushort> randomNextPos = falseKeys[rndNextIndex];
                        assignedPhoto[randomNextPos] = randomKey.ToString();
                    }
                }
            }
            foreach(Button btn in matrixGrid.Children)
            {
                ushort row = (ushort)Grid.GetRow(btn);
                ushort col = (ushort)Grid.GetColumn(btn);
                Tuple<ushort, ushort> pos = new Tuple<ushort, ushort>(row, col);
                btn.Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"Images/" + assignedPhoto[pos] +".jpg", UriKind.Relative)) ,
                    VerticalAlignment = VerticalAlignment.Center,
                    Stretch = Stretch.Fill,
                    Height = 256,
                    Width = 256
                };
            }
        }

        private void SaveGameClick(object sender, RoutedEventArgs e)
        {
            string filepath = @"Saves/" + Username + ".txt";
            using(var sw = new StreamWriter(filepath, true))
            {
                foreach(Button btn in matrixGrid.Children)
                {
                    Image buttonImage = btn.Content as Image;
                    BitmapSource bitmapSource = buttonImage.Source as BitmapSource;
                    string imagePath = (bitmapSource!= null) ? bitmapSource.ToString() : null;
                    sw.WriteLine(imagePath);
                }
            }
        }
        private void OpenGameClick(object sender, RoutedEventArgs e)
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
        private void StatisticsClick(object sender, RoutedEventArgs e)
        {

        }

        private void OptionCustom(object sender, RoutedEventArgs e)
        {

        }


    }
}
