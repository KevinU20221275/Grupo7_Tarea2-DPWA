using System.ComponentModel.DataAnnotations;

namespace AlcaldiaApp.Models
{
    public class MunicipalServiceModel
    {
        public int Id { get; set; }

        [Display (Name = "Nombre del Servicio")]
        public string ServiceName { get; set; }

        [Display (Name = "Descripcion")]
        public string Description { get; set; }

        [Display (Name = "Costo")]
        public double Cost { get; set; }
    }
}
