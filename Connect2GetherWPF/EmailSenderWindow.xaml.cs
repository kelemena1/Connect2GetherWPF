using Connect2GetherWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
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
    /// Interaction logic for EmailSenderWindow.xaml
    /// </summary>
    public partial class EmailSenderWindow : Window
    {
        public static string _BaseUrl = "";
        public static string JwToken = "";
        public static HttpClient client = new HttpClient();
        public static List<User> UsersList = new List<User>();
        public static JWTtoken JwtTokenDetails = new JWTtoken();
        public static int selectedId = 0;
        

        public EmailSenderWindow(string url, string token,List<User> userList, int id)
        {
            InitializeComponent();
            _BaseUrl = url;
            JwToken = token;
            UsersList = userList;
            users_cmbx.ItemsSource = UsersList;
            users_cmbx.SelectedItem = userList.FirstOrDefault(x => x.id == id);
            selectedId = id;

            JwtTokenDetails = new JWTtoken(token);
        }

        private void Close_btn_Click(object sender, RoutedEventArgs e)
        {
            AdminHome w = new AdminHome(JwToken,_BaseUrl);
            w.Show();
            this.Close();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            if (users_cmbx.SelectedItem != null)
            {
                try {
                    int id = (users_cmbx.SelectedItem as User).id;
                    string url = $"{_BaseUrl}AdminUsers/EmailSender?id={id}&sender={JwtTokenDetails.Name}";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwToken);
                    
                    string ToAdressee = JsonConvert.SerializeObject( new emailSender()
                    {
                        body = body_txtb.Text,
                        subject = title_txtb.Text,
                    });
                    var content = new StringContent(ToAdressee, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Email sent successfully");
                    }
                }
                catch {
                    MessageBox.Show("Network Error!");
                }

            }
            else
            {
                MessageBox.Show("Please select and adressee!");
            }
        }

        private void users_cmbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
