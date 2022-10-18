namespace EFQueries.Data.Seeds;

public static class VehicleSeed
{
    public static List<Vehicle> Get()
    {
        var vehicles = new List<Vehicle>()
        {
            new()
            {
                Id = 1,
                Model = "Model1",
            },
            new()
            {
                Id = 2,
                Model = "Model2",
            },
        };
        return vehicles;
    }
}
