namespace OnlineExaminationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("Users", "StudentNumber", c => c.Long(nullable: false));
            AddColumn("Users", "Department", c => c.String(nullable: false, maxLength: 50, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("Users", "Department");
            DropColumn("Users", "StudentNumber");
        }
    }
}
