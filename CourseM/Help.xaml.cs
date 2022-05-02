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
        public Help()
        {
            InitializeComponent();
           
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            ((TreeViewItem)sender).IsSelected = false;

        }


        private void TreeViewItem_Selected4(object sender, RoutedEventArgs e)
        {
            TreeViewItem currentTreeItem = (TreeViewItem)sender;
            currentTreeItem.IsSelected = false;

            switch (((TreeViewItem)currentTreeItem.Parent).Items.IndexOf(currentTreeItem))
            {
                case 0:
                    MessageBox.Show("0");
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
                    MessageBox.Show("0");
                    break;
                case 1:
                    MessageBox.Show("1");
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
                    MessageBox.Show("0");
                    break;
                case 1:
                    MessageBox.Show("1");
                    break;
                case 2:
                    MessageBox.Show("2");
                    break;
                case 3:
                    MessageBox.Show("3");
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
                    MessageBox.Show("0");
                    break;
                case 1:
                    MessageBox.Show("1");
                    break;

            }
        }
    }
}
