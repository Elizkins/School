using School.Database;
using School.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Логика взаимодействия для OrderListPage.xaml
    /// </summary>
    public partial class OrderListPage : Page
    {
        private Timer timer;
        public OrderListPage()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 30000;
            timer.Elapsed += new ElapsedEventHandler(UpdateFunction);
            timer.Start();
        }

        private void UpdateFunction(object sender, EventArgs e)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                if(this.NavigationService != null)
                {
                    this.NavigationService.Refresh();
                }
                else
                {
                    timer.Close();
                }
            });
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void UpdatePage(object sender, RoutedEventArgs e)
        {
            DateTime today = DateTime.Now;
            DateTime tomorrow = DateTime.Today.AddDays(2);
            ordersListView.ItemsSource = MainWindow.Context.ClientServices.Where(cs => cs.StartTime >= today && cs.StartTime < tomorrow).OrderBy(t => t.StartTime).ToList();
        }
    }
}
