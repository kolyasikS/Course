using CourseM.Service;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace CourseM
{
    public partial class Blank : Window
    {
        public SecondBlank secondBlank { get; }
        public FirstBlank firstBlank { get; }
        public Blank(MainWindow mainwin, string path)
        {
            InitializeComponent();

            SetPositionInScreen();

            firstBlank = new FirstBlank(mainwin, this, path);
            secondBlank = new SecondBlank(firstBlank, mainwin, this, path);

            BlankPage.Content = firstBlank;
        }

        private void SetPositionInScreen()
        {
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 2;
            this.Left = (screenWidth - this.Width) / 2;
        }
    }
}
