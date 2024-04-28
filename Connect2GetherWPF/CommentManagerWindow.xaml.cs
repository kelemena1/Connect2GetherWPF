using Connect2GetherWPF.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public static HttpClient client = new HttpClient();
        public static Post selectedPost = new Post();
        public static string _baseUrl = "";
        public static string jwToken = "";
        List<Comment> commentsList = new List<Comment>();
        public static int SelectedPostId = 0;
        public CommentManagerWindow(Post post,string url,string token)
        {
            _baseUrl = url;
            InitializeComponent();
            selectedPost = post;
            fetchComments();
            jwToken = token;
            SelectedPostId = post.Id;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void DeleteComments_Click(object sender, RoutedEventArgs e)
        {
            
            Comment SelectedComment = comments_dg.SelectedItem as Comment;
            
            if (SelectedComment != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want deleted the selected user?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes) {
                    try
                    {
                        string url = _baseUrl + $"Comment/AdminOperation/DeleteCommentByAdmin?id={SelectedComment.id}";
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwToken);
                        HttpResponseMessage response = await client.DeleteAsync(url);
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Sucessfully deleted the comment!");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Network Error!");
                    }
                    finally { 
                        LoadAndDisplayUserData();
                    }
              }
            }
            else
            {
                MessageBox.Show("Please select a comment to delete it!");
            }
        }

        private async void SuspiciousUserByComment_click(object sender, RoutedEventArgs e)
        {
            try
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

                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwToken}");

                        string url = $"{_baseUrl}Moderator/AddSuspicious?id={id}";


                        HttpResponseMessage response = await client.PostAsync(url, null);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadAndDisplayUserData();
            }
        }

        private void Close_btn_Click(object sender, RoutedEventArgs e)
        {
            AdminHome w = new AdminHome(jwToken, _baseUrl);
            w.Show();
            this.Close();
        }

        private async void LoadAndDisplayUserData()
        {
            await fetchComments();
            comments_dg.ItemsSource = commentsList;
        }

        public async Task fetchComments()
        {
            await Console.Out.WriteLineAsync(selectedPost.Id.ToString());
            string url = $"{_baseUrl}Comment/CommentByPostId?id={selectedPost.Id}";
            await Console.Out.WriteLineAsync(_baseUrl);
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    await Console.Out.WriteLineAsync(responseBody);
                    commentsList = JsonConvert.DeserializeObject<List<Comment>>(responseBody);

                    comments_dg.ItemsSource = commentsList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching user data: {ex.Message}");
            }
        }

    }
}
