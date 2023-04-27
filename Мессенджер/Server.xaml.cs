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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Мессенджер
{
    /// <summary>
    /// Логика взаимодействия для Главная.xaml
    /// </summary>
    public partial class Server : Page
    {
        public Server()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NUser.Text == "Admin")(Application.Current.MainWindow as MainWindow).Page.Content = new Page_Admin(NUser.Text);
            else MessageBox.Show("Неправильное имя сервера");
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            if (NUser.Text == "Bob" && Ip_.Text == "127.0.0.1") (Application.Current.MainWindow as MainWindow).Page.Content = new CClientPage(NUser.Text);
            else { MessageBox.Show("Неправильное имя клиента"); }
        }
    }
}
