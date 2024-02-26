using AlcaldiaApp.Models;

namespace AlcaldiaApp.Repositories
{
    public interface IResidentRepository
    {
        IEnumerable<ResidentModel> GetAllResidents();

        ResidentModel GetResidentById(int id);

        void Add(ResidentModel resident);

        void Edit(ResidentModel resident);

        void Delete(int id);
    }
}
