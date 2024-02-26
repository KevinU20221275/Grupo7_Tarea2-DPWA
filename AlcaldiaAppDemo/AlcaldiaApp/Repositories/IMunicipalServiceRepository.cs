using AlcaldiaApp.Models;

namespace AlcaldiaApp.Repositories
{
    public interface IMunicipalServiceRepository
    {
        IEnumerable<MunicipalServiceModel> GetAllMunicipalServices();

        MunicipalServiceModel GetMunicipalServiceById(int id);

        void Add (MunicipalServiceModel municipalService);

        void Edit(MunicipalServiceModel municipalService);

        void Delete(int id);
    }
}
