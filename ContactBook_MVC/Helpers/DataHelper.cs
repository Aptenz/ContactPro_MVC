using ContactPro_MVC.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactPro_MVC.Helpers
{
    public static class DataHelper
    {
        public static async Task ManageDataAsync(IServiceProvider svcProvider)
        {
            //get an instance of db application context
            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();

            //migration: this is equivalent to update-database
            await dbContextSvc.Database.MigrateAsync();
        }
    }
}
