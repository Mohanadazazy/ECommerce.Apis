
using Domain.Contracts;
using ECommerce.Extensions;
using ECommerce.Middlewares;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Persistence;
using Persistence.Data;
using Services;
using Services.Abstraction;
using Shared.ErrorModels;


namespace ECommerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.RegisterAllServices(builder.Configuration);

            var app = builder.Build();

            

            await app.ConfigureMiddelware();

            app.Run();
        }
    }
}
