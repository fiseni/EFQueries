namespace EFQueries;

public class Query1
{
    public static async Task RunAsync()
    {
        PrintSeparator(nameof(Query1).ToUpper());

        await LinqExtensionsAsync();
        await LinqQueryAsync();
    }

    public static async Task LinqExtensionsAsync()
    {
        using var dbContext = new AppDbContext();

        var query = dbContext.Retailers
            .Select(r => new
            {
                Id = r.Id,
                Name = r.Name,
                MarketName = r.Market.Name,
            });

        var result = await query.ToListAsync();

        Print(result);

        /*
         
        SELECT [r].[Id], [r].[Name], [m].[Name] AS [MarketName]
        FROM [Retailers] AS [r]
        LEFT JOIN [Markets] AS [m] ON [r].[MarketId] = [m].[Id]

        */
    }

    public static async Task LinqQueryAsync()
    {
        using var dbContext = new AppDbContext();

        var query = from r in dbContext.Retailers
                    select new
                    {
                        Id = r.Id,
                        Name = r.Name,
                        MarketName = r.Market.Name,
                    };

        var result = await query.ToListAsync();

        Print(result);

        /*
         
        SELECT [r].[Id], [r].[Name], [m].[Name] AS [MarketName]
        FROM [Retailers] AS [r]
        LEFT JOIN [Markets] AS [m] ON [r].[MarketId] = [m].[Id]

        */
    }
}
