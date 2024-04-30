using Connect2GetherWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
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
using Newtonsoft.Json.Linq;
using System.Diagnostics.Metrics;

namespace Connect2GetherWPF
{
    /// <summary>
    /// Interaction logic for SuspiciousUsersWindow.xaml
    /// </summary>
    public partial class SuspiciousUsersWindow : Window
    {
        public static HttpClient client = new HttpClient();
        public static List<SuspiciousUser> SusUserlist = new List<SuspiciousUser>();
        public static string _baseUrl = "";
        public static string jwToken = "";


        public async Task LoadAndDisplayUserData()
        {
            //await SusUserLoader();

            
            await fetchSusUsers();
           dg_susUsers.ItemsSource = SusUserlist;
            
        }
        public SuspiciousUsersWindow(string url, string token)
        {
            InitializeComponent();
            _baseUrl = url;
            jwToken = token;
            LoadAndDisplayUserData();

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }
        public async Task fetchSusUsers()
        {
            string url = _baseUrl + "Moderator/AllSuspiciousUser";
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
                    dg_susUsers.ItemsSource = SusUserlist; // Set the ItemsSource here
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching user data: {ex.Message}");
            }
        }



        private void button_Click(object sender, RoutedEventArgs e)
        {
           
            AdminHome w = new AdminHome(jwToken,_baseUrl);
            w.Show();
            this.Close();
        }


        //Sus User Delete permamently
        private async void Delete_sus_mark_click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (dg_susUsers.SelectedItem != null)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to remove the suspicious mark?", "Confirmation", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        int id = (dg_susUsers.SelectedItem as SuspiciousUser).id;
                        string url = $"{_baseUrl}Moderator/DeleteSuspiciousById?id={id}";
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwToken);

                        HttpResponseMessage response = await client.DeleteAsync(url);
                        await Console.Out.WriteLineAsync(response.StatusCode.ToString());
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Sucessfully deleted the suspicious user!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the user. Please check your connection!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a suspicious user to delete it!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //Remove the suspicious mark
        private async void Delete_sus_user_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg_susUsers.SelectedItem != null)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to remove the suspicious user?", "Confirmation", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        int id = (dg_susUsers.SelectedItem as SuspiciousUser).userId;
                        if (client.DefaultRequestHeaders.Authorization != null)
                        {

                            client.DefaultRequestHeaders.Remove("Authorization");
                        }

                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwToken}");

                        string url = $"{_baseUrl}AdminUsers/DeleteUserById?id={id}";


                        HttpResponseMessage response = await client.DeleteAsync(url);
                        await Console.Out.WriteLineAsync(response.StatusCode.ToString());
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Successfully deleted the suspicious user!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the user. Please check your connection!");
                        }
                    }
                }  
            }
            catch {
                MessageBox.Show("Failed to delete the user. Please check your connection!");
            }

        }

        private void dg_susUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            description_txtb.Text = (dg_susUsers.SelectedItem as SuspiciousUser).description;
        }

        private void Sus_searchbar_txtb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = Sus_searchbar_txtb.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                dg_susUsers.ItemsSource = SusUserlist;
            }
            else
            {
                var filteredUsers = SusUserlist.Where(user =>
                    (user.username != null && user.username.ToLower().Contains(searchText)) ||
                    (user.userId.ToString().Contains(searchText)) || 
                    (user.id.ToString().Contains(searchText))
                ).ToList();

                dg_susUsers.ItemsSource = filteredUsers;
            }
        }


    }
}
