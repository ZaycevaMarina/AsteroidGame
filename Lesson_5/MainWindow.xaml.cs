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
        ObservableCollection<Employee> items = new ObservableCollection<Employee>();
        List<Employee> LEmployee = new List<Employee>();
        public MainWindow()
        {
            InitializeComponent();
            FillList();
        }
        void FillList()
        {
            lbEmployee.ItemsSource = items;
            LEmployee.Add(new Employee("Vasya", 22, 3000));
            LEmployee.Add(new Employee("Petya", 25, 6000));
            LEmployee.Add(new Employee("Kolya", 23, 8000));
            foreach (Employee emp in LEmployee)
                items.Add(emp);
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
            Employee emp = new Employee("Sergey", 26, 7000);
            LEmployee.Add(emp);
            items.Add(emp);
        }
    }    
}
