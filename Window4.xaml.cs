using Smart_Fitness_WPF;
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
    public partial class Window4 : Window
    {
        private string connectionString = "Server=DESKTOP-3AN1TNE\\SQLEXPRESS;Database=Smart-Fitness_WPF;Integrated Security=True;";
        private int UserID; // Хранит ID текущего пользователя

        public Window4(int userId) // Передаем ID пользователя при инициализации окна
        {
            InitializeComponent();
            UserID = userId; // Сохраняем ID пользователя
            LoadTrainings();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Предположим, что вы проверили логин и пароль
            int userId = UserID;

            if (userId != -1) // Если пользователь найден
            {
                Window4 window4 = new Window4(userId);
                window4.Show();
                this.Close(); // Закрываем текущее окно
            }
            else
            {
                MessageBox.Show("Неверные учетные данные.");
            }
        }
        private void LoadTrainings()
        {
            List<Training> trainings = new List<Training>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT TrainingID, DateTime FROM Trainings";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
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

            TrainingsListView.ItemsSource = trainings;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (TrainingsListView.SelectedItem is Training selectedTraining)
            {
                RegisterForTraining(UserID, selectedTraining.TrainingID);
                MessageBox.Show("Вы успешно записались на тренировку!");
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите тренировку.");
            }
        }

        private void RegisterForTraining(int UserID, int trainingId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO TrainingRegistrations (UserID, TrainingID) VALUES (@UserID, @TrainingID)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@TrainingID", trainingId);
                    command.ExecuteNonQuery();
                }
            }
        }
        public class Training
        {
            public int TrainingID { get; set; }
            public DateTime DateTime { get; set; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window2 wind = new Window2(1);
            wind.Show(); Close();
        }
    }
}