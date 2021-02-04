using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Lesson_7_DB_ADO.NET
{
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        SqlDataAdapter adapter;
        DataTable dt;
        public MainWindow()
        {
            InitializeComponent();
        }
    //     <startup> 
    //    <supportedRuntime version = "v4.0" sku=".NETFramework,Version=v4.7.2" />
    //</startup>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string connectionString = "data source=(LocalDb)\\MSSQLLocalDB;initial catalog=Lesson_7;integrated security=True;providerName="System.Data.SqlClient";
            //connectionString = Properties.Settings.Default.Test7ConnectionString;
            string connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Lesson_7; Integrated Security = True";

            connection = new SqlConnection(connectionString);
            adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT Id, Name, Age, Salary FROM Employee", connection);
            adapter.SelectCommand = command;
            //insert
            command = new SqlCommand(@"INSERT INTO Employee (Name, Age, Salary) VALUES (@Name, @Age, @Salary); SET @Id = @@IDENTITY;", connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Age", SqlDbType.Int, 0, "Age");
            command.Parameters.Add("@Salary", SqlDbType.Float, 0, "Salary");
            SqlParameter param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.Direction = ParameterDirection.Output;
            adapter.InsertCommand = command;
            // update
            command = new SqlCommand(@"UPDATE Employee SET Name = @Name, Age = @Age, Salary = @Salary WHERE Id = @Id", connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, -1, "Name");
            command.Parameters.Add("@Age", SqlDbType.Int, 0, "Age");
            command.Parameters.Add("@Salary", SqlDbType.Float, 0, "Salary");
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapter.UpdateCommand = command;
            //delete
            command = new SqlCommand("DELETE FROM Employee WHERE Id = @Id", connection);
            param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
            param.SourceVersion = DataRowVersion.Original;
            adapter.DeleteCommand = command;
            dt = new DataTable();
            adapter.Fill(dt);
            EmployeeDataGrid.DataContext = dt.DefaultView;
        }
        private void btAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            // Добавим новую строку
            DataRow newRow = dt.NewRow();
            EditWindow editWindow = new EditWindow(newRow);
            editWindow.ShowDialog();
            if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
            {
                dt.Rows.Add(editWindow.ResultRow);
                adapter.Update(dt);
            }
        }
        private void btUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)EmployeeDataGrid.SelectedItem;
            if(newRow != null)
            {
                newRow.BeginEdit();
                EditWindow editWindow = new EditWindow(newRow.Row);
                editWindow.ShowDialog();
                if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
                {
                    newRow.EndEdit();
                    adapter.Update(dt);
                }
                else
                {
                    newRow.CancelEdit();
                }
            }           
        }
        private void btDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)EmployeeDataGrid.SelectedItem;
            if (newRow != null)
            {
                newRow.Row.Delete();
                adapter.Update(dt);
            }
        }


    }
}
