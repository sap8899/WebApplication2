using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class checkDBUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_UserID",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Reservation",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_UserID",
                table: "Reservation",
                newName: "IX_Reservation_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Reservation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Reservation",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                columns: new[] { "RestaurantID", "UserID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_UserId",
                table: "Reservation",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_User_UserId",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Reservation",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_UserId",
                table: "Reservation",
                newName: "IX_Reservation_UserID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                columns: new[] { "RestaurantID", "UserID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_User_UserID",
                table: "Reservation",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
