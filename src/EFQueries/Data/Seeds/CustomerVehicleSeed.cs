namespace EFQueries.Data.Seeds;

public static class CustomerVehicleSeed
{
    public static List<CustomerVehicle> Get()
    {
        var customerVehicles = new List<CustomerVehicle>()
        {
            new()
            {
                CustomerId = 1,
                VehicleId = 1
            },
            new()
            {
                CustomerId = 1,
                VehicleId = 2
            },
        };
        return customerVehicles;
    }
}
