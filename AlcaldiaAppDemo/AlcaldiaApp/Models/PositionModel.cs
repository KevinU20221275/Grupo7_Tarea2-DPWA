using System.ComponentModel.DataAnnotations;

namespace AlcaldiaApp.Models
{
    public class PositionModel
    {
        public int Id { get; set; }

        [Display(Name = "Cargo")]
        public string Position { get; set; }

        [Display(Name = "Descripcion")]
        public string Description { get; set; }
    }
}
