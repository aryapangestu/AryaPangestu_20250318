using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AryaPangestu_20250318.Data.Models
{
    public class Shipment
    {
        public Shipment()
        {
            StatusHistory = new HashSet<ShipmentStatusHistory>();
        }

        [Key]
        public int ShipmentId { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Tracking Number")]
        public string TrackingNumber { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Sender Information
        [Required]
        [StringLength(100)]
        [Display(Name = "Sender Name")]
        public string SenderName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Sender Phone")]
        public string SenderPhone { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Sender Address")]
        public string SenderAddress { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Sender Postal Code")]
        public string SenderPostCode { get; set; }

        // Recipient Information
        [Required]
        [StringLength(100)]
        [Display(Name = "Recipient Name")]
        public string RecipientName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Recipient Phone")]
        public string RecipientPhone { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Recipient Address")]
        public string RecipientAddress { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Recipient Postal Code")]
        public string RecipientPostCode { get; set; }

        // Package Information
        [Required]
        [Display(Name = "Weight (kg)")]
        public decimal Weight { get; set; }

        [Required]
        [Display(Name = "Length (cm)")]
        public decimal Length { get; set; }

        [Required]
        [Display(Name = "Width (cm)")]
        public decimal Width { get; set; }

        [Required]
        [Display(Name = "Height (cm)")]
        public decimal Height { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Current Status")]
        public string CurrentStatus { get; set; } = "Created";

        // Navigation property
        public virtual ICollection<ShipmentStatusHistory> StatusHistory { get; set; }
    }
}
