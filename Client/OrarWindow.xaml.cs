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
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for OrarWindow.xaml
    /// </summary>
    public partial class OrarWindow : Window
    {
        public OrarWindow()
        {
            InitializeComponent();
            Init();
            titlu.Text = CClient.name;
        }
            private void Init()
            {
         
                foreach (var item in CClient.orar)
                {
              

                    StringBuilder materie = new StringBuilder("TB_");
                    StringBuilder day = new StringBuilder("TB_1_");
                    day.Append(item.Nr_Row.ToString());
                    day.Append("_D");

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
                    var TBday = (TextBlock)FindName(day.ToString());
                    TBday.Text = item.Zi.ToString("dd.MM.yyyy");
          

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

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }
    }
    }
