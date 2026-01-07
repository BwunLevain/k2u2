using k2u2.LibraryClient;
using k2u2.LibraryClient.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<LibraryDbContext>();

builder.Services.AddScoped<MenuService>();
builder.Services.AddScoped<MenuHandlers>();
builder.Services.AddScoped<MenuUI>();

using IHost host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    try
    {
        var menuUi = scope.ServiceProvider.GetRequiredService<MenuUI>();
        menuUi.ShowMainMenu();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n[SYSTEM ERROR]: {ex.Message}");
        Console.WriteLine("Press any key to return to menu...");
        Console.ReadKey();
    }
}