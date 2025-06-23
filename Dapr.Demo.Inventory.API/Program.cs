
namespace Dapr.Demo.Inventory.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://0.0.0.0:5067");

            // Add services to the container.

            builder.Services.AddControllers().AddDapr();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            var app = builder.Build();


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCloudEvents();
            app.MapControllers();

            app.Run();
        }
    }
}
