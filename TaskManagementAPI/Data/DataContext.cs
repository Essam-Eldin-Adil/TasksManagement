using Microsoft.EntityFrameworkCore;

namespace TaskManagementAPI.Data
{
    public class AppContext : DbContext
    {
        public AppContext() { }
        public AppContext(DbContextOptions<DbContext> options) : base(options) { }
    }
}
