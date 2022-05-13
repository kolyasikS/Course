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
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        private int isTableOn = 0;
        public Help()
        {
            InitializeComponent();
           
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            ((TreeViewItem)sender).IsSelected = false;
            if (isTableOn != 0)
            {
                isTableOn--;
            }
            else
            {
                tableTB.Visibility = Visibility.Collapsed;
            }


        }


        private void TreeViewItem_Selected4(object sender, RoutedEventArgs e)
        {
            TreeViewItem currentTreeItem = (TreeViewItem)sender;
            currentTreeItem.IsSelected = false;

            switch (((TreeViewItem)currentTreeItem.Parent).Items.IndexOf(currentTreeItem))
            {
                case 0:

                    break;
                case 1:

                    MessageBox.Show("1");
                    break;
            }
        }

        private void TreeViewInfo3(object sender, RoutedEventArgs e)
        {
            TreeViewItem currentTreeItem = (TreeViewItem)sender;
            currentTreeItem.IsSelected = false;
            switch (((TreeViewItem)currentTreeItem.Parent).Items.IndexOf(currentTreeItem))
            {
                case 0:
                    information.Text = "\tThe client can take advantage of two operations:" +
                        "\n\t1.deposit - to put money on your personal account, the size of the deposit can vary from 10.000 to 1.000.000 for 1 deposit.Please note that the Term deposit category does not provide for repeated deposit." +
                        "\n\t2.Withdraw - to withdraw money from your personal account, the amount of withdrawal can not be more than the amount available on the account.Please note that the Term deposit category does not provide for withdrawal of money before the expiry date.";
                    break;
                case 1:
                    information.Text = "The bank provides 5 different currencies. Each currency has a different interest rate, which varies with the term of the deposit. A table is attached.";
                    
                    tableTB.Visibility = Visibility.Visible;
                    isTableOn = 2;    
                    /*Table table = new Table();
                    table.CellSpacing = 5;
                    for (int i = 0; i < 7; i++)
                    {
                        table.Columns.Add(new TableColumn());
                        table.Columns[i].Width = new GridLength(60);
                    }
                    table.Columns[0].Width = new GridLength(100);

                    table.RowGroups.Add(new TableRowGroup());
                    TableRow tableRow = new TableRow();
                    tableRow.Background = Brushes.DarkGoldenrod;
                    Paragraph paragraph = new Paragraph(new Run("Table of currencies"));
                    paragraph.FontSize = 18;
                    paragraph.FontWeight = FontWeights.Bold;
                    TableCell cell = new TableCell(paragraph);
                    cell.ColumnSpan = 7;
                    cell.TextAlignment = TextAlignment.Center;
                    tableRow.Cells.Add(cell);
                    table.RowGroups[0].Rows.Add(tableRow);
                    information.Text = "";
                    tableFD.Blocks.Add(table);*/
                    break;
                case 2:
                    MessageBox.Show("2");
                    break;
                case 3:
                    MessageBox.Show("3");
                    break;
            }
        }

        private void TreeViewInfo2(object sender, RoutedEventArgs e)
        {
            TreeViewItem currentTreeItem = (TreeViewItem)sender;
            currentTreeItem.IsSelected = false;

            switch (((TreeViewItem)currentTreeItem.Parent).Items.IndexOf(currentTreeItem))
            {
                case 0:
                    information.Text = "\tThe list of clients is in the information table at the bottom left of the screen. When registering a new client will be added to its end, when deleting it will be properly lumped together." +
                        "\n\tThe list is designed to find your account and the possibility to enter it.";
                    break;
                case 1:
                    information.Text = "\tInformation tables contain information about your passport data and personal account data, which you can view by logging into your account as a client or administrator.";
                    break;
                case 2:
                    information.Text = "\tChange user - option that allows you to change the user if you accidentally choose the wrong one when logging in. For Administrator, it requires a special password.";
                    break;
                case 3:
                    information.Text = "\tOur bank provides 4 main options available to the user:" +
                        "\n\t1.Register - allows the Client to register an account with the bank, having correctly fulfilled all the prescribed conditions when entering the data in the form." +
                        "\n\t2.Log in -allows the user to log in to the selected account in the list of clients and view the relevant information." +
                        "\n\t3.Delete - allows the user to delete the account, in this case, he / she has to log in to the account, the money will be returned without interest." +
                        "\n\t4.Withdraw / Deposit - allows the client to withdraw/ deposit money to the account, if the deposit category and the term of the deposit allow this, besides there is a number of conditions to this operation.Read the relevant section.";
                    break;

            }
        }

        private void TreeViewInfo1(object sender, RoutedEventArgs e)
        {
            TreeViewItem currentTreeItem = (TreeViewItem)sender;
            currentTreeItem.IsSelected = false;

            switch (((TreeViewItem)currentTreeItem.Parent).Items.IndexOf(currentTreeItem))
            {
                case 0:
                    information.Text = "\tThe application user can log in as Client by " +
                        "clicking on the Enter as Client button in the login window. " +
                        "If you want to change the user, then in the menu you need to " +
                        "click on the Change User....\n\tThe Client can register a new bank " +
                        "account by filling in all the fields correctly, has the possibility " +
                        "to log in to his account by entering his password correctly, review " +
                        "information about his passport data and personal account data, " +
                        "see the date of the last operation performed, withdraw and deposit " +
                        "money according to the conditions described in the appropriate sections, " +
                        "has the right to delete his account, and return all the money without " +
                        "charging any interest.\n\tIn order to expand the target audience our clients " +
                        "have the opportunity to use the application in 3 languages - English, Spanish, French.";
                    break;
                case 1:
                    information.Text = "\tThe user of the application can log in as Administrator, to do this in the authorization window, click on Enter as Administrator and enter the password to log in. If you want to change the user, in the menu you need to click on Change User... and enter the password for the Administrator account. " +
                        "\n\tAdministrator has the right to log in to the account of any client without entering the password, view the information of the client's passport data and personal account data, see the date of the last operation performed. However. " +
                        "\n\tThe Administrator has no right to register anyone or himself, he is forbidden to withdraw and invest client's money, but has the right to delete the account in case of non-compliance with the rules and regulations of our bank.";
                    break;

            }
        }
    }
}
