using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Мессенджер
{
    public class MyServer
    {
        //private static Page_Admin page = new Page_Admin("");
        CancellationToken token;
        private Socket _socket;
        public static Socket _client;
        private List<Socket> clients = new List<Socket>();
        public static CancellationTokenSource g;
        static Task task;
        static Dictionary<string, Socket> users = new Dictionary<string, Socket>();
        // MyServer  myServer = new MyServer();
        static List<string> strings = new List<string>();
        public void Into(string a, ListBox Message_, ListBox ListClients)
        {
            g = new CancellationTokenSource();
            IPEndPoint iP = new IPEndPoint(IPAddress.Any, 7777);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(iP);
            _socket.Listen(1000);
            ListenClient(Message_,ListClients);

            _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _client.Connect("127.0.0.1", 7777);
            ReverseMessage(_client, Message_, ListClients);

            strings.Add($"[{a}]");
            ListClients.ItemsSource = strings;
            SendMassage(_client, $"/username[{a}]");
        }
        private async Task ListenClient(ListBox Message_, ListBox ListClients)
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
                ReverseMessage(client,Message_,ListClients);


            }
        }
        private async Task ReverseMessage(Socket client, ListBox Message_,ListBox ListClients)
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
        public async Task SendMassage(Socket client, string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
        }
    }
}
