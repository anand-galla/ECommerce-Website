using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try 
            {
                if (!context.ProductBrand.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    brands.ForEach((brand) => {
                        context.ProductBrand.Add(brand);
                    });

                    await context.SaveChangesAsync();
                }

                if (!context.ProductType.Any())
                {
                    var productTypesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);
                    types.ForEach((type) => {
                        context.ProductType.Add(type);
                    });

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    products.ForEach((product) => {
                        context.Products.Add(product);
                    });

                    await context.SaveChangesAsync();
                }            
            }
            catch (Exception ex) 
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, "An error occured while seeding the data");
                
            }
        }
    }
}