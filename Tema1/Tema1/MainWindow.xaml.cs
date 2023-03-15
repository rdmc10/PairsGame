using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Tema1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<BitmapImage> bitmapImages { get; set; }
        private int Index { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            bitmapImages = new List<BitmapImage>()
            {
               new BitmapImage(new Uri(@"Resources/beard.png",UriKind.Relative)),
               new BitmapImage(new Uri(@"Resources/dog.png",UriKind.Relative)),
               new BitmapImage(new Uri(@"Resources/hacker.png",UriKind.Relative)),
               new BitmapImage(new Uri(@"Resources/indian-man.png",UriKind.Relative)),
               new BitmapImage(new Uri(@"Resources/man.png",UriKind.Relative)),
               new BitmapImage(new Uri(@"Resources/panda.png",UriKind.Relative)),
               new BitmapImage(new Uri(@"Resources/user.png",UriKind.Relative)),
               new BitmapImage(new Uri(@"Resources/user2.png",UriKind.Relative)),
            };
            Index = 0;
            ImageHolder.Source = bitmapImages.FirstOrDefault();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Previous_Image_Click(object sender, RoutedEventArgs e)
        {
            Index = (Index - 1) == -1 ? bitmapImages.Count -1 : (Index - 1);
            ImageHolder.Source = bitmapImages[Index];
        }

        private void Next_Image_Click(object sender, RoutedEventArgs e)
        {
            Index = (Index + 1) % bitmapImages.Count;
            ImageHolder.Source = bitmapImages[Index];
        }

        private void New_User_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_User_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
