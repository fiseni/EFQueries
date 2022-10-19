namespace EFQueries;

public class Query2
{
    public static async Task RunAsync()
    {
        PrintSeparator(nameof(Query2).ToUpper());

        await LinqExtensionsAsync();
        await LinqQueryAsync();
    }

    public static async Task LinqExtensionsAsync()
    {
        using var dbContext = new AppDbContext();

        // SelectMany generates inner join

        var query = dbContext.Retailers
            .SelectMany(r => r.CustomerRetailers, (r, cr) => new
            {
                Id = r.Id,
                Name = r.Name,
                CustomerId = cr.Customer.Id,
                CustomerName = cr.Customer.Name,
            });

        var result = await query.ToListAsync();

        Print(result);

        /*
         
      SELECT [r].[Id], [r].[Name], [c0].[Id] AS [CustomerId], [c0].[Name] AS [CustomerName]
      FROM [Retailers] AS [r]
      INNER JOIN [CustomerRetailers] AS [c] ON [r].[Id] = [c].[RetailerId]
      INNER JOIN [Customers] AS [c0] ON [c].[CustomerId] = [c0].[Id]

        */
    }

    public static async Task LinqQueryAsync()
    {
        using var dbContext = new AppDbContext();

        var query = from r in dbContext.Retailers
                    join cr in dbContext.CustomerRetailers on r.Id equals cr.RetailerId
                    select new
                    {
                        Id = r.Id,
                        Name = r.Name,
                        CustomerId = cr.Customer.Id,
                        CustomerName = cr.Customer.Name,
                    };

        var result = await query.ToListAsync();

        Print(result);

        /*
         
      SELECT [r].[Id], [r].[Name], [c0].[Id] AS [CustomerId], [c0].[Name] AS [CustomerName]
      FROM [Retailers] AS [r]
      INNER JOIN [CustomerRetailers] AS [c] ON [r].[Id] = [c].[RetailerId]
      INNER JOIN [Customers] AS [c0] ON [c].[CustomerId] = [c0].[Id]

        */
    }
}
