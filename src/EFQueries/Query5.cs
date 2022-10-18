namespace EFQueries;

public class Query5
{
    public static async Task RunAsync()
    {
        // Same as Query4, but LEFT join

        PrintSeparator("QUERY 5");

        await LinqExtensionsAsync();
        await LinqQueryAsync();
    }

    public static async Task LinqExtensionsAsync()
    {
        using var dbContext = new AppDbContext();

        // As seen below can not generate all LEFT joins

        var query = dbContext.Retailers
            .SelectMany(r => r.CustomerRetailers.DefaultIfEmpty().SelectMany(cr => cr.Customer.CustomerVehicles.DefaultIfEmpty()).DefaultIfEmpty(), (r, cv) => new
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
      LEFT JOIN (
          SELECT [c1].[CustomerId], [c1].[VehicleId], [c].[RetailerId]
          FROM [CustomerRetailers] AS [c]
          LEFT JOIN [Customers] AS [c0] ON [c].[CustomerId] = [c0].[Id]
          INNER JOIN [CustomerVehicles] AS [c1] ON [c0].[Id] = [c1].[CustomerId]
      ) AS [t] ON [r].[Id] = [t].[RetailerId]
      LEFT JOIN [Customers] AS [c2] ON [t].[CustomerId] = [c2].[Id]
      LEFT JOIN [Vehicles] AS [v] ON [t].[VehicleId] = [v].[Id]

        */
    }

    public static async Task LinqQueryAsync()
    {
        using var dbContext = new AppDbContext();

        var query = from r in dbContext.Retailers
                    join cr in dbContext.CustomerRetailers on r.Id equals cr.RetailerId into crGrouping
                    from cr in crGrouping.DefaultIfEmpty()
                    join cv in dbContext.CustomerVehicles on cr.Customer.Id equals cv.CustomerId into cvGrouping
                    from cv in cvGrouping.DefaultIfEmpty()
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
      LEFT JOIN [CustomerRetailers] AS [c] ON [r].[Id] = [c].[RetailerId]
      LEFT JOIN [Customers] AS [c0] ON [c].[CustomerId] = [c0].[Id]
      LEFT JOIN [CustomerVehicles] AS [c1] ON [c0].[Id] = [c1].[CustomerId]
      LEFT JOIN [Vehicles] AS [v] ON [c1].[VehicleId] = [v].[Id]

        */
    }
}
