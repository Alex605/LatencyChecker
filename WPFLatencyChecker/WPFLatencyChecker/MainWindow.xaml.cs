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
using System.Net.NetworkInformation;

//A ping checker for games which allow users to ping their servers
// Currently only LoL and Pubg, but will add more if I find a work around 
namespace WpfApp10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, string[]> gamesAndServers = new Dictionary<string, string[]>();

        public MainWindow()
        {
            InitializeComponent();
            pingText.Text = "";

            gamesAndServers["League Of Legends"] = new string[] { "North America", "Europe West", "Europe Nordic & East", "Korea", "Brazil", "Oceania" };
            gamesAndServers["Player Unknown's Battlegrounds"] = new string[] { "US East", "US West", "US Central", "EU West", "EU East", "Tokyo", "Seoul", "Singapore", "Australia", "India", "South America" };

            foreach (string game in gamesAndServers.Keys)   //Fills the "Games" drop down list with all the games we can choose
                GamesBox.Items.Add(game);
        }

        //method for finding the ping
        public static double PingAverage(string host, int echoNum)
        {
            try
            {
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(host, 120);
                if (reply.Status == IPStatus.Success)
                {
                    return reply.RoundtripTime;
                }
                return -1;
            }
            catch(Exception)
            {
                return 0;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Ping pingSender = new Ping();
            double temp = 0;
            string chosenGame = GamesBox.SelectedItem.ToString();
            string designatedServer = Servers.SelectedItem.ToString();

            if (chosenGame == "League Of Legends")
            {
                switch (designatedServer)   //Need to include statements to calculate each ping
                {
                    case "North America":
                        temp = PingAverage("104.160.131.3", 10);
                        break;
                    case "Europe West":
                        temp = PingAverage("104.160.141.3", 10);
                        break;
                    case "Europe Nordic & East":
                        temp = PingAverage("104.160.142.3", 10);
                        break;
                    case "Latin America North":
                        temp = PingAverage("104.160.136.3", 10);
                        break;
                    case "Brazil":
                        temp = PingAverage("104.160.152.3", 10);
                        break;
                    case "Oceania":
                        temp = PingAverage("104.160.156.1", 10);
                        break;
                }
            }
            

            if (chosenGame == "Player Unknown's Battlegrounds")
            {
                switch (designatedServer)  
                {
                    case "US East":
                        temp = PingAverage("34.192.0.54", 100);
                        break;
                    case "US West":
                        temp = PingAverage("50.18.56.1", 100);
                        break;
                    case "US Central":
                        temp = PingAverage("35.182.0.251", 100);
                        break;
                    case "EU West":
                        temp = PingAverage("46.51.178.50", 100);
                        break;
                    case "EU East":
                        temp = PingAverage("52.28.63.252", 100);
                        break;
                    case "Tokyo":
                        temp = PingAverage("52.192.63.252", 100);
                        break;
                    case "Seoul":
                        temp = PingAverage("13.124.63.251", 100);
                        break;
                    case "Singapore":
                        temp = PingAverage("46.51.216.14", 100);
                        break;
                    case "Australia":
                        temp = PingAverage("52.62.63.252", 100);
                        break;
                    case "India":
                        temp = PingAverage("13.126.0.252", 100);
                        break;
                    case "South America":
                        temp = PingAverage("52.67.255.254", 100);
                        break;
                }
            }

            if (temp > 0)  
            {
                pingText.Text = temp.ToString() + " ms";
            }
           
            else    //Handling the error where Ping fails
            {
                pingText.Text = "Error: Please try again!";
            }
        }

        private void GamesBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Every time the user chooses a different game, the drop down list will clear and fill with the appropriate servers.
            if (GamesBox.SelectedIndex > -1)
            {
                string[] SelectedServers = gamesAndServers.Values.ElementAt(GamesBox.SelectedIndex);    //Determine the game's index.

                Servers.ItemsSource = SelectedServers;  //Set the server list based on which game is selected
            }
        }

        private void Servers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
