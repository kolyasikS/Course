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

        public PasswordClient1(string password, MainWindow.Screen SetPositionInScreen, MainWindow.ELanguage language)
        {
            InitializeComponent();

            this.password = password;
            SetPositionInScreen(this);
            SetLanguage(language);
        }
        private void SetLanguage(MainWindow.ELanguage language)
        {
            if (language == MainWindow.ELanguage.spanish)
            {
                this.Title = "Iniciar";
                enterPassLabel.Content = "Introducir la contraseña:";
                enterPassAgLabel.Content = "Ingrese de nuevo\nla contraseña:";

                cancelButton.Content = "Cancelar";
                logInButton.Content = "Iniciar";
            }
            else if (language == MainWindow.ELanguage.french)
            {
                this.Title = "Connexion";
                enterPassLabel.Content = "Entrer le mot de passe:";
                enterPassAgLabel.Content = "Saisir à nouveau\nle mot de passe:";

                cancelButton.Content = "Annuler";
                logInButton.Content = "Connexion";
            }
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
        private void Cancel(object sender, RoutedEventArgs e)
        {
            isChecked = false;
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                Help help = new Help();
                help.Show();
            }
        }
    }
}
