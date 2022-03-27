using Microsoft.EntityFrameworkCore;

namespace RehnusTestWebAPI.DataModels
{
    /// <summary>
    /// DBContext created to perform CRUD operations in DB.
    /// </summary>
    public class RehnusTestDBContext : DbContext
    {
        public RehnusTestDBContext()
        { 
        
        }
        public RehnusTestDBContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
