using System;
using CryptoCoins.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoCoins
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }
        public DbSet<Coins> CryptoCoins { get; set; }
    }
}
