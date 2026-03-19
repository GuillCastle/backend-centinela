using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class unidadmedida2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoEmpresa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Nit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Representante = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Regimen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AfiliacionIva = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Permiso",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permiso", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaCodigo = table.Column<int>(type: "int", nullable: false),
                    CodigoSucursal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Abreviatura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Encargado = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoSucursal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Departamento = table.Column<int>(type: "int", nullable: true),
                    Municipio = table.Column<int>(type: "int", nullable: true),
                    Direccion = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    UsuarioCertificado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorreoCopia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LLaveSucursal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LlaveFirma = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursales", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Sucursales_Empresa_EmpresaCodigo",
                        column: x => x.EmpresaCodigo,
                        principalTable: "Empresa",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolPermiso",
                columns: table => new
                {
                    Rol = table.Column<int>(type: "int", nullable: false),
                    Permiso = table.Column<int>(type: "int", nullable: false),
                    Busqueda = table.Column<int>(type: "int", nullable: false),
                    Insertar = table.Column<int>(type: "int", nullable: false),
                    Reimpresion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermiso", x => new { x.Rol, x.Permiso });
                    table.ForeignKey(
                        name: "FK_RolPermiso_Permiso",
                        column: x => x.Permiso,
                        principalTable: "Permiso",
                        principalColumn: "Codigo");
                    table.ForeignKey(
                        name: "FK_RolPermiso_Rol",
                        column: x => x.Rol,
                        principalTable: "Rol",
                        principalColumn: "Codigo");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Rol = table.Column<int>(type: "int", nullable: false),
                    UsuarioRegistro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Usuario_Rol",
                        column: x => x.Rol,
                        principalTable: "Rol",
                        principalColumn: "Codigo");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioEmpSuc",
                columns: table => new
                {
                    Usuario = table.Column<int>(type: "int", nullable: false),
                    Sucursal = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioRegistro = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioEmpSuc_1", x => new { x.Usuario, x.Sucursal });
                    table.ForeignKey(
                        name: "FK_UsuarioEmpSuc_Sucursales",
                        column: x => x.Sucursal,
                        principalTable: "Sucursales",
                        principalColumn: "Codigo");
                    table.ForeignKey(
                        name: "FK_UsuarioEmpSuc_Usuario",
                        column: x => x.Usuario,
                        principalTable: "Usuario",
                        principalColumn: "Codigo");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioPermiso",
                columns: table => new
                {
                    Usuario = table.Column<int>(type: "int", nullable: false),
                    Permiso = table.Column<int>(type: "int", nullable: false),
                    Busqueda = table.Column<int>(type: "int", nullable: false),
                    Insertar = table.Column<int>(type: "int", nullable: false),
                    Reimpresion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPermiso", x => new { x.Usuario, x.Permiso });
                    table.ForeignKey(
                        name: "FK_UsuarioPermiso_Permiso",
                        column: x => x.Permiso,
                        principalTable: "Permiso",
                        principalColumn: "Codigo");
                    table.ForeignKey(
                        name: "FK_UsuarioPermiso_Usuario",
                        column: x => x.Usuario,
                        principalTable: "Usuario",
                        principalColumn: "Codigo");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolPermiso_Permiso",
                table: "RolPermiso",
                column: "Permiso");

            migrationBuilder.CreateIndex(
                name: "IX_Sucursales_EmpresaCodigo",
                table: "Sucursales",
                column: "EmpresaCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario",
                table: "Usuario",
                column: "Usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Rol",
                table: "Usuario",
                column: "Rol");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioEmpSuc_Sucursal",
                table: "UsuarioEmpSuc",
                column: "Sucursal");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPermiso_Permiso",
                table: "UsuarioPermiso",
                column: "Permiso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropTable(
                name: "RolPermiso");

            migrationBuilder.DropTable(
                name: "UsuarioEmpSuc");

            migrationBuilder.DropTable(
                name: "UsuarioPermiso");

            migrationBuilder.DropTable(
                name: "Sucursales");

            migrationBuilder.DropTable(
                name: "Permiso");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
