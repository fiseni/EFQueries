using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFQueries.Data;

public class AppDbContext : DbContext
{
    public DbSet<Market> Markets => Set<Market>();
    public DbSet<Retailer> Retailers => Set<Retailer>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<CustomerRetailer> CustomerRetailers => Set<CustomerRetailer>();
    public DbSet<CustomerVehicle> CustomerVehicles => Set<CustomerVehicle>();


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EFQueries;Integrated Security=SSPI;ConnectRetryCount=0;";

        optionsBuilder.EnableSensitiveDataLogging().UseSqlServer(connectionString).LogTo(Console.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Market>().Property(x => x.Id).ValueGeneratedNever();
        modelBuilder.Entity<Retailer>().Property(x => x.Id).ValueGeneratedNever();
        modelBuilder.Entity<Vehicle>().Property(x => x.Id).ValueGeneratedNever();
        modelBuilder.Entity<Customer>().Property(x => x.Id).ValueGeneratedNever();

        modelBuilder.Entity<CustomerRetailer>().HasKey(x => new { x.CustomerId, x.RetailerId });
        modelBuilder.Entity<CustomerVehicle>().HasKey(x => new { x.CustomerId, x.VehicleId });
    }
}
