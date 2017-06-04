using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations.Infrastructure;
using planB.Models;

namespace planBMigrations
{
    [ContextType(typeof(PlanBDbContext))]
    partial class MyDatabase
    {
        public override string Id
        {
            get { return "20170603105310_MyDatabase"; }
        }

        public override string ProductVersion
        {
            get { return "7.0.0-beta6-13815"; }
        }

        public override void BuildTargetModel(ModelBuilder builder)
        {
            builder
                .Annotation("ProductVersion", "7.0.0-beta6-13815");

            builder.Entity("planB.DBModels.Follow", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Following_KorisnikID");

                    b.Property<string>("KorisnikID");

                    b.Key("ID");
                });

            builder.Entity("planB.Models.Korisnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatumRodjenja");

                    b.Property<string>("Email");

                    b.Property<string>("Ime");

                    b.Property<string>("KorisnickoIme");

                    b.Property<int?>("KorisnikID");

                    b.Property<string>("Lozinka");

                    b.Property<string>("Prezime");

                    b.Property<byte[]>("Slika");

                    b.Property<string>("idAzure");

                    b.Key("ID");
                });

            builder.Entity("planB.Models.MuzickaKolekcija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatumKreiranja");

                    b.Property<int>("KorisnikID");

                    b.Property<string>("Naziv");

                    b.Property<string>("idAzure");

                    b.Key("ID");
                });

            builder.Entity("planB.Models.Obaveza", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Datum");

                    b.Property<int?>("KorisnikID");

                    b.Property<int>("Prioritet");

                    b.Property<string>("Sadrzaj");

                    b.Property<int>("Vidljivost");

                    b.Property<string>("kreatorAzure");

                    b.Key("ID");
                });

            builder.Entity("planB.Models.Pjesma", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Izvodjac");

                    b.Property<int?>("MuzickaKolekcijaID");

                    b.Property<string>("Naziv");

                    b.Property<string>("Preview");

                    b.Property<string>("UrlSlike");

                    b.Property<string>("kolekcijaAzure");

                    b.Key("ID");
                });

            builder.Entity("planB.Models.Poruka", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DatumSlanja");

                    b.Property<int>("StatusPoruke");

                    b.Property<string>("Tekst");

                    b.Property<string>("idAzure");

                    b.Property<string>("posiljaocAzure");

                    b.Property<string>("primaocAzure");

                    b.Key("ID");
                });

            builder.Entity("planB.Models.StavkaDnevnika", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Datum");

                    b.Property<int?>("KorisnikID");

                    b.Property<string>("Naslov");

                    b.Property<string>("Sadrzaj");

                    b.Property<int>("Vidljivost");

                    b.Property<string>("kreatorAzure");

                    b.Key("ID");
                });

            builder.Entity("planB.Models.Korisnik", b =>
                {
                    b.Reference("planB.Models.Korisnik")
                        .InverseCollection()
                        .ForeignKey("KorisnikID");
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
