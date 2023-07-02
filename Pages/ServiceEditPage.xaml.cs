using Microsoft.Win32;
using School.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Логика взаимодействия для ServiceEditPage.xaml
    /// </summary>
    public partial class ServiceEditPage : Page
    {
        private string imageSourse;
        private Service service;
        private Service serviceClone;
        private ObservableCollection<Service> source;
        private ObservableCollection<ServicePhoto> imageList;

        public ServiceEditPage(Service service, ObservableCollection<Service> source)
        {
            InitializeComponent();

            this.service = service;
            this.source = source;

            if (this.service.Id == 0)
            {
                idBox.Visibility = Visibility.Collapsed;
                image.Visibility = Visibility.Collapsed;
            }
            serviceClone = (Service)this.service.Clone();
            this.DataContext = serviceClone;

            imageSourse = serviceClone.MainImagePath;

            imageList = new ObservableCollection<ServicePhoto>(serviceClone.ServicePhotoes);
            imagesListView.ItemsSource = imageList;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void LoadImages(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png"
            };
            if (dlg.ShowDialog() == true)
            {
                imageList.Add(new ServicePhoto
                {
                    PhotoPath = dlg.FileName.Split('\\').Last()
                });
                try
                {
                    File.Copy(dlg.FileName, System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Environment.CurrentDirectory)) + "\\Assets\\" + dlg.FileName.Split('\\').Last(), true);
                }
                catch
                {
                }
            }
        }

        private void SaveService(object sender, RoutedEventArgs e)
        {
            decimal cost;
            int duration;
            double sale;
            if (!string.IsNullOrEmpty(titleBox.Text) && Int32.TryParse(durationBox.Text, out duration) && Decimal.TryParse(costBox.Text, out cost) && duration > 0 && duration < 241 && imageSourse != null)
            {
                if (MainWindow.Context.Services.Where(t => t.Title == titleBox.Text).FirstOrDefault() != null && this.service.Id == 0)
                {
                    new DialogWindow(new Message("Такая услуга уже существует в системе", "", "ОК")).ShowDialog();
                }
                else
                {
                    this.service.Title = titleBox.Text;
                    this.service.Description = descriptionBox.Text;
                    this.service.Cost = cost;
                    this.service.DurationInSeconds = duration;
                    this.service.Discount = Double.TryParse(saleBox.Text, out sale) == true ? sale : 0;
                    this.service.MainImagePath = imageSourse.Split('\\').Count() > 0 ? imageSourse.Split('\\').Last() : serviceClone.MainImagePath;

                    ObservableCollection<ServicePhoto> products = new ObservableCollection<ServicePhoto>(service.ServicePhotoes);
                    foreach (var p in products)
                    {
                        MainWindow.Context.ServicePhotoes.Remove(p);
                    }

                    foreach (var i in imageList)
                    {
                        ServicePhoto servicePhoto = new ServicePhoto
                        {
                            Service = service,
                            PhotoPath = i.PhotoPath
                        };
                        MainWindow.Context.ServicePhotoes.Add(servicePhoto);
                    }

                    if (this.service.Id == 0)
                    {
                        MainWindow.Context.Services.Add(this.service);
                        source.Add(this.service);
                    }
                    MainWindow.Context.SaveChanges();
                    new DialogWindow(new Message("Данные успешно обновлены", "", "ОК")).ShowDialog();
                    this.NavigationService.GoBack();
                }
            }
            else
            {
                new DialogWindow(new Message("Введите корректные данные", "", "ОК")).ShowDialog();
            }
        }

        private void DeletePhoto(object sender, RoutedEventArgs e)
        {
            if((bool)new DialogWindow(new Message("Вы хотите удалить фото?", "Нет", "Да")).ShowDialog())
            {
                imageList.Remove((sender as Button).DataContext as ServicePhoto);
            }
        }

        private void LoadMainImages(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png"
            };
            if (dlg.ShowDialog() == true)
            {
                imageSourse = dlg.FileName;
                imageBrush.ImageSource = new BitmapImage(new Uri(dlg.FileName));
                image.Visibility = Visibility.Visible;
                try
                {
                    File.Copy(imageSourse, System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Environment.CurrentDirectory)) + "\\Assets\\" + imageSourse.Split('\\').Last(), true);
                }
                catch
                {
                }
            }
        }
    }
}
