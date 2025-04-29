
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Persistence;
using Persistence.Data;
using Services;
using Services.Abstraction;
using assmbly = Services.AssemblyReference;

namespace ECommerce
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

            builder.Services.AddDbContext<ECommerceDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IDbInitializer,DbInitializer>();

            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(assmbly).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            var app = builder.Build();


            #region Data Seeding
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync(); 
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
