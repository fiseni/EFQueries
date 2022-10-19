using EFQueries;

// Comment it out after you run it once.
await DbInitializer.SeedAsync();

await Query1.RunAsync();
await Query2.RunAsync();
await Query3.RunAsync();
await Query4.RunAsync();
await Query5.RunAsync();
await Query6.RunAsync();







// Seed information
// Retailer1 is related to Customer1 and Customer2, while Customer1 is related to Vehicle1 and Vehicle2
// Retailer2 is not related to any customers.
// Customer2 is not related to any vehicles.

/*
select * from Retailers r
left join Markets m on r.MarketId = m.Id
left join CustomerRetailers cr on cr.RetailerId = r.Id
left join Customers c on c.Id = cr.CustomerId
left join CustomerVehicles cv on cv.CustomerId = c.Id
left join Vehicles v on cv.VehicleId = v.Id

Id	Name	    MarketId	Id	    Name	    RetailerId	CustomerId	Id	    Name	    CustomerId	VehicleId	Id	    Model
1	Retailer1	1	        1	    Market1	    1	        1	        1	    Customer1	1	        1	        1	    Model1
1	Retailer1	1	        1	    Market1	    1	        1	        1	    Customer1	1	        2	        2	    Model2
1	Retailer1	1	        1	    Market1	    1	        2	        2	    Customer2	NULL	    NULL	    NULL	NULL
2	Retailer2	NULL	    NULL	NULL	    NULL	    NULL	    NULL	NULL	    NULL	    NULL	    NULL	NULL
*/
