
using Amazon.API.Middleware;
using E_Commerce.API.Filters;
using E_Commerce.API.Middleware;
using E_Commerce.API.Profiles;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.DataSeed;
using E_Commerce.Infrastructure.Utilities;
using E_Commerce.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<ECommerceContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            builder.Services.AddScoped(typeof(IUnitOfWork) , typeof(UnitOfWork));

            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddLogging(options =>
            {
                options.AddDebug();
            });

            builder.Services.AddScoped<ValidationFilter>();


            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<ECommerceContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {

                await _dbContext.Database.MigrateAsync();
                DataSeeding.AddData(_dbContext);

            }catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred while applying migrations");
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<ProfilingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
