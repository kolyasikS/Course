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
        FirstBlank firstBlank;
        public SecondBlank(FirstBlank firstBlank)
        {
            InitializeComponent();

            this.firstBlank = firstBlank;
        }

        private void OpenLastPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(firstBlank);

        }
    }
}
