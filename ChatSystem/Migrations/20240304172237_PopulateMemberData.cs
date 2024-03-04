using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatSystem.Migrations
{
    public partial class PopulateMemberData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Members(Name, Email) values('Zain','zain@domain.com')");
            migrationBuilder.Sql("Insert into Members(Name, Email) values('Shariq','shariq@domain.com')");
            migrationBuilder.Sql("Insert into Members(Name, Email) values('Talha','talha@domain.com')");
            migrationBuilder.Sql("Insert into Members(Name, Email) values('Shoaib','shoaib@domain.com')");
            migrationBuilder.Sql("Insert into Members(Name, Email) values('Asghar','asghar@domain.com')");
            migrationBuilder.Sql("Insert into Members(Name, Email) values('Imran','imran@domain.com')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Members");
        }
    }
}
