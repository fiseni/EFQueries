namespace EFQueries.Data;

public class Customer
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public List<CustomerRetailer> CustomerRetailers { get; set; } = new();
    public List<CustomerVehicle> CustomerVehicles { get; set; } = new();
}
