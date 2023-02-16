using Microsoft.EntityFrameworkCore;
using Scaffold.Models;

namespace Scaffold
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DatabaseContext(DbContextOptions<DatabaseContext> options,
                                                IConfiguration configuration) : base(options) 
        {
            this._configuration = configuration;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!String.IsNullOrEmpty(_sQLConnectionString))
            //{
            //    connectionString = _sQLConnectionString;
            //}
            //else
            //{
            //    connectionString = this._configuration.GetConnectionString("cityInfoDBConnectionString");
            //}
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(this._configuration.GetConnectionString("ScaffoldConnectionString"));
        }
    }
}
