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
        MainWindow mainwin;
        public Blank(MainWindow mainwin, string path)
        {
            InitializeComponent();

            firstBlank = new FirstBlank(mainwin, this, path);
            secondBlank = new SecondBlank(firstBlank, mainwin, this, path);
            
            this.mainwin = mainwin;
            mainwin.SetPositionInScreen(this);

            BlankPage.Content = firstBlank;
            SetLanguage();
        }

        private void SetLanguage()
        {
            if (mainwin.language == MainWindow.ELanguage.spanish)
            {
                this.Title = "Blanco";
            }
            else if (mainwin.language == MainWindow.ELanguage.french)
            {

            }
        }
    }
}
