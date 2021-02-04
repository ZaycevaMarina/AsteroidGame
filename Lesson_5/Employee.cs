using System.ComponentModel;

namespace Lesson_6_Binding_Trigger
{
    public class Employee : INotifyPropertyChanged
    {
        protected static int __IdEmp = 1;
        public int IdEmp { get; private set; }        
        public int Age { get; private set;}
        public double Salary { get; private set; }
        //public string Name { get; private set; }
        private string _Name;

        public string Name
        {
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this._Name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public Employee(string name, int age, double salary)
        {
            IdEmp = __IdEmp++;
            Name = name;
            Age = age;
            Salary = salary;
        }

        public void UpdateEmployee(string name, int age, double salary)
        {
            Name = name;
            Age = age;
            Salary = salary;
        }
        public override string ToString()
        {
            return $"{IdEmp}\t{Name}\t{Age}\t{Salary}";
        }
    }
}
