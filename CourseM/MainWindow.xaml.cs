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
        public enum ELanguage {english = 1, french = 2, spanish = 3};
        public ELanguage language;
        public BindingList<Client> Clients
        {
            get { return clients; }
            set { clients = value; }
        }
        private bool isAdmin;
        private readonly string PATH = $"{Environment.CurrentDirectory}\\Bank.json";
        private FileIO _fileIO;
        private uint numberOfAccount;
        public delegate void Screen(Window win);

        public MainWindow()
        {
            _fileIO = new FileIO(PATH);
            SavaLoadFile("load");

            InitializeComponent();

            SetPositionInScreen(this);
            Clients ??= new BindingList<Client>();
            language = ELanguage.english;
        }
        private void Add_Client(object sender, RoutedEventArgs e)
        {
            Blank blank = new Blank(this, PATH);
            blank.ShowDialog();
        }
        public void SetPositionInScreen(Window win)
        {
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            win.Top = (screenHeight - win.Height) / 2;
            win.Left = (screenWidth - win.Width) / 2;
        }
        private void UpdateDemandDeposit(Client _client)
        {
            while (_client.dateOfDepositing.AddMonths(_client.amountOfMonth) < DateTime.Now)
            {
                _client.sum += _client.sum * (_client.interestRate);
                _client.amountOfMonth += 12;
            }
        }
        private void UpdateTermDeposit(Client _client)
        {
            if (_client.dateOfDepositing.AddMonths(_client.amountOfMonth) < DateTime.Now && (!_client.isEndedTerm))
            {
                _client.sum += _client.sum * (_client.interestRate);
                _client.isEndedTerm = true;
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

                Screen SetPositionInScreenDEL = new Screen(SetPositionInScreen);

                PasswordClient1 passwordClient = new PasswordClient1(((Client)list.SelectedItem).password, SetPositionInScreenDEL, language);
                passwordClient.ShowDialog();

                if (!passwordClient.isChecked)
                {
                    return;
                }
            }

            Client temp = new Client((Client)list.SelectedItem);

            string tempTermDeposit;
            if (temp.termOfDeposit == "No term")
            {
                tempTermDeposit = "1 year";
                UpdateDemandDeposit((Client)list.SelectedItem);

                temp.isEndedTerm = ((Client)list.SelectedItem).isEndedTerm;
                temp.amountOfMonth = ((Client)list.SelectedItem).amountOfMonth;
                SavaLoadFile("save");
            }
            else
            {
                tempTermDeposit = temp.termOfDeposit;
                UpdateTermDeposit((Client)list.SelectedItem);

                temp.isEndedTerm = ((Client)list.SelectedItem).isEndedTerm;
                SavaLoadFile("save");
            }
            numberOfAccount = temp.numOfAccount;
            temp.sum = Math.Round(((Client)list.SelectedItem).sum, 2);

            double sumAfterTerm = Math.Round(temp.sum + temp.sum * (temp.interestRate), 2);

            ShowDataClient(temp, tempTermDeposit, sumAfterTerm);
        }
        private void Delete_Client(object sender, RoutedEventArgs e)
        {
            if (list.SelectedItem == null || ((Client)list.SelectedItem).numOfAccount != numberOfAccount)
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
            if (list.SelectedItem == null || ((Client)list.SelectedItem).numOfAccount != numberOfAccount)
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
        public void ShowDataClient(Client temp, string tempTermDeposit, double sumAfterTerm)
        {
            string String_SumAfterTerm = "";
            if (!temp.isEndedTerm)
            {
                String_SumAfterTerm = "After " + ((temp.dateOfDepositing.AddDays(temp.amountOfMonth * 30) - DateTime.Now).Days).ToString() + " days you give: " + sumAfterTerm;
            }
            else
            {
                String_SumAfterTerm = "Expired. You can withdraw your money.";
            }
            passport.Text = "Full name: " + temp.nameClient + " " + temp.surname + ".\n"
               + "Date of birthday: " + temp.birthDate.ToString("d", CultureInfo.GetCultureInfo("de-De")) + ".\n"
               + "Gender: " + temp.gender + ".\n"
               + "Number of passport: " + temp.passportNo + ".";
            accountData.Text = "Number of account: " + temp.numOfAccount + ".\n"
                + "Sum of Deposit: " + temp.sum + " " + temp.currency + "\n\n"
                + "Category of deposit: " + temp.categoryOfDeposit + ". " + temp.termOfDeposit + ".\n"
                + "Date of depositing: " + temp.dateOfDepositing.ToString("d", CultureInfo.GetCultureInfo("de-De")) + ".  Interest rate: " + temp.interestRate + "%\n"
                + String_SumAfterTerm;
            lastOperation.Content = "Last operation was carried out at\n";
            lastOperation.Content += temp.lastOperation.ToString();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Enter enter = new Enter(this);
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
                ShowEnteredUser(true);
                
            }
            else if (enter.isAdmin == 0)
            {
                isAdmin = false;
                registerButton.IsEnabled = true;
                withdraw_deposit.IsEnabled = true;
                ShowEnteredUser(false);            }
        }
        private void ShowEnteredUser(bool _isAdmin)
        {
            if (_isAdmin)
            {
                switch(language)
                {
                    case ELanguage.english:
                        IsUser.Content = "You entered as an Administrator";
                        break;
                    case ELanguage.spanish:
                        IsUser.Content = "Estás registrado como administrador";
                        break;
                    case ELanguage.french:
                        IsUser.Content = "Vous êtes entré en tant qu'administrateur";
                        break;
                }
            }
            else if (isAdmin == false)
            {
                switch (language)
                {
                    case ELanguage.english:
                        IsUser.Content = "You entered as a Client";
                        break;
                    case ELanguage.spanish:
                        IsUser.Content = "Entraste como Cliente";
                        break;
                    case ELanguage.french:
                        IsUser.Content = "Vous êtes entré en tant que client";
                        break;
                }
            }
        }
        private void SetEnglishLanguage(object sender, RoutedEventArgs e)
        {
            // MainWindow 
            this.Title = "Bank";

            TitleAccountData.Text  = "Bank account DATA";
            TitlePassportData.Text = "Passport DATA";
            lastOperation.Content  = "Last operation was carried out at\n";

            login.Content = "Log in";
            registerButton.Content = "Register";
            DeleteButton.Content = "Delete";
            changeUserButton.Content = "Change user...";
            withdraw_deposit.Content = "Withdraw / Deposit";
            // MainWindow 

            language = ELanguage.english;
            ShowEnteredUser(isAdmin);
        }
        private void SetSpanishLanguage(object sender, RoutedEventArgs e)
        {
            // MainWindow 
            this.Title = "Banco";

            TitleAccountData.Text = "DATOS de la cuenta bancaria";
            TitlePassportData.Text = "Datos del pasaporte";
            lastOperation.Content = "La última operación se llevó a cabo en\n";

            login.Content = "Iniciar";
            registerButton.Content = "Registro";
            DeleteButton.Content = "Borrar";
            changeUserButton.Content = "Cambiar usuario...";
            withdraw_deposit.Content = "Retirar / Depósito";
            // MainWindow 

            language = ELanguage.spanish;
            ShowEnteredUser(isAdmin);
        }
        private void SetFrenchLanguage(object sender, RoutedEventArgs e)
        {
            // MainWindow 
            this.Title = "Banque";

            TitleAccountData.Text = "DONNÉES de compte bancaire";
            TitlePassportData.Text = "DONNÉES de passeport";
            lastOperation.Content = "La dernière transaction a eu lieu en\n";

            login.Content = "Connexion";
            registerButton.Content = "S'inscrire";
            DeleteButton.Content = "Supprimer";
            changeUserButton.Content = "Changer\nd'utilisateur...";
            withdraw_deposit.Content = "Retirer / Verser";

            // MainWindow 

            language = ELanguage.french;
            ShowEnteredUser(isAdmin);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.F1)
            {
                MessageBox.Show("Help");
            }
        }
    }
    
}

