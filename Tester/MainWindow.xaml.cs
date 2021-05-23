using OBSWS;
using OBSWS.EventTypes;
using OBSWS.Types;
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

namespace Tester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObsConnection obs = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void changeClick(object sender, RoutedEventArgs e)
        {
            obs.setScene((string)tbScenes.SelectedItem);
        }

        private void changescClick(object sender, RoutedEventArgs e)
        {
            obs.setSceneCollection((string)cbSceneCollection.SelectedItem);
        }

        private void connectClick(object sender, RoutedEventArgs e)
        {
            if(btnCDC.Content.Equals("Connect"))
            {
                if (!string.IsNullOrEmpty(tbIp.Text) && !string.IsNullOrEmpty(tbPort.Text))
                {
                    obs = new ObsConnection(tbIp.Text, tbPort.Text, tbPass.Text);

                    obs.onConnect += onConnection;
                    obs.onDisconnect += onDisconnection;
                    obs.onError += onError;
                    obs.onInformation += onInformation;

                    obs.onActiveSceneChanged += onActiveSceneChanged;
                    obs.onCustomMessage += onCustomMessage;
                    obs.onSceneCollectionChanged += onSceneCollecionChanged;
                    obs.onSceneCollectionListChanged += onSceneCollectionListChanged;
                    obs.onSceneListChanged += onSceneListChanged;

                    obs.connect();
                }
            }
            else
            {
                obs.disconnect();
            }
        }

        private void sendClick(object sender, RoutedEventArgs e)
        {

            obs.sendChat(tbChatMessage.Text);
        }

        private void versionClick(object sender, RoutedEventArgs e)
        {
            lblObsVersion.Content = "OBS Studio: " + obs.getObsVersion();
            lblWsVersion.Content = "Plugin: " + obs.getObsWsVersion();
            lblVersion.Content = "OBS WS: " + obs.getVersion();
        }

        private void onConnection(object sender, Connected connection)
        {
            this.Dispatcher.Invoke(() =>
            {
                tbOutput.Text += "CONNECTED: " +
                                 connection.message + "\n";

                btnCDC.Content = "Disconnect";
            });
        }

        private void onCustomMessage(object sender, CustomMessage message)
        {
            this.Dispatcher.Invoke(() =>
            {
                switch (message.messagetype)
                {
                    case CustomType.chat:
                        {
                            tbChatOutput.Text += (string)message.data + "\n\n";
                            break;
                        }

                    case CustomType.chatsent:
                        {
                            tbChatOutput.Text += tbChatMessage.Text + "\n\n";
                            tbChatMessage.Clear();
                            break;
                        }

                    default:
                        {
                            tbOutput.Text += "UNKNOWN CUSTOM TYPE: " +
                                             message.messagetype + "\n";
                            break;
                        }
                }
            });
        }

        private void onDisconnection(object sender, Disconnected disconnection)
        {
            this.Dispatcher.Invoke(() =>
            {
                tbOutput.Text += "DISCONNECTED: " +
                                 disconnection.message + "\n";

                btnCDC.Content = "Connect";
            });
        }

        private void onError(object sender, Error error)
        {
            this.Dispatcher.Invoke(() =>
            {
                tbOutput.Text += "ERROR: " + 
                                 error.message + "\n";
            });
        }

        private void onInformation(object sender, Information info)
        {
            this.Dispatcher.Invoke(() =>
            {
                tbOutput.Text += "INFORMATION: " +
                                 info.message + "\n";
            });
        }


        private void onActiveSceneChanged(object sender, Scene active)
        {
            this.Dispatcher.Invoke(() =>
            {
                lblActiveScene.Content = "Current scene: " + active.name;
                tbOutput.Text += "CHANGED SCENE TO: " +
                                 active.name + "\n";
            });
        }

        private void onSceneListChanged(object sender, List<Scene> scenes)
        {
            this.Dispatcher.Invoke(() =>
            {
                tbScenes.Items.Clear();

                foreach(Scene scene in scenes) 
                {
                    tbScenes.Items.Add(scene.name);
                    tbScenes.SelectedIndex = 0;
                }
            });
        }

        private void onSceneCollecionChanged(object sender, string scname)
        {
            this.Dispatcher.Invoke(() =>
            {
                lblActiveCollection.Content = "Current scene collection: " + scname;
                tbOutput.Text += "CHANGED SCENECOLLECTION TO: " +
                                 scname + "\n";
            });
        }

        private  void onSceneCollectionListChanged(object sender, List<string> scenecollections)
        {
            this.Dispatcher.Invoke(() =>
            {
                cbSceneCollection.Items.Clear();

                foreach (string sc in scenecollections)
                {
                    cbSceneCollection.Items.Add(sc);
                    cbSceneCollection.SelectedIndex = 0;
                }
            });
        }
    }
}
