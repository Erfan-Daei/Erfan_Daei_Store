using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Store.Persistence.Contexts
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            string _ConnectionString = @"Data Source=LENOVO-THINKBOO\SQLEXPRESS; Initial Catalog=Practice_Store_DB; Integrated Security=True; TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(_ConnectionString);

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
