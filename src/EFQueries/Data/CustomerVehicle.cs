namespace EFQueries.Data;

public class CustomerVehicle
{
    public int CustomerId { get; set; }
    public int VehicleId { get; set; }

    public Customer Customer { get; set; } = default!;
    public Vehicle Vehicle { get; set; } = default!;
}
