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

namespace School
{
    /// <summary>
    /// Логика взаимодействия для AdminCheckWindow.xaml
    /// </summary>
    public partial class AdminCheckWindow : Window
    {
        public AdminCheckWindow()
        {
            InitializeComponent();
        }

        private void PasswordCheck(object sender, RoutedEventArgs e)
        {
            if(textBox.Text == "0000")
            {
                this.DialogResult = true;
            }
            else
            {
                new DialogWindow(new Message("Пароль не верный", "", "Ок")).ShowDialog();
            }
        }
    }
}
