using AlcaldiaApp.Data;
using AlcaldiaApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace AlcaldiaApp.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        // This variable is used to establish the connection to the database
        private readonly SqlDataAccess _dbConnection;

        public PositionRepository(SqlDataAccess dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // GetAllPositions
        public IEnumerable<PositionModel> GetAllPositions()
        {
            List<PositionModel> positionsList = new List<PositionModel>();

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand()) 
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT Id, Position, Description FROM Positions;";

                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PositionModel position = new PositionModel();
                            position.Id = Convert.ToInt32(reader["Id"]);
                            position.Position = reader["Position"].ToString();
                            position.Description = reader["Description"].ToString();

                            positionsList.Add(position);
                        }
                    }
                }
            }
            return positionsList;
        }


        // GetPositionById
        public PositionModel? GetPositionById(int id)
        {
            PositionModel position = new PositionModel();

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT Id, Position, Description FROM Positions WHERE Id=@Id;";

                    command.Parameters.AddWithValue("@Id", id);

                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            position.Id = Convert.ToInt32(reader["Id"]);
                            position.Position = reader["Position"].ToString();
                            position.Description = reader["Description"].ToString();
                        } 
                    }
                }
            }
            return position;
        }


        // Add Position
        public void Add(PositionModel positionModel)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"INSERT INTO Positions VALUES(@Position, @Description);";

                    command.Parameters.AddWithValue("@Position", positionModel.Position);
                    command.Parameters.AddWithValue("@Description", positionModel.Description);

                    command.CommandType = CommandType.Text;

                    command.ExecuteNonQuery();
                }
            }
        }


        // Edit Position
        public void Edit(PositionModel positionModel)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"UPDATE Positions SET Position=@Position, Description=@Description " +
                                          "WHERE Id=@Id;";

                    command.Parameters.AddWithValue("@Position", positionModel.Position);
                    command.Parameters.AddWithValue("@Description", positionModel.Description);
                    command.Parameters.AddWithValue("@Id", positionModel.Id);

                    command.CommandType = CommandType.Text;

                    command.ExecuteNonQuery();
                }
            }
        }


        // Delete Position
        public void Delete(int id)
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"DELETE Positions WHERE Id=@id;";

                    command.Parameters.AddWithValue("@id", id);

                    command.CommandType = CommandType.Text;

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
