using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Lesson_5
{
    public partial class LBDepartment : Window
    {
        ObservableCollection<Employee> ItemsEmployees = new ObservableCollection<Employee>();
        ObservableCollection<string> ItemsDepNames = new ObservableCollection<string>();
        private string EmployeeSelected = "";
        private string DepartmentSelected = "";
        public LBDepartment()
        {
            InitializeComponent();
        }
        public void FillListEmployees()
        {            
            foreach(Department dep in MainWindow.__Departments)
                if(dep.Name == _CurrentDepartmentName)
                {
                    ItemsEmployees = dep.LEmployees;
                }
            lbEmployee.ItemsSource = ItemsEmployees;
        }
        void FillListCbDepartment()
        {            
            foreach (Department dep in MainWindow.__Departments)
                ItemsDepNames.Add(dep.Name);
            cbDepNames.ItemsSource = ItemsDepNames;
        }
        public string _CurrentDepartmentName { get; set; }
        public void ShowViewModel()
        {
            FillListEmployees();
            FillListCbDepartment();
        }

        private void lbEmployee_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                EmployeeSelected = e.AddedItems[0].ToString();
                string[] s = EmployeeSelected.Split('\t');
                if (s.Length != 4)
                    return;
                cbDepNames.Text = _CurrentDepartmentName;
                tbId.Text = s[0];
                tbName.Text = s[1];
                tbAge.Text = s[2];
                tbSalary.Text = s[3];
            }
            catch(Exception)
            {
                //СРабатывает при удалении, когда после удаления обновляется список сотрудников в lbEmployee.ItemsSource, т.е. в e.AddedItems[0]
            }
        }

        private void btnRemoveEmployee(object sender, RoutedEventArgs e)
        {
            foreach (Department dep in MainWindow.__Departments)
                if (dep.Name == _CurrentDepartmentName && dep.LEmployees.Count > 0)
                {
                    dep.RemoveEmployee(EmployeeSelected);
                }
        }
        private void btnAddEmployee(object sender, RoutedEventArgs e)
        {
            int age;
            double salary;
            if (!int.TryParse(tbAge.Text, out age) && !double.TryParse(tbSalary.Text, out salary))
                return;
            foreach (Department dep in MainWindow.__Departments)
                if (dep.Name == _CurrentDepartmentName/* && dep.LEmployees.Count > 0*/)
                {
                    try
                    {
                        dep.AddEmployee($"{tbName.Text} {age} {tbSalary.Text}");
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                }
        }
        private void cbDepNames_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            cbDepNames.Text = e.AddedItems[0].ToString();
            DepartmentSelected = e.AddedItems[0].ToString();
        }

        public void btUpdateEmployee(object sender, RoutedEventArgs e)
        {
            int age;
            double salary;
            string name = tbName.Text;
            if (name == "" || !int.TryParse(tbAge.Text, out age) || !double.TryParse(tbSalary.Text, out salary))
                return;
            
            if (_CurrentDepartmentName == DepartmentSelected)
            {
                if (EmployeeSelected == tbId.Text.ToString() + "\t" + name + "\t" + age + "\t" + salary)
                    return;
                foreach (Department dep in MainWindow.__Departments)
                    if (dep.Name == _CurrentDepartmentName && dep.LEmployees.Count > 0)
                    {
                        dep.UpdateEmployee(EmployeeSelected, tbId.Text.ToString() + "\t" + name + "\t" + age + "\t" + salary);
                    }
            }
            else if(DepartmentSelected != "")
            {
                foreach (Department dep in MainWindow.__Departments)
                    if (dep.Name == _CurrentDepartmentName && dep.LEmployees.Count > 0)
                    {
                        dep.RemoveEmployee(EmployeeSelected);
                    }
                foreach (Department dep in MainWindow.__Departments)
                    if (dep.Name == DepartmentSelected && dep.LEmployees.Count > 0)
                    {
                        dep.AddEmployee($"{name} {age} {salary}");
                    }
            }
        }
    }
}
