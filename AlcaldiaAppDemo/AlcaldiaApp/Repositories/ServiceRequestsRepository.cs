using AlcaldiaApp.Data;
using AlcaldiaApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;

namespace AlcaldiaApp.Repositories
{
    public class ServiceRequestRepository : IServiceRequestRepository
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
                    command.CommandText = "SELECT A.Id, A.ResidentId, A.ServiceId,B.FirstName + ' ' + B.LastName AS ResidentName, " +
                                            "C.Service AS ServiceName, RequestDate,Status " +
                                            "FROM ServiceRequests AS A " +
                                            "INNER JOIN Residents AS B " +
                                            "ON A.ResidentId = B.Id " +
                                            "INNER JOIN MunicipalServices AS C " +
                                            "ON A.ServiceId = C.Id";
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
                            serviceRequest.ResidentName = reader["ResidentName"].ToString();
                            serviceRequest.ServiceName = reader["ServiceName"].ToString();

                            serviceRequestList.Add(serviceRequest);
                        }
                    }
                }
            }
            return serviceRequestList;
        }


        // GetServiceRequestById
        public ServiceRequestModel? GetServiceRequestById(int id)
        {
            ServiceRequestModel serviceRequest = new ServiceRequestModel();

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT A.Id, ResidentId, ServiceId, B.FirstName + ' ' + B.LastName AS ResidentName, 
                                            C.Service AS ServiceName, RequestDate,Status " +
                                            "FROM ServiceRequests AS A " +
                                            "INNER JOIN Residents AS B " +
                                            "ON A.ResidentId = B.Id " +
                                            "INNER JOIN MunicipalServices AS C " +
                                            "ON A.ServiceId = C.Id " +
                                            "WHERE A.Id=@Id";

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
                            serviceRequest.ResidentName = reader["ResidentName"].ToString();
                            serviceRequest.ServiceName = reader["ServiceName"].ToString();
                        }
                    }
                }
            }
            return serviceRequest;
        }


        // Get Residents
        public IEnumerable<ResidentModel> GetAllResidents()
        {
            List<ResidentModel> residentsList = new List<ResidentModel>();
            using (var conection = _dbConnection.GetConnection())
            {
                conection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = conection;
                    command.CommandText = @"SELECT Id, FirstName + ' ' + LastName AS ResidentName FROM Residents";
                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ResidentModel resident = new ResidentModel();
                            resident.Id = Convert.ToInt32(reader["Id"]);
                            resident.FirstName = reader["ResidentName"].ToString();

                            residentsList.Add(resident);
                        }
                    }
                }
            }
            return residentsList;
        }


        // Get Municipal Services
        public IEnumerable<MunicipalServiceModel> GetAllMunicipalServices()
        {
            List<MunicipalServiceModel> municipalServicesList = new List<MunicipalServiceModel>();
            using (var conection = _dbConnection.GetConnection())
            {
                conection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = conection;
                    command.CommandText = "SELECT Id, Service FROM MunicipalServices";
                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MunicipalServiceModel municipalService = new MunicipalServiceModel();
                            municipalService.Id = Convert.ToInt32(reader["Id"]);
                            municipalService.ServiceName = reader["Service"].ToString();

                            municipalServicesList.Add(municipalService);
                        }
                    }
                }
            }
            return municipalServicesList;
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
                    command.CommandText = @"INSERT INTO ServiceRequests VALUES(@ResidentId, @ServiceId, @RequestDate, @Status)";

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
                    command.CommandText = @"UPDATE ServiceRequests SET ResidentId=@ResidentId, ServiceId=@ServiceId, 
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
                    command.CommandText = @"DELETE ServiceRequests WHERE Id=@Id";

                    command.Parameters.AddWithValue("@Id", id);

                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}