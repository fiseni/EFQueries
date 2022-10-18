namespace EFQueries;

public class Query4
{
    public static async Task RunAsync()
    {
        PrintSeparator("QUERY 4");

        await LinqExtensionsAsync();
        await LinqQueryAsync();
    }

    public static async Task LinqExtensionsAsync()
    {
        using var dbContext = new AppDbContext();

        var query = dbContext.Retailers
            .SelectMany(r => r.CustomerRetailers.SelectMany(cr => cr.Customer.CustomerVehicles), (r, cv) => new
            {
                Id = r.Id,
                Name = r.Name,
                CustomerName = cv.Customer.Name,
                VehicleModel = cv.Vehicle.Model
            });

        var result = await query.ToListAsync();

        Print(result);

        /*
         
      SELECT [r].[Id], [r].[Name], [c2].[Name] AS [CustomerName], [v].[Model] AS [VehicleModel]
      FROM [Retailers] AS [r]
      INNER JOIN (
          SELECT [c1].[CustomerId], [c1].[VehicleId], [c].[RetailerId]
          FROM [CustomerRetailers] AS [c]
          INNER JOIN [Customers] AS [c0] ON [c].[CustomerId] = [c0].[Id]
          INNER JOIN [CustomerVehicles] AS [c1] ON [c0].[Id] = [c1].[CustomerId]
      ) AS [t] ON [r].[Id] = [t].[RetailerId]
      INNER JOIN [Customers] AS [c2] ON [t].[CustomerId] = [c2].[Id]
      INNER JOIN [Vehicles] AS [v] ON [t].[VehicleId] = [v].[Id]

        */
    }

    public static async Task LinqQueryAsync()
    {
        using var dbContext = new AppDbContext();

        // Generates much better and cleaner query

        var query = from r in dbContext.Retailers
                    join cr in dbContext.CustomerRetailers on r.Id equals cr.RetailerId
                    join cv in dbContext.CustomerVehicles on cr.Customer.Id equals cv.CustomerId
                    select new
                    {
                        Id = r.Id,
                        Name = r.Name,
                        CustomerName = cr.Customer.Name,
                        VehicleModel = cv.Vehicle.Model
                    };

        var result = await query.ToListAsync();

        Print(result);

        /*
         
      SELECT [r].[Id], [r].[Name], [c0].[Name] AS [CustomerName], [v].[Model] AS [VehicleModel]
      FROM [Retailers] AS [r]
      INNER JOIN [CustomerRetailers] AS [c] ON [r].[Id] = [c].[RetailerId]
      INNER JOIN [Customers] AS [c0] ON [c].[CustomerId] = [c0].[Id]
      INNER JOIN [CustomerVehicles] AS [c1] ON [c0].[Id] = [c1].[CustomerId]
      INNER JOIN [Vehicles] AS [v] ON [c1].[VehicleId] = [v].[Id]

        */
    }
}
