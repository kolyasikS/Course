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

            _fileIO = new FileIO(path);

            SetPositionInScreen();

        }

        private void ToWithdraw(object sender, RoutedEventArgs e)
        {
            double temp_double;
            if (!double.TryParse(sumWithdraw.Text, out temp_double) || temp_double > client.sum)
            {
                MessageBox.Show("You typed incorrect amount!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("You typed incorrect amount!", "Wrong", MessageBoxButton.OK, MessageBoxImage.Error);
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

    }
}
