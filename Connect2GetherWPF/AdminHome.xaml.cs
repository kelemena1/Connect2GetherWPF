using Connect2GetherWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
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
    /// Interaction logic for AdminHome.xaml
    /// </summary>
    public partial class AdminHome : Window
    {
        //variables
        public static bool isactive = false;
        public static string jwToken = "";
        public static HttpClient client = new HttpClient();
        public static string _baseUrl = "";
        public static int countPost = 0;
        public static int countUser = 0;
        public static List<User> UserList = new List<User>();
        public static List<Post> PostList = new List<Post>();
        public static List<SuspiciousUser> SusUserlist = new List<SuspiciousUser>();
        public class UserData
        {
            public string DisplayPermissionName { get; set; }
            public string DisplayName { get; set; }
            public string DisplayEmail { get; set; }
            public bool DisplayActiveUser { get; set; }
            public DateTime DisplayLastLogin { get; set; }
            public bool DisplayIsValidated { get; set; }
        }



        public static async Task SusUserLoader() 
        {

            string url = _baseUrl+ "Moderator/AllSuspiciousUser";

            try
            {
                await Console.Out.WriteLineAsync(jwToken);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwToken);
                HttpResponseMessage response = await client.GetAsync(url);
              
                if (response.IsSuccessStatusCode)
                {
                    dynamic responseBody = await response.Content.ReadAsStringAsync();
                    await Console.Out.WriteLineAsync(responseBody);
                    List<SuspiciousUser> users = JsonConvert.DeserializeObject<List<SuspiciousUser>>(responseBody);

                    SusUserlist = users;
                    Console.WriteLine(SusUserlist.Count);

                }
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while fetching user data: {ex.Message}");
            }

        }

        public async Task PostCounter()
        {
            string url = _baseUrl + "AdminUserPost/UserPostCount";
            await Console.Out.WriteLineAsync(jwToken);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwToken);
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                dynamic responseBody = await response.Content.ReadAsStringAsync();
                await Console.Out.WriteLineAsync(responseBody);
                countPost = int.Parse(responseBody);


            }
            else {
                MessageBox.Show("Network Error!");
            }

        }
        public async Task UserCounter()
        {
            string url = _baseUrl + "AdminUsers/UserCount";
            await Console.Out.WriteLineAsync(jwToken);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwToken);
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                dynamic responseBody = await response.Content.ReadAsStringAsync();
                await Console.Out.WriteLineAsync(responseBody);
                countUser = int.Parse(responseBody);
            }
            else
            {
                MessageBox.Show("Network Error!");
            }

        }




        public async Task LoadAndDisplayUserData()
        {
            await SusUserLoader(); // SusUserList betöltése
            await PostCounter();
            await UserCounter();
            await fetchUsers();
            await fetchPosts();
            Postcountlbl.Content = countPost.ToString();
            UserCountlbl.Content = countUser.ToString();
            dg_users.ItemsSource = UserList;
            dg_Post.ItemsSource = PostList;
        }

        public AdminHome(string Token, string url)
        {
           
            InitializeComponent();
            jwToken = Token;
            _baseUrl = url;
            LoadAndDisplayUserData();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchbar_txtb.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                dg_users.ItemsSource = UserList;
            }
            else
            {
                var filteredUsers = UserList.Where(user =>
                    (user.username != null && user.username.ToLower().Contains(searchText)) ||
                    (user.email != null && user.email.ToLower().Contains(searchText)) ||
                    (user.displayName != null && user.displayName.ToLower().Contains(searchText))
                ).ToList();

                dg_users.ItemsSource = filteredUsers;
            }
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void Change_data_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want deleted the selected user?", "Confirmation", MessageBoxButton.YesNo);

            if (dg_users.SelectedItem != null)
            {
                if (result == MessageBoxResult.Yes) { 
                    try
                    {
                        int id = (dg_users.SelectedItem as User).id;
                        string url = _baseUrl + $"AdminUsers/DeleteUserById?id={id}";
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwToken);
                        HttpResponseMessage response = await client.DeleteAsync(url);
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("User deleted successfully");
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
                MessageBox.Show("Please select a user to delete!");
            }
        
        }

        public async Task fetchUsers() 
        {

            string url = _baseUrl + "AdminUsers/AllUser";

            try
            {
                await Console.Out.WriteLineAsync(jwToken);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwToken);
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    dynamic responseBody = await response.Content.ReadAsStringAsync();
                    await Console.Out.WriteLineAsync(responseBody);
                    List<User> users = JsonConvert.DeserializeObject<List<User>>(responseBody, new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy-MM-ddTHH:mm:ss" 
                    });
                    UserList = users;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while fetching user data: {ex.Message}");
            }

            SusUserLoader();

            foreach (var x in UserList)
            {
                foreach (var y in SusUserlist)
                {
                    if (x.id == y.userId)
                    {
                        x.suspicious = true;
                    }
                }
            }
        }
        public async Task fetchPosts()
        {

            string url = _baseUrl + "UserPost/AllUserPost";

            try
            {
                await Console.Out.WriteLineAsync(jwToken);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwToken);
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    dynamic responseBody = await response.Content.ReadAsStringAsync();
                    await Console.Out.WriteLineAsync(responseBody);
                    List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(responseBody);
                    PostList = posts;
                }
                await Console.Out.WriteLineAsync(PostList[0].user.username);
                foreach (var x in PostList) {
                    await Console.Out.WriteLineAsync(x.displayUsername);
                }



            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while fetching user data: {ex.Message}");
            }

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        
          


        }

        private void Displaydg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SuspiciousUsersWindow w = new SuspiciousUsersWindow(_baseUrl,jwToken);
            w.Show();
            this.Close();
           
        }

        private void Change_data_Click(object sender, RoutedEventArgs e)
        {
            UserDataChangeWindow w = new UserDataChangeWindow(jwToken, _baseUrl);
            w.Show();
            this.Close();
        }

        private async void MarkSus_btn_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (dg_users.SelectedItem != null)
                {
                    if ((dg_users.SelectedItem as User).suspicious == false)
                    {
                        DescriptionForSuspicious w = new DescriptionForSuspicious(_baseUrl,jwToken, (dg_users.SelectedItem as User).id);
                        w.Show();
                        this.Close();
                    }
                    else {
                        MessageBox.Show("User already marked!");
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

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
                
            Post selectedPost =  dg_Post.SelectedItem as Post;
            if (selectedPost != null)
            {
               

                    CommentManagerWindow w = new CommentManagerWindow(selectedPost, _baseUrl, jwToken);
                    w.Show();
                    this.Close();
                    
                
             

            }
            else {

                MessageBox.Show("Please select a Post!");
            }
        }
        private void SearchPosts(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                dg_Post.ItemsSource = PostList;
            }
            else
            {
                int postId;
                bool isNumeric = int.TryParse(searchText, out postId);

                var filteredPosts = PostList.Where(post =>
                    (post.user.username != null && post.user.username.ToLower().Contains(searchText)) ||
                    (post.Title != null && post.Title.ToLower().Contains(searchText)) ||
                    (isNumeric && post.Id == postId)
                ).ToList();

                dg_Post.ItemsSource = filteredPosts; 
            }
        }


        private void PostSearchbar_txtb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = PostSearchbar_txtb.Text.ToLower();
            SearchPosts(searchText);
        }

        private void logout_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            w.Show();
            this.Close();
        }

        private async void send_mail_onclick(object sender, RoutedEventArgs e)
        {
            if (dg_users.SelectedItem != null)
            {
                int id = (dg_users.SelectedItem as User).id;
                EmailSenderWindow w = new EmailSenderWindow(_baseUrl, jwToken, UserList, id);
                w.Show();
                this.Close();
            }
            else{
                MessageBox.Show("Please select a user!");
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg_Post.SelectedItem != null)
                {
                    if (!SusUserlist.Any(user => user.userId == (dg_Post.SelectedItem as Post).user.id))
                    {
                        DescriptionForSuspicious w = new DescriptionForSuspicious(_baseUrl, jwToken, (dg_Post.SelectedItem as Post).user.id);
                        w.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("User already marked!");
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
    }
}
