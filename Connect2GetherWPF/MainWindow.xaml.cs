using Connect2GetherWPF.API;
using Connect2GetherWPF.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace Connect2GetherWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        const string _baseUrl = "https://localhost:7043/";

        public static JwtSecurityToken JWTokenDecoder(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            return jwtHandler.ReadJwtToken(token);
        }

        public MainWindow()
        {
            //ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            InitializeComponent();

        }

      

        private void LoginBtn(object sender, RoutedEventArgs e)
        {
            string url = _baseUrl + "Auth/Login";

            HttpClient client = new();
            //tb_username.Text,tb_password.Password
            Login user = new Login("kortez33","Asdasd123+");
            Console.WriteLine(user);
            Console.WriteLine(url);
            string JsonConvertedUser = JsonConvert.SerializeObject(user);
            StringContent stringContent = new(JsonConvertedUser, Encoding.UTF8, "application/json");


            Console.WriteLine(JsonConvertedUser);
            try
            {
                HttpResponseMessage result = client.PostAsync(url, stringContent).Result;
                Console.WriteLine(client.PostAsync(url, stringContent).Result);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                JwtSecurityToken tokenData = JWTokenDecoder(responseBody);
                Console.WriteLine(tokenData.ToString());
                string tokenTemp = JsonConvert.SerializeObject(tokenData);
                dynamic tokenPayload = JsonConvert.DeserializeObject(tokenTemp);
                dynamic tokenObject = tokenPayload.Payload;
                MessageBox.Show("" + responseBody);
                if (tokenObject.role == "Admin" || tokenObject.role == "Moderator")
                {
                    
                    AdminHome adminHome = new AdminHome(responseBody,_baseUrl);
                    adminHome.Show();
                    this.Close();
                }
                else 
                {
                    MessageBox.Show("You do not have permission!");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Rossz belépési adatok!");
            }
        }
    }
}