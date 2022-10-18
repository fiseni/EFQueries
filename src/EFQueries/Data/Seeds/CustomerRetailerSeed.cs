namespace EFQueries.Data.Seeds;

public static class CustomerRetailerSeed
{
    public static List<CustomerRetailer> Get()
    {
        var customerRetailers = new List<CustomerRetailer>()
        {
            new()
            {
                RetailerId = 1,
                CustomerId = 1,
            },
            new()
            {
                RetailerId = 1,
                CustomerId = 2,
            },
        };
        return customerRetailers;
    }
}
