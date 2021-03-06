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
using System.Windows.Shapes;

namespace CourseM
{
    public partial class Withdraw : Window
    {
        Client client;
        MainWindow mainwin;
        private FileIO _fileIO;

        public Withdraw(MainWindow mainwin, Client client, string path)
        {
            InitializeComponent();

            havingMoney.Content = Math.Round(client.sum, 2) + "$";
            this.client = client;
            this.mainwin = mainwin;

            SetVisibleButtons(client);



            _fileIO = new FileIO(path);

            SetPositionInScreen();
            SetLanguage();
        }
        public Withdraw()
        {
            InitializeComponent();
        }

        private void SetVisibleButtons(Client client)
        {
            if (client.categoryOfDeposit == "Demand deposit")
            {
                depositButton.IsEnabled = true;
                withdrawButton.IsEnabled = true;
            }
            else
            {
                depositButton.IsEnabled = false;
                if (!client.isEndedTerm)
                {
                    withdrawButton.IsEnabled = false;
                }
                else
                {
                    withdrawButton.IsEnabled = true;
                }
            }
        }
        private void SetLanguage()
        {
            if (mainwin.language == MainWindow.ELanguage.spanish)
            {
                this.Title = "Operación";

                moneyYouHave.Content = "Dinero que tienes:";
                sumForOper.Content = "Suma para la operación:";

                depositButton.Content = "Depósito";
                withdrawButton.Content = "Retirar";
                cancelButton.Content = "Cancelar";
            }
            else if (mainwin.language == MainWindow.ELanguage.french)
            {
                this.Title = "Opération";

                moneyYouHave.Content = "L'argent que vous avez:";
                sumForOper.Content = "Somme pour l'opération:";

                depositButton.Content = "Verser";
                withdrawButton.Content = "Retirer";
                cancelButton.Content = "Annuler";
            }
        }
        private void ToWithdraw(object sender, RoutedEventArgs e)
        {
            double temp_double;
            if (!double.TryParse(sumWithdraw.Text, out temp_double) || temp_double > client.sum)
            {
                switch (mainwin.language)
                {
                    case MainWindow.ELanguage.english:
                        MessageBox.Show("You typed incorrect amount!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case MainWindow.ELanguage.spanish:
                        MessageBox.Show("¡Escribió una cantidad incorrecta!", "Equivocado", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case MainWindow.ELanguage.french:
                        MessageBox.Show("Vous avez tapé un montant incorrect!", "Mauvais", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
                return;
            }
            ((Client)mainwin.list.SelectedItem).sum = client.sum -= temp_double;
            ((Client)mainwin.list.SelectedItem).lastOperation = DateTime.Now;
            try
            {
                _fileIO.SaveData(mainwin.Clients);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            havingMoney.Content = Math.Round(client.sum, 2) + "$";
            SetClientInfo(client);

            return;
        }
        private void SetPositionInScreen()
        {
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 2;
            this.Left = (screenWidth - this.Width) / 2;
        }
        private void ToDeposit(object sender, RoutedEventArgs e)
        {
            double temp_double;
            if (!double.TryParse(sumWithdraw.Text, out temp_double) || temp_double > double.MaxValue - client.sum)
            {
                switch (mainwin.language)
                {
                    case MainWindow.ELanguage.english:
                        MessageBox.Show("You typed incorrect amount!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case MainWindow.ELanguage.spanish:
                        MessageBox.Show("¡Escribió una cantidad incorrecta!", "Equivocado", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case MainWindow.ELanguage.french:
                        MessageBox.Show("Vous avez tapé un montant incorrect!", "Mauvais", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
                return;
            }
            ((Client)mainwin.list.SelectedItem).sum = client.sum += temp_double;
            ((Client)mainwin.list.SelectedItem).lastOperation = DateTime.Now;
            try
            {
                _fileIO.SaveData(mainwin.Clients);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            havingMoney.Content = Math.Round(client.sum, 2) + "$";
            SetClientInfo(client);

            return;
        }
        void SetClientInfo(Client temp)
        {
            string tempTermDeposit;
            if (temp.termOfDeposit == "No term")
            {
                tempTermDeposit = "1 year";
            }
            else
            {
                tempTermDeposit = temp.termOfDeposit;
            }

            double sumAfterTerm = Math.Round(temp.sum + temp.sum * (temp.interestRate), 2);
            temp.sum = Math.Round(temp.sum, 2);
            sumWithdraw.Text = "";

            mainwin.ShowDataClient(temp, tempTermDeposit, sumAfterTerm);
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                Help help = new Help();
                help.Show();
            }
        }
    }
}
