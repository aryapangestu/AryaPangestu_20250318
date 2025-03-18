namespace AryaPangestu_20250318.Data.Models;

public partial class ShipmentStatusHistory
{
    public int StatusId { get; set; }

    public int ShipmentId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime StatusDate { get; set; }

    public string? Notes { get; set; }

    public virtual Shipment Shipment { get; set; } = null!;
}
