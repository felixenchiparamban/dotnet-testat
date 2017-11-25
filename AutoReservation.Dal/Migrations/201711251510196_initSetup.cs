namespace AutoReservation.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Marke = c.String(maxLength: 20),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Tagestarif = c.Int(nullable: false),
                        Basistarif = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AutoId = c.Int(nullable: false),
                        Bis = c.DateTime(nullable: false),
                        KundeId = c.Int(nullable: false),
                        ReservationsNr = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Von = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Autoes", t => t.AutoId, cascadeDelete: true)
                .ForeignKey("dbo.Kundes", t => t.KundeId, cascadeDelete: true)
                .Index(t => t.AutoId)
                .Index(t => t.KundeId);
            
            CreateTable(
                "dbo.Kundes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Geburtsdatum = c.DateTime(nullable: false),
                        Nachname = c.String(maxLength: 20),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Vorname = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "KundeId", "dbo.Kundes");
            DropForeignKey("dbo.Reservations", "AutoId", "dbo.Autoes");
            DropIndex("dbo.Reservations", new[] { "KundeId" });
            DropIndex("dbo.Reservations", new[] { "AutoId" });
            DropTable("dbo.Kundes");
            DropTable("dbo.Reservations");
            DropTable("dbo.Autoes");
        }
    }
}
