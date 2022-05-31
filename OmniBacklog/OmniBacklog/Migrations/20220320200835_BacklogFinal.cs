using Microsoft.EntityFrameworkCore.Migrations;

namespace OmniBacklog.Migrations
{
    public partial class BacklogFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    AutorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.AutorId);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    GeneroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.GeneroId);
                });

            migrationBuilder.CreateTable(
                name: "Sagas",
                columns: table => new
                {
                    SagaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Numerado = table.Column<int>(nullable: true),
                    Saga1Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sagas", x => x.SagaId);
                    table.ForeignKey(
                        name: "FK_Sagas_Sagas_Saga1Id",
                        column: x => x.Saga1Id,
                        principalTable: "Sagas",
                        principalColumn: "SagaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 30, nullable: false),
                    Contraseña = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    LibroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(maxLength: 50, nullable: false),
                    Numerado = table.Column<int>(nullable: true),
                    SagaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.LibroId);
                    table.ForeignKey(
                        name: "FK_Libros_Sagas_SagaId",
                        column: x => x.SagaId,
                        principalTable: "Sagas",
                        principalColumn: "SagaId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AutorLibros",
                columns: table => new
                {
                    AutorId = table.Column<int>(nullable: false),
                    LibroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorLibros", x => new { x.AutorId, x.LibroId });
                    table.ForeignKey(
                        name: "FK_AutorLibros_Autores_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Autores",
                        principalColumn: "AutorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutorLibros_Libros_LibroId",
                        column: x => x.LibroId,
                        principalTable: "Libros",
                        principalColumn: "LibroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BibliotecasPersonales",
                columns: table => new
                {
                    LibroId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false),
                    Leyendo = table.Column<bool>(nullable: false),
                    Favorito = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibliotecasPersonales", x => new { x.UsuarioId, x.LibroId });
                    table.ForeignKey(
                        name: "FK_BibliotecasPersonales_Libros_LibroId",
                        column: x => x.LibroId,
                        principalTable: "Libros",
                        principalColumn: "LibroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BibliotecasPersonales_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenerosLibros",
                columns: table => new
                {
                    GeneroId = table.Column<int>(nullable: false),
                    LibroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenerosLibros", x => new { x.LibroId, x.GeneroId });
                    table.ForeignKey(
                        name: "FK_GenerosLibros_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "GeneroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenerosLibros_Libros_LibroId",
                        column: x => x.LibroId,
                        principalTable: "Libros",
                        principalColumn: "LibroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Autores",
                columns: new[] { "AutorId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Sin asignar" },
                    { 2, "J.K. Rowling" },
                    { 3, "George R.R. Martin" },
                    { 4, "Brandon Sanderson" }
                });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "GeneroId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Sin asignar" },
                    { 2, "Fantasía medieval" },
                    { 3, "Ciencia ficción" },
                    { 4, "Ópera espacial" },
                    { 5, "Ficción histórica" }
                });

            migrationBuilder.InsertData(
                table: "Sagas",
                columns: new[] { "SagaId", "Nombre", "Numerado", "Saga1Id" },
                values: new object[,]
                {
                    { 1, "Huérfanos", 0, null },
                    { 2, "Únicos", 0, null },
                    { 3, "Harry Potter", 0, null }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Contraseña", "Nombre" },
                values: new object[] { 1, "abc123.", "Usuario" });

            migrationBuilder.InsertData(
                table: "Libros",
                columns: new[] { "LibroId", "Numerado", "SagaId", "Titulo" },
                values: new object[,]
                {
                    { 1, 1, 3, "Harry Potter y la Piedra Filosofal" },
                    { 2, 2, 3, "Harry Potter y la Cámara de los secretos" },
                    { 3, 3, 3, "Harry Potter y el Prisionero de Azkaban" },
                    { 4, 4, 3, "Harry Potter y el Cáliz de Fuego" },
                    { 5, 5, 3, "Harry Potter y la Orden del Fénix" },
                    { 6, 6, 3, "Harry Potter y el Príncipe Mestizo" },
                    { 7, 7, 3, "Harry Potter y las Reliquias de la Muerte" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutorLibros_LibroId",
                table: "AutorLibros",
                column: "LibroId");

            migrationBuilder.CreateIndex(
                name: "IX_BibliotecasPersonales_LibroId",
                table: "BibliotecasPersonales",
                column: "LibroId");

            migrationBuilder.CreateIndex(
                name: "IX_GenerosLibros_GeneroId",
                table: "GenerosLibros",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_SagaId",
                table: "Libros",
                column: "SagaId");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_Titulo",
                table: "Libros",
                column: "Titulo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sagas_Nombre",
                table: "Sagas",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sagas_Saga1Id",
                table: "Sagas",
                column: "Saga1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Nombre",
                table: "Usuarios",
                column: "Nombre",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutorLibros");

            migrationBuilder.DropTable(
                name: "BibliotecasPersonales");

            migrationBuilder.DropTable(
                name: "GenerosLibros");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Libros");

            migrationBuilder.DropTable(
                name: "Sagas");
        }
    }
}
