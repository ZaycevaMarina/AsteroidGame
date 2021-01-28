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
        ObservableCollection<string> ItemsDepNames = new ObservableCollection<string>();
        public static List<Department> __Departments = new List<Department>();
        public MainWindow()
        {
            InitializeComponent();
            FillListEmployee();
        }
        void FillListEmployee()
        {
            __Departments.Add(new Department("Department1.txt"));
            __Departments.Add(new Department("Department2.txt"));
            __Departments.Add(new Department("Department3.txt"));
            foreach (Department dep in __Departments)
                ItemsDepNames.Add(dep.Name);
            cbDepNames.ItemsSource = ItemsDepNames;
        }
        private void cbDepNames_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            LBDepartment window_department = new LBDepartment();
            window_department.ViewModel = e.AddedItems[0].ToString();
            window_department.Show();
            window_department.ShowViewModel();
        }

        private void btnAddDepartment(object sender, RoutedEventArgs e)
        {
            Department new_dep = new Department(tbFileName.Text);
            foreach (Department dep in __Departments)
                if (dep.Name == new_dep.Name)
                    return;
            __Departments.Add(new_dep);
            ItemsDepNames.Add(new_dep.Name);
        }
    }    
}
