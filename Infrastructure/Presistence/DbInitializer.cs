using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ECommerceDbContext _context;

        public DbInitializer(ECommerceDbContext eCommerceDb)
        {
            _context = eCommerceDb;
        }

        

        public async Task InitializeAsync()
        {
            try
            {
                // Create DataBase If It Doesnot Exist && Apply To Any Pending Migrations
                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }

                // Data Seeding

                // Seeding ProductTypes From Json File



                if (!_context.ProductTypes.Any())
                {
                    // 1. Read All Data From types Json File As String
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\types.json");

                    // 2. Transform String To C# Objects [List<ProductTypes>]

                    var productTypes = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    // 3. Add List<ProductTypes> To DataBase
                    if (productTypes is not null && productTypes.Any())
                    {
                        await _context.ProductTypes.AddRangeAsync(productTypes);
                        await _context.SaveChangesAsync();
                    }
                }


                // Seeding ProductBrands From Json File
                if (!_context.ProductBrands.Any())
                {
                    // 1. Read All Data From types Json File As String

                    var brandsdata = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\brands.json");

                    // 2. Transform String To C# Objects [List<ProductBrand>]

                    var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsdata);

                    // 3. Add List<ProductBrand> To DataBase
                    if (productBrands is not null && productBrands.Any())
                    {
                        await _context.ProductBrands.AddRangeAsync(productBrands);
                        await _context.SaveChangesAsync();
                    }
                }
                // Seeding Products From Json File

                if (!_context.Products.Any())
                {
                    // 1. Read All Data From Json File As String
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presistence\Data\Seeding\products.json");

                    // 2. Transform String To C# Object [List<Product>]
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    // 3. Add List<Product> To DataBase
                    if (products is not null && products.Any())
                    {
                        await _context.Products.AddRangeAsync(products);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
