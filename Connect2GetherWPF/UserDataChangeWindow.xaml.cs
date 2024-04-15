using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Connect2GetherWPF
{
    /// <summary>
    /// Interaction logic for UserDataChangeWindow.xaml
    /// </summary>
    public partial class UserDataChangeWindow : Window
    {
        public UserDataChangeWindow()
        {
            InitializeComponent();
        }

        private void email_txtb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SendData_Click(object sender, RoutedEventArgs e)
        {


            if (Regex.IsMatch(email_txtb.Text, ))
            {

                MessageBox.Show("Az e-mail cím fasza!");
            }
            else
            {
                MessageBox.Show("Az e-mail cím érvénytelen!");

            }

        }
    }
}
