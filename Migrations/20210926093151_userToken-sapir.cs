using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class userTokensapir : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Restaurant_RestaurantId",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_RestaurantId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "RestaurantID",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "RestaurantId",
                table: "Reservation",
                newName: "RestaurantID");

            migrationBuilder.AddColumn<string>(
                name: "UserToken",
                table: "Reservation",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                columns: new[] { "RestaurantID", "UserToken" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Restaurant_RestaurantID",
                table: "Reservation",
                column: "RestaurantID",
                principalTable: "Restaurant",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Restaurant_RestaurantID",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "UserToken",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "RestaurantID",
                table: "Reservation",
                newName: "RestaurantId");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantID",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "RestaurantID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_RestaurantId",
                table: "Reservation",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Restaurant_RestaurantId",
                table: "Reservation",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
