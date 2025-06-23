
namespace Dapr.Demo.OrderingService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseUrls("http://0.0.0.0:5122","http://0.0.0.0:5000");

            // Add services to the container.

            builder.Services.AddControllers().AddDapr();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


            var app = builder.Build();


            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Enable Dapr middleware
            app.UseCloudEvents();
            app.MapSubscribeHandler(); // for pub/sub

            app.MapControllers();

            app.Run();
        }
    }
}
//http://172.24.234.110:5000/orders?productId=1&qty=20