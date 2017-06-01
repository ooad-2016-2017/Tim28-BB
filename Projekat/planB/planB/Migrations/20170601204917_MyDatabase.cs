using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Migrations.Builders;
using Microsoft.Data.Entity.Migrations.Operations;

namespace planBMigrations
{
    public partial class MyDatabase : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateTable(
                name: "Follow",
                columns: table => new
                {
                    ID = table.Column(type: "INTEGER", nullable: false),
                        //.Annotation("Sqlite:Autoincrement", true),
                    Following_KorisnikID = table.Column(type: "TEXT", nullable: true),
                    KorisnikID = table.Column(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follow", x => x.ID);
                });
            migration.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    ID = table.Column(type: "INTEGER", nullable: false),
                        //.Annotation("Sqlite:Autoincrement", true),
                    DatumRodjenja = table.Column(type: "TEXT", nullable: false),
                    Email = table.Column(type: "TEXT", nullable: true),
                    Ime = table.Column(type: "TEXT", nullable: true),
                    KorisnickoIme = table.Column(type: "TEXT", nullable: true),
                    KorisnikID = table.Column(type: "INTEGER", nullable: true),
                    Lozinka = table.Column(type: "TEXT", nullable: true),
                    Prezime = table.Column(type: "TEXT", nullable: true),
                    Slika = table.Column(type: "BLOB", nullable: true),
                    idAzure = table.Column(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Korisnik_Korisnik_KorisnikID",
                        columns: x => x.KorisnikID,
                        referencedTable: "Korisnik",
                        referencedColumn: "ID");
                });
            migration.CreateTable(
                name: "Poruka",
                columns: table => new
                {
                    ID = table.Column(type: "INTEGER", nullable: false),
                        //.Annotation("Sqlite:Autoincrement", true),
                    DatumSlanja = table.Column(type: "TEXT", nullable: false),
                    StatusPoruke = table.Column(type: "INTEGER", nullable: false),
                    Tekst = table.Column(type: "TEXT", nullable: true),
                    posiljaocAzure = table.Column(type: "TEXT", nullable: true),
                    primaocAzure = table.Column(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poruka", x => x.ID);
                });
            migration.CreateTable(
                name: "MuzickaKolekcija",
                columns: table => new
                {
                    ID = table.Column(type: "INTEGER", nullable: false),
                        //.Annotation("Sqlite:Autoincrement", true),
                    DatumKreiranja = table.Column(type: "TEXT", nullable: false),
                    KorisnikID = table.Column(type: "INTEGER", nullable: false),
                    Naziv = table.Column(type: "TEXT", nullable: true),
                    idAzure = table.Column(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuzickaKolekcija", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MuzickaKolekcija_Korisnik_KorisnikID",
                        columns: x => x.KorisnikID,
                        referencedTable: "Korisnik",
                        referencedColumn: "ID");
                });
            migration.CreateTable(
                name: "Obaveza",
                columns: table => new
                {
                    ID = table.Column(type: "INTEGER", nullable: false),
                        //.Annotation("Sqlite:Autoincrement", true),
                    Datum = table.Column(type: "TEXT", nullable: false),
                    KorisnikID = table.Column(type: "INTEGER", nullable: true),
                    Prioritet = table.Column(type: "INTEGER", nullable: false),
                    Sadrzaj = table.Column(type: "TEXT", nullable: true),
                    Vidljivost = table.Column(type: "INTEGER", nullable: false),
                    kreatorAzure = table.Column(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obaveza", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Obaveza_Korisnik_KorisnikID",
                        columns: x => x.KorisnikID,
                        referencedTable: "Korisnik",
                        referencedColumn: "ID");
                });
            migration.CreateTable(
                name: "StavkaDnevnika",
                columns: table => new
                {
                    ID = table.Column(type: "INTEGER", nullable: false),
                        //.Annotation("Sqlite:Autoincrement", true),
                    Datum = table.Column(type: "TEXT", nullable: false),
                    KorisnikID = table.Column(type: "INTEGER", nullable: true),
                    Naslov = table.Column(type: "TEXT", nullable: true),
                    Sadrzaj = table.Column(type: "TEXT", nullable: true),
                    Vidljivost = table.Column(type: "INTEGER", nullable: false),
                    kreatorAzure = table.Column(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkaDnevnika", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StavkaDnevnika_Korisnik_KorisnikID",
                        columns: x => x.KorisnikID,
                        referencedTable: "Korisnik",
                        referencedColumn: "ID");
                });
            migration.CreateTable(
                name: "Pjesma",
                columns: table => new
                {
                    ID = table.Column(type: "INTEGER", nullable: false),
                        //.Annotation("Sqlite:Autoincrement", true),
                    Izvodjac = table.Column(type: "TEXT", nullable: true),
                    MuzickaKolekcijaID = table.Column(type: "INTEGER", nullable: true),
                    Naziv = table.Column(type: "TEXT", nullable: true),
                    Preview = table.Column(type: "TEXT", nullable: true),
                    UrlSlike = table.Column(type: "TEXT", nullable: true),
                    kolekcijaAzure = table.Column(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pjesma", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pjesma_MuzickaKolekcija_MuzickaKolekcijaID",
                        columns: x => x.MuzickaKolekcijaID,
                        referencedTable: "MuzickaKolekcija",
                        referencedColumn: "ID");
                });
        }

        public override void Down(MigrationBuilder migration)
        {
            migration.DropTable("Follow");
            migration.DropTable("Obaveza");
            migration.DropTable("Pjesma");
            migration.DropTable("Poruka");
            migration.DropTable("StavkaDnevnika");
            migration.DropTable("MuzickaKolekcija");
            migration.DropTable("Korisnik");
        }
    }
}
