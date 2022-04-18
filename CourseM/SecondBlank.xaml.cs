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
        }
        private bool CheckData(string genderType, ref int temp_int, ref double temp_double)
        {
            if (firstBlank.name.Text == "" || firstBlank.surname.Text == "" || firstBlank.numOfPass.Text == ""
                || genderType == "" || firstBlank.sum.Text == ""
                || categoryOfDeposit.SelectedItem == null || firstBlank.currency.SelectedItem == null || (termDeposit.SelectedItem == null
                && ((TextBlock)categoryOfDeposit.SelectedItem).Text == "Term deposit"))
            {
                MessageBox.Show("You didn`t fill out all fileds!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (passwordField.Password.Length < 6)
            {
                MessageBox.Show("Password must contain >5 symbols!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!int.TryParse(firstBlank.numOfPass.Text, out temp_int)
                || firstBlank.numOfPass.Text.Length != 9)
            {
                MessageBox.Show("Passport No has to be none-digit number!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!double.TryParse(firstBlank.sum.Text, out temp_double)
                || firstBlank.sum.Text.Length < 5
                || firstBlank.sum.Text.Length > 9)
            {
                MessageBox.Show("We cannot accept that sum. Reread the conditions of our bank again!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
        private void CreateClient(object sender, RoutedEventArgs e)
        {
            string genderType = firstBlank.genderType;
            int temp_int = 0;
            double temp_double = 0;
            if (!CheckData(genderType, ref temp_int, ref temp_double))
            {
                return;
            }

            string catOfDeposit = ((TextBlock)categoryOfDeposit.SelectedItem).Text;
            string termOfDep;
            if (catOfDeposit == "Demand deposit")
            {
                termOfDep = "No term";
            }
            else
            {
                termOfDep = ((TextBlock)termDeposit.SelectedItem).Text;
            }

            string currency;
            switch (((TextBlock)firstBlank.currency.SelectedItem).Text[0])
            {
                case 'P':
                    currency = "£";
                    break;
                case 'E':
                    currency = "€";
                    break;
                case 'U':
                    currency = "$";
                    break;
                case 'J':
                    currency = "¥";
                    break;
                case 'S':
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
            ComboBox comboBox = (ComboBox)sender;
            TextBlock boxItem = (TextBlock)comboBox.SelectedItem;
            if (boxItem.Text == "Demand deposit")
            {
                termDeposit.IsEnabled = false;
            }
            else
            {
                termDeposit.IsEnabled = true;
            }
        }
    }
}
