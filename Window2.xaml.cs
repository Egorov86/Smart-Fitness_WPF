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
using System.Windows.Threading;

namespace Smart_Fitness_WPF
{
    public partial class Window2 : Window
    {
        private string connectionString = "Server=DESKTOP-3AN1TNE\\SQLEXPRESS;Database=Smart-Fitness_WPF;Integrated Security=True;";
        private int id;
        private DispatcherTimer _timer;
        private bool _isListBoxVisible = false;
        public Window2(int id)
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(2);
            _timer.Tick += Timer_Tick;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_isListBoxVisible)
            {
                // Если ListBox уже виден, скрываем его
                ProjectInfoListBox.Visibility = Visibility.Collapsed;
                _isListBoxVisible = false;
                _timer.Stop();
            }
            else
            {
                // Показывать ListBox с информацией о проекте
                ProjectInfoListBox.Visibility = Visibility.Visible;
                _isListBoxVisible = true;

                // Запускаем таймер для автоматического скрытия через 2 секунды
                _timer.Start();
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Скрываем ListBox после 2 секунд
            ProjectInfoListBox.Visibility = Visibility.Collapsed;
            _isListBoxVisible = false;
            _timer.Stop();
        }
        
        private void OtherButton_Click(object sender, RoutedEventArgs e)
        {
            // Скрыть ListBox с информацией о проекте, если он виден
            if (_isListBoxVisible)
            {
                ProjectInfoListBox.Visibility = Visibility.Collapsed;
                _isListBoxVisible = false;
                _timer.Stop();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window3 wind = new Window3();
            wind.Show();
            Close();
        }

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            Window4 wind = new Window4(2);
            wind.Show();Close();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Window5 wind = new Window5(1);
            wind.Show(); Close();
        }

        private void Payment_Click(object sender, RoutedEventArgs e)
        {
            Window6 wind = new Window6();
            wind.Show(); Close();
        }
    }
}
