using System.ComponentModel.DataAnnotations;

namespace AlcaldiaApp.Models
{
    public class ResidentModel
    {
        public int Id { get; set; }

        [Display(Name ="Nombres")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Display(Name = "Direccion")]
        public string Address { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public DateTime BirthDate { get; set; }
    }
}
