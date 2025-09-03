using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace MrLeastAcademy.Models
{
    public class AppDbContext : DbContext
    {
      public  DbSet<Department> Departments { get; set; }
      public  DbSet<Employee> Employees { get; set; }

        public AppDbContext():base()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("Models\\appsetting.json")
                .Build();
            var constr = configuration.GetSection("constr").Value;

            optionsBuilder.UseSqlServer(constr);

            base.OnConfiguring(optionsBuilder);

        }

    }
}
