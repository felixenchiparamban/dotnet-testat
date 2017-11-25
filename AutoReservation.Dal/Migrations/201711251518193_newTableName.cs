namespace AutoReservation.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newTableName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Autoes", newName: "Auto");
            RenameTable(name: "dbo.Reservations", newName: "Reservation");
            RenameTable(name: "dbo.Kundes", newName: "Kunde");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Kunde", newName: "Kundes");
            RenameTable(name: "dbo.Reservation", newName: "Reservations");
            RenameTable(name: "dbo.Auto", newName: "Autoes");
        }
    }
}
