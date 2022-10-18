namespace EFQueries;

public class Query3
{
    public static async Task RunAsync()
    {
        // Same as Query2, but LEFT join

        PrintSeparator("QUERY 3");

        await LinqExtensionsAsync();
        await LinqQueryAsync();
    }

    public static async Task LinqExtensionsAsync()
    {
        using var dbContext = new AppDbContext();

        // DefaultIfEmpty generates left join

        var query = dbContext.Retailers
            .SelectMany(r => r.CustomerRetailers.DefaultIfEmpty(), (r, cr) => new
            {
                Id = r.Id,
                Name = r.Name,
                CustomerName = cr.Customer.Name,
            });

        var result = await query.ToListAsync();

        Print(result);

        /*
         
      SELECT [r].[Id], [r].[Name], [c0].[Name] AS [CustomerName]
      FROM [Retailers] AS [r]
      LEFT JOIN [CustomerRetailers] AS [c] ON [r].[Id] = [c].[RetailerId]
      LEFT JOIN [Customers] AS [c0] ON [c].[CustomerId] = [c0].[Id]

        */
    }

    public static async Task LinqQueryAsync()
    {
        using var dbContext = new AppDbContext();

        var query = from r in dbContext.Retailers
                    join cr in dbContext.CustomerRetailers on r.Id equals cr.RetailerId into crGrouping
                    from cr in crGrouping.DefaultIfEmpty()
                    select new
                    {
                        Id = r.Id,
                        Name = r.Name,
                        CustomerName = cr.Customer.Name,
                    };

        var result = await query.ToListAsync();

        Print(result);

        /*
         
      SELECT [r].[Id], [r].[Name], [c0].[Name] AS [CustomerName]
      FROM [Retailers] AS [r]
      LEFT JOIN [CustomerRetailers] AS [c] ON [r].[Id] = [c].[RetailerId]
      LEFT JOIN [Customers] AS [c0] ON [c].[CustomerId] = [c0].[Id]

        */
    }
}
