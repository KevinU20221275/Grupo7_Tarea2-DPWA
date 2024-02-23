using AlcaldiaApp.Models;

namespace AlcaldiaApp.Repositories
{
    public interface IServiceRequestRepository
    {
        ServiceRequestModel GetServiceRequestById(int id);

        IEnumerable<ServiceModel> GetServices();

        void Add(ServiceRequestModel serviceRequest);

        void Edit(ServiceRequestModel serviceRequest);

        void Delete(int id);
    }
}

