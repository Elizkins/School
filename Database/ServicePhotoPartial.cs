using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace School.Database
{
    public partial class ServicePhoto
    {
        public BitmapImage Image
        {
            get
            {
                if (PhotoPath != null)
                {
                    string path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(Environment.CurrentDirectory)) + "\\Assets\\" + PhotoPath;
                    return new BitmapImage(new Uri(path));
                }
                return null;
            }
        }
    }
}
