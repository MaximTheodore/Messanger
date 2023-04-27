using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для Page_Admin.xaml
    /// </summary>
    public partial class Page_Admin : Page
    {
       /* CancellationToken token;
        private Socket _socket;
        private Socket _client;
        private List<Socket> clients = new List<Socket>();
        static CancellationTokenSource g;
        static Task task;
        static Dictionary<string, Socket> users = new Dictionary<string, Socket>();
        // MyServer  myServer = new MyServer();
        static List<string> strings = new List<string>();*/
       MyServer server = new MyServer();
        public Page_Admin(string a)
        {
            InitializeComponent();
            (Application.Current.MainWindow as MainWindow).Width = 800;
            (Application.Current.MainWindow as MainWindow).Height = 450;
            /* IPEndPoint iP = new IPEndPoint(IPAddress.Any, 7777);
             g = new CancellationTokenSource();
             _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
             _socket.Bind(iP);
             _socket.Listen(1000);
             ListenClient();

             _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
             _client.Connect("127.0.0.1", 7777);
             ReverseMessage(_client);

             strings.Add($"[{a}]");
             ListClients.ItemsSource = strings;
             SendMassage(_client,$"/username[{a}]");*/
            server.Into(a, Message_, ListClients);

        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Lett.Text == "/disconnect")
            {
                MyServer.g.Cancel();
                (Application.Current.MainWindow as MainWindow).Width = 400;
                (Application.Current.MainWindow as MainWindow).Height = 400;
                (Application.Current.MainWindow as MainWindow).Page.Content = new Server();
            }
            else
            {
                await server.SendMassage(MyServer._client, Lett.Text);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            MyServer.g.Cancel();
            (Application.Current.MainWindow as MainWindow).Page.Content = new Server();
            (Application.Current.MainWindow as MainWindow).Width = 400;
            (Application.Current.MainWindow as MainWindow).Height = 400;
        }
       /* private async Task ListenClient()
        {
            while (!g.IsCancellationRequested)
            {
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }
                var client = await _socket.AcceptAsync();
                if (client.RemoteEndPoint.ToString() != _client.LocalEndPoint.ToString())
                {
                    clients.Add(client);
                }
                    users.Add("", client);
                ReverseMessage(client);


            }
        }
        private async Task ReverseMessage(Socket client)
        {
            while (!g.IsCancellationRequested)
            {
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }
                byte[] bytes = new byte[1024];
                await client.ReceiveAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);
                if (message.StartsWith("/username"))
                {
                    //Send("/username" + a);
                    strings.Add($"[{message.Substring(9)}]");

                    ListClients.ItemsSource = null;
                    ListClients.ItemsSource = strings;
                    var ourUser = users.FirstOrDefault(i => i.Value == client);
                    users.Remove(ourUser.Key);
                    users.Add($"[{message.Substring(9)}]", client);

                    //внутри имя пользователя, надо добавить в лист с именами
                    Message_.Items.Add($"[{message.Substring(9)}] подключился к чату");

                }
                var our11User = users.FirstOrDefault(i => i.Value == client);
                                Message_.Items.Add($"[{our11User.Key}] ({DateTime.Now}): {message}");
                foreach (var item in clients)
                {
                    SendMassage(item, message);
                }

            }
        }
        private async Task SendMassage(Socket client, string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
        }*/
    }
}
