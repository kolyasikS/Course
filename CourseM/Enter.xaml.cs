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
    /// Interaction logic for Enter.xaml
    /// </summary>
    public partial class Enter : Window
    {
        public int isAdmin = -1;
        private string passwordOfAdmin;
        public Enter(MainWindow mainWindow)
        {
            InitializeComponent();

            this.Owner = mainWindow;
            passwordOfAdmin = "Course2022";
        }

        private void EnterAdmin(object sender, RoutedEventArgs e)
        {
            password1.Visibility = Visibility.Visible;
            password2.Visibility = Visibility.Visible;
            passwordAttempt1.Visibility = Visibility.Visible;
            passwordAttempt2.Visibility = Visibility.Visible;
            EnterButton.Visibility = Visibility.Visible;
        }

        private void EnterAdministrator(object sender, RoutedEventArgs e)
        {
            if (passwordAttempt1.Password != passwordAttempt2.Password)
            {
                MessageBox.Show("The passwords don`t match!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (passwordAttempt1.Password != passwordOfAdmin)
            {
                MessageBox.Show("You entered a wrong password!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            isAdmin = 1;
            Close();
        }



        private void QuitBank(object sender, RoutedEventArgs e)
        {

            this.Owner.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isAdmin == -1)
            {
                this.Owner.Close();
            }
        }

        private void EnterClient(object sender, RoutedEventArgs e)
        {
            isAdmin = 0;
            Close();
        }
    }
}
