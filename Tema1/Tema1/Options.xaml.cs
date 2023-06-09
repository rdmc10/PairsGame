﻿using System;
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
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        public Options()
        {
            InitializeComponent();
        }

        public int CustomRows { get; set; }
        public int CustomCols { get; set; }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(FirstTextBox.Text, out int firstInteger) && int.TryParse(SecondTextBox.Text, out int secondInteger))
            {
                CustomRows = firstInteger;
                CustomCols = secondInteger;
                Window.GetWindow(this).DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please enter valid integers.");
            }
        }
    }
}
