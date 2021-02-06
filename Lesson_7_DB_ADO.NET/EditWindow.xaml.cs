using System.Collections.ObjectModel;
using System.Data;
using System.Windows;

namespace Lesson_7_DB_ADO.NET
{
    public partial class EditWindow : Window
    {
        public DataRow ResultRow { get; private set; }
        public string ResultDepartment { get; private set; }
        private string _CurrentDepartmentName;
        private ObservableCollection<string> _DepartmentNames;
        public EditWindow(DataRow dataRow, ObservableCollection<string> department_names, string current_department_name)
        {
            InitializeComponent();
            ResultRow = dataRow;
            _DepartmentNames = department_names;
            _CurrentDepartmentName = current_department_name;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbName.Text = ResultRow["Name"].ToString();
            tbAge.Text = ResultRow["Age"].ToString();
            tbSalary.Text = ResultRow["Salary"].ToString();
            cbDepartment.SelectedItem = _CurrentDepartmentName;
            cbDepartment.ItemsSource = _DepartmentNames;
        }
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            ResultRow["Name"] = tbName.Text;
            ResultRow["Age"] = tbAge.Text;
            ResultRow["Salary"] = tbSalary.Text;
            ResultDepartment = cbDepartment.SelectedItem.ToString();
            DialogResult = true;
        }
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
