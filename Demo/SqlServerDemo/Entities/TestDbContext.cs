using Microsoft.EntityFrameworkCore;
using Suyaa.EFCore.SqlServer;
using Suyaa.Hosting.EFCore.Dependency;

namespace SqlServerDemo.Entities
{
    public class TestDbContext : SqlServerContext
    {
        private readonly IDbConnectionDescriptorFactory _dbConnectionDescriptorFactory;

        public TestDbContext(
            IDbConnectionDescriptorFactory dbConnectionDescriptorFactory
            ) : base(dbConnectionDescriptorFactory.DefaultConnection)
        {
            _dbConnectionDescriptorFactory = dbConnectionDescriptorFactory;
            //this.Set<SystemObjects>();
            //this.Set<SystemTables>();
        }

        public DbSet<SystemTables> SystemTableses { get; set; }
    }
}
