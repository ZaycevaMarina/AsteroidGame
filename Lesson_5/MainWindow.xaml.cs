using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows;

namespace Lesson_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //ObservableCollection<Employee> items = new ObservableCollection<Employee>();
        ObservableCollection<Department> items = new ObservableCollection<Department>();
        //List<Employee> LEmployee = new List<Employee>();
        Department _Department;
        public MainWindow()
        {
            InitializeComponent();
            FillListEmployee();
        }
        void FillListEmployee()
        {
            lbEmployee.ItemsSource = items;
            //LEmployee.Add(new Employee(1, "Vasya", 22, 3000));
            //LEmployee.Add(new Employee(1, "Petya", 25, 6000));
            //LEmployee.Add(new Employee(2, "Kolya", 23, 8000));
            //foreach (Employee emp in LEmployee)
            //    items.Add(emp);
            _Department = new Department("Employees.txt");
            //foreach(List<int> iddep in _Department.IdsDep)
                items.Add(_Department);
        }

        private void lbEmployee_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(e.Source.ToString());
        }

        private void lbEmployee_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            MessageBox.Show(e.AddedItems[0].ToString());
        }

        private void btnAddEmployee(object sender, RoutedEventArgs e)
        {
            Employee emp = new Employee(2, "Sergey", 26, 7000);
            //LEmployee.Add(emp);
            //items.Add(emp);
            _Department.AddEmployee(emp);
            items.Clear();
            items.Add(_Department);
        }
    }    
}
