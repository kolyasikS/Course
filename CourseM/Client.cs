using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseM
{
    public class Client
    {
        private string nameClient;
        private string surname;
        private string password;
        private string passportNo;
        private string gender;
        private string categoryOfDeposit;
        private string termOfDeposit;
        private string info;
        private bool isEndedTerm;
        private double sum;
        private uint numOfAccount;
        private DateTime dateOfDepositing;
        private DateTime lastOperation;
        private DateTime birthDate;
 

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public DateTime LastOperation
        {
            get { return lastOperation; }
            set { lastOperation = value; }
        }
        public DateTime DateOfDepositing
        {
            get { return dateOfDepositing; }
            set { dateOfDepositing = value; }
        }
        private int amountOfYears;
        public int AmountOfYears
        {
            get { return amountOfYears; }
            set { amountOfYears = value; }
        }
        public uint NumOfAccount
        {
            get { return numOfAccount; }
            set { numOfAccount = value; }
        }
        public string TermOfDeposit
        {
            get { return termOfDeposit; }
            set { termOfDeposit = value; }
        }
        public bool IsEndedTerm
        {
            get { return isEndedTerm; }
            set { isEndedTerm = value; }
        }
        public string CategoryOfDeposit
        {
            get { return categoryOfDeposit; }
            set { categoryOfDeposit = value; }
        }
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        public string NameClient
        {
            get { return nameClient; }
            set
            {
                if (value == nameClient)
                {
                    return;
                }
                else nameClient = value;
                //OnPropertyChanged("NameClient");
            }
        }
        public double Sum
        {
            get { return sum; }
            set
            {
                if (value == sum)
                {
                    return;
                }
                else sum = value;
                //OnPropertyChanged("Sum");
            }

        }
        public string Info
        {
            get { return info; }
            set
            {
                if (value == info)
                {
                    return;
                }
                else
                {
                    info = value;
                }

                //OnPropertyChanged("Info");
            }
        }
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }
        public string PassportNo
        {
            get { return passportNo; }
            set { passportNo = value; }
        }



        public Client(string name, string sname, DateTime bDate, string passNo, string gen, double s, string catOfDep, string termOfDep, string pass)
        {

            NameClient = name;
            Surname = sname;
            BirthDate = bDate;
            PassportNo = passNo;
            Gender = gen;
            Sum = s;
            CategoryOfDeposit = catOfDep;
            TermOfDeposit = termOfDep;
            dateOfDepositing = DateTime.Now;
            IsEndedTerm = false;

            Random random = new Random();
            NumOfAccount = (uint)random.Next(100000000, 1000000000);

            lastOperation = DateTime.Now;
            Info = NameClient + " " + Surname;
            amountOfYears = 1;
            password = pass;
        }

        public Client(Client cl)
        {

            NameClient = cl.NameClient;
            Surname = cl.Surname;
            BirthDate = cl.BirthDate;
            PassportNo = cl.PassportNo;
            Gender = cl.Gender;
            Sum = cl.Sum;
            CategoryOfDeposit = cl.CategoryOfDeposit;
            TermOfDeposit = cl.TermOfDeposit;
            NumOfAccount = cl.NumOfAccount;
            Info = cl.Info;
            dateOfDepositing = cl.dateOfDepositing;
            IsEndedTerm = cl.IsEndedTerm;
            AmountOfYears = cl.AmountOfYears;
            lastOperation = cl.LastOperation;
            password = cl.Password;
        }

        public Client()
        {

        }

       /* public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string str = "NONE")
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));

        }*/

    }
}
