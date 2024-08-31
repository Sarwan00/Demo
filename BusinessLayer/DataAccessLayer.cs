using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DataAccessLayer
    {
        public List<Employee> GetEmployeeDetails()
        {
            string sqlQuery = "SELECT * FROM People";
            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString"].ConnectionString;
            List<Employee> employees = new List<Employee>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        Employee employee = new Employee();

                        // Assuming your table has columns in this order: EmployeeID, FirstName, LastName, Department, HireDate, Salary
                        employee.Id = reader.GetInt32(0); // Assuming EmployeeID is the first column and of type int
                        employee.Name = reader.GetString(1); // Assuming FirstName is the second column and of type string
                        employee.City = reader.GetString(2); // Assuming LastName is the third column and of type string
                        employee.Gender = reader.GetString(3); // Assuming Department is the fourth column and of type string
                        employee.DateOfBirth = reader.GetString(4); // Assuming HireDate is the fifth column and of type DateTime
                        employees.Add(employee);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            return employees;
        }

        public void InsertEmployee(Employee employee)
        {
            string sqlInsert = "INSERT INTO Employee (Name, City, Gender, DateOfBirth) " +
                               "VALUES (@FirstName, @LastName, @Department, @HireDate, @Salary)";
            string connectionString = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString"].ConnectionString;


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlInsert, connection);

                // Add parameters to the command
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@City", employee.City);
                command.Parameters.AddWithValue("@Gender", employee.Gender);
                command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);


                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"Rows Inserted: {rowsAffected}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
