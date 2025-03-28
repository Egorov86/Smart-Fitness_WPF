using System;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using System.Diagnostics;
using System.Xml.Linq;
using System.Security.Policy;
using System.Windows.Controls;
using System.Windows.Navigation;
using Smart_Fitness_WPF.Properties;
using System.Reflection.Emit;
using System.Windows.Documents;
using System.Windows.Input;


namespace Smart_Fitness_WPF
{

    public partial class MainWindow : Window
    {

        private string connectionString = "Server=DESKTOP-3AN1TNE\\SQLEXPRESS;Database=Smart-Fitness_WPF;Integrated Security=True;";
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string phone = txtPhone.Text; // 
            string password = txtPassword.Password; // 
            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Телефон и пароль не могут быть пустыми.");
                return;
            }
            if (AuthenticateUser(phone, password))
            {
                MessageBox.Show("Вход выполнен успешно!");
                Window2 wind = new Window2(1);
                wind.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool AuthenticateUser(string phone, string password)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Phone = @Phone AND Password = @Password", connection);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Password", password);

                int count = (int)command.ExecuteScalar();

                return count > 0; 
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            Window1 wind = new Window1();
            wind.Show();
            Close();
        }
    }
    public class UserID
    {
        public int Age { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

