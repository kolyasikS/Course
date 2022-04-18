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


        public string Info
        {
            get { return info; }
            set { info = value; }
        }

        public Client(string name, string sname, DateTime bDate, string passNo,
            string gen, double s, string catOfDep, string termOfDep, string pass,
            string curren, float rateInt, int amountOfM)
        {

            nameClient = name;
            surname = sname;
            birthDate = bDate;
            passportNo = passNo;
            gender = gen;
            sum = s;
            categoryOfDeposit = catOfDep;
            termOfDeposit = termOfDep;
            dateOfDepositing = DateTime.Now;
            isEndedTerm = false;

            Random random = new Random();
            numOfAccount = (uint)random.Next(100000000, 1000000000);

            lastOperation = DateTime.Now;
            info = nameClient + " " + surname;
            amountOfMonth = amountOfM;
            password = pass;
            currency = curren;
            interestRate = rateInt;
        }

        public Client(Client cl)
        {

            nameClient = cl.nameClient;
            surname = cl.surname;
            birthDate = cl.birthDate;
            passportNo = cl.passportNo;
            gender = cl.gender;
            sum = cl.sum;
            categoryOfDeposit = cl.categoryOfDeposit;
            termOfDeposit = cl.termOfDeposit;
            numOfAccount = cl.numOfAccount;
            info = cl.info;
            dateOfDepositing = cl.dateOfDepositing;
            isEndedTerm = cl.isEndedTerm;
            amountOfMonth = cl.amountOfMonth;
            lastOperation = cl.lastOperation;
            password = cl.password;
            currency = cl.currency;
            interestRate = cl.interestRate;
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
