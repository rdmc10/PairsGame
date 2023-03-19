using System;
using System.IO;
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
        private List<User> userList { get; set; }
        private string selectedUser { get; set; }

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
            userList = new List<User>();
            GetUsersFromText();
            LoadUsers();
            selectedUser = "";
    }

    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count != 0)
                selectedUser = e.AddedItems[0].ToString();
            if(selectedUser != "" && selectedUser != "pop")
            {
                string read = File.ReadAllText(@"Users/"+selectedUser+ ".txt");
                ImageHolder.Source = new BitmapImage(new Uri(read, UriKind.Absolute));
                ButtonDelete.IsEnabled = true;
                ButtonPlay.IsEnabled = true;
            }
        }

        private void Previous_Image_Click(object sender, RoutedEventArgs e)
        {
            Index = (Index - 1) == -1 ? bitmapImages.Count -1 : (Index - 1);
            ImageHolder.Source = bitmapImages[Index];

            if(selectedUser!= "")
            {
                string filepath = @"Users/" + selectedUser + ".txt";
                using(var sw = new StreamWriter(filepath, false))
                {
                    sw.WriteLine(ImageHolder.Source.ToString());
                }
            }
        }

        private void Next_Image_Click(object sender, RoutedEventArgs e)
        {
            Index = (Index + 1) % bitmapImages.Count;
            ImageHolder.Source = bitmapImages[Index];
            
            string filepath = @"Users/" + selectedUser + ".txt";
            using(var sw = new StreamWriter(filepath, false))
            {
                sw.WriteLine(ImageHolder.Source.ToString());
            }

        }

        private void New_User_Click(object sender, RoutedEventArgs e)
        {
            string username = UserTextField.GetLineText(0);
            User user = new User(username);
            userList.Add(user);

            string filepath = @"Users/" + username + ".txt";
            using(var sw = new StreamWriter(filepath, true))
            {
                sw.WriteLine(ImageHolder.Source.ToString());
            }

            ExportUserToText();
            UserListView.Items.Add(user.UserName);
        }

        private void GetUsersFromText()
        {
            List<string> read = File.ReadLines("users.txt").ToList();
            foreach ( string line in read )
            {
                userList.Add(new User(line));
            }
        }

        private void LoadUsers()
        {
            foreach(User u in userList)
            {
                UserListView.Items.Add(u.UserName);
            }
        }

        private void ExportUserToText()
        {
            using (StreamWriter writer = new StreamWriter("users.txt", append: true))
            {
                writer.WriteLine(userList[userList.Count - 1].UserName);
            }

        }

        private void Delete_User_Click(object sender, RoutedEventArgs e)
        {
            UserListView.Items.RemoveAt(UserListView.SelectedIndex);
            File.Delete(@"Users/" + selectedUser + ".txt");
            File.Delete(@"Saves/" + selectedUser + ".txt");
            File.Delete(@"Saves/" + selectedUser + "_assigned.bin");
            File.Delete(@"Saves/" + selectedUser + "_guessed.bin");
            using (StreamWriter writer = new StreamWriter("users.txt", append: false))
            {
                foreach(User user in userList)
                {
                    if (user.UserName != selectedUser)
                        writer.WriteLine(user.UserName);
                }
            }
            selectedUser = "";
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            string filepath = @"Users/" + selectedUser + ".txt";
            string pathtoimg = "";
            using(var sw = new StreamWriter(filepath, true))
            {
                pathtoimg = ImageHolder.Source.ToString();
            }
            GameWindow gw = new GameWindow(selectedUser, pathtoimg );
            gw.Show();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
