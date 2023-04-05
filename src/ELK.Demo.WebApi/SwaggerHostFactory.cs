namespace ELK.Demo.WebApi
{
    public class SwaggerHostFactory
    {
        public static IHost CreateHost()
        {
            return Host.CreateDefaultBuilder(new string[0])
               .ConfigureWebHostDefaults(b => b.UseStartup<SwaggerStartup>())
               .Build();
        }

        private class SwaggerStartup
        {
            public SwaggerStartup(IConfiguration configuration)
            {
                Configuration = configuration;
            }

            public IConfiguration Configuration { get; }

            public void ConfigureServices(IServiceCollection services)
            {
                services.AddServices(Configuration);
            }

            public void Configure(IApplicationBuilder app) { }
        }
    }
}