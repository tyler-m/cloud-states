
namespace CloudStates
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            WebApplication app = builder.Build();

            app.MapGet("/", () => "Cloud States");

            app.Run();
        }
    }
}
