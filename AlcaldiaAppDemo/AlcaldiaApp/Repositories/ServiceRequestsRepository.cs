using AlcaldiaApp.Data;
using AlcaldiaApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace AlcaldiaApp.Repositories
{
    public class ServiceRequestRepository : ServiceRequestRepository
    {
        // This variable is used to establish the connection to the database
        private readonly SqlDataAccess _dbConnection;

       public ServiceRequestRepository(SqlDataAccess dbConnection)
    {
        _dbConnection = dbConnection;
    }

    // GetAllServiceRequestRepository
    public IEnumerable<ServiceRequestModel> GetAllServiceRequests()
    {
        List<ServiceRequestModel> serviceRequestList = new List<ServiceRequestModel>();

        using (var connection = _dbConnection.GetConnection())
        {
            connection.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "SELECT * FROM tbl_ServiceRequests";
                command.CommandType = CommandType.Text;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ServiceRequestModel serviceRequest = new ServiceRequestModel();
                        serviceRequest.Id = Convert.ToInt32(reader["Id"]);
                        serviceRequest.ResidentId = Convert.ToInt32(reader["ResidentId"]);
                        serviceRequest.ServiceId = Convert.ToInt32(reader["ServiceId"]);
                        serviceRequest.RequestDate = Convert.ToDateTime(reader["RequestDate"]);
                        serviceRequest.Status = Convert.ToBoolean(reader["Status"]);

                        serviceRequestList.Add(serviceRequest);
                    }
                }
            }
        }
        return serviceRequestList;
    }


    // GetServiceRequestById
    public ServiceRequestModel GetServiceRequestById(int id)
    {
        ServiceRequestModel serviceRequest = new ServiceRequestModel();

        using (var connection = _dbConnection.GetConnection())
        {
            connection.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = @"SELECT * FROM tbl_ServiceRequests WHERE Id=@Id";

                command.Parameters.AddWithValue("@Id", id);

                command.CommandType = CommandType.Text;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        serviceRequest.Id = Convert.ToInt32(reader["Id"]);
                        serviceRequest.ResidentId = Convert.ToInt32(reader["ResidentId"]);
                        serviceRequest.ServiceId = Convert.ToInt32(reader["ServiceId"]);
                        serviceRequest.RequestDate = Convert.ToDateTime(reader["RequestDate"]);
                        serviceRequest.Status = Convert.ToBoolean(reader["Status"]);
                    }
                }
            }
        }
        return serviceRequest;
    }


    // GetServices
    public IEnumerable<ServiceModel> GetServices()
    {
        List<ServiceModel> servicesList = new List<ServiceModel>();
        using (var conection = _dbConnection.GetConnection())
        {
            conection.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = conection;
                command.CommandText = "SELECT Id, Service FROM tbl_MunicipalServices";
                command.CommandType = CommandType.Text;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ServiceModel service = new ServiceModel();
                        service.Id = Convert.ToInt32(reader["Id"]);
                        service.Service = reader["Service"].ToString();

                        servicesList.Add(service);
                    }
                }
            }
        }
        return servicesList;
    }


    // Add ServiceRequest
    public void Add(ServiceRequestModel serviceRequest)
    {
        using (var connection = _dbConnection.GetConnection())
        {
            connection.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = @"INSERT INTO tbl_ServiceRequests VALUES(@ResidentId, @ServiceId, @RequestDate, @Status)";

                command.Parameters.AddWithValue("@ResidentId", serviceRequest.ResidentId);
                command.Parameters.AddWithValue("@ServiceId", serviceRequest.ServiceId);
                command.Parameters.AddWithValue("@RequestDate", serviceRequest.RequestDate);
                command.Parameters.AddWithValue("@Status", serviceRequest.Status);

                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
        }
    }


    // Edit ServiceRequest
    public void Edit(ServiceRequestModel serviceRequest)
    {
        using (var connection = _dbConnection.GetConnection())
        {
            connection.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = @"UPDATE tbl_ServiceRequests SET ResidentId=@ResidentId, ServiceId=@ServiceId, 
                                       RequestDate=@RequestDate, Status=@Status WHERE Id=@Id";

                command.Parameters.AddWithValue("@ResidentId", serviceRequest.ResidentId);
                command.Parameters.AddWithValue("@ServiceId", serviceRequest.ServiceId);
                command.Parameters.AddWithValue("@RequestDate", serviceRequest.RequestDate);
                command.Parameters.AddWithValue("@Status", serviceRequest.Status);
                command.Parameters.AddWithValue("@Id", serviceRequest.Id);

                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
        }
    }


    // Delete ServiceRequest
    public void Delete(int id)
    {
        using (var connection = _dbConnection.GetConnection())
        {
            connection.Open();
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = @"DELETE tbl_ServiceRequests WHERE Id=@Id";

                command.Parameters.AddWithValue("@Id", id);

                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
        }
    }
  }
}