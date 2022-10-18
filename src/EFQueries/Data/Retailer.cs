namespace EFQueries.Data;

public class Retailer
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public int? MarketId { get; set; }
    public Market Market { get; set; } = default!;

    public List<CustomerRetailer> CustomerRetailers { get; set; } = new();
}
