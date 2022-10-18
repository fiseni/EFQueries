namespace EFQueries.Data;

public class Vehicle
{
    public int Id { get; set; }
    public string? Model { get; set; }

    public List<CustomerVehicle> CustomerVehicles { get; set; } = new();
}
