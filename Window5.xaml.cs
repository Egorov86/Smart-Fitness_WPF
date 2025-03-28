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
    public partial class Window5 : Window
    {
        private string connectionString = "Server=DESKTOP-3AN1TNE\\SQLEXPRESS;Database=Smart-Fitness_WPF;Integrated Security=True;";
        private int UserID;
        /*private void OpenWindow5(int userId)
        {
            Window5 window5 = new Window5(userId);
            window5.Show();
            this.Close(); // Закрываем текущее окно, если это необходимо
        }*/

        public Window5(int userId)
        {
            InitializeComponent();
            this.UserID = userId;
            LoadUserRegistrations();
        }

        private void LoadUserRegistrations()
        {
            List<Training> trainings = new List<Training>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT t.TrainingID, t.DateTime FROM TrainingRegistrations tr JOIN Trainings t ON tr.TrainingID = t.TrainingID WHERE tr.UserID = @UserID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                trainings.Add(new Training
                                {
                                    TrainingID = reader.GetInt32(0),
                                    DateTime = reader.GetDateTime(1)
                                });
                            }
                        }
                    }
                }

                TrainingListView.ItemsSource = trainings;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке тренировок: {ex.Message}");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (TrainingListView.SelectedItem is Training selectedTraining)
            {
                DeleteTrainingRegistration(selectedTraining.TrainingID);
                LoadUserRegistrations(); // Обновляем список после удаления
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите тренировку для удаления.");
            }
            Window2 wind = new Window2(1);
            wind.Show(); Close();
        }

        private void DeleteTrainingRegistration(int trainingId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM TrainingRegistrations WHERE UserID = @UserID AND TrainingID = @TrainingID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.Parameters.AddWithValue("@TrainingID", trainingId);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            MessageBox.Show("Не удалось удалить тренировку. Возможно, она не существует.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении тренировки: {ex.Message}");
            }
        }
    }

    public class Training
    {
        public int TrainingID { get; set; }
        public DateTime DateTime { get; set; }
    }
}
