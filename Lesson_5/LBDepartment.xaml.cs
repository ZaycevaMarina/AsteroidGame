using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Lesson_5
{
    public partial class LBDepartment : Window
    {
        ObservableCollection<Employee> ItemsEmployees = new ObservableCollection<Employee>();
        private string EmployeeToRemove = "";
        public LBDepartment()
        {
            InitializeComponent();
        }
        public void FillListEmployees()
        {
            
            foreach(Department dep in MainWindow.__Departments)
                if(dep.Name == ViewModel)
                {
                    ItemsEmployees = dep.LEmployees;
                }
            lbEmployee.ItemsSource = ItemsEmployees;
        }
        public string ViewModel { get; set; }
        public void ShowViewModel()
        {
            FillListEmployees();
        }

        private void lbEmployee_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                EmployeeToRemove = e.AddedItems[0].ToString();
            }
            catch(Exception)
            {
                //СРабатывает при удалении, когда после удаления обновляется список сотрудников в lbEmployee.ItemsSource, т.е. в e.AddedItems[0]
            }
        }

        private void btnRemoveEmployee(object sender, RoutedEventArgs e)
        {
            foreach (Department dep in MainWindow.__Departments)
                if (dep.Name == ViewModel && dep.LEmployees.Count > 0)
                {
                    dep.RemoveEmployee(EmployeeToRemove);
                }
        }
    }
}
