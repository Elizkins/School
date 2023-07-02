using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace School.Database
{
    public partial class Service : INotifyPropertyChanged, ICloneable
    {
        private string title;
        private decimal? cost;
        private string mainImagePath;
        private int? durationInSeconds;
        private double? discount;

        public event PropertyChangedEventHandler PropertyChanged;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public Nullable<decimal> Cost
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
                OnPropertyChanged(nameof(Cost));
            }
        }
        public string MainImagePath
        {
            get
            {
                return mainImagePath;
            }
            set
            {
                mainImagePath = value;
                OnPropertyChanged(nameof(MainImagePath));
                OnPropertyChanged(nameof(Image));
            }
        }
        public Nullable<int> DurationInSeconds
        {
            get
            {
                return durationInSeconds;
            }
            set
            {
                durationInSeconds = value;
                OnPropertyChanged(nameof(DurationInSeconds));
            }
        }
        public Nullable<double> Discount
        {
            get
            {
                return discount;
            }
            set
            {
                discount = value;
                OnPropertyChanged(nameof(Discount));
            }
        }
        public BitmapImage Image
        {
            get
            {
                if(MainImagePath != null)
                {
                    string path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Environment.CurrentDirectory)) + "\\Assets\\" + MainImagePath;
                    return new BitmapImage(new Uri(path));
                }
                return null;
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public object Clone()
        {
            return new Service
            {
                Id = Id,
                Title = Title,
                Cost = Cost,
                DurationInSeconds = DurationInSeconds,
                Description = Description,
                Discount = Discount,
                MainImagePath = MainImagePath,
                ServicePhotoes = ServicePhotoes
            };
        }
    }
}
