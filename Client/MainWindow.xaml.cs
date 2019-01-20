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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           
                CClient.connect();
                InitializeComponent();
  
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            CClient.name = TBnume.Text;
            CClient.parola = TBparola.Password.ToString();

            CClient.Login();
            System.Threading.Thread.Sleep(500);
            log.Text = CClient.result;
            if (CClient.gate)
                {
                    Home home = new Home();
                    home.Show();
                    this.Close();
                    CClient.gate = false;
            
                }

    
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow window = new RegisterWindow();
            window.Show();
            this.Close();
        }
    }
}
