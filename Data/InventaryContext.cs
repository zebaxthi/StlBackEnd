using Microsoft.EntityFrameworkCore;
using StlBackend.Models;
using StlBackend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StlBackend.Data
{
    public class InventaryContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<InventoryModel> Inventories { get; set; }
        public DbSet<ProductModel> Products { get; set; }

        public InventaryContext(DbContextOptions<InventaryContext> options) : base(options)
        {

        }

    }
}
