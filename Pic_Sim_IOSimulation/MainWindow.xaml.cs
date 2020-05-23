using System;
using System.IO.Ports;
using System.Windows;
using System.Windows.Media;

namespace Pic_Sim_IOSimulation
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class HwSimGui : Window
    {
        Controller ctr;  

        public HwSimGui()
        {
            InitializeComponent();
            this.searchPorts();
            ctr = new Controller(this);
        }

        private void searchPorts()
        {
            string[] ports = SerialPort.GetPortNames();

            this.comboBox_ports.Items.Clear();

            foreach (string s in ports)
            {
                this.comboBox_ports.Items.Add(s);
            }
            if (ports.Length == 0)
            {
                this.comboBox_ports.Items.Add("No devices found");
            }
            this.comboBox_ports.SelectedIndex = 0;
        }
        private void btn_SimuConnect(object sender, RoutedEventArgs e)
        {
            string ip = this.txtBox_IP.Text;
            int port = Convert.ToInt32(this.txtBox_Port.Text);

            ctr.connectToSimu(sender, e, ip, port);

        }

        public void updateIO(string responseData)
        {
            string[] received = responseData.Split(':');
            this.ctr.raReceive = Int32.Parse(received[0]);
            this.ctr.rbReceive = Int32.Parse(received[1]);

            if ((ctr.raReceive & 0x01) == 1)
            {
                this.cvRA0.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                this.cvRA0.Background = new SolidColorBrush(Colors.Red);
            }

            if (((ctr.raReceive >> 1) & 0x01) == 1)
            {
                this.cvRA1.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                this.cvRA1.Background = new SolidColorBrush(Colors.Red);
            }

            if (((ctr.raReceive >> 2) & 0x01) == 1)
            {
                this.cvRA2.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                this.cvRA2.Background = new SolidColorBrush(Colors.Red);
            }

            if (((ctr.raReceive >> 3) & 0x01) == 1)
            {
                this.cvRA3.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                this.cvRA3.Background = new SolidColorBrush(Colors.Red);
            }

            if (((ctr.raReceive >> 4) & 0x01) == 1)
            {
                this.cvRA4.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                this.cvRA4.Background = new SolidColorBrush(Colors.Red);
            }

            if ((ctr.rbReceive & 0x01) == 1)
            {
                this.cvRB0.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                this.cvRB0.Background = new SolidColorBrush(Colors.Red);
            }
            if (((ctr.rbReceive >> 1) & 0x01) == 1)
            {
                this.cvRB1.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                this.cvRB1.Background = new SolidColorBrush(Colors.Red);
            }
            if (((ctr.rbReceive >> 2) & 0x01) == 1)
            {
                this.cvRB2.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                this.cvRB2.Background = new SolidColorBrush(Colors.Red);
            }
            if (((ctr.rbReceive >> 3) & 0x01) == 1)
            {
                this.cvRB3.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                this.cvRB3.Background = new SolidColorBrush(Colors.Red);
            }
            if (((ctr.rbReceive >> 4) & 0x01) == 1)
            {
                this.cvRB4.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                this.cvRB4.Background = new SolidColorBrush(Colors.Red);
            }
            if (((ctr.rbReceive >> 5) & 0x01) == 1)
            {
                this.cvRB5.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                this.cvRB5.Background = new SolidColorBrush(Colors.Red);
            }
            if (((ctr.rbReceive >> 6) & 0x01) == 1)
            {
                this.cvRB6.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                this.cvRB6.Background = new SolidColorBrush(Colors.Red);
            }
            if (((ctr.rbReceive >> 7) & 0x01) == 1)
            {
                this.cvRB7.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                this.cvRB7.Background = new SolidColorBrush(Colors.Red);
            }
        }


        private void btn_ChangeRA1(object sender, RoutedEventArgs e)
        {
            if (((this.ctr.raSend >> 1) & 0x01) == 1)
            {
                this.ctr.raSend = this.ctr.raSend & 0xFD;
            }
            else
            {
                this.ctr.raSend = this.ctr.raSend | 0x02;
            }
        }

        private void btn_ChangeRA2(object sender, RoutedEventArgs e)
        {
            if (((this.ctr.raSend >> 2) & 0x01) == 1)
            {
                this.ctr.raSend = this.ctr.raSend & 0xFB;
            }
            else
            {
                this.ctr.raSend = this.ctr.raSend | 0x04;
            }
        }

        private void btn_ChangeRA3(object sender, RoutedEventArgs e)
        {
            if (((this.ctr.raSend >> 3) & 0x01) == 1)
            {
                this.ctr.raSend = this.ctr.raSend & 0xF7;
            }
            else
            {
                this.ctr.raSend = this.ctr.raSend | 0x08;
            }
        }

        private void btn_ChangeRA4(object sender, RoutedEventArgs e)
        {
            if (((this.ctr.raSend >> 4) & 0x01) == 1)
            {
                this.ctr.raSend = this.ctr.raSend & 0xEF;
            }
            else
            {
                this.ctr.raSend = this.ctr.raSend | 0x10;
            }

        }

        private void btn_ChangeMCLR(object sender, RoutedEventArgs e)
        {
            if (this.ctr.mclr == 1)
            {
                this.ctr.mclr = 0;
                this.cvMCLR.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                this.ctr.mclr = 1;
                this.cvMCLR.Background = new SolidColorBrush(Colors.Green);
            }
        }

        private void btn_ChangeVss(object sender, RoutedEventArgs e)
        {
            if (this.ctr.vss == 1)
            {
                this.ctr.vss = 0;
                this.cvVSS.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                this.ctr.vss = 1;
                this.cvVSS.Background = new SolidColorBrush(Colors.Green);
            }
        }

        private void btn_ChangeRB0(object sender, RoutedEventArgs e)
        {
            if ((this.ctr.rbSend & 0x01) == 1)
            {
                this.ctr.rbSend = this.ctr.rbSend & 0xFE;
            }
            else
            {
                this.ctr.rbSend = this.ctr.rbSend | 0x01;
            }
        }

        private void btn_ChangeRB1(object sender, RoutedEventArgs e)
        {
            if (((this.ctr.rbSend >> 1) & 0x01) == 1)
            {
                this.ctr.rbSend = this.ctr.rbSend & 0xFD;
            }
            else
            {
                this.ctr.rbSend = this.ctr.rbSend | 0x02;
            }
        }

        private void btn_ChangeRB2(object sender, RoutedEventArgs e)
        {
            if (((this.ctr.rbSend >> 2) & 0x01) == 1)
            {
                this.ctr.rbSend = this.ctr.rbSend & 0xFB;
            }
            else
            {
                this.ctr.rbSend = this.ctr.rbSend | 0x04;
            }
        }

        private void btn_ChangeRB3(object sender, RoutedEventArgs e)
        {
            if (((this.ctr.rbSend >> 3) & 0x01) == 1)
            {
                this.ctr.rbSend = this.ctr.rbSend & 0xF7;
            }
            else
            {
                this.ctr.rbSend = this.ctr.rbSend | 0x08;
            }
        }

        private void btn_ChangeRA0(object sender, RoutedEventArgs e)
        {
            if ((this.ctr.raSend & 0x01) == 1)
            {
                this.ctr.raSend = this.ctr.raSend & 0xFE;
            }
            else
            {
                this.ctr.raSend = this.ctr.raSend | 0x01;
            }
        }

        private void btn_ChangeOSC1(object sender, RoutedEventArgs e)
        {
            if (this.ctr.osc1 == 1)
            {
                this.ctr.osc1 = 0;
                this.cvOSC1.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                this.ctr.osc1 = 1;
                this.cvOSC1.Background = new SolidColorBrush(Colors.Green);
            }
        }

        private void btn_ChangeOSC2(object sender, RoutedEventArgs e)
        {
            if (this.ctr.osc2 == 1)
            {
                this.ctr.osc2 = 0;
                this.cvOSC2.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                this.ctr.osc2 = 1;
                this.cvOSC2.Background = new SolidColorBrush(Colors.Green);
            }
        }

        private void btn_ChangeVDD(object sender, RoutedEventArgs e)
        {
            if (this.ctr.vdd == 1)
            {
                this.ctr.vdd = 0;
                this.cvVDD.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                this.ctr.vdd = 1;
                this.cvVDD.Background = new SolidColorBrush(Colors.Green);
            }
        }

        private void btn_ChangeRB7(object sender, RoutedEventArgs e)
        {
            if (((this.ctr.rbSend >> 7) & 0x01) == 1)
            {
                this.ctr.rbSend = this.ctr.rbSend & 0x7F;
            }
            else
            {
                this.ctr.rbSend = this.ctr.rbSend | 0x80;
            }
        }

        private void btn_ChangeRB6(object sender, RoutedEventArgs e)
        {
            if (((this.ctr.rbSend >> 6) & 0x01) == 1)
            {
                this.ctr.rbSend = this.ctr.rbSend & 0xBF;
            }
            else
            {
                this.ctr.rbSend = this.ctr.rbSend | 0x40;
            }
        }

        private void btn_ChangeRB5(object sender, RoutedEventArgs e)
        {
            if (((this.ctr.rbSend >> 5) & 0x01) == 1)
            {
                this.ctr.rbSend = this.ctr.rbSend & 0xDF;
            }
            else
            {
                this.ctr.rbSend = this.ctr.rbSend | 0x20;
            }
        }

        private void btn_ChangeRB4(object sender, RoutedEventArgs e)
        {
            if (((this.ctr.rbSend >> 4) & 0x01) == 1)
            {
                this.ctr.rbSend = this.ctr.rbSend & 0xEF;
            }
            else
            {
                this.ctr.rbSend = this.ctr.rbSend | 0x10;
            }
        }

        private void Btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            this.searchPorts();
        }

        private void Btn_ConnectPort_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Selected Port: "+this.comboBox_ports.SelectedItem.ToString());
            ctr.hwConnect(this.comboBox_ports.SelectedItem.ToString(),9600);
        }

        private void Btn_SimuDisconnect_Click(object sender, RoutedEventArgs e)
        {
            ctr.disconnectFromSimu();
        }
    }
}
