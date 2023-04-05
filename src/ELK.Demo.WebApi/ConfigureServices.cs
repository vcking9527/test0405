using System.Text.Json.Serialization;
using ELK.Demo.WebApi.Models;
using ELK.Demo.WebApi.Route;
using ELK.Demo.WebApi.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Models;
using Nest;

namespace ELK.Demo.WebApi
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWebApi(configuration);
            services.AddSwagger(configuration);
            services.AddElasticSearch(configuration);

            services.AddSingleton<IAccountSearch, AccountSearch>();

            return services;
        }

        public static void AddWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            // 設定Controller
            services.AddControllers(o =>
            {
                // 回應的格式
                // o.Filters.Add(new ConsumesAttribute("application/json"));
                o.Filters.Add(new ProducesAttribute("application/json"));
                // 路由小寫
                o.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                // options.SuppressModelStateInvalidFilter = true;
                options.SuppressMapClientErrors = true;
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ELK-Demo-WebApi",
                    Version = "v1",
                    Description = "Demo ELK",
                    TermsOfService = new Uri("https://www.google.com/"),
                    Contact = new OpenApiContact
                    {
                        Name = "YOUR_NAME",
                        Email = "YOUR_EMAIL"
                    },
                });

                // 添加所有專案的XML註解
                foreach (var xmlFilename in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
                {
                    c.IncludeXmlComments(xmlFilename);
                }
            });

        }

        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var baseUrl = configuration["ElasticSettings:baseUrl"];
            var index = configuration["ElasticSettings:defaultIndex"];
            var username = configuration["ElasticSettings:username"];
            var password = configuration["ElasticSettings:password"];
            var settings = new ConnectionSettings(new Uri(baseUrl ?? ""))
                .BasicAuthentication(username, password)
                .PrettyJson()
                .DefaultIndex(index)
                .DefaultMappingFor<Account>(i => i.IndexName("accounts").IdProperty(p => p.AccountNumber));
            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);
        }
    }
}