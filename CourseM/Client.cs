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
        public string nameClient;
        public string surname;
        public string password;
        public string passportNo;
        public string gender;
        public string categoryOfDeposit;
        public string termOfDeposit;
        public string currency;
        private string info;
        public bool isEndedTerm;
        public double sum;
        public float interestRate;
        public uint numOfAccount;
        public int amountOfMonth;
        public DateTime dateOfDepositing;
        public DateTime lastOperation;
        public DateTime birthDate; 
        

                else sum = value;
                //OnPropertyChanged("Sum");
            }

        }
        public string Info
        {
            get { return info; }
            set { info = value; }
        }

            nameClient = name;
            surname = sname;
            birthDate = bDate;
            passportNo = passNo;
            gender = gen;
            sum = s;
            categoryOfDeposit = catOfDep;
            termOfDeposit = termOfDep;
            PassportNo = passNo;
            Gender = gen;
            Sum = s;
            CategoryOfDeposit = catOfDep;
            TermOfDeposit = termOfDep;
            dateOfDepositing = DateTime.Now;
            isEndedTerm = false;
            info = nameClient + " " + surname;
            amountOfMonth = amountOfM;
            password = pass;
            currency = curren;
            interestRate = rateInt;

            lastOperation = DateTime.Now;
            Info = NameClient + " " + Surname;
            amountOfYears = 1;
            password = pass;
        }

        public Client(Client cl)
        {

            nameClient = cl.nameClient;
            surname = cl.surname;
            birthDate = cl.birthDate;
            passportNo = cl.passportNo;
            gender = cl.gender;
            sum = cl.sum;
            isEndedTerm = cl.isEndedTerm;
            amountOfMonth = cl.amountOfMonth;
            lastOperation = cl.lastOperation;
            password = cl.password;
            currency = cl.currency;
            interestRate = cl.interestRate;
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
