using CourseM.Service;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseM
{
    /// <summary>
    /// Interaction logic for SecondBlank.xaml
    /// </summary>
    public partial class SecondBlank : Page
    {
        MainWindow mainwin;
        FirstBlank firstBlank;
        Blank blank;
        private FileIO _fileIO;
        private readonly string PATH;
        DateTime dateTime;
        public SecondBlank(FirstBlank firstBlank, MainWindow mainwin, Blank blank, string path)
        {
            InitializeComponent();

            this.mainwin = mainwin;
            this.blank = blank;
            this.firstBlank = firstBlank;

            PATH = path;

            dateTime = DateTime.Now;

            termDeposit.IsEnabled = false;   

            SetLanguage();
        }
        private void SetLanguage()
        {
            if (mainwin.language == MainWindow.ELanguage.spanish)
            {
                registerButton.Content = "Registro";
                passwordLabel.Content = "Clave:";
                termOfDepLabel.Content = "Plazo de depósito:";
                catOfDepLabel.Content = "Categoría de\ndepósito:";

                ((TextBlock)categoryOfDeposit.Items[0]).Text = "Depósito a la vista";
                ((TextBlock)categoryOfDeposit.Items[1]).Text = "Depósito a plazo";

                ((TextBlock)termDeposit.Items[0]).Text = "1 mes";
                ((TextBlock)termDeposit.Items[1]).Text = "3 meses";
                ((TextBlock)termDeposit.Items[2]).Text = "6 meses";
                ((TextBlock)termDeposit.Items[3]).Text = "1 año";
                ((TextBlock)termDeposit.Items[4]).Text = "3 años";


            }
            else if (mainwin.language == MainWindow.ELanguage.french)
            {
                registerButton.Content = "S'inscrire";
                passwordLabel.Content = "Mot de passe:";
                termOfDepLabel.Content = "Durée du dépôt:";
                catOfDepLabel.Content = "Catégorie\nde dépôt:";

                ((TextBlock)categoryOfDeposit.Items[0]).Text = "Dépôt de la demande";
                ((TextBlock)categoryOfDeposit.Items[1]).Text = "Dépôt à terme";

                ((TextBlock)termDeposit.Items[0]).Text = "1 mois";
                ((TextBlock)termDeposit.Items[1]).Text = "3 mois";
                ((TextBlock)termDeposit.Items[2]).Text = "6 mois";
                ((TextBlock)termDeposit.Items[3]).Text = "1 an";
                ((TextBlock)termDeposit.Items[4]).Text = "3 ans";
            }
        }
        private void ErrorFillOut()
        {
            switch (mainwin.language)
            {
                case MainWindow.ELanguage.english:
                    MessageBox.Show("You didn't fill out all fileds!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case MainWindow.ELanguage.spanish:
                    MessageBox.Show("¡No has rellenado todos los campos!", "Equivocado", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case MainWindow.ELanguage.french:
                    MessageBox.Show("Vous n'avez pas rempli tous les champs!", "Mauvais", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
            }
        }
        private bool CheckData(string genderType, string catOfDeposit, string termOfDep, ref int temp_int, ref double temp_double)
        {
            if (firstBlank.name.Text == "" || firstBlank.surname.Text == ""
                || firstBlank.numOfPass.Text == ""
                || firstBlank.sum.Text == ""
                || categoryOfDeposit.SelectedItem == null || firstBlank.currency.SelectedItem == null
                || termDeposit.SelectedItem == null)
            {
                ErrorFillOut();
                return false;
            }
            else
            {
                for (int i = 0; i < firstBlank.gender.Children.Count; i++)
                {
                    if (!((RadioButton)firstBlank.gender.Children[i]).IsChecked ?? false)
                    {
                        switch (i)
                        {
                            case 0:
                                genderType = "Male";
                                break;
                            case 1:
                                genderType = "Female";
                                break;

                        }
                    }
                }
                if (genderType == "")
                {
                    ErrorFillOut();
                    return false;
                }
            }
            
            switch (categoryOfDeposit.SelectedIndex)
            {
                case 0:
                    catOfDeposit = "Demand deposit";
                    termOfDep = "No term";
                    break;
                case 1:
                    catOfDeposit = "Term deposit";
                    switch (termDeposit.SelectedIndex)
                    {
                        case 0:
                            termOfDep = "1 month";
                            break;
                        case 1:
                            termOfDep = "3 months";
                            break;
                        case 2:
                            termOfDep = "6 months";
                            break;
                        case 3:
                            termOfDep = "1 year";
                            break;
                        case 4:
                            termOfDep = "3 years";
                            break;
                    }
                    break;
            }
            if (passwordField.Password.Length < 6)
            {
                switch (mainwin.language)
                {
                    case MainWindow.ELanguage.english:
                        MessageBox.Show("Password must contain > 5 symbols!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    case MainWindow.ELanguage.spanish:
                        MessageBox.Show("¡La contraseña debe contener más de 5 símbolos!", "Equivocado", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    case MainWindow.ELanguage.french:
                        MessageBox.Show("Le mot de passe doit contenir > 5 symboles !", "Mauvais", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                }
                return false;
            }

            if (!int.TryParse(firstBlank.numOfPass.Text, out temp_int)
                || firstBlank.numOfPass.Text.Length != 9)
            {
                switch (mainwin.language)
                {
                    case MainWindow.ELanguage.english:
                        MessageBox.Show("Passport No has to be nine-digit number!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    case MainWindow.ELanguage.spanish:
                        MessageBox.Show("Pasaporte No tiene que ser un número de nueve dígitos!", "Equivocado", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    case MainWindow.ELanguage.french:
                        MessageBox.Show("Le numéro de passeport doit être un numéro à neuf chiffres !", "Mauvais", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                }
                return false;
            }

            if (!double.TryParse(firstBlank.sum.Text, out temp_double)
                || firstBlank.sum.Text.Length < 5
                || firstBlank.sum.Text.Length > 9)
            {
                switch (mainwin.language)
                {
                    case MainWindow.ELanguage.english:
                        MessageBox.Show("We cannot accept that sum. Read the conditions of our bank again!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    case MainWindow.ELanguage.spanish:
                        MessageBox.Show("No podemos aceptar esa suma. ¡Vuelve a leer las condiciones de nuestro banco!", "Equivocado", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                    case MainWindow.ELanguage.french:
                        MessageBox.Show("Nous ne pouvons pas accepter cette somme. Relisez à nouveau les conditions de notre banque !", "Mauvais", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                }
                return false;
            }

            return true;
        }
        private void CreateClient(object sender, RoutedEventArgs e)
        {
            
            int temp_int = 0;
            double temp_double = 0;
            string genderType = "";
            string catOfDeposit = "";
            string termOfDep = "";
            if (!CheckData(genderType, catOfDeposit, termOfDep, ref temp_int, ref temp_double))
            {
                return;
            }
            string currency;

            switch (((TextBlock)firstBlank.currency.SelectedItem).Text[0])
            {
                case 'P':
                case 'L':
                    currency = "£";
                    break;
                case 'E':
                    currency = "€";
                    break;
                case 'U':
                case 'D':
                    currency = "$";
                    break;
                case 'J':
                case 'Y':
                    currency = "¥";
                    break;
                case 'S':
                case 'W':
                    currency = "₩";
                    break;
                default:
                    return;
            }

            int amountOfM = 0;
            float rateInt = SetInterestR(termOfDep, currency, ref amountOfM);
        

            Client temp_client = new Client(firstBlank.name.Text, firstBlank.surname.Text, dateTime, 
                firstBlank.numOfPass.Text, genderType, Convert.ToDouble(firstBlank.sum.Text), 
                catOfDeposit, termOfDep, passwordField.Password, currency, rateInt, amountOfM);

            mainwin.Clients.Add(temp_client);
            try
            {
                _fileIO.SaveData(mainwin.Clients);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                blank.Close();
            }
            blank.Close();

        }
        private float SetInterestR(string termOfDep, string currency, ref int amountOfM)
        {
            float[,] tableOfInterestsRate = new float[5, 6]
            {
                {0.01f, 0.015f, 0.02f, 0.025f, 0.037f, 0.030f},
                {0.02f, 0.026f, 0.032f, 0.40f, 0.042f, 0.046f},
                {0.025f, 0.032f, 0.039f, 0.044f, 0.046f, 0.053f},
                {0.03f, 0.038f, 0.046f, 0.052f, 0.054f, 0.062f},
                {0.035f, 0.045f, 0.055f, 0.063f, 0.065f, 0.075f}
            };
            float interestRate;
            int rowInTable;
            int ColumnInTable;

            switch (currency[0])
            {
                case '£':
                    rowInTable = 0;
                    break;
                case '€':
                    rowInTable = 1;
                    break;
                case '$':
                    rowInTable = 2;
                    break;
                case '¥':
                    rowInTable = 3;
                    break;
                case '₩':
                    rowInTable = 4;
                    break;
                default:
                    rowInTable = -1;
                    break;
            }
            if (termOfDep == "1 month")
            {
                ColumnInTable = 0;
                amountOfM = 1;
            }
            else if (termOfDep == "3 months")
            {
                ColumnInTable = 1;
                amountOfM = 3;
            }
            else if (termOfDep == "6 months")
            {
                ColumnInTable = 2;
                amountOfM = 6;
            }
            else if (termOfDep == "No term")
            {
                ColumnInTable = 3;
                amountOfM = 12;
            }
            else if (termOfDep == "1 year")
            {
                ColumnInTable = 4;
                amountOfM = 12;
            }
            else
            {
                ColumnInTable = 5;
                amountOfM = 36;
            }

            interestRate = tableOfInterestsRate[rowInTable,ColumnInTable];

            return interestRate;
        }
        private void OpenLastPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(blank.firstBlank);

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIO = new FileIO(PATH);
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            blank.Close();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categoryOfDeposit.SelectedIndex == 0 )
            {
                termDeposit.IsEnabled = false;
            }
            else
            {
                termDeposit.IsEnabled = true;
            }

        }
        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                Help help = new Help();
                help.Show();
            }
        }
    }
}
