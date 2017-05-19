using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using planB.Models;

namespace planBMigrations
{
    [ContextType(typeof(PlanBDbContext))]
    partial class InitialMigration
    {
        public override string Id
        {
            get { return "20170518113110_InitialMigration"; }
        }

        public override string ProductVersion
        {
            get { return "7.0.0-beta6-13815"; }
        }

        public override void BuildTargetModel(ModelBuilder builder)
        {
            builder
                .Annotation("ProductVersion", "7.0.0-beta6-13815");

            builder.Entity("planB.Models.Korisnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatumRodjenja");

                    b.Property<string>("Email");

                    b.Property<string>("Ime");

                    b.Property<string>("KorisnickoIme");

                    b.Property<string>("Lozinka");

                    b.Property<string>("Prezime");

                    b.Property<byte[]>("Slika");

                    b.Key("ID");
                });

            builder.Entity("planB.Models.MuzickaKolekcija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("KorisnikID");

                    b.Property<string>("Naziv");

                    b.Key("ID");
                });

            builder.Entity("planB.Models.Obaveza", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Datum");

                    b.Property<int?>("KorisnikID");

                    b.Property<int>("KreatorID");

                    b.Property<int>("Prioritet");

                    b.Property<string>("Sadrzaj");

                    b.Property<int>("Vidljivost");

                    b.Key("ID");
                });

            builder.Entity("planB.Models.Pjesma", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Izvodjac");

                    b.Property<int?>("MuzickaKolekcijaID");

                    b.Property<string>("Naziv");

                    b.Key("ID");
                });

            builder.Entity("planB.Models.StavkaDnevnika", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Datum");

                    b.Property<int?>("KorisnikID");

                    b.Property<int>("KreatorID");

                    b.Property<string>("Naslov");

                    b.Property<string>("Sadrzaj");

                    b.Property<int>("Vidljivost");

                    b.Key("ID");
                });

            builder.Entity("planB.Models.MuzickaKolekcija", b =>
                {
                    b.Reference("planB.Models.Korisnik")
                        .InverseCollection()
                        .ForeignKey("KorisnikID");
                });

            builder.Entity("planB.Models.Obaveza", b =>
                {
                    b.Reference("planB.Models.Korisnik")
                        .InverseCollection()
                        .ForeignKey("KorisnikID");
                });

            builder.Entity("planB.Models.Pjesma", b =>
                {
                    b.Reference("planB.Models.MuzickaKolekcija")
                        .InverseCollection()
                        .ForeignKey("MuzickaKolekcijaID");
                });

            builder.Entity("planB.Models.StavkaDnevnika", b =>
                {
                    b.Reference("planB.Models.Korisnik")
                        .InverseCollection()
                        .ForeignKey("KorisnikID");
                });
        }
    }
}
