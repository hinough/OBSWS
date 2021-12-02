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

using KrogerDev;

namespace Tester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObsClient obs = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void onEvent(object sender, EventNotification evt)
        {

        } 

        public void onRequest(object sender, RequestNotification req)
        {

        }

        private void btnCDC_Click(object sender, RoutedEventArgs e)
        {
            obs = new ObsClient(tbIP.Text, tbPort.Text, tbPassword.Text);

            obs.connect();
        }
    }
}
