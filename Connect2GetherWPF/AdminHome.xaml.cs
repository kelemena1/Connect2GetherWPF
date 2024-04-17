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
        public static List<SuspiciousUser> SusUserlist = new List<SuspiciousUser>();
        public static string jwToken = "";
        public static HttpClient client = new HttpClient();
        public static string _baseUrl = "";
        public static int countPost = 0;
        public static int countUser = 0;
        public static List<User> UserList = new List<User>();

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

        public async Task UserDataDisplay()
        {

            

            // Create a new list with only display properties
            List<UserData> userDataList = UserList.Select(user => new UserData
            {
                DisplayPermissionName = user.displayPermissionName,
                DisplayName = user.displayName,
                DisplayEmail = user.displayEmail,
                DisplayActiveUser = user.displayActiveUser,
                DisplayLastLogin = user.displayLastLogin,
                DisplayIsValidated = user.displayIsValidated
            }).ToList();

            // Create DataGrid
            DataGrid dataGrid = new DataGrid();

            // Create and add columns dynamically
            foreach (var propertyInfo in typeof(UserData).GetProperties())
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = propertyInfo.Name; // Column header is the property name
                column.Binding = new System.Windows.Data.Binding(propertyInfo.Name);
                dataGrid.Columns.Add(column);
            }

            // Set data source
            dataGrid.ItemsSource = userDataList;

            // Add DataGrid to your container
           // YourContainer.Children.Add(dataGrid);
        }
    


        public async Task LoadAndDisplayUserData()
        {
            await SusUserLoader();
            await PostCounter();
            await UserCounter();
            await fetchUsers();
            Postcountlbl.Content =countPost.ToString();
            UserCountlbl.Content = countUser.ToString();
            dg_Sus.ItemsSource = SusUserlist;
            Displaydg.ItemsSource = UserList;
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
            UserDataChangeWindow w = new UserDataChangeWindow(jwToken,_baseUrl);
            w.Show();
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

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //User
            if(comboBox.SelectedIndex == 0) 
            {
                
            
            }else if(comboBox.SelectedIndex == 1) 
            {
                
                
            }



        }

        private void Displaydg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
