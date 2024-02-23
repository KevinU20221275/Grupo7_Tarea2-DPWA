using System.ComponentModel.DataAnnotations;

namespace AlcaldiaApp.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }

        [Display (Name="Nombre")]
        public string FirstName { get; set; }

        [Display (Name = "Apellido") ]
        public string LastName { get; set; }

        [Display (Name = "Cargo")]
        public int PositionId { get; set; }

        [Display (Name = "Salario")]
        public decimal Salary {  get; set; }

        [Display (Name ="Cargo")]
        public string PositionName {  get; set; }
    }
}
