using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
            Nickname.Text = CClient.name;
            Rank.Text = CClient.rank;

            if(!CClient.hasOrar)
            CClient.RequestOrar();
        }
        private void Init()
        {
          
                foreach(var item in CClient.orar)
                {
                StringBuilder materie = new StringBuilder("TB_");
                int x = ++item.Modul;
                materie.Append(x.ToString());
                materie.Append("_");
                materie.Append(item.Nr_Row.ToString());
                materie.Append("_");
                StringBuilder profesor = new StringBuilder();
                profesor.Append(materie);
                profesor.Append("R");
                materie.Append("U");


                var TBmaterie = (TextBlock)FindName(materie.ToString());
                TBmaterie.Text = item.Denumire.ToString();           
                var TBprofesor = (TextBlock)FindName(profesor.ToString());
                TBprofesor.Text = item.Nume.ToString();


           
            }
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }


        private void ListViewItem1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OrarWindow orar = new OrarWindow();
            orar.Show();
            this.Close();
        }

 
    }
}
