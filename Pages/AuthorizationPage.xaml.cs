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

namespace School
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void GoToClientPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ServicesPage(false));
        }

        private void GoToAdminPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ServicesPage(true));

            //if ((bool)new AdminCheckWindow().ShowDialog())
            //{
            //    this.NavigationService.Navigate(new ServicesPage(true));
            //}
        }
    }
}
