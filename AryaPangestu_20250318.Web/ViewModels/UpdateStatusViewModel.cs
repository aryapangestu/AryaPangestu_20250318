using System.ComponentModel.DataAnnotations;

namespace AryaPangestu_20250318.Web.ViewModels
{
    public class UpdateStatusViewModel
    {
        public int ShipmentId { get; set; }

        [Display(Name = "Tracking Number")]
        public string TrackingNumber { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }
    }
}
