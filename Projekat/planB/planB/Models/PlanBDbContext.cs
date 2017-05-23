using Microsoft.Data.Entity;
using planB.DBModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace planB.Models
{
    public class PlanBDbContext : DbContext
    {
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Obaveza> Obaveze { get; set; }
        public DbSet<StavkaDnevnika> Dnevnik { get; set; }
        public DbSet<Pjesma> Pjesme { get; set; }
        public DbSet<MuzickaKolekcija> MuzickaKolekcija { get; set; }
        public DbSet<Follow> Follow { get; set; }
        


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databaseFilePath = "PlanBBaza.db";
            try
            {
                databaseFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseFilePath);
            }
            catch (InvalidOperationException) { }

            optionsBuilder.UseSqlite($"Data source={databaseFilePath}");
        }



    }
}
