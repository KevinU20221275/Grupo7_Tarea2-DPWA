using AlcaldiaApp.Models;

namespace AlcaldiaApp.Repositories
{
    public interface IServiceRequestRepository
    {
		IServiceRequestRepository GetServiceRequestById(int id);

        IEnumerable<ServiceRequestRepository> GetServices();

        void Add(ServiceRequestRepository serviceRequest);

        void Edit(ServiceRequestRepository serviceRequest);

        void Delete(int id);
    }
}

