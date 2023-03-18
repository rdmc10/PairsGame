using System;
using System.Collections;
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
        private Dictionary<Tuple<ushort, ushort>, Tuple<ushort, ushort>> assignedPairs { get; set; }
        private Dictionary<Tuple<ushort,ushort>,bool> guessedPhotos { get; set; }
        private List<ushort> allPhotos { get; set; }

        private Tuple<ushort, ushort> firstClick { get; set; }
        private Tuple<ushort, ushort> secondClick { get; set; }

        public GameWindow(string username, string pathToImg)
        {
            Username = username;
            InitializeComponent();
            LabelUsername.Content = Username;
            PathToImg = pathToImg;
            LevelNumber = 1;
            NumberOfRows = 5;
            NumberOfCols = 5;
            allPhotos= new List<ushort>();
            for(ushort i = 1; i <= 10; i++)
            {
                allPhotos.Add(i);
            }
            UserImageHolder.Source = new BitmapImage(new Uri(PathToImg, UriKind.Absolute));
        }

        private void InitializeBoard(int noRows, int noCols)
        {
            assignedPhoto = new Dictionary<Tuple<ushort,ushort>,string>();
            assignedPairs = new Dictionary<Tuple<ushort, ushort>, Tuple<ushort, ushort>>();
            guessedPhotos = new Dictionary<Tuple<ushort, ushort>, bool>();
            NumberOfRows = noRows;
            NumberOfCols = noCols;
            for(ushort row = 0; row < NumberOfRows; row++)
            {
                for(ushort col = 0; col < NumberOfCols; col++)
                {
                    Tuple <ushort,ushort> pos = new Tuple<ushort,ushort>(row, col);
                    guessedPhotos[pos] = false;
                    assignedPhoto[pos] = "0";
                }
            }
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
                    button.Click += Button_Click;
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, col);
                    matrixGrid.Children.Add(button);
                }
            }
            RandomAssingPhoto();
        }
        private void NewGameClick(object sender, RoutedEventArgs e)
        {
            InitializeBoard(NumberOfRows, NumberOfCols);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedBtn = sender as Button;
            if(secondClick != null)
            {
                if (guessedPhotos[firstClick] == false && guessedPhotos[secondClick] == false)
                {
                    ChangeButtonImage(firstClick, "0");
                    ChangeButtonImage(secondClick, "0");
                }
                firstClick = null; secondClick = null;
            }
            if(firstClick == null)
            {
                ushort rowpos = (ushort)Grid.GetRow(clickedBtn);
                ushort colpos = (ushort)Grid.GetColumn(clickedBtn);
                Tuple<ushort, ushort> firstpos= new Tuple<ushort,ushort>(rowpos, colpos);
                firstClick = firstpos;
                if (guessedPhotos[firstClick] == false)
                    ChangeButtonImage(firstpos, assignedPhoto[firstClick]);
            }
            else
            {
                ushort rowpos = (ushort)Grid.GetRow(clickedBtn);
                ushort colpos = (ushort)Grid.GetColumn(clickedBtn);
                Tuple<ushort, ushort> secondpos= new Tuple<ushort,ushort>(rowpos, colpos);
                secondClick = secondpos;
                if (guessedPhotos[secondClick] == false)
                    ChangeButtonImage(secondpos, assignedPhoto[secondClick]);
                //TODO: FIX SAME PHOTO CLICK
                if (assignedPhoto[firstClick] == assignedPhoto[secondClick] )
                {
                    guessedPhotos[firstClick] = true;
                    guessedPhotos[secondClick] = true;
                    ChangeButtonImage(firstClick, "correct");
                    ChangeButtonImage(secondClick, "correct");
                }
            }

            if(CheckIfGameIsWon() == true)
            {
                LabelLevel.Content="YOU WON!";
            }
        }

        private bool CheckIfGameIsWon()
        {
            foreach(KeyValuePair<Tuple<ushort,ushort>,bool> kvp in guessedPhotos)
            {
                if (kvp.Value == false)
                    return false;
            }
            return true;
        }
        private void RandomAssingPhoto()
        {
            Random rnd = new Random();
            if ((NumberOfRows * NumberOfCols) % 2 != 0)
            {
                ushort row = (ushort)((ushort)NumberOfRows - 1);
                ushort col = (ushort)((ushort)NumberOfCols - 1);
                Tuple<ushort, ushort> pos = new Tuple<ushort, ushort>(row, col);
                assignedPhoto[pos] = "correct";
                Button lastButton = matrixGrid.Children[(NumberOfRows * NumberOfCols) - 1] as Button;
                lastButton.Content = new Image
                {
                    Source = new BitmapImage(new Uri(@"Images/correct.jpg", UriKind.Relative)) ,
                    VerticalAlignment = VerticalAlignment.Center,
                    Stretch = Stretch.Fill,
                    Height = 256,
                    Width = 256
                };
            }
            foreach(Button btn in matrixGrid.Children)
            {
                int randomIndex = rnd.Next(1, 11);

                ushort row = (ushort)Grid.GetRow(btn);
                ushort col = (ushort)Grid.GetColumn(btn);
                Tuple<ushort, ushort> pos = new Tuple<ushort, ushort>(row, col);
                if(assignedPhoto[pos] == "0")
                {
                    assignedPhoto[pos] = randomIndex.ToString();
                    List<Tuple<ushort,ushort>> falseKeys = assignedPhoto.Where(pair => pair.Value == 0.ToString()).Select(pair => pair.Key).ToList();
                    int rndNextIndex = rnd.Next(0, falseKeys.Count);
                    Tuple<ushort,ushort> randomNextPos = falseKeys[rndNextIndex];
                    assignedPhoto[randomNextPos] = assignedPhoto[pos];
                    assignedPairs[pos] = randomNextPos;
                    assignedPairs[randomNextPos] = pos;
                }
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

        private void ChangeButtonImage(Tuple<ushort,ushort> pos, string imgname)
        {
            Button btn = matrixGrid.Children[(pos.Item1 * NumberOfCols) + pos.Item2 ] as Button;
            btn.Content =  new Image
            {
                Source = new BitmapImage(new Uri(@"Images/"+imgname+".jpg", UriKind.Relative)) ,
                VerticalAlignment = VerticalAlignment.Center,
                Stretch = Stretch.Fill,
                Height = 256,
                Width = 256
            };
        }

    }
}
