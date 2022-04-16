using CourseM.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for FirstBlank.xaml
    /// </summary>
    public partial class FirstBlank : Page
    {
        MainWindow mainwin;
        Blank blank;
        private FileIO _fileIO;
        private readonly string PATH;
        DateTime dateTime;
        private string genderType;

        public FirstBlank(MainWindow mainwin, Blank blank, string path)
        {
            InitializeComponent();

            this.mainwin = mainwin;
            this.blank = blank;

            PATH = path;

            dateTime = DateTime.Now;
            datePicker.SelectedDate = dateTime;

            Button butInfo = buttonInfo;
            SetButtonToolTip(butInfo);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public void Create_Client()
        {

            

        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dateTime = (DateTime)datePicker.SelectedDate;
            dateLabel.Content = dateTime.ToString("d", CultureInfo.GetCultureInfo("de-De"));
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radiobut = (RadioButton)sender;
            genderType = radiobut.Content.ToString();
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

        private void SetButtonToolTip(Button butInfo)
        {
            ToolTip toolTip = new ToolTip();

            toolTip.Content =
                  "1. Field \"Name\" - write your first name.\n"
                + "2. Field \"Surname\" - write your last name.\n"
                + "3. Field \"Birth date\" - choose your date of birth.\n"
                + "4. Field \"Passport No\" - write number of your passport. A number has to be none-digit.\n"
                + "5. Field \"Gender\" - choose your gender, clicking on relevant point.\n"
                + "6. Field \"Sum of deposit\" - write sum you want to deposit in the bank.\n"
                + "\t WARNING! There are 2 conditions for depositing:\n"
                + "\t\t a. The sum has to be more than 10.000$.\n"
                + "\t\t b. The sum has to be less than 1.000.000.000$.\n"
                + "\t *These conditions are that because any other sum is non-profit for us.\n"
                + "7. Field \"Category of deposit\" - choose your prefer category.\n"
                + "\t\t a. Demand deposit - you can withdraw money from bank any time.\n"
                + "\t\t However, ANNUAL interest rate constantly is 1.0% from the sum being in bank.\n"
                + "\t\t b. Term deposit - you can withdraw money only when time your chose terminates.\n"
                + "\t\t However, interest rate depends on the term your chose.\n"
                + "\t\t The more time, the more interest rate.\n"
                + "\t\t\t 1 month - 0.4%\n"
                + "\t\t\t 3 months - 0.6%\n"
                + "\t\t\t 6 months - 0.8%\n"
                + "\t\t\t 1 year - 1.0%\n"
                + "\t\t\t 3 yers - 1.2%\n"
                + "8. Field \"Term of deposit\" - choose a term your money will be in bank for.\n"
                + "\t *This field is available only if you chose \"Term deposit\".";


            butInfo.ToolTip = toolTip;
        }

        private void OpenNextPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(blank.secondBlank);
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            blank.Close();
        }
    }
}
