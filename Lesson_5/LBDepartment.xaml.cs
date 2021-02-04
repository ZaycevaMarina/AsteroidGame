using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Lesson_6_Binding_Trigger
{
    public partial class LBDepartment : Window
    {
        private ObservableCollection<Employee> _ItemsEmployees = new ObservableCollection<Employee>();
        private ObservableCollection<string> _ItemsDepNames = new ObservableCollection<string>();
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
                    _ItemsEmployees = dep.LEmployees;
                }
            lvEmployee.ItemsSource = _ItemsEmployees;
        }
        void FillListCbDepartment()
        {            
            foreach (Department dep in MainWindow.__Departments)
                _ItemsDepNames.Add(dep.Name);
            cbDepNames.ItemsSource = _ItemsDepNames;
        }
        public string _CurrentDepartmentName { get; set; }
        public void ShowViewModel()
        {
            FillListEmployees();
            FillListCbDepartment();
        }

        private void lvEmployee_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
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
            catch(System.IndexOutOfRangeException)
            {
                //СРабатывает при удалении, когда после удаления обновляется список сотрудников в lvEmployee.ItemsSource, 
                //т.е. в e.AddedItems[0]
                //MessageBox.Show(exp.ToString());
            }
        }

        private void btnRemoveEmployee(object sender, RoutedEventArgs e)
        {
            foreach (Department dep in MainWindow.__Departments)
                if (dep.Name == _CurrentDepartmentName && dep.LEmployees.Count > 0)
                {
                    if (int.TryParse(EmployeeSelected.Remove(EmployeeSelected.IndexOf('\t')), out int id))
                    {
                        dep.RemoveEmployee(id);
                        //lvEmployee.Items.Refresh();
                    }
                    break;
                }
        }
        private void btnAddEmployee(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbAge.Text, out int age))
                return;
            if (!double.TryParse(tbSalary.Text, out double salary))
                return;
            foreach (Department dep in MainWindow.__Departments)
                if (dep.Name == _CurrentDepartmentName/* && dep.LEmployees.Count > 0*/)
                {
                    dep.AddEmployee(tbName.Text, age, salary);
                    break;
                }
        }
        private void cbDepNames_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                cbDepNames.Text = e.AddedItems[0].ToString();
                DepartmentSelected = e.AddedItems[0].ToString();
            }
        }

        public void btUpdateEmployee(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text;
            if (!int.TryParse(tbId.Text, out int id) || name == "" || !int.TryParse(tbAge.Text, out int age) || !double.TryParse(tbSalary.Text, out double salary))
                return;
            
            if (_CurrentDepartmentName == DepartmentSelected)
            {
                if (EmployeeSelected == tbId.Text.ToString() + "\t" + name + "\t" + age + "\t" + salary)
                {
                    MessageBox.Show("Данный сотрудник уже есть в текущем отделе"); ;
                    return;
                }
                foreach (Department dep in MainWindow.__Departments)
                    if (dep.Name == _CurrentDepartmentName && dep.LEmployees.Count > 0)
                    {
                        dep.UpdateEmployee(id, name, age, salary);
                        lvEmployee.Items.Refresh();
                        break;
                    }
            }
            else if(DepartmentSelected != "")
            {
                foreach (Department dep in MainWindow.__Departments)
                    if (dep.Name == _CurrentDepartmentName && dep.LEmployees.Count > 0)
                    {
                        if (int.TryParse(EmployeeSelected.Remove(EmployeeSelected.IndexOf('\t')), out id))
                        {
                            dep.RemoveEmployee(id);
                            lvEmployee.Items.Refresh();
                        }
                        break;
                    }
                foreach (Department dep in MainWindow.__Departments)
                    if (dep.Name == DepartmentSelected && dep.LEmployees.Count > 0)
                    {
                        dep.AddEmployee(name, age, salary);
                        break;
                        //try
                        //{
                            //dep.AddEmployee(name, age, salary);
                        //}
                        //catch(DuplicateWaitObjectException exp)
                        //{ MessageBox.Show(exp.ToString()); }//Исключение генерирутся в Depatment.cs
                    }
            }
        }
        //<ListBox Margin = "10,10,351,0" Name="lvEmployee" SelectionMode="Single" 
        //         SelectionChanged="lvEmployee_SelectionChanged" VerticalAlignment="Top" Height="352" Grid.ColumnSpan="3"></ListBox>
    }
}
