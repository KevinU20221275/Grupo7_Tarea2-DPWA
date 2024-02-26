using System.ComponentModel.DataAnnotations;

namespace AlcaldiaApp.Models
{
    public class MunicipalServiceModel
    {
        public int Id { get; set; }

<<<<<<< HEAD
        [Display (Name = "Nombre del Servicio")]
        public string ServiceName { get; set; }

        [Display (Name = "Descripcion")]
        public string Description { get; set; }

        [Display (Name = "Costo")]
=======
        [Display(Name = "Nombre del servicio")]
        public string ServiceName { get; set; }

        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [Display(Name = "Costo")]
>>>>>>> d128b33e7bbc75fc55fdc6fee633c06c15ebf477
        public double Cost { get; set; }
    }
}
