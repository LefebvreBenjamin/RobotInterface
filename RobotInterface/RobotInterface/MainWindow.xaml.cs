using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RobotInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


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

        public void sendMsg()
        {

            string msg = textBoxEmission.Text;
            msg.Replace("\n", "");
            msg.Replace("\r", "");
            if (textBoxReception.Text == "")
            {
                textBoxReception.Text = "Recu : " + msg;
            }
            else
            {
                textBoxReception.Text += "\nRecu : " + msg;
            }
            textBoxEmission.Text = "";
            
        }

        private void textBoxEmission_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBoxEmission_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                sendMsg();
            }
        }
    }
}