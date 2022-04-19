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
        public int isAdmin;
        private string passwordOfAdmin;
        public Enter(MainWindow mainWindow, int IsAdmin = -1)
        {
            InitializeComponent();

            this.isAdmin = IsAdmin;
            this.Owner = mainWindow;

            mainWindow.SetPositionInScreen(this);
            passwordOfAdmin = "Course2022";
            SetLanguage(mainWindow.language);
        }
        private void SetLanguage(MainWindow.ELanguage language)
        {
            if (language == MainWindow.ELanguage.spanish)
            {
                this.Title = "Usuarios";

                welcomeLabel.Content = "¡Bienvenido a \"El club\", amigo!\nPara entrar elige quién eres.";
                enterAdButton.Content = "Entrar como Administrador";
                enterClButton.Content = "Entrar como Cliente";

                EnterButton.Content = "Ingresar";
                CancelButton.Content = "Cancelar";
                Quit.Content = "Salir del banco";

                password1.Content = "Introducir la contraseña:";

                password2.VerticalContentAlignment = VerticalAlignment.Top;
                password2.Content = "Ingrese de nuevo la\ncontraseña:";
            }
            else if (language == MainWindow.ELanguage.french)
            {
                this.Title = "Utilisateurs";

                welcomeLabel.Content = "Bienvenue \"Au club\", mon pote!\nPour participer, choisissez qui vous êtes.";

                enterAsSP.Width = 340;
                enterAdButton.Width = 175;
                enterAdButton.Content = "Entrer en tant qu'Administrateur";
                enterClButton.Content = "Entrer en tant que Client";

                EnterButton.Content = "Entrer";
                CancelButton.Content = "Annuler";
                Quit.Content = "Quitter la banque";

                password1.Content = "Entrer le mot de passe:";

                password2.VerticalContentAlignment = VerticalAlignment.Top;
                password2.Content = "Saisissez à nouveau le\nmot de passe:";
            }
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
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
