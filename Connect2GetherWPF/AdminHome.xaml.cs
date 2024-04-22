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

            string url = _baseUrl+"AdminSuspiciousUsers/AllSuspiciousUser";

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
            //await SusUserLoader();
            await SusUserLoader();
            await PostCounter();
            await UserCounter();
            await fetchUsers();
            await fetchPosts();
            Postcountlbl.Content =countPost.ToString();
            UserCountlbl.Content = countUser.ToString();
           // dg_Post.ItemsSource = post;
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

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Change_data_btn_Click(object sender, RoutedEventArgs e)
        {
           
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
                    List<User> users = JsonConvert.DeserializeObject<List<User>>(responseBody);
                    UserList = users;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occurred while fetching user data: {ex.Message}");
            }

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
           
        }

        private void Change_data_Click(object sender, RoutedEventArgs e)
        {
            UserDataChangeWindow w = new UserDataChangeWindow(jwToken, _baseUrl);
            w.Show();
        }

        private async void MarkSus_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg_users.SelectedItem != null)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to mark the user?", "Confirmation", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        int id = (dg_users.SelectedItem as User).id;
                        if (client.DefaultRequestHeaders.Authorization != null)
                        {

                            client.DefaultRequestHeaders.Remove("Authorization");
                        }

                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwToken}");

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
                CommentManagerWindow w = new CommentManagerWindow(selectedPost,jwToken,_baseUrl);
                w.Show();
            }
            else {

                MessageBox.Show("Please select a Post!");
            }
        }
    }
}
