using Connect2GetherWPF.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using static System.Net.WebRequestMethods;

namespace Connect2GetherWPF
{
    /// <summary>
    /// Interaction logic for UserDataChangeWindow.xaml
    /// </summary>
    public partial class UserDataChangeWindow : Window
    {
        List<User> userList = new List<User>();
        string jwToken = "";
        public static string _baseUrl = "";
        HttpClient client = new();
        List<Permission> permissions = new List<Permission>();
        JWTtoken Admin = new JWTtoken();

        public async Task SendEmailForDataChange( int Uid,string sender,string Body,string Subject)
        {
            string url = $"{_baseUrl}AdminUsers/EmailSender?id={Uid}&sender={sender}";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwToken);
            string JsonConvertedUser = JsonConvert.SerializeObject(new             {
                body = Body,
                subject = Subject
            });
            StringContent stringContent = new(JsonConvertedUser, Encoding.UTF8, "application/json");

            Console.WriteLine(JsonConvertedUser);
            try
            {
                HttpResponseMessage response = await client.PostAsync(url, stringContent);
                if (response.IsSuccessStatusCode)
                {
                    dynamic responseBody = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("User is notified!");
                }
                else
                {
                    MessageBox.Show("Email sending is failed!");

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public async Task fetchPermissions()
        {
            string url = $"{_baseUrl}AdminPermission/AllPermission";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwToken);
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                dynamic responseBody = await response.Content.ReadAsStringAsync();
                await Console.Out.WriteLineAsync(responseBody);
                permissions = JsonConvert.DeserializeObject<List<Permission>>(responseBody.ToString());
            }
            else
            {
                MessageBox.Show("Network Error!");
            }
        }
        public UserDataChangeWindow(string token, string url)
        {
            InitializeComponent();
            jwToken = token;
            _baseUrl = url;
            Admin = new JWTtoken(token);
            LoadAndDisplayUserData();
        }

        public async Task UserData()
        {
            string url = _baseUrl + "AdminUsers/AllUser";
            await Console.Out.WriteLineAsync(jwToken);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwToken);
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                dynamic responseBody = await response.Content.ReadAsStringAsync();
                await Console.Out.WriteLineAsync(responseBody);
                userList = JsonConvert.DeserializeObject<List<User>>(responseBody.ToString());


            }
            else
            {
                MessageBox.Show("Network Error!");
            }

        }
        public async Task ChangeUserData(int id)
        {
            string url =  $"{_baseUrl}AdminUsers/ChangeRegisterById?id={id}";

            HttpClient client = new HttpClient();
            Console.WriteLine(url);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwToken);
            string JsonConvertedUser = JsonConvert.SerializeObject(new UserDataChange()
            {
                userName = Username_txtb.Text,
                email = email_txtb.Text,
                permissionId = (permission_cmbx.SelectedItem as Permission).Id
            }); 
            StringContent stringContent = new(JsonConvertedUser, Encoding.UTF8, "application/json");

            Console.WriteLine(JsonConvertedUser);
            try
            {
                HttpResponseMessage response = await client.PutAsync(url, stringContent);
                if (response.IsSuccessStatusCode)
                {
                    dynamic responseBody = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("User data changed successfully!");
                    string subject = "Connect2Gether Adatváltoztatás!";
                    string body = $"Ezúton értesítjük, hogy a fiókjában sikeresen megváltoztattuk a az adatait az alábbi adatokra:\nFelhasználónév: {Username_txtb.Text}\nEmail cím: {email_txtb.Text}";
                    SendEmailForDataChange((cmb_users.SelectedItem as User).id,Admin.Name,body,subject);
                    AdminHome w =  new AdminHome(jwToken,_baseUrl);
                    w.Show();
                    this.Close();
                    
                }
                else
                {
                    MessageBox.Show("Network error!");
                    AdminHome w = new AdminHome(jwToken,_baseUrl);
                    w.Show();
                    this.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public async Task LoadAndDisplayUserData()
        {
            
            await UserData();
            await fetchPermissions();
            cmb_users.ItemsSource = userList;
            cmb_users.SelectedIndex = 0;
            permission_cmbx.ItemsSource = permissions;
            permission_cmbx.SelectedIndex = 0;
            Username_txtb.Text = (cmb_users.Items[0] as User).username;
            email_txtb.Text = (cmb_users.Items[0] as User).email;
        }


        private void email_txtb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void SendData_Click(object sender, RoutedEventArgs e)
        {
            bool isValidEmail = false;
            
            if (!Regex.Match(email_txtb.Text, @"^[0-9]+\s+([a-zA-Z]+|[a-zA-Z]+\s[a-zA-Z]+)$").Success && email_txtb.Text != ""&& email_txtb.Text.Contains('@')&&email_txtb.Text.Contains('.')){
                isValidEmail = true;
            }
            else
            {
                MessageBox.Show("Invalid email!");

            }

            if (isValidEmail && Username_txtb != null)
            {
                int id = (cmb_users.SelectedItem as User).id;
                Console.WriteLine(id);
                await ChangeUserData(id);
                
                this.Close();
                }
            else {
                MessageBox.Show("Wrong datas!");
            }


        }

        private void cmb_users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Username_txtb.Text = (cmb_users.SelectedItem as User).username;
            email_txtb.Text = (cmb_users.SelectedItem as User).email;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            AdminHome w = new AdminHome(jwToken, _baseUrl);
            w.Show();
            this.Close();
        }

        private void permission_cmbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
