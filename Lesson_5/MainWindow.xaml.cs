using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace Lesson_6_Binding_Trigger
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> _ItemsDepNames = new ObservableCollection<string>();
        public static List<Department> __Departments = new List<Department>();
        public MainWindow()
        {
            InitializeComponent();
            FillListDepartment();
            //Binding binding = new Binding();
            //binding.ElementName = "__Departments";                 // Элемент-источник
            //binding.Path = new PropertyPath("Name");               // Свойство элемента-источника
            //lvDepNames.SetBinding(TextBlock.TextProperty, binding);// Установка привязки для элемента-приемника
        }
        void FillListDepartment()
        {
            __Departments.Add(new Department("Department1.txt"));
            __Departments.Add(new Department("Department2.txt"));
            __Departments.Add(new Department("Department3.txt"));
            foreach (Department dep in __Departments)
                _ItemsDepNames.Add(dep.Name);
            lvDepNames.ItemsSource = _ItemsDepNames;//ItemsSource ="{Binding ElementName=MainWindow.__Departments, Path=Name }"
            //lvDepNames.ItemsSource = "{Binding ElementName=__Departments, Path=Name}";
        }
        private void lvDepNames_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            LBDepartment window_department = new LBDepartment();
            window_department.Title = "Отдел " + e.AddedItems[0].ToString();
            window_department._CurrentDepartmentName = e.AddedItems[0].ToString();
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
            _ItemsDepNames.Add(new_dep.Name);
        }
    }    
}
