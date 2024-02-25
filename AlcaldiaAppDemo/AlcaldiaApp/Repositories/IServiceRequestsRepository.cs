using AlcaldiaApp.Models;

namespace AlcaldiaApp.Repositories
{
    public interface IServiceRequestRepository
    {
        IEnumerable<ServiceRequestModel> GetAllServiceRequests();

        ServiceRequestModel GetServiceRequestById(int id);

        IEnumerable<MunicipalServiceModel> GetAllMunicipalServices();

        IEnumerable<ResidentModel> GetAllResidents();

        void Add(ServiceRequestModel serviceRequest);

        void Edit(ServiceRequestModel serviceRequest);

        void Delete(int id);
    }
}

