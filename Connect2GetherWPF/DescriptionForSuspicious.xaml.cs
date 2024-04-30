using Connect2GetherWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Connect2GetherWPF
{
    /// <summary>
    /// Interaction logic for DescriptionForSuspicious.xaml
    /// </summary>
    public partial class DescriptionForSuspicious : Window
    {
        public static string _baseUrl = "";
        public static string jwToken = "";
        public static int selectedUserId = 0;
        HttpClient client = new();
        public DescriptionForSuspicious(string url, string token, int id)
        {
            InitializeComponent();
            _baseUrl = url;
            jwToken = token;
            selectedUserId = id;

        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Send_sus_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Description_txtb != null)
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to mark the user?", "Confirmation", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        int id = selectedUserId;
                        if (client.DefaultRequestHeaders.Authorization != null)
                        {

                            client.DefaultRequestHeaders.Remove("Authorization");
                        }

                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwToken}");

                        string url = $"{_baseUrl}Moderator/AddSuspicious?id={id}";
                        string JsonConvertedUser = JsonConvert.SerializeObject(new
                        {
                            descrpition = Description_txtb.Text,

                        });
                        StringContent stringContent = new(JsonConvertedUser, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(url, stringContent);
                        await Console.Out.WriteLineAsync(response.StatusCode.ToString());
                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Sucessfully marked the user as suspicious!");
                            AdminHome w = new AdminHome(jwToken,_baseUrl);
                            w.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to mark the user. Please check your connection!");

                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else {
                MessageBox.Show("Description field can't be empty");
                
            }
        }
    }
}
