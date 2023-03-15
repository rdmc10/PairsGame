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
        private Dictionary<string,int> UserImageDict { get; set; }
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
            UserImageDict = new Dictionary<string, int>();
            GetUsersFromText();
            LoadUsrImgDict();
            LoadUsers();
            selectedUser = "";

    }

    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ImageHolder.Source = bitmapImages[UserImageDict[e.AddedItems[0].ToString()]];
            selectedUser = e.AddedItems[0].ToString();
        }

        private void Previous_Image_Click(object sender, RoutedEventArgs e)
        {
            Index = (Index - 1) == -1 ? bitmapImages.Count -1 : (Index - 1);
            ImageHolder.Source = bitmapImages[Index];
            if(selectedUser != "")
            {
                UserImageDict[selectedUser] = Index;
            }
        }

        private void Next_Image_Click(object sender, RoutedEventArgs e)
        {
            Index = (Index + 1) % bitmapImages.Count;
            ImageHolder.Source = bitmapImages[Index];
            if(selectedUser != "")
            {
                UserImageDict[selectedUser] = Index;
            }
        }

        private void New_User_Click(object sender, RoutedEventArgs e)
        {
            string username = UserTextField.GetLineText(0);
            UInt16 usrimgindex = (ushort)Index;
            User user = new User(username, usrimgindex);
            UserImageDict.Add(username, usrimgindex);
            userList.Add(user);

            ExportUserToText();
            ExportUsrImgDict();
            UserListView.Items.Add(user.UserName);
        }

        private void GetUsersFromText()
        {
            List<string> read = File.ReadLines("users.txt").ToList();
            foreach ( string line in read )
            {
                string[] info = line.Split(' ');
                ushort userimgindex = UInt16.Parse(info[1]);
                userList.Add(new User(info[0], userimgindex));
                if (!UserImageDict.ContainsKey(info[0]))
                    UserImageDict.Add(info[0], userimgindex);
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
                writer.WriteLine(userList[userList.Count - 1].UserName + " " + userList[userList.Count - 1].UserImageIndex);
            }

        }

        private void ExportUsrImgDict()
        {
            using (StreamWriter writer = new StreamWriter("usrimgdict.txt", append: false))
            {
                foreach(KeyValuePair<string,int> el in UserImageDict)
                {
                    Console.WriteLine(el.Key + " " + el.Value);
                    writer.WriteLine(el.Key + " " + el.Value);
                }
            }
        }

        private void LoadUsrImgDict()
        {
            List<string> read = File.ReadLines("usrimgdict.txt").ToList();
            foreach ( string line in read )
            {
                string[] info = line.Split(' ');
                ushort userimgindex = UInt16.Parse(info[1]);
                if (!UserImageDict.ContainsKey(info[0]))
                    UserImageDict.Add(info[0], userimgindex);
            }
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
