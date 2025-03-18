using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AryaPangestu_20250318.Data.Models
{
    public class ShipmentStatusHistory
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        public int ShipmentId { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Status Date")]
        public DateTime StatusDate { get; set; } = DateTime.Now;

        [StringLength(255)]
        public string Notes { get; set; }

        [ForeignKey("ShipmentId")]
        public virtual Shipment Shipment { get; set; }
    }
}
