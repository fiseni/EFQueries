namespace EFQueries;

public class Query6
{
    public static async Task RunAsync()
    {
        PrintSeparator(nameof(Query6).ToUpper());

        // The case when we need data from the first item in the inner collection

        await LinqExtensionsAsync();
        await LinqExtensionsOption2Async();
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
                CustomerName = r.CustomerRetailers.First().Customer.Name,
                CustomerType = r.CustomerRetailers.First().Customer.Type,
            });


        var result = await query.ToListAsync();

        Print(result);


        /*
         
      SELECT [r].[Id], [r].[Name], (
          SELECT TOP(1) [c0].[Name]
          FROM [CustomerRetailers] AS [c]
          INNER JOIN [Customers] AS [c0] ON [c].[CustomerId] = [c0].[Id]
          WHERE [r].[Id] = [c].[RetailerId]) AS [CustomerName], (
          SELECT TOP(1) [c2].[Type]
          FROM [CustomerRetailers] AS [c1]
          INNER JOIN [Customers] AS [c2] ON [c1].[CustomerId] = [c2].[Id]
          WHERE [r].[Id] = [c1].[RetailerId]) AS [CustomerType]
      FROM [Retailers] AS [r]

        */
    }

    public static async Task LinqExtensionsOption2Async()
    {
        using var dbContext = new AppDbContext();

        var query = dbContext.Retailers
            .SelectMany(r => r.CustomerRetailers.DefaultIfEmpty().Take(1), (r, cr) => new
            {
                Id = r.Id,
                Name = r.Name,
                CustomerName = cr.Customer.Name,
                CustomerType = cr.Customer.Type,
            });


        var result = await query.ToListAsync();

        Print(result);


        /*
         
      SELECT [r].[Id], [r].[Name], [c0].[Name] AS [CustomerName], [c0].[Type] AS [CustomerType]
      FROM [Retailers] AS [r]
      LEFT JOIN (
          SELECT [t].[CustomerId], [t].[RetailerId]
          FROM (
              SELECT [c].[CustomerId], [c].[RetailerId], ROW_NUMBER() OVER(PARTITION BY [c].[RetailerId] ORDER BY [c].[CustomerId], [c].[RetailerId]) AS [row]
              FROM [CustomerRetailers] AS [c]
          ) AS [t]
          WHERE [t].[row] <= 1
      ) AS [t0] ON [r].[Id] = [t0].[RetailerId]
      LEFT JOIN [Customers] AS [c0] ON [t0].[CustomerId] = [c0].[Id]

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
                        CustomerName = r.CustomerRetailers.First().Customer.Name,
                        CustomerType = r.CustomerRetailers.First().Customer.Type,
                    };

        var result = await query.ToListAsync();

        Print(result);

        /*
         
      SELECT [r].[Id], [r].[Name], (
          SELECT TOP(1) [c0].[Name]
          FROM [CustomerRetailers] AS [c]
          INNER JOIN [Customers] AS [c0] ON [c].[CustomerId] = [c0].[Id]
          WHERE [r].[Id] = [c].[RetailerId]) AS [CustomerName], (
          SELECT TOP(1) [c2].[Type]
          FROM [CustomerRetailers] AS [c1]
          INNER JOIN [Customers] AS [c2] ON [c1].[CustomerId] = [c2].[Id]
          WHERE [r].[Id] = [c1].[RetailerId]) AS [CustomerType]
      FROM [Retailers] AS [r]

        */
    }
}
