namespace EFQueries.Data.Seeds;

public static class RetailerSeed
{
    public static List<Retailer> Get()
    {
        var retailers = new List<Retailer>()
        {
            new()
            {
                Id = 1,
                MarketId = 1,
                Name = "Retailer1",
            },
            new()
            {
                Id = 2,
                Name = "Retailer2",
            },
        };
        return retailers;
    }
}
