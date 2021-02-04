using System.Data;
using System.Windows;

namespace Lesson_7_DB_ADO.NET
{
    public partial class EditWindow : Window
    {
        public DataRow ResultRow { get; set; }
        public EditWindow(DataRow dataRow)
        {
            InitializeComponent();
            ResultRow = dataRow;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbName.Text = ResultRow["Name"].ToString();
            tbAge.Text = ResultRow["Age"].ToString();
            tbSalary.Text = ResultRow["Salary"].ToString();
        }
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            ResultRow["Name"] = tbName.Text;
            ResultRow["Age"] = tbAge.Text;
            ResultRow["Salary"] = tbSalary.Text;
            DialogResult = true;
        }
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
