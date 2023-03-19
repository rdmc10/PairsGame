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
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        private int GamesPlayed { get; set; }
        private int GamesWon { get; set; }
        public StatisticsWindow(string user, int gp, int gw)
        {
            GamesPlayed= gp;
            GamesWon= gw;
            InitializeComponent();
            LabelStatistics.Content = "Username: " + user + " | Games Played: " + gp.ToString() + " | Games Won: " + gw.ToString();
        }
    }
}
