using AlcaldiaApp.Data;
using AlcaldiaApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace AlcaldiaApp.Repositories
{
    public class MunicipalServiceRepository : IMunicipalServiceRepository
    {
        // This variable is used to establish the connection to the database
        private readonly SqlDataAccess _dbConnection;

        public MunicipalServiceRepository(SqlDataAccess dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // GetAllMunicipalServices
        public IEnumerable<MunicipalServiceModel> GetAllMunicipalServices()
        {
            List<MunicipalServiceModel> municipalServicesList = new List<MunicipalServiceModel>();

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT Id, Service, Description, Cost FROM MunicipalServices;";
                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MunicipalServiceModel municipalService = new MunicipalServiceModel();
                            municipalService.Id = Convert.ToInt32(reader["Id"]);
                            municipalService.ServiceName = reader["Service"].ToString();
                            municipalService.Description = reader["Description"].ToString();
                            municipalService.Cost = Convert.ToDouble(reader["Cost"]);

                            municipalServicesList.Add(municipalService);
                        }
                    }
                }
            }
            return municipalServicesList;
        }


        // GetMunicipalServiceById
        public MunicipalServiceModel? GetMunicipalServiceById(int id)
        {
            MunicipalServiceModel municipalService = new MunicipalServiceModel();

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT Id, Service, Description, Cost FROM MunicipalServices" +
                                            " WHERE Id=@id;";
                    command.Parameters.AddWithValue("@id", id);
                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            municipalService.Id = Convert.ToInt32(reader["Id"]);
                            municipalService.ServiceName = reader["Service"].ToString();
                            municipalService.Description = reader["Description"].ToString();
                            municipalService.Cost = Convert.ToDouble(reader["Cost"]);
                        }
                    }
                }
            }
            return municipalService;
        }


        // Add Municipal Service
        public void Add(MunicipalServiceModel municipalService)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO MunicipalServices VALUES(@Service, @Description, @Cost)";

                    command.Parameters.AddWithValue("@Service", municipalService.ServiceName);
                    command.Parameters.AddWithValue("@Description", municipalService.Description);
                    command.Parameters.AddWithValue("@Cost", municipalService.Cost);

                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }


        // Edit Municipal Service
        public void Edit(MunicipalServiceModel municipalService)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE MunicipalServices SET Service=@Service, Description=@Description, 
                                           Cost=@Cost WHERE Id=@Id";

                    command.Parameters.AddWithValue("@Service", municipalService.ServiceName);
                    command.Parameters.AddWithValue("@Description", municipalService.Description);
                    command.Parameters.AddWithValue("@Cost", municipalService.Cost);
                    command.Parameters.AddWithValue("@Id", municipalService.Id);

                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }


        // Delete Municipal Service
        public void Delete(int id)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"DELETE MunicipalServices WHERE Id=@Id";

                    command.Parameters.AddWithValue("@Id", id);

                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
