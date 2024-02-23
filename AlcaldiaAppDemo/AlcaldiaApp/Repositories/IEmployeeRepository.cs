using AlcaldiaApp.Models;

namespace AlcaldiaApp.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeModel> GetAllEmployees();

        EmployeeModel GetEmployeeById(int id);

        IEnumerable<PositionModel> GetPositions();

        void Add (EmployeeModel employee);

        void Edit (EmployeeModel employee);

        void Delete (int id);
    }
}
