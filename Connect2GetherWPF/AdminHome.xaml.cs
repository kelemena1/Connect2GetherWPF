using Connect2GetherWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
    /// Interaction logic for AdminHome.xaml
    /// </summary>
    public partial class AdminHome : Window
    {
        public static List<SuspiciousUser> SusUserlist = new List<SuspiciousUser>();
        public static string jwToken = "";
        public static HttpClient client = new HttpClient();
        const string _baseUrl = "https://localhost:7043/";
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
        public async Task LoadAndDisplayUserData()
        {
            await SusUserLoader();
            dg_Sus.ItemsSource = SusUserlist;
        }

        public AdminHome(string Token)
        {
           
            InitializeComponent();
            jwToken = Token;
            SusUserLoader();
            Console.WriteLine(SusUserlist);
            LoadAndDisplayUserData();

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
