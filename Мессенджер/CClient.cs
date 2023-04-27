using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Мессенджер
{
    internal class CClient
    {
        private static Socket socket;
        static Dictionary<string,Socket> users = new Dictionary<string, Socket>();

        private static List<string> strings = new List<string>();
        
            
        public void Into(string a,ListBox b,ListBox c)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.ConnectAsync("127.0.0.1", 7777);
            Send($"/username{a}");
            ReverseMess(b,c);
            strings.Add($"[{a}]");
            c.Items.Add($"[{a}]");

        }
        private async Task ReverseMess(ListBox Message_,ListBox ListClients)
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                //socket.Receive(bytes);
                await socket.ReceiveAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);
                //Message_.Items.Add(message);
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
        public async Task Send(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await socket.SendAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
        }
    }
}
