using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SimpleSales.Api.Databases;
using SimpleSales.Api.MappingProfiles.Order;
using SimpleSales.Api.MappingProfiles.Product;
using SimpleSales.Api.Services;

namespace SimpleSales.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            string connectionString = Configuration.GetConnectionString("default");
            services.AddDbContext<AppDbContext>(c => c.UseSqlServer(connectionString));

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProductDtoMappingProfile());
                mc.AddProfile(new OrderModelMappingProfile());
                mc.AddProfile(new OrderDtoMappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo { Title = "Simple Sales API", Version = "v1"});
                o.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "ApiKey",
                    Description = "Authorization header using the API Key",
                });
                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
                        },
                        new string[] { }
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(o => { o.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple Sales API v1"); });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
