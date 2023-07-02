using School.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private Service service;

        public OrderPage(Service service)
        {
            InitializeComponent();

            this.service = service;
            this.DataContext = this.service;
            datePicker.SelectedDate = DateTime.Now;

            MainWindow.Context.Clients.Load();
            clientComboBox.ItemsSource = MainWindow.Context.Clients.Local;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (((sender as ComboBox).Parent as Grid).Children[0] as TextBoxExtended).Text = (sender as ComboBox).SelectedItem.ToString();
        }

        private void SaveOrder(object sender, RoutedEventArgs e)
        {
            if (clientComboBox.SelectedIndex != -1 && datePicker.SelectedDate != null && !string.IsNullOrEmpty(startBox.Text) &&
                startBox.Foreground.ToString() != ((SolidColorBrush)new BrushConverter().ConvertFrom("#ff0000")).ToString())
            {
                DateTime date = (DateTime)datePicker.SelectedDate;
                TimeSpan time = TimeSpan.ParseExact(startBox.Text, @"hh\:mm", CultureInfo.CurrentCulture, TimeSpanStyles.None);
                DateTime dateTime = date.Add(time);
                ClientService clientService = new ClientService
                {
                    Client = clientComboBox.SelectedItem as Client,
                    Service = this.service,
                    StartTime = dateTime
                };
                MainWindow.Context.ClientServices.Add(clientService);
                MainWindow.Context.SaveChanges();
                new DialogWindow(new Message("Данные успешно обновлены", "", "ОК")).ShowDialog();
                this.NavigationService.GoBack();
            }
            else
            {
                new DialogWindow(new Message("Введите корректные данные", "", "ОК")).ShowDialog();
            }
        }

        private void TextCheck(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsNumber(e.Text, 0) && e.Text != ":")
            {
                e.Handled = true;
            }
        }


        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch((sender as TextBoxExtended).Text, @"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$"))
            {
                (sender as TextBoxExtended).Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#ff0000");
                closeBox.Text = "00:00";
            }
            else
            {
                (sender as TextBoxExtended).Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#000000");
                TimeSpan time = TimeSpan.ParseExact((sender as TextBoxExtended).Text, @"hh\:mm", CultureInfo.CurrentCulture, TimeSpanStyles.None);
                time = time.Add(new DateTime().AddMinutes((double)this.service.DurationInSeconds) - new DateTime());
                closeBox.Text = time.ToString(@"hh\:mm");
            }
        }
    }
}
