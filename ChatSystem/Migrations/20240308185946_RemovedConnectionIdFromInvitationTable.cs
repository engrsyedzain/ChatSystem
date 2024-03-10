using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatSystem.Migrations
{
    public partial class RemovedConnectionIdFromInvitationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverConnectionId",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "SenderConnectionId",
                table: "Invitations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiverConnectionId",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderConnectionId",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
