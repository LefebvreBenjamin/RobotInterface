using ExtendedSerialPort_NS;
using System.IO.Ports;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RobotInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary

    public partial class MainWindow : Window
    {
        ExtendedSerialPort serialPort1;
        Robot robot = new Robot();
        DispatcherTimer timerAffichage;
        byte[] byteList = new byte[20]; // tablau de 20 byute



        public MainWindow()
        {
            // Port, vitesse, parité, bits de données, bits de stop
            serialPort1 = new ExtendedSerialPort("COM5", 115200, Parity.None, 8, StopBits.One);

            serialPort1.Open();
            serialPort1.DataReceived += SerialPort1_DataReceived;

            timerAffichage = new DispatcherTimer();
            timerAffichage.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timerAffichage.Tick += TimerAffichage_Tick;
            timerAffichage.Start();

            InitializeComponent();
        }

        private void TimerAffichage_Tick(object? sender, EventArgs e)
        {
            // Tant qu y a des octets
            while (robot.byteListReceived.Count > 0)
            {
                byte octet = robot.byteListReceived.Dequeue(); // retire le plus ieuv
                textBoxReception.Text += octet.ToString("X2") + " ";
            }
        }

        public void SerialPort1_DataReceived(object sender, DataReceivedArgs e)
        {
            for (int i = 0; i < e.Data.Length; i++)

            {
                robot.byteListReceived.Enqueue(e.Data[i]);
            }
            robot.receivedText += Encoding.UTF8.GetString(e.Data, 0, e.Data.Length);
        }


        private void buttonEnvoyer_Click(object sender, RoutedEventArgs e)
        {
            sendMsg();

            if (buttonEnvoyer.Background == Brushes.RoyalBlue)
            {
                buttonEnvoyer.Background = Brushes.Beige;
            }
            else
            {
                buttonEnvoyer.Background = Brushes.RoyalBlue;
            }
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            clearMsgs();
        }

        public void sendMsg()
        {
            string msg = textBoxEmission.Text.Trim();
            serialPort1.WriteLine(msg);
            textBoxEmission.Text = "";
        }


        public void clearMsgs()
        {
            textBoxReception.Text = "";
        }

        private void textBoxEmission_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                sendMsg();
            }
        }

        private void buttonTester_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < byteList.Length; i++)

            {
                byteList[i] = (byte)(2 * i);
            }
            serialPort1.Write(byteList, 0, byteList.Length);

        }
    }
}
