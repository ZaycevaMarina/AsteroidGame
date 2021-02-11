using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;
using System;

namespace Lesson_7_DB_ADO.NET
{
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        SqlDataAdapter adapter;
        DataTable dt;
        string connectionString;
        public MainWindow()
        {
            InitializeComponent();
        }
        private ObservableCollection<string> _ItemsDepNames = new ObservableCollection<string>();

        private void GetDepartmentNames()
        {
            string sqlExpression = "SELECT TABLE_NAME FROM information_schema.TABLES WHERE TABLE_TYPE LIKE '%TABLE%'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.HasRows)        // Если есть данные
                {
                    while (reader.Read())  // Построчно считываем данные
                    {
                        if (reader.GetString(0) != "Employee")
                            _ItemsDepNames.Add(reader.GetString(0));
                    }
                }
                reader.Close();
            }
        }
        private void cbDeparments_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string sqlExpression = $"SELECT Id, Name, Age, Salary FROM Employee WHERE Id IN (SELECT * FROM [dbo].[{cbDeparments.SelectedItem.ToString()}])";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            adapter.SelectCommand = command;
            dt = new DataTable();
            adapter.Fill(dt);
            EmployeeDataGrid.DataContext = dt.DefaultView;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string connectionString = "data source=(LocalDb)\\MSSQLLocalDB;initial catalog=Lesson_7;integrated security=True;providerName="System.Data.SqlClient";
            //connectionString = Properties.Settings.Default.Test7ConnectionString;
            connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Lesson_7; Integrated Security = True";

            connection = new SqlConnection(connectionString);
            adapter = new SqlDataAdapter();
            GetDepartmentNames();
            cbDeparments.ItemsSource = _ItemsDepNames;
            string sqlExpression;
            if (_ItemsDepNames.Count > 0)
            {
                sqlExpression = $"SELECT Id, Name, Age, Salary FROM Employee WHERE Id IN (SELECT * FROM [dbo].[{_ItemsDepNames[0]}])";
                cbDeparments.SelectedItem = _ItemsDepNames[0];
            }
            else
                sqlExpression = "SELECT Id, Name, Age, Salary FROM Employee";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
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
            ObservableCollection<string> item = new ObservableCollection<string>();
            item.Add(cbDeparments.SelectedItem.ToString());
            EditWindow editWindow = new EditWindow(newRow, item, cbDeparments.SelectedItem.ToString());
            editWindow.Title = "Добавление нового сотрудника";
            editWindow.ShowDialog();
            if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
            {
                dt.Rows.Add(editWindow.ResultRow);                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    int age = Convert.ToInt32(editWindow.ResultRow["Age"]);
                    string name = editWindow.ResultRow["Name"].ToString();
                    double salary = Convert.ToDouble(editWindow.ResultRow["Salary"]);
                    string sqlExpression = @"INSERT INTO Employee (Name, Age, Salary) VALUES (@name, @age, @salary); SET @Id = @@IDENTITY;";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);                 
                    command.Parameters.Add(new SqlParameter("@name", name));
                    command.Parameters.Add(new SqlParameter("@age", age));
                    command.Parameters.Add(new SqlParameter("@salary", salary));
                    SqlParameter param = command.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                    param.Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();

                    // получим значения выходного параметра для вставки в талице Department_#(название хранится в cbDeparments.SelectedItem)
                    int id = Convert.ToInt32(param.Value);
                    editWindow.ResultRow["Id"] = id;
                    command = new SqlCommand($"INSERT INTO {cbDeparments.SelectedItem} (Id) VALUES (@id);", connection);
                    command.Parameters.Add(new SqlParameter("@id", id));
                    param.SourceVersion = DataRowVersion.Original;
                    command.ExecuteNonQuery();
                }
            }
        }
        private void btUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            DataRowView newRow = (DataRowView)EmployeeDataGrid.SelectedItem;
            if(newRow != null)
            {
                newRow.BeginEdit();
                EditWindow editWindow = new EditWindow(newRow.Row, _ItemsDepNames, cbDeparments.SelectedItem.ToString());
                editWindow.ShowDialog();
                if (editWindow.DialogResult.HasValue && editWindow.DialogResult.Value)
                {
                    newRow.EndEdit();
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        int id = Convert.ToInt32(editWindow.ResultRow["Id"]);
                        int age = Convert.ToInt32(editWindow.ResultRow["Age"]);
                        string name = editWindow.ResultRow["Name"].ToString();
                        double salary = Convert.ToDouble(editWindow.ResultRow["Salary"]);
                        string sqlExpression = @"UPDATE Employee SET Name = @Name, Age = @Age, Salary = @Salary WHERE Id = @Id";
                        SqlCommand command = new SqlCommand(sqlExpression, connection);
                        command.Parameters.Add(new SqlParameter("@Id", id));
                        command.Parameters.Add(new SqlParameter("@Name", name));
                        command.Parameters.Add(new SqlParameter("@Age", age));
                        command.Parameters.Add(new SqlParameter("@Salary", salary));
                        command.ExecuteNonQuery();

                        if(editWindow.ResultDepartment != cbDeparments.SelectedItem.ToString())
                        {
                            //Изменение отдела для сотрудника
                            //1. Удаление из таблицы Department_#(название хранится в cbDeparments.SelectedItem)
                            sqlExpression = $"DELETE FROM  {cbDeparments.SelectedItem} WHERE Id = @Id";
                            command = new SqlCommand(sqlExpression, connection);
                            command.Parameters.Add(new SqlParameter("@Id", id));
                            command.ExecuteNonQuery();
                            newRow.Row.Delete();
                            //2. Добавление в таблицу Department_#(название хранится в editWindow.ResultDepartment)
                            command = new SqlCommand($"INSERT INTO {editWindow.ResultDepartment} (Id) VALUES (@id);", connection);
                            command.Parameters.Add(new SqlParameter("@id", id));
                            command.ExecuteNonQuery();
                        }
                    }
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    int id = Convert.ToInt32(newRow["Id"]);
                    //Удаление из таблицы Employee
                    string sqlExpression = "DELETE FROM Employee WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlParameter param = command.Parameters.Add(new SqlParameter("@Id", id));
                    param.SourceVersion = DataRowVersion.Original;
                    command.ExecuteNonQuery();
                    //Удаление из таблицы Department_#(название хранится в cbDeparments.SelectedItem)
                    sqlExpression = $"DELETE FROM  {cbDeparments.SelectedItem} WHERE Id = @Id";
                    command = new SqlCommand(sqlExpression, connection);
                    param = command.Parameters.Add(new SqlParameter("@Id", id));
                    param.SourceVersion = DataRowVersion.Original;
                    command.ExecuteNonQuery();
                }
                newRow.Row.Delete();
            }
        }

        private void btAddDepartment_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string department_name = $"Department_{_ItemsDepNames.Count + 1}";
                string sqlExpression = $"CREATE TABLE[dbo].[{department_name}] ([Id] INT NOT NULL,CONSTRAINT[PK_dbo.{department_name}] PRIMARY KEY CLUSTERED([Id] ASC)); ";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                _ItemsDepNames.Add(department_name);
            }
        }
    }
}
