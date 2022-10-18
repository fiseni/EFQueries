namespace EFQueries.Data;

public class CustomerRetailer
{
    public int RetailerId { get; set; }
    public int CustomerId { get; set; }

    public Retailer Retailer { get; set; } = default!;
    public Customer Customer { get; set; } = default!;
}
