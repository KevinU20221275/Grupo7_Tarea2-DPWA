using AlcaldiaApp.Models;

namespace AlcaldiaApp.Repositories
{
    public interface IPositionRepository
    {
        IEnumerable<PositionModel> GetAllPositions();

        PositionModel? GetPositionById(int id);

        void Add(PositionModel positionMoldel);

        void Edit(PositionModel positionMoldel);

        void Delete(int id);
    }
}
