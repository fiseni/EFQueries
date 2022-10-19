namespace EFQueries.Data.Seeds;

public static class CustomerSeed
{
    public static List<Customer> Get()
    {
        var customers = new List<Customer>()
        {
            new()
            {
                Id = 1,
                Name = "Customer1",
                Type = "CustomerType1",
            },
            new()
            {
                Id = 2,
                Name = "Customer2",
                Type = "CustomerType2",
            },
        };
        return customers;
    }
}
