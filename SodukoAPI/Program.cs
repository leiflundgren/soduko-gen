namespace SodukoAPI
{
    public static class Program
    {

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            IWebHostBuilder builder = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

            IWebHost host = builder.Build();
            return host;
        }

        //var builder = WebApplication.CreateBuilder(args);


        //// Add services to the container.

        //var app = builder.Build();

        //// Configure the HTTP request pipeline.

        //var summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //app.MapGet("/weatherforecast", () =>
        //{
        //    var forecast = Enumerable.Range(1, 5).Select(index =>
        //       new WeatherForecast
        //       (
        //           DateTime.Now.AddDays(index),
        //           Random.Shared.Next(-20, 55),
        //           summaries[Random.Shared.Next(summaries.Length)]
        //       ))
        //        .ToArray();
        //    return forecast;
        //});

        //app.Use(async (context, next) =>
        //{
        //    Console.WriteLine($"1. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
        //    await next(context);
        //});

        //app.UseRouting();

        //app.Use(async (context, next) =>
        //{
        //    Console.WriteLine($"2. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
        //    await next(context);
        //});

        //app.MapGet("/", (HttpContext context) =>
        //{
        //    Console.WriteLine($"3. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
        //    return "Hello World!";
        //}).WithDisplayName("Hello");

        //app.UseEndpoints(_ => { });

        //app.Use(async (context, next) =>
        //{
        //    Console.WriteLine($"4. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
        //    await next(context);
        //});

        //app.UseEndpoints(endpoints =>
        //{
        //    endpoints.MapControllerRoute(
        //        name: "default",
        //        pattern: "{controller=Home}/{action=Index}/{id?}");
        //});

        //app.Run();

        //internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
        //{
        //    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        //}

    }
}