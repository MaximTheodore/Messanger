using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
    /// Логика взаимодействия для CClientPage.xaml
    /// </summary>
    public partial class CClientPage : Page
    {
        private Socket socket;

        /*static List<string> list = new List<string>();
        static Server server = new Server();*/
        static Server server = new Server();

        static Dictionary<string, Socket> users = new Dictionary<string, Socket>();
        private static List<string> strings = new List<string>();
        public CClientPage(string a)
        {
           CClient client = new CClient();
            InitializeComponent();
            (Application.Current.MainWindow as MainWindow).Width = 800;
            (Application.Current.MainWindow as MainWindow).Height = 450;
            //CClient.Into(a);
            /* socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
             socket.ConnectAsync("127.0.0.1", 7777);
             Send($"/username{a}");
             ReverseMess();
             strings.Add($"[{a}]");
             ListClients.Items.Add($"[{a}]");*/
            client.Into(a,Message_,ListClients);
        }
        private async void Send__Click(object sender, RoutedEventArgs e)
        {
            CClient client = new CClient();
            if (Lett.Text == "/disconnect")
            {
                MyServer.g.Cancel();
                (Application.Current.MainWindow as MainWindow).Width = 400;
                (Application.Current.MainWindow as MainWindow).Height = 400;
                (Application.Current.MainWindow as MainWindow).Page.Content = new Server();
            }
            else
            {
                await client.Send(Lett.Text);
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyServer.g.Cancel();
            (Application.Current.MainWindow as MainWindow).Page.Content = new Server();
            (Application.Current.MainWindow as MainWindow).Width = 400;
            (Application.Current.MainWindow as MainWindow).Height = 400;
        }
        /* private async  Task ReverseMess()
{
    while (true)
    {
        byte[] bytes = new byte[1024];
        //socket.Receive(bytes);
        await socket.ReceiveAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
        string message = Encoding.UTF8.GetString(bytes);
        Message_.Items.Add(message);
        if (message.StartsWith("/username"))
        {
            //Send("/username" + a);
            strings.Add($"[{message.Substring(9)}]");
            ListClients.ItemsSource = null;
            ListClients.ItemsSource = strings;
            //внутри имя пользователя, надо добавить в лист с именами
            var ourUser = users.FirstOrDefault(i => i.Value == socket);
            users.Remove(ourUser.Key);
            users.Add($"[{message.Substring(9)}]", socket);
            Message_.Items.Add($"[{message.Substring(9)}] подключился к чату");
            users.Add(message.Substring(9), socket);
        }
        var our11User = users.FirstOrDefault(i => i.Value == socket);
        Message_.Items.Add($"[{our11User.Key}] ({DateTime.Now}): {message}");





    }
}
public  async Task Send(string message)
{
    byte[] bytes = Encoding.UTF8.GetBytes(message);
    await socket.SendAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
}*/
    }
}
