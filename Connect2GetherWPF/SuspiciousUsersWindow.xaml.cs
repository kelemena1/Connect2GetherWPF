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
            string url = _baseUrl + "AdminSuspiciousUsers/AllSuspiciousUser";

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


        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

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
                        if (client.DefaultRequestHeaders.Authorization != null)
                        {
                            
                            client.DefaultRequestHeaders.Remove("Authorization");
                        }

                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwToken}");

                        string url = $"{_baseUrl}AdminSuspiciousUsers/DeleteSuspiciousById?id={id}";


                        HttpResponseMessage response = await client.DeleteAsync(url);
                        await Console.Out.WriteLineAsync(response.StatusCode.ToString());
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Successfully removed the suspicious mark!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to remove the suspicious mark. Please try again later.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a suspicious user to remove the mark from.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Delete_sus_user_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
