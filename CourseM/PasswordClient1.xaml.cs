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

namespace CourseM
{
    /// <summary>
    /// Interaction logic for PasswordClient1.xaml
    /// </summary>
    public partial class PasswordClient1 : Window
    {
        private string password;
        public bool isChecked = false;
        public PasswordClient1(string password)
        {
            InitializeComponent();

            this.password = password;
            SetPositionInScreen();
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            if (passwordAttempt1.Password != passwordAttempt2.Password)
            {
                MessageBox.Show("The passwords don`t match!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (passwordAttempt1.Password != password)
            {
                MessageBox.Show("You entered a wrong password!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            isChecked = true;
            Close();
        }

        private void SetPositionInScreen()
        {
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 2;
            this.Left = (screenWidth - this.Width) / 2;
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            isChecked = false;
            Close();
        }
    }
}
