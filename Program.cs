
using EssentialProducts.Data;
using EssentialProducts.Service;
using Microsoft.EntityFrameworkCore;

namespace EssentialProducts.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var Configuration = builder.Configuration;
            // Add services to the container.
            builder.Services.AddDbContextPool<EssentialProductsDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Dbcontext")); 
            });

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

//for migration
//dotnet ef migrations add initialCreate -s ../EssentialProducts.API/EssentialProducts.API.csproj
//for database creation and update
//dotnet ef database update -s ../EssentialProducts.API/EssentialProducts.API.csproj
