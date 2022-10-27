using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OnlineShopDal.Databases.Postgres
{
    /// <summary>
    /// for ef migrations and updates
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PostgresDbContext>
    {
        public PostgresDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PostgresDbContext>();
            optionsBuilder.UseNpgsql("User ID=ShopUser;Password=AVNS_vEntuOQrfN6Rt3BhVxQ;Host=online-shop-database-do-user-12751369-0.b.db.ondigitalocean.com;Port=25060;Database=OnlineShop;sslmode=Require;Trust Server Certificate=true;");

            return new PostgresDbContext(optionsBuilder.Options);
        }
    }
}
