using AlcaldiaApp.Data;
using AlcaldiaApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace AlcaldiaApp.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        // This variable is used to establish the connection to the database
        private readonly SqlDataAccess _dbConnection;

        public EmployeeRepository(SqlDataAccess dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // GetAllEmployees
        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            List<EmployeeModel> employeeslist = new List<EmployeeModel>();

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT A.Id, FirstName, LastName, PositionId, Salary, B.Position AS PositionName " +
                                           "FROM Employees AS A " +
                                           "INNER JOIN Positions AS B " +
                                           "ON A.PositionId = B.Id";
                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read()) 
                        {
                            EmployeeModel employee = new EmployeeModel();
                            employee.Id = Convert.ToInt32(reader["Id"]);
                            employee.FirstName = reader["FirstName"].ToString();
                            employee.LastName = reader["LastName"].ToString();
                            employee.PositionId = Convert.ToInt32(reader["PositionId"]);
                            employee.Salary = Convert.ToDecimal(reader["Salary"]);
                            employee.PositionName = reader["PositionName"].ToString();

                            employeeslist.Add(employee);
                        }
                    }
                }
            }
            return employeeslist;
        }


        // GetEmployeeById
        public EmployeeModel GetEmployeeById(int id)
        {
            EmployeeModel employee = new EmployeeModel();

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT A.Id, FirstName, LastName, PositionId, Salary, B.Position AS PositionName 
                                            FROM Employees AS A 
                                            INNER JOIN Positions AS B 
                                            ON A.PositionID=B.Id 
                                            WHERE A.Id=@Id";

                    command.Parameters.AddWithValue("@Id", id);

                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employee.Id = Convert.ToInt32(reader["Id"]);
                            employee.FirstName = reader["FirstName"].ToString();
                            employee.LastName = reader["LastName"].ToString();
                            employee.PositionId = Convert.ToInt32(reader["PositionId"]);
                            employee.Salary = Convert.ToDecimal(reader["Salary"]);
                            employee.PositionName = reader["PositionName"].ToString();
                        }
                    }
                }
            }
            return employee;
        }


        // GetPositions
        public IEnumerable<PositionModel> GetPositions()
        {
            List<PositionModel> positionsList = new List<PositionModel>();
            using (var conection = _dbConnection.GetConnection())
            {
                conection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = conection;
                    command.CommandText = "SELECT Id, Position FROM Positions";
                    command.CommandType = CommandType.Text;
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PositionModel position = new PositionModel();
                            position.Id = Convert.ToInt32(reader["Id"]);
                            position.Position = reader["Position"].ToString();

                            positionsList.Add(position);
                        }
                    }
                }
            }
            return positionsList;
        }


        // Add Employee
        public void Add(EmployeeModel employee)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO Employees VALUES(@FirstName, @LastName, @PositionId, @Salary)";

                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@LastName", employee.LastName);
                    command.Parameters.AddWithValue("@PositionId", employee.PositionId);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);

                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }


        // Edit Employee
        public void Edit(EmployeeModel employee)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE Employees SET FirstName=@FirstName, LastName=@LastName, 
                                           PositionId=@PositionId, Salary=@Salary WHERE Id=@Id";

                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@LastName", employee.LastName);
                    command.Parameters.AddWithValue("@PositionId", employee.PositionId);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);
                    command.Parameters.AddWithValue("@Id", employee.Id);

                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }


        // Delete Employee
        public void Delete(int id)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"DELETE Employees WHERE Id=@Id";

                    command.Parameters.AddWithValue("@Id", id);

                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
