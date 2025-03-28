using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
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
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Smart_Fitness_WPF
{
    public partial class Window1 : Window
    {
        private string connectionString = "Server=DESKTOP-3AN1TNE\\SQLEXPRESS;Database=Smart-Fitness_WPF;Integrated Security=True;";
        private int userId;
        public Window1()
        {
            
            InitializeComponent();
            LoadComboBoxes();
        }
        private void LoadComboBoxes()
        {
            // Заполнение ComboBox для возраста от 18 до 60
            for (int i = 18; i <= 60; i++)
            {
                txtAge.Items.Add(i);
            }

            // Заполнение ComboBox для роста от 145 до 195
            for (decimal i = 145; i <= 195; i++)
            {
                txtHeight.Items.Add(i);
            }

            // Заполнение ComboBox для веса от 50 до 120
            for (decimal i = 50; i <= 120; i++)
            {
                txtWeight.Items.Add(i);
            }
            //txtRole.Visibility = System.Windows.Visibility.Collapsed;
            txtRole.Items.Add("Client");
            txtRole.Items.Add("Traner");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {    
            var name = txtName.Text;
            int age = Convert.ToInt32(txtAge.SelectedItem);
            decimal height = Convert.ToDecimal(txtHeight.SelectedItem);
            decimal weight = Convert.ToDecimal(txtWeight.SelectedItem);
            string role = txtRole.Text;
            string phone = txtPhone.Text;
            string password = txtPassword.Text;

            var user = new User
            {
                Name = name,
                Age = age,
                Height = height,
                Weight = weight,
                Phone = phone,
                Password = password,
                Role = role
            };

            // Добавление данных в базу данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Users (Name, Age, Height, Weight, Phone, Password, Role) VALUES (@Name, @Age, @Height, @Weight, @Phone, @Password, @Role)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Age", age);
                        command.Parameters.AddWithValue("@Height", height);
                        command.Parameters.AddWithValue("@Weight", weight);
                        command.Parameters.AddWithValue("@Phone", phone);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Role", role);

                        command.ExecuteNonQuery();
                    }
                    SaveUserToJson(user);

                    MessageBox.Show("Вы успешно зарегистрированы!");
                    MainWindow wind = new MainWindow();
                    wind.Show();
                    Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ошибка при добавлении пользователя: " + ex.Message);
                }
            }
        }
        private void SaveUserToJson(User user)
        {
            string json = JsonConvert.SerializeObject(user, Formatting.Indented);
            string filePath = "users.json"; // Путь к файлу

            // Сохранение JSON в файл
            try
            {
                File.AppendAllText(filePath, json + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении данных в JSON: " + ex.Message);
            }
        }
    }
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
