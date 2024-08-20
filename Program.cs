using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _20._08._24
{
    internal class Program
    {
        static async Task Main()
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);
            using (Socket tcpListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try {
                    tcpListener.Bind(ipPoint);
                    tcpListener.Listen();
                    Console.WriteLine($"");

                    while (true)
                    {
                        using (var tcpClient = await tcpListener.AcceptAsync())
                        {
                            //byte[] data = Encoding.UTF8.GetBytes(DateTime.Now.ToLongTimeString());
                            //await tcpClient.SendAsync(data);
                            //Console.WriteLine($"");

                            //byte[] responseData = new byte[512];
                            //int bytes = 0;
                            //var response = new StringBuilder();

                            //do
                            //{
                            //    bytes = await tcpClient.ReceiveAsync(responseData);
                            //    response.Append(Encoding.UTF8.GetString(responseData, 0, bytes));
                            //} while (bytes > 0);
                            //Console.WriteLine(response);

                            //var buffer = new List<byte>();
                            //var bytesRead = new byte[1];
                            //while (true)
                            //{
                            //    var count = await tcpClient.ReceiveAsync(bytesRead);
                            //    if (count == 0 || bytesRead[0] == '\n') break;
                            //    var message = Encoding.UTF8.GetString(buffer.ToArray());
                            //    Console.WriteLine($"");
                            //}

                            //byte[] sizeBuffer = new byte[4];
                            //await tcpClient.ReceiveAsync(sizeBuffer);
                            //int size = BitConverter.ToInt32(sizeBuffer, 0);
                            //byte[] data = new byte[size];
                            //int bytes = await tcpClient.ReceiveAsync(data);
                            //var message = Encoding.UTF8.GetString(data, 0, bytes);
                            //Console.WriteLine($"");
                            //Console.WriteLine($"");

                            var buffer = new List<byte>();
                            var bytesRead = new byte[1];
                            while (true)
                            {
                                while (true)
                                {
                                    var count = await tcpClient.ReceiveAsync(bytesRead);
                                    if (count == 0 || bytesRead[0] == '\n') break;
                                    buffer.Add(bytesRead[0]);
                                }
                                var message = Encoding.UTF8.GetString(buffer.ToArray());
                                if (message == "END") break;
                                Console.WriteLine($"");
                                buffer.Clear();
                            }
                        }
                    }
                }
                catch (Exception ex) 
                { 
                    Console.WriteLine(ex.Message);
                }
            }
            Console.ReadLine();
        }
    }
}
