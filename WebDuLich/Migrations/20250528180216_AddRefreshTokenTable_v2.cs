using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDuLich.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenTable_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_TAIKHOAN_UserId",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                newName: "REFRESHTOKEN");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "REFRESHTOKEN",
                newName: "USERID");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "REFRESHTOKEN",
                newName: "TOKEN");

            migrationBuilder.RenameColumn(
                name: "JwtId",
                table: "REFRESHTOKEN",
                newName: "JWTID");

            migrationBuilder.RenameColumn(
                name: "IssuedAt",
                table: "REFRESHTOKEN",
                newName: "ISSUEDAT");

            migrationBuilder.RenameColumn(
                name: "IsUsed",
                table: "REFRESHTOKEN",
                newName: "ISUSED");

            migrationBuilder.RenameColumn(
                name: "IsRevoked",
                table: "REFRESHTOKEN",
                newName: "ISREVOKED");

            migrationBuilder.RenameColumn(
                name: "ExpiredAt",
                table: "REFRESHTOKEN",
                newName: "EXPIREDAT");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "REFRESHTOKEN",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_UserId",
                table: "REFRESHTOKEN",
                newName: "IX_REFRESHTOKEN_USERID");

            migrationBuilder.AlterColumn<string>(
                name: "TOKEN",
                table: "REFRESHTOKEN",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "JWTID",
                table: "REFRESHTOKEN",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_REFRESHTOKEN",
                table: "REFRESHTOKEN",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_REFRESHTOKEN_TAIKHOAN_USERID",
                table: "REFRESHTOKEN",
                column: "USERID",
                principalTable: "TAIKHOAN",
                principalColumn: "MaTK",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_REFRESHTOKEN_TAIKHOAN_USERID",
                table: "REFRESHTOKEN");

            migrationBuilder.DropPrimaryKey(
                name: "PK_REFRESHTOKEN",
                table: "REFRESHTOKEN");

            migrationBuilder.RenameTable(
                name: "REFRESHTOKEN",
                newName: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "USERID",
                table: "RefreshToken",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "TOKEN",
                table: "RefreshToken",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "JWTID",
                table: "RefreshToken",
                newName: "JwtId");

            migrationBuilder.RenameColumn(
                name: "ISUSED",
                table: "RefreshToken",
                newName: "IsUsed");

            migrationBuilder.RenameColumn(
                name: "ISSUEDAT",
                table: "RefreshToken",
                newName: "IssuedAt");

            migrationBuilder.RenameColumn(
                name: "ISREVOKED",
                table: "RefreshToken",
                newName: "IsRevoked");

            migrationBuilder.RenameColumn(
                name: "EXPIREDAT",
                table: "RefreshToken",
                newName: "ExpiredAt");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "RefreshToken",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_REFRESHTOKEN_USERID",
                table: "RefreshToken",
                newName: "IX_RefreshToken_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<string>(
                name: "JwtId",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_TAIKHOAN_UserId",
                table: "RefreshToken",
                column: "UserId",
                principalTable: "TAIKHOAN",
                principalColumn: "MaTK",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
