using System;
using System.Collections.Generic;

namespace AryaPangestu_20250318.Data.Models;

public partial class Shipment
{
    public int ShipmentId { get; set; }

    public string TrackingNumber { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string SenderName { get; set; } = null!;

    public string SenderPhone { get; set; } = null!;

    public string SenderAddress { get; set; } = null!;

    public string SenderPostCode { get; set; } = null!;

    public string RecipientName { get; set; } = null!;

    public string RecipientPhone { get; set; } = null!;

    public string RecipientAddress { get; set; } = null!;

    public string RecipientPostCode { get; set; } = null!;

    public decimal Weight { get; set; }

    public decimal Length { get; set; }

    public decimal Width { get; set; }

    public decimal Height { get; set; }

    public string CurrentStatus { get; set; } = null!;

    public virtual ICollection<ShipmentStatusHistory> ShipmentStatusHistories { get; set; } = new List<ShipmentStatusHistory>();
}
