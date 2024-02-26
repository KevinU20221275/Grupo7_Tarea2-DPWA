using AlcaldiaApp.Data;
using AlcaldiaApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace AlcaldiaApp.Repositories
{
    public class ResidentRepository : IResidentRepository
    {
        private readonly SqlDataAccess _dbConnection;

        public ResidentRepository(SqlDataAccess dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Get all residents
        public IEnumerable<ResidentModel> GetAllResidents()
        {
            List<ResidentModel> residentsList = new List<ResidentModel>();

            using (var connetion = _dbConnection.GetConnection())
            {
                connetion.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connetion;
                    command.CommandText = "SELECT Id, FirstName, LastName, Address, BirthDate FROM Residents;";
                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            ResidentModel resident = new ResidentModel();
                            resident.Id = Convert.ToInt32(reader["Id"]);
                            resident.FirstName = reader["FirstName"].ToString();
                            resident.LastName = reader["LastName"].ToString();
                            resident.Address = reader["Address"].ToString();
                            resident.BirthDate = Convert.ToDateTime(reader["BirthDate"]);

                            residentsList.Add(resident);
                        }
                    }
                }
            }
            return residentsList;
        }


        // Get one resident by Id
        public ResidentModel GetResidentById(int id)
        {
            ResidentModel resident = new ResidentModel();

            using (var connetion = _dbConnection.GetConnection())
            {
                connetion.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connetion;
                    command.CommandText = "SELECT Id, FirstName, LastName, Address, BirthDate FROM Residents " +
                                           " WHERE Id=@id;";
                    command.Parameters.AddWithValue("@id", id);
                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resident.Id = Convert.ToInt32(reader["Id"]);
                            resident.FirstName = reader["FirstName"].ToString();
                            resident.LastName = reader["LastName"].ToString();
                            resident.Address = reader["Address"].ToString();
                            resident.BirthDate = Convert.ToDateTime(reader["BirthDate"]);
                        }
                    }
                }
            }
            return resident;
        }


        // add a resident
        public void Add(ResidentModel resident)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO Residents VALUES(@FirstName, @LastName, @Address, @BirthDate)";

                    command.Parameters.AddWithValue("@FirstName", resident.FirstName);
                    command.Parameters.AddWithValue("@LastName", resident.LastName);
                    command.Parameters.AddWithValue("@Address", resident.Address);
                    command.Parameters.AddWithValue("@BirthDate", resident.BirthDate);

                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }


        // Edit a resident
        public void Edit(ResidentModel resident)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE Residents SET FirstName=@FirstName, LastName=@LastName, 
                                           Address=@Address, BirthDate=@BirthDate WHERE Id=@Id";

                    command.Parameters.AddWithValue("@FirstName", resident.FirstName);
                    command.Parameters.AddWithValue("@LastName", resident.LastName);
                    command.Parameters.AddWithValue("@Address", resident.Address);
                    command.Parameters.AddWithValue("@BirthDate", resident.BirthDate);
                    command.Parameters.AddWithValue("@Id", resident.Id);

                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }


        // Delete a resident
        public void Delete(int id)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"DELETE Residents WHERE Id=@Id";

                    command.Parameters.AddWithValue("@Id", id);

                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
