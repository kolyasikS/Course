using CourseM.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class MainWindow : Window
    {
        private BindingList<Client> clients;            
        public BindingList<Client> Clients
        {
            get { return clients; }
            set { clients = value; }
        }
        private bool isAdmin;

        private readonly string PATH = $"{Environment.CurrentDirectory}\\Bank.json";
        private FileIO _fileIO;
        private uint numberOfAccount;
        
        public MainWindow()
        {
            _fileIO = new FileIO(PATH);
            SavaLoadFile("load");

            InitializeComponent();

            SetPositionInScreen();
            Clients ??= new BindingList<Client>();
        }
        private void Add_Client(object sender, RoutedEventArgs e)
        {
            Blank blank = new Blank(this, PATH);
            blank.ShowDialog();
        }

        private void SetPositionInScreen()
        {
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 2;
            this.Left = (screenWidth - this.Width) / 2;
        }

        void UpdateDemandDeposit(Client _client, float interestRate)
        {
            while (_client.DateOfDepositing.AddYears(_client.AmountOfYears) < DateTime.Now)
            {
                _client.Sum += _client.Sum * (interestRate / 100f);
                _client.AmountOfYears++;
            }
        }

        void UpdateTermDeposit(Client _client, int amountMonth, float interestRate)
        {
            if (_client.DateOfDepositing.AddMonths(amountMonth) < DateTime.Now && (!_client.IsEndedTerm))
            {
                _client.Sum += _client.Sum * (interestRate / 100f);
                _client.IsEndedTerm = true;
            }
        }
     
        private void Log_in(object sender, RoutedEventArgs e)
        {
            if (list.SelectedItem == null)
            {
                MessageBox.Show("You didn`t choose an account!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (passport.Text != "")
            {
                MessageBox.Show("You are already in this account!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!isAdmin)
            {
                PasswordClient1 passwordClient = new PasswordClient1(((Client)list.SelectedItem).Password);
                passwordClient.ShowDialog();

                if (!passwordClient.isChecked)
                {
                    return;
                }
            }

            Client temp = new Client((Client)list.SelectedItem);

            string tempTermDeposit;
            if (temp.TermOfDeposit == "No term")
            {
                tempTermDeposit = "1 year";
                UpdateDemandDeposit((Client)list.SelectedItem, 1.0f);
            }
            else
            {
                tempTermDeposit = temp.TermOfDeposit;
            }

            float interestRate = SetInterestR(temp);
            
            SavaLoadFile("save");
            
            numberOfAccount = temp.NumOfAccount;
            temp.Sum = ((Client)list.SelectedItem).Sum;
            double sumAfterTerm = Math.Round(temp.Sum + temp.Sum * (interestRate / 100f), 2);
            temp.Sum = Math.Round(temp.Sum, 2);

            ShowDataClient(temp, interestRate, tempTermDeposit, sumAfterTerm);
        }

        private void Delete_Client(object sender, RoutedEventArgs e)
        {
            if (list.SelectedItem == null || ((Client)list.SelectedItem).NumOfAccount != numberOfAccount)
            {
                MessageBox.Show("You didn`t log in an account!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MessageBox.Show("Do you want to delete this account?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                Clients.Remove((Client)list.SelectedItem);
                SavaLoadFile("save");
            }
            
        }

        private void Withdraw(object sender, RoutedEventArgs e)
        {
            if (list.SelectedItem == null || ((Client)list.SelectedItem).NumOfAccount != numberOfAccount)
            {
                MessageBox.Show("Log in to withdraw money!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Withdraw withdraw = new Withdraw(this, (Client)list.SelectedItem, PATH);
            withdraw.ShowDialog();

        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            passport.Text = "";
            accountData.Text = "";
            lastOperation.Content = "Last operation was carried out at\n";
            numberOfAccount = 0;

    }

    private void SavaLoadFile(string choice)
        {
            if (choice == "load")
            {
                try
                {
                    Clients = _fileIO.LoadData();
                    Clients ??= new BindingList<Client>();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else if (choice == "save")
            {
                try
                {
                    _fileIO.SaveData(Clients);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }
        
        public float SetInterestR(Client temp)
        {
            float interestRate;

            if (temp.TermOfDeposit == "1 month")
            {
                interestRate = 0.4f;
                UpdateTermDeposit((Client)list.SelectedItem, 1, interestRate);
            }
            else if (temp.TermOfDeposit == "3 months")
            {
                interestRate = 0.6f;
                UpdateTermDeposit((Client)list.SelectedItem, 3, interestRate);
            }
            else if (temp.TermOfDeposit == "6 months")
            {
                interestRate = 0.8f;
                UpdateTermDeposit((Client)list.SelectedItem, 6, interestRate);
            }
            else if (temp.TermOfDeposit == "1 year" || temp.TermOfDeposit == "No term")
            {
                interestRate = 1.0f;
                UpdateTermDeposit((Client)list.SelectedItem, 12, interestRate);
            }
            else
            {
                interestRate = 1.2f;
                UpdateTermDeposit((Client)list.SelectedItem, 36, interestRate);

            }

            return interestRate;
        }

        public void ShowDataClient(Client temp, float interestRate, string tempTermDeposit, double sumAfterTerm)
        {
            passport.Text = "Full name: " + temp.NameClient + " " + temp.Surname + ".\n"
               + "Date of birthday: " + temp.BirthDate.ToString("d", CultureInfo.GetCultureInfo("de-De")) + ".\n"
               + "Gender: " + temp.Gender + ".\n"
               + "Number of passport: " + temp.PassportNo + ".";
            accountData.Text = "Number of account: " + temp.NumOfAccount + ".\n"
                + "Sum of Deposit: " + temp.Sum + "$\n\n"
                + "Category of deposit: " + temp.CategoryOfDeposit + ". " + temp.TermOfDeposit + ".\n"
                + "Date of depositing: " + temp.DateOfDepositing.ToString("d", CultureInfo.GetCultureInfo("de-De")) + ".  Interest rate: " + interestRate + "%\n"
                + "After " + tempTermDeposit + " you give: " + sumAfterTerm;
            lastOperation.Content = "Last operation was carried out at\n";
            lastOperation.Content += temp.LastOperation.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Enter enter = new Enter(this);
            enter.ShowDialog();
            if (enter.isAdmin == 1)
            {
                isAdmin = true;
                IsUser.Content = "You entered as an Administrator";
            }
            else if (enter.isAdmin == 0)
            {
                isAdmin = false;
                IsUser.Content = "You entered as a Client";
            }
        }

        private void ChangeUser(object sender, RoutedEventArgs e)
        {
            int isAdminToEnter = Convert.ToInt32(isAdmin);

            Enter enter = new Enter(this, isAdminToEnter);
            enter.CancelButton.Visibility = Visibility.Visible;
            enter.ShowDialog();
            if (enter.isAdmin == 1)
            {
                isAdmin = true;
                registerButton.IsEnabled = false;
                withdraw_deposit.IsEnabled = false;
                IsUser.Content = "You entered as an Administrator";
            }
            else if (enter.isAdmin == 0)
            {
                isAdmin = false;
                registerButton.IsEnabled = true;
                withdraw_deposit.IsEnabled = true;
                IsUser.Content = "You entered as a Client";
            }
        }
    }
    
}

