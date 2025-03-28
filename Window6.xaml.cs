using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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

namespace Smart_Fitness_WPF
{
    public partial class Window6 : Window
    {
        private string connectionString = "Server=DESKTOP-3AN1TNE\\SQLEXPRESS;Database=Smart-Fitness_WPF;Integrated Security=True;";
        private const decimal subscriptionPrice = 1000m; // цена абонемента 1000 условно
        private int UserID;

        public Window6()
        {
            MessageBox.Show("Вход на стр 6.");
            InitializeComponent();
            LoadUserName();
            //this.UserID = 0;
        }

        private void LoadUserName()
        {
            // ГРУЗИМ имя пользователя из БД
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT UserName FROM Users WHERE UserID = @UserID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);
                    var userName = command.ExecuteScalar();
                    if (userName != null)
                    {
                        UserNameTextBox.Text = userName.ToString();
                    }
                }
            }
        }

        private void SubscriptionCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CalculateFinalPrice();
        }

        private void SubscriptionCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CalculateFinalPrice();
        }

        private void CalculateFinalPrice()
        {
            decimal finalPrice = subscriptionPrice;
            if (SubscriptionCheckBox.IsChecked == true)
            {
                finalPrice -= finalPrice * 0.1m; // Скидка 10%
            }
            FinalPriceTextBlock.Text = $"${finalPrice:F2}"; // Форматирование цены
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика для возврата на предыдущую страницу
            //this.Close(); // Закрываем текущее окно (можно заменить на навигацию)
        }
    }
}