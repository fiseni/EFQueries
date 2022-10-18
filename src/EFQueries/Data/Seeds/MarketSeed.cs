namespace EFQueries.Data.Seeds;

public static class MarketSeed
{
    public static List<Market> Get()
    {
        var markets = new List<Market>()
        {
            new()
            {
                Id = 1,
                Name = "Market1",
            },
            new()
            {
                Id = 2,
                Name = "Market2",
            },
        };
        return markets;
    }
}
