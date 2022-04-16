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
        private string genderType;

        public SecondBlank(FirstBlank firstBlank, MainWindow mainwin, Blank blank, string path)
        {
            InitializeComponent();

            this.mainwin = mainwin;
            this.blank = blank;
            this.firstBlank = firstBlank;

            PATH = path;

            dateTime = DateTime.Now;
        }
        private void CreateClient(object sender, RoutedEventArgs e)
        {
            if (firstBlank.name.Text == "" || firstBlank.surname.Text == "" || firstBlank.numOfPass.Text == "" 
                || genderType == "" || firstBlank.sum.Text == ""
                || firstBlank.categoryOfDeposit.SelectedItem == null || (firstBlank.termDeposit.SelectedItem == null 
                && ((TextBlock)firstBlank.categoryOfDeposit.SelectedItem).Text == "Term deposit"))
            {
                MessageBox.Show("You didn`t fill out all fileds!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int temp_int;
            if (!int.TryParse(firstBlank.numOfPass.Text, out temp_int)
                || firstBlank.numOfPass.Text.Length != 9)
            {
                MessageBox.Show("Passport No has to be none-digit number!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            double temp_double;
            if (!double.TryParse(firstBlank.sum.Text, out temp_double) 
                || firstBlank.sum.Text.Length < 5 
                || firstBlank.sum.Text.Length > 9)
            {
                MessageBox.Show("We cannot accept that sum. Reread the conditions of our bank again!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string catOfDeposit = ((TextBlock)firstBlank.categoryOfDeposit.SelectedItem).Text;
            string termOfDep;
            if (catOfDeposit == "Demand deposit")
            {
                termOfDep = "No term";
            }
            else
            {
                termOfDep = ((TextBlock)firstBlank.termDeposit.SelectedItem).Text;
            }

            Client temp_client = new Client(firstBlank.name.Text, firstBlank.surname.Text, dateTime, 
                firstBlank.numOfPass.Text, genderType, Convert.ToDouble(firstBlank.sum.Text), 
                catOfDeposit, termOfDep, "weq");

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

        
    }
}
