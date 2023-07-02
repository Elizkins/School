using School.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        private ObservableCollection<Service> sourse;

        public ServicesPage(bool isAdmin)
        {
            InitializeComponent();

            if (!isAdmin)
            {
                addButton.Visibility = Visibility.Collapsed;
                servicesListView.ItemTemplate = (DataTemplate)this.FindResource("ClientStyle");
                ordersButton.Visibility = Visibility.Collapsed;
            }

            sortingComboBox.ItemsSource = new List<string>() { "По возрастанию", "По убыванию" };
            saleComboBox.ItemsSource = new List<string>() { "Все", "0% - 5%", "5% - 15%", "15% - 30%", "30% - 70%", "70% - 100%" };

            MainWindow.Context.Services.Load();
            sourse = MainWindow.Context.Services.Local;
            servicesListView.ItemsSource = sourse;

            finalCount.Text = $"{servicesListView.Items.Count} из {MainWindow.Context.Services.Count()}";
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            Service service = (sender as Button).DataContext as Service;
            if (service.ClientServices.Count > 0)
            {
                new DialogWindow(new Message("На данную услугу присутствуют записи", "", "Ок")).ShowDialog();
            }
            else if ((bool)new DialogWindow(new Message("Удалить услугу?", "Нет", "Да")).ShowDialog())
            {
                MainWindow.Context.ServicePhotoes.RemoveRange(service.ServicePhotoes);
                MainWindow.Context.Services.Remove(service);
                MainWindow.Context.SaveChanges();
                sourse.Remove(service);
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            Service service = new Service();
            this.NavigationService.Navigate(new ServiceEditPage(service, sourse));
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ServiceEditPage((sender as Button).DataContext as Service, sourse));
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (((sender as ComboBox).Parent as Grid).Children[0] as TextBoxExtended).Text = (sender as ComboBox).SelectedItem.ToString();
            GetDataWithTreatment();
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            GetDataWithTreatment();
        }

        private void GetDataWithTreatment()
        {
            switch (saleComboBox.SelectedIndex)
            {
                case 0:
                    sourse = new ObservableCollection<Service>(sourse);
                    break;
                case 1:
                    sourse = new ObservableCollection<Service>(sourse.Where(s => ((int)s.Discount.Value) >= 0 && (int)s.Discount.Value < 5));
                    break;
                case 2:
                    sourse = new ObservableCollection<Service>(sourse.Where(s => ((int)s.Discount.Value) >= 5 && (int)s.Discount.Value < 15));
                    break;
                case 3:
                    sourse = new ObservableCollection<Service>(sourse.Where(s => ((int)s.Discount.Value) >=15 && (int)s.Discount.Value < 30));
                    break;
                case 4:
                    sourse = new ObservableCollection<Service>(sourse.Where(s => ((int)s.Discount.Value) >= 30 && (int)s.Discount.Value < 70));
                    break;
                case 5:
                    sourse = new ObservableCollection<Service>(sourse.Where(s => ((int)s.Discount.Value) >= 70 && (int)s.Discount.Value < 100));
                    break;
                default:
                    break;
            }
            sourse = new ObservableCollection<Service>(sourse.Where(s => s.Title.Contains(searchBox.Text) || (s.Description?.Contains(searchBox.Text) ?? false)));

            switch (sortingComboBox.SelectedIndex)
            {
                case 0:
                    sourse = new ObservableCollection<Service>(sourse.OrderBy(s => s.Cost));
                    break;
                case 1:
                    sourse = new ObservableCollection<Service>(sourse.OrderByDescending(s => s.Cost));
                    break;
                default:
                    break;
            }

            servicesListView.ItemsSource = sourse;
            finalCount.Text = $"{servicesListView.Items.Count} из {MainWindow.Context.Services.Count()}";

            MainWindow.Context.Services.Load();
            sourse = MainWindow.Context.Services.Local;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void GoToOrderPage(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new OrderPage((sender as Border).DataContext as Service));
        }

        private void GoToOrderListPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new OrderListPage());
        }
    }
}
