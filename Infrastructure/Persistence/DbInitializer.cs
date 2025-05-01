﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Data.Identity;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;
        private readonly StoreIdentityDbContext _identityDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            StoreDbContext context,
            StoreIdentityDbContext identityDbContext,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            ) 
        {
            _context = context;
            _identityDbContext = identityDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task InitializeAsync()
        {
            try
            {
                // Create Database If it Dosen't Exists && Apply To Any Pending Migrations

                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }

                // Data Seeding

                // Seeding ProductTypes From Json Files

                if (!_context.ProductTypes.Any())
                {
                    // 1. Read All Data From Types Json File as String

                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");


                    // 2. Transform String To C# Objects [List<ProductTypes>]

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    // 3. Add List<ProductTypes> To Database

                    if (types is not null && types.Any())
                    {
                        await _context.ProductTypes.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }

                }

                // Seeding ProductBrands From Json Files

                if (!_context.ProductBrands.Any())
                {
                    // 1. Read All Data From brands Json File as String

                    var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");


                    // 2. Transform String To C# Objects [List<ProductBrand>]

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    // 3. Add List<ProductBrands> To Database

                    if (brands is not null && brands.Any())
                    {
                        await _context.ProductBrands.AddRangeAsync(brands);
                        await _context.SaveChangesAsync();
                    }

                }

                // Seeding Products From Json Files

                if (!_context.Products.Any())
                {
                    // 1. Read All Data From Products Json File as String

                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");


                    // 2. Transform String To C# Objects [List<Products>]

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    // 3. Add List<Products> To Database

                    if (products is not null && products.Any())
                    {
                        await _context.Products.AddRangeAsync(products);
                        await _context.SaveChangesAsync();
                    }

                }

            }
            catch (Exception) 
            {
                throw;
            }
        }

        public async Task InitializeIdentityeAsync()
        {
            // Create Database If it Dosen't Exists && Apply To Any Pending Migrations
            if (_identityDbContext.Database.GetPendingMigrations().Any())
            {
               await _identityDbContext.Database.MigrateAsync();
            }

            if(!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin",
                });
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "SuperAdmin",
                });
            }

            // Seeding Identity Data

            if(! _userManager.Users.Any())
            {
                var superAdminUser = new AppUser()
                {
                    DisplayName = "Super Admin",
                    Email = "SuperAdmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "0123456789",
                };
                var adminUser = new AppUser()
                {
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "0123456789",
                };
                await _userManager.CreateAsync(superAdminUser, "P@ssW0rd");
                await _userManager.CreateAsync(adminUser, "P@ssW0rd");

                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(adminUser, "Admin");

            }

        }

    }
}
