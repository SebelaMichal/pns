using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnsApp.Maui.Data
{
    public class Zakaznik
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(254)]
        public string Jmeno { get; set; }
        [MaxLength(254)]
        public string Prijmeni { get; set; }
        [MaxLength(50)]
        public string Telefon { get; set; }
        [MaxLength(254)]
        public string Email { get; set; }


    }

    public enum BarvaPozadi: int
    {
        Cervena = 1,
        Zelena = 2,
        Modra = 3
    }

    public class Pozadi
    {
        [Key]
        public int Id { get;set; }

        public BarvaPozadi BarvaPozadi { get; set; }
    }

    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            //this.Database.SetConnectionString("Server=192.168.1.57;Database=LuRaMi;User Id=lurami_user;Password=lurami_user;");
        }

        public virtual DbSet<Zakaznik> Zakaznik { get; set; }

        public virtual DbSet<Pozadi> Pozadi { get; set; }
    }

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=192.168.1.57;Database=LuRaMi;User Id=lurami_user;Password=lurami_user;TrustServerCertificate=True");
            //optionsBuilder.UseSqlServer("Server=192.168.88.209;Database=LuRaMi3;User Id=lurami_user;Password=lurami;Persist Security Info=True;Encrypt=True;TrustServerCertificate=True");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
