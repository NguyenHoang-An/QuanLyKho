using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Client.Data;

namespace Client.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-588LLJ4;Database=Client-Data;Integrated Security=True;Pooling=False;Encrypt=False;Trust Server Certificate=True");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
