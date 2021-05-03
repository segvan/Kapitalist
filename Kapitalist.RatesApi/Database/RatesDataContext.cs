using Kapitalist.RatesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Kapitalist.RatesApi.Database
{
    public class RatesDataContext : DbContext
    {
        public DbSet<RatesSnapshot> RatesSnapshots { get; set; }
        
        public RatesDataContext(DbContextOptions<RatesDataContext> options)
            : base(options)
        {
        }
    }
}