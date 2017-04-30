namespace Biblioteka.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    FirstName = c.String(name: "First Name", maxLength: 50),
                    LastName = c.String(name: "Last Name", maxLength: 50),
                    YearofBirth = c.DateTime(name: "Year of Birth", storeType: "date"),
                    Nationality = c.String(maxLength: 50),
                    Signature = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Authors",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    ID_Book = c.Int(nullable: false),
                    ID_Author = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Book", t => t.ID_Book)
                .ForeignKey("dbo.Author", t => t.ID_Author)
                .Index(t => t.ID_Book)
                .Index(t => t.ID_Author);

            CreateTable(
                "dbo.Book",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false, maxLength: 200),
                    ISBN = c.String(nullable: false, maxLength: 50),
                    Release_Date = c.DateTime(nullable: false, storeType: "date"),
                    ID_Publisher = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Publisher", t => t.ID_Publisher)
                .Index(t => t.ID_Publisher);

            CreateTable(
                "dbo.Book_Category",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    ID_Book = c.Int(nullable: false),
                    ID_Category = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.ID_Category)
                .ForeignKey("dbo.Book", t => t.ID_Book)
                .Index(t => t.ID_Book)
                .Index(t => t.ID_Category);

            CreateTable(
                "dbo.Category",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Copy",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    ID_Book = c.Int(nullable: false),
                    Book_State = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Book_State", t => t.Book_State)
                .ForeignKey("dbo.Book", t => t.ID_Book)
                .Index(t => t.ID_Book)
                .Index(t => t.Book_State);

            CreateTable(
                "dbo.Book_State",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    State = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Return",
                c => new
                {
                    ID_Rent = c.Int(nullable: false),
                    Return_Date = c.DateTime(nullable: false),
                    State_PreReturn = c.Int(name: "State_Pre-Return"),
                    State_AfterReturn = c.Int(name: "State_After-Return"),
                })
                .PrimaryKey(t => t.ID_Rent)
                .ForeignKey("dbo.Rent", t => t.ID_Rent)
                .ForeignKey("dbo.Book_State", t => t.State_AfterReturn)
                .ForeignKey("dbo.Book_State", t => t.State_PreReturn)
                .Index(t => t.ID_Rent)
                .Index(t => t.State_PreReturn)
                .Index(t => t.State_AfterReturn);

            CreateTable(
                "dbo.Rent",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    ID_Copy = c.Int(nullable: false),
                    Rent_Date = c.DateTime(nullable: false),
                    ID_Card = c.Int(nullable: false),
                    Expected_Return_Date = c.DateTime(nullable: false, storeType: "date"),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Card", t => t.ID_Card)
                .ForeignKey("dbo.Copy", t => t.ID_Copy)
                .Index(t => t.ID_Copy)
                .Index(t => t.ID_Card);

            CreateTable(
                "dbo.Card",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    FirstName = c.String(name: "First Name", nullable: false, maxLength: 50),
                    LastName = c.String(name: "Last Name", nullable: false, maxLength: 50),
                    City = c.String(nullable: false, maxLength: 50),
                    PostalCode = c.String(name: "Postal Code", nullable: false, maxLength: 6, fixedLength: true),
                    Street = c.String(nullable: false, maxLength: 50),
                    HouseNumber = c.String(name: "House Number", nullable: false, maxLength: 5),
                    ApartmentNuber = c.String(name: "Apartment Nuber", maxLength: 5),
                    PhoneNumber = c.String(name: "Phone Number", maxLength: 9),
                    CreateDate = c.DateTime(name: "Create Date", nullable: false, storeType: "date"),
                    State = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Card_State", t => t.State)
                .Index(t => t.State);

            CreateTable(
                "dbo.Card_State",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Publisher",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 200),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Employee",
                c => new
                {
                    Login = c.String(nullable: false, maxLength: 50),
                    Password = c.String(nullable: false, maxLength: 100),
                    FirstName = c.String(name: "First Name", nullable: false, maxLength: 50),
                    LastName = c.String(name: "Last Name", nullable: false, maxLength: 50),
                    isAdmin = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Login);

            CreateTable(
                "dbo.sysdiagrams",
                c => new
                {
                    diagram_id = c.Int(nullable: false, identity: true),
                    name = c.String(nullable: false, maxLength: 128),
                    principal_id = c.Int(nullable: false),
                    version = c.Int(),
                    definition = c.Binary(),
                })
                .PrimaryKey(t => t.diagram_id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Authors", "ID_Author", "dbo.Author");
            DropForeignKey("dbo.Book", "ID_Publisher", "dbo.Publisher");
            DropForeignKey("dbo.Copy", "ID_Book", "dbo.Book");
            DropForeignKey("dbo.Rent", "ID_Copy", "dbo.Copy");
            DropForeignKey("dbo.Return", "State_Pre-Return", "dbo.Book_State");
            DropForeignKey("dbo.Return", "State_After-Return", "dbo.Book_State");
            DropForeignKey("dbo.Return", "ID_Rent", "dbo.Rent");
            DropForeignKey("dbo.Rent", "ID_Card", "dbo.Card");
            DropForeignKey("dbo.Card", "State", "dbo.Card_State");
            DropForeignKey("dbo.Copy", "Book_State", "dbo.Book_State");
            DropForeignKey("dbo.Book_Category", "ID_Book", "dbo.Book");
            DropForeignKey("dbo.Book_Category", "ID_Category", "dbo.Category");
            DropForeignKey("dbo.Authors", "ID_Book", "dbo.Book");
            DropIndex("dbo.Card", new[] { "State" });
            DropIndex("dbo.Rent", new[] { "ID_Card" });
            DropIndex("dbo.Rent", new[] { "ID_Copy" });
            DropIndex("dbo.Return", new[] { "State_After-Return" });
            DropIndex("dbo.Return", new[] { "State_Pre-Return" });
            DropIndex("dbo.Return", new[] { "ID_Rent" });
            DropIndex("dbo.Copy", new[] { "Book_State" });
            DropIndex("dbo.Copy", new[] { "ID_Book" });
            DropIndex("dbo.Book_Category", new[] { "ID_Category" });
            DropIndex("dbo.Book_Category", new[] { "ID_Book" });
            DropIndex("dbo.Book", new[] { "ID_Publisher" });
            DropIndex("dbo.Authors", new[] { "ID_Author" });
            DropIndex("dbo.Authors", new[] { "ID_Book" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Employee");
            DropTable("dbo.Publisher");
            DropTable("dbo.Card_State");
            DropTable("dbo.Card");
            DropTable("dbo.Rent");
            DropTable("dbo.Return");
            DropTable("dbo.Book_State");
            DropTable("dbo.Copy");
            DropTable("dbo.Category");
            DropTable("dbo.Book_Category");
            DropTable("dbo.Book");
            DropTable("dbo.Authors");
            DropTable("dbo.Author");
        }
    }
}
