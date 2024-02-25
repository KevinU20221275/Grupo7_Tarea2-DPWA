using System.ComponentModel.DataAnnotations;

namespace AlcaldiaApp.Models
{
    public class ServiceRequestModel
    {
		[Display(Name = "id")]
		public int Id { get; set; }

		[Display(Name = "Residente")]
        public int ResidentId { get; set; }

        [Display(Name = "Servicio")]
        public int ServiceId { get; set; }

        [Display(Name = "Fecha de Solicitud")]
        public DateTime RequestDate { get; set; }

        [Display(Name = "Estado")]
        public bool Status { get; set; }

        [Display(Name = "Nombre del Residente")]
        public string ResidentName { get; set; }

        [Display(Name = "Servicio Solicitado")]
        public string ServiceName { get; set; }

    }
}
