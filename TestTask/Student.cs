using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;



namespace TestTask
{
    [Serializable()]
    public class Student : INotifyPropertyChanged
    {
        private string firstName;
        private string last;
        private string fullName;
        private int age;
        private int gender;


        public Student()
        {

        }

        [System.Xml.Serialization.XmlElement("FirstName")]
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        [System.Xml.Serialization.XmlElement("Last")]
        public string Last
        {
            get { return last; }
            set
            {
                last = value;
                OnPropertyChanged("Last");
            }
        }

        [System.Xml.Serialization.XmlElement("FullName")]
        public string FullName
        {
            get { return fullName = FirstName + " " + Last; }
            set
            {
                fullName = value;
                OnPropertyChanged("FullName");
            }
        }

        [System.Xml.Serialization.XmlElement("Age")]
        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged("Age");
            }
        }

        [System.Xml.Serialization.XmlElement("Gender")]
        public string Gender
        {
            get {
                if (gender == 0)
                    return "Мужской";
                else
                    return "Женский";
            }
            set
            {
                if (value == "Мужской")
                    gender = 0;
                else
                    gender = 1;
                
                OnPropertyChanged("Gender");
            }
        }




    public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
