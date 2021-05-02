using Microsoft.EntityFrameworkCore;

namespace Kapitalist.RatesApi.Database
{
    public class RatesDataContext : DbContext
    {
        public RatesDataContext(DbContextOptions<RatesDataContext> options)
            : base(options)
        {
        }
    }
}