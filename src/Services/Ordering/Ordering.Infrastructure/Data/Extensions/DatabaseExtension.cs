using Microsoft.AspNetCore.Builder;

namespace Ordering.Infrastructure.Data.Extensions;

public static class DatabaseExtension
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await context.SeedAsync();
    }

    private static async Task SeedAsync(this ApplicationDbContext context)
    {
        await SeedCustomerAsync(context);
        await SeedProductAsync(context);
        await SeedOrdersWithItemsAsync(context);
    }
    
    private static async Task SeedCustomerAsync(ApplicationDbContext context)
    {
        if (!await context.Customers.AnyAsync())
        {
            await context.Customers.AddRangeAsync(InitialData.Customers);
            await context.SaveChangesAsync();
        }
    }
    
    private static async Task SeedProductAsync(ApplicationDbContext context)
    {
        if (!await context.Products.AnyAsync())
        {
            await context.Products.AddRangeAsync(InitialData.Products);
            await context.SaveChangesAsync();
        }
    }
    
    private static async Task SeedOrdersWithItemsAsync(ApplicationDbContext context)
    {
        foreach (var order in InitialData.OrdersWithItems)
        {
            var customer = InitialData.Customers.FirstOrDefault(c => c.Id == order.CustomerId);
            if (customer == null)
            {
                Console.WriteLine($"Customer with ID {order.CustomerId.Value} not found for order {order.Id.Value}.");
            }
        }

        
        if (!await context.Orders.AnyAsync())
        {
            await context.Orders.AddRangeAsync(InitialData.OrdersWithItems);
            await context.SaveChangesAsync();
        }
    }
}