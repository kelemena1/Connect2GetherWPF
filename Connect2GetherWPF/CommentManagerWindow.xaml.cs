using Connect2GetherWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public static string JwToken = "";
        public static string _baseUrl = "";
        public static HttpClient client = new HttpClient();


        public CommentManagerWindow(Post post, string jwToken, string url)
        {
            InitializeComponent();
            selectedPost = post;
            comments_dg.ItemsSource = selectedPost.Comments;
            JwToken = jwToken;
            _baseUrl = url;

        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void SuspiciousUserByComment_click(object sender, RoutedEventArgs e)
        {
              if (comments_dg.SelectedItem != null)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to mark the user?", "Confirmation", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        int id = (comments_dg.SelectedItem as Comment).userId;
                        if (client.DefaultRequestHeaders.Authorization != null)
                        {

                            client.DefaultRequestHeaders.Remove("Authorization");
                        }

                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {JwToken}");

                        string url = $"{_baseUrl}Moderator/AddSuspicious?id={id}";


                        HttpResponseMessage response = await client.PostAsync(url,null);
                        await Console.Out.WriteLineAsync(response.StatusCode.ToString());
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Sucessfully marked the user as suspicious!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to mark the user. Please check your connection!");
                            MessageBox.Show(response.StatusCode.ToString());
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select the user to mark it!");
                }
            
    }

        private async void DeleteComments_Click(object sender, RoutedEventArgs e)
        {
            if (comments_dg.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the selected comment?", "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    int id = (comments_dg.SelectedItem as Comment).id;
                    if (client.DefaultRequestHeaders.Authorization != null)
                    {
                        client.DefaultRequestHeaders.Remove("Authorization");
                    }

                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {JwToken}");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string url = $"{_baseUrl}Comment/AdminOperation/DeleteCommentByAdmin?id={id}";

                    HttpResponseMessage response = await client.DeleteAsync(url);
                    await Console.Out.WriteLineAsync(response.StatusCode.ToString());
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Sucessfully deleted the selected comment!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the comment. Please check your connection!");
                        MessageBox.Show(response.StatusCode.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a comment to delete!");
            }
        }

    }
}