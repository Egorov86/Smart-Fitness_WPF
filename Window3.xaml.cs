using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;

namespace Smart_Fitness_WPF
{
    public partial class Window3 : Window  // в базе данных UserID, Name, Age, Height, Weight, Phone, Password, Role.
    {
        private string connectionString = "Server=DESKTOP-3AN1TNE\\SQLEXPRESS;Database=Smart-Fitness_WPF;Integrated Security=True;";
        private int UserID;
        public Window3()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window2 wind = new Window2(1);
            wind.Show();
            Close();
        }

        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            int age = int.Parse(((ComboBoxItem)txtAge.SelectedItem).Content.ToString());
            decimal height = decimal.Parse(txtHeight.Text);
            decimal weight = decimal.Parse(txtWeight.Text);
            string phone = txtPhone.Text;
            string password = txtPassword.Password;
            string role = ((ComboBoxItem)txtRole.SelectedItem).Content.ToString();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Users SET Name = @Name, Age = @Age, Height = @Height, Weight = @Weight, Phone = @Phone, Password = @Password, Role = @Role WHERE UserID = @UserID"; // Укажите ваш UserID
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Age", age);
                        command.Parameters.AddWithValue("@Height", height);
                        command.Parameters.AddWithValue("@Weight", weight);
                        command.Parameters.AddWithValue("@Phone", phone);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Role", role);
                        command.Parameters.AddWithValue("@UserID", UserID);

                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Вы успешно сохранили профиль!");
                    MainWindow wind = new MainWindow();
                    wind.Show();
                    Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ошибка при редактировании пользователя: " + ex.Message);
            }
        }
    }
}
