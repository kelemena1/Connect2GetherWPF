using Connect2GetherWPF.Models;
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

namespace Connect2GetherWPF
{
    /// <summary>
    /// Interaction logic for CommentManagerWindow.xaml
    /// </summary>
    public partial class CommentManagerWindow : Window
    {
        public static Post selectedPost = new Post();
        public CommentManagerWindow(Post post)
        {
            InitializeComponent();
            selectedPost = post;
            comments_dg.ItemsSource = selectedPost.Comments;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
