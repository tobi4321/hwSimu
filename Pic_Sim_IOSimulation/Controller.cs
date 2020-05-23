using System;
using System.IO.Ports;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Pic_Sim_IOSimulation
{
    public class Controller
    {
        HwSimGui gui;

        SerialPort sp;
        TcpClient client;
        NetworkStream stream;
        Action action;

        public int raSend;
        public int rbSend;
        public int raReceive;
        public int rbReceive;

        public int mclr;
        public int vss;
        public int vdd;
        public int osc1;
        public int osc2;

        public Controller(HwSimGui pGui)
        {
            gui = pGui;
        }

        public void hwConnect(String pPortName, int pBaudRate)
        {
            try
            {
                sp = new SerialPort();
                sp.PortName = pPortName;
                sp.BaudRate = pBaudRate;
                sp.ReadTimeout = 1000;
                sp.Open();
                MessageBox.Show("Arduino erkannt!");
            }
            catch (Exception)
            {
                MessageBox.Show("HW nicht angeschlossen/erkannt");
            }
        }

        public void connectToSimu(object sender, RoutedEventArgs e,string ip,int port)
        {
            client = new TcpClient(ip, port);

            Console.WriteLine(client.Connected);

            stream = client.GetStream();
            stream.ReadTimeout = 2000;

            action = new Action(() => {
                NetworkStream ns = client.GetStream();
                byte[] receivedBytes = new byte[1024];
                String responseData = String.Empty;
                Action<string> invokeAction = new Action<string>((i) => { gui.updateIO(i); });
                while (client.Connected)
                {
                    string message = raSend.ToString() + ":" + rbSend.ToString() + "\n";
                    Byte[] data;
                    data = new Byte[256];

                    Int32 bytes = ns.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Received: {0}", responseData);

                    // Send the message to the connected TcpServer.
                    data = System.Text.Encoding.ASCII.GetBytes(message);
                    ns.Write(data, 0, data.Length);
                    Console.WriteLine("Sent: {0}", message);

                    gui.Dispatcher.BeginInvoke(invokeAction, responseData);

                    Thread.Sleep(200);
                }
            });

            Task.Run(action);
        }

        public void disconnectFromSimu()
        {
            stream.Close();
            client.Close();
        }



        static void SendData(NetworkStream ns, String s)
        {

            Console.WriteLine("Sending Data\n");
            byte[] buffer = Encoding.ASCII.GetBytes(s);
            ns.Write(buffer, 0, buffer.Length);
            ns.Flush();
        }
    }
}
