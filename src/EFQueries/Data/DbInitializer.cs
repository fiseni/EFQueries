using EFQueries.Data.Seeds;

namespace EFQueries.Data;

public class DbInitializer
{
    public static async Task SeedAsync()
    {
        using (var dbContext = new AppDbContext())
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            if (!await dbContext.Customers.AnyAsync())
            {
                dbContext.AddRange(MarketSeed.Get());
                dbContext.AddRange(RetailerSeed.Get());
                dbContext.AddRange(VehicleSeed.Get());
                dbContext.AddRange(CustomerSeed.Get());
                dbContext.AddRange(CustomerRetailerSeed.Get());
                dbContext.AddRange(CustomerVehicleSeed.Get());

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
