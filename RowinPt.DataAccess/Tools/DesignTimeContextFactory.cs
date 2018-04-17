using Microsoft.EntityFrameworkCore.Design;
using System;

namespace RowinPt.DataAccess.Tools
{
    public class DesignTimeContextFactory : IDesignTimeDbContextFactory<RowinPtContext>
    {
        public RowinPtContext CreateDbContext(string[] args)
        {
            var database = "RowinPt";

            var connectionString = $"Server=.;Database={database};Trusted_Connection=True;MultipleActiveResultSets=true";
            return new RowinPtContext(connectionString, Guid.Empty);
        }
    }
}
