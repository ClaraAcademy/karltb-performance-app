using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerformanceApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "padb");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DateInfo",
                schema: "padb",
                columns: table => new
                {
                    Bankday = table.Column<DateOnly>(type: "date", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateInfo", x => x.Bankday);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentType",
                schema: "padb",
                columns: table => new
                {
                    InstrumentTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentTypeName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentType", x => x.InstrumentTypeID);
                });

            migrationBuilder.CreateTable(
                name: "KeyFigureInfo",
                schema: "padb",
                columns: table => new
                {
                    KeyFigureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyFigureName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyFigureInfo", x => x.KeyFigureID);
                });

            migrationBuilder.CreateTable(
                name: "Staging",
                schema: "padb",
                columns: table => new
                {
                    Bankday = table.Column<DateOnly>(type: "date", nullable: true),
                    InstrumentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InstrumentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(19,4)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "TransactionType",
                schema: "padb",
                columns: table => new
                {
                    TransactionTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionTypeName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionType", x => x.TransactionTypeID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Portfolio",
                schema: "padb",
                columns: table => new
                {
                    PortfolioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortfolioName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolio", x => x.PortfolioID);
                    table.ForeignKey(
                        name: "FK_Portfolio_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Instrument",
                schema: "padb",
                columns: table => new
                {
                    InstrumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentTypeID = table.Column<int>(type: "int", nullable: true),
                    InstrumentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrument", x => x.InstrumentID);
                    table.ForeignKey(
                        name: "FK_Instrument_InstrumentTypeID",
                        column: x => x.InstrumentTypeID,
                        principalSchema: "padb",
                        principalTable: "InstrumentType",
                        principalColumn: "InstrumentTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Benchmark",
                schema: "padb",
                columns: table => new
                {
                    PortfolioID = table.Column<int>(type: "int", nullable: false),
                    BenchmarkID = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benchmark", x => new { x.PortfolioID, x.BenchmarkID });
                    table.ForeignKey(
                        name: "FK_Benchmark_BenchmarkID",
                        column: x => x.BenchmarkID,
                        principalSchema: "padb",
                        principalTable: "Portfolio",
                        principalColumn: "PortfolioID");
                    table.ForeignKey(
                        name: "FK_Benchmark_PortfolioID",
                        column: x => x.PortfolioID,
                        principalSchema: "padb",
                        principalTable: "Portfolio",
                        principalColumn: "PortfolioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeyFigureValue",
                schema: "padb",
                columns: table => new
                {
                    PortfolioID = table.Column<int>(type: "int", nullable: false),
                    KeyFigureID = table.Column<int>(type: "int", nullable: false),
                    KeyFigureValue = table.Column<decimal>(type: "decimal(24,16)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyFigureValue", x => new { x.PortfolioID, x.KeyFigureID });
                    table.ForeignKey(
                        name: "FK_KeyFigureValue_KeyFigureID",
                        column: x => x.KeyFigureID,
                        principalSchema: "padb",
                        principalTable: "KeyFigureInfo",
                        principalColumn: "KeyFigureID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeyFigureValue_PortfolioID",
                        column: x => x.PortfolioID,
                        principalSchema: "padb",
                        principalTable: "Portfolio",
                        principalColumn: "PortfolioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioCumulativeDayPerformance",
                schema: "padb",
                columns: table => new
                {
                    PortfolioID = table.Column<int>(type: "int", nullable: false),
                    Bankday = table.Column<DateOnly>(type: "date", nullable: false),
                    CumulativeDayPerformance = table.Column<decimal>(type: "decimal(24,16)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioCumulativeDayPerformance", x => new { x.PortfolioID, x.Bankday });
                    table.ForeignKey(
                        name: "FK_PortfolioCumulativeDayPerformance_Bankday",
                        column: x => x.Bankday,
                        principalSchema: "padb",
                        principalTable: "DateInfo",
                        principalColumn: "Bankday");
                    table.ForeignKey(
                        name: "FK_PortfolioCumulativeDayPerformance_PortfolioID",
                        column: x => x.PortfolioID,
                        principalSchema: "padb",
                        principalTable: "Portfolio",
                        principalColumn: "PortfolioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioDayPerformance",
                schema: "padb",
                columns: table => new
                {
                    PortfolioID = table.Column<int>(type: "int", nullable: false),
                    Bankday = table.Column<DateOnly>(type: "date", nullable: false),
                    DayPerformance = table.Column<decimal>(type: "decimal(24,16)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioDayPerformance", x => new { x.PortfolioID, x.Bankday });
                    table.ForeignKey(
                        name: "FK_PortfolioDayPerformance_Bankday",
                        column: x => x.Bankday,
                        principalSchema: "padb",
                        principalTable: "DateInfo",
                        principalColumn: "Bankday");
                    table.ForeignKey(
                        name: "FK_PortfolioDayPerformance_PortfolioID",
                        column: x => x.PortfolioID,
                        principalSchema: "padb",
                        principalTable: "Portfolio",
                        principalColumn: "PortfolioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioHalfYearPerformance",
                schema: "padb",
                columns: table => new
                {
                    PortfolioID = table.Column<int>(type: "int", nullable: false),
                    PeriodStart = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    HalfYearPerformance = table.Column<decimal>(type: "decimal(24,16)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioHalfYearPerformance", x => new { x.PortfolioID, x.PeriodStart, x.PeriodEnd });
                    table.ForeignKey(
                        name: "FK_PortfolioHalfYearPerformance_PeriodEnd",
                        column: x => x.PeriodEnd,
                        principalSchema: "padb",
                        principalTable: "DateInfo",
                        principalColumn: "Bankday");
                    table.ForeignKey(
                        name: "FK_PortfolioHalfYearPerformance_PeriodStart",
                        column: x => x.PeriodStart,
                        principalSchema: "padb",
                        principalTable: "DateInfo",
                        principalColumn: "Bankday");
                    table.ForeignKey(
                        name: "FK_PortfolioHalfYearPerformance_PortfolioID",
                        column: x => x.PortfolioID,
                        principalSchema: "padb",
                        principalTable: "Portfolio",
                        principalColumn: "PortfolioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioMonthPerformance",
                schema: "padb",
                columns: table => new
                {
                    PortfolioID = table.Column<int>(type: "int", nullable: false),
                    PeriodStart = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    MonthPerformance = table.Column<decimal>(type: "decimal(24,16)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioMonthPerformance", x => new { x.PortfolioID, x.PeriodStart, x.PeriodEnd });
                    table.ForeignKey(
                        name: "FK_PortfolioMonthPerformance_PeriodEnd",
                        column: x => x.PeriodEnd,
                        principalSchema: "padb",
                        principalTable: "DateInfo",
                        principalColumn: "Bankday");
                    table.ForeignKey(
                        name: "FK_PortfolioMonthPerformance_PeriodStart",
                        column: x => x.PeriodStart,
                        principalSchema: "padb",
                        principalTable: "DateInfo",
                        principalColumn: "Bankday");
                    table.ForeignKey(
                        name: "FK_PortfolioMonthPerformance_PortfolioID",
                        column: x => x.PortfolioID,
                        principalSchema: "padb",
                        principalTable: "Portfolio",
                        principalColumn: "PortfolioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioValue",
                schema: "padb",
                columns: table => new
                {
                    PortfolioID = table.Column<int>(type: "int", nullable: false),
                    Bankday = table.Column<DateOnly>(type: "date", nullable: false),
                    PortfolioValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioValue", x => new { x.PortfolioID, x.Bankday });
                    table.ForeignKey(
                        name: "FK_PortfolioValue_Bankday",
                        column: x => x.Bankday,
                        principalSchema: "padb",
                        principalTable: "DateInfo",
                        principalColumn: "Bankday");
                    table.ForeignKey(
                        name: "FK_PortfolioValue_PortfolioID",
                        column: x => x.PortfolioID,
                        principalSchema: "padb",
                        principalTable: "Portfolio",
                        principalColumn: "PortfolioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentDayPerformance",
                schema: "padb",
                columns: table => new
                {
                    InstrumentID = table.Column<int>(type: "int", nullable: false),
                    Bankday = table.Column<DateOnly>(type: "date", nullable: false),
                    DayPerformance = table.Column<decimal>(type: "decimal(24,16)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentDayPerformance", x => new { x.InstrumentID, x.Bankday });
                    table.ForeignKey(
                        name: "FK_InstrumentDayPerformance_Bankday",
                        column: x => x.Bankday,
                        principalSchema: "padb",
                        principalTable: "DateInfo",
                        principalColumn: "Bankday");
                    table.ForeignKey(
                        name: "FK_InstrumentDayPerformance_InstrumentID",
                        column: x => x.InstrumentID,
                        principalSchema: "padb",
                        principalTable: "Instrument",
                        principalColumn: "InstrumentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentHalfYearPerformance",
                schema: "padb",
                columns: table => new
                {
                    InstrumentID = table.Column<int>(type: "int", nullable: false),
                    PeriodStart = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    HalfYearPerformance = table.Column<decimal>(type: "decimal(24,16)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentHalfYearPerformance", x => new { x.InstrumentID, x.PeriodStart, x.PeriodEnd });
                    table.ForeignKey(
                        name: "FK_InstrumentHalfYearPerformance_InstrumentID",
                        column: x => x.InstrumentID,
                        principalSchema: "padb",
                        principalTable: "Instrument",
                        principalColumn: "InstrumentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstrumentHalfYearPerformance_PeriodEnd",
                        column: x => x.PeriodEnd,
                        principalSchema: "padb",
                        principalTable: "DateInfo",
                        principalColumn: "Bankday");
                    table.ForeignKey(
                        name: "FK_InstrumentHalfYearPerformance_PeriodStart",
                        column: x => x.PeriodStart,
                        principalSchema: "padb",
                        principalTable: "DateInfo",
                        principalColumn: "Bankday");
                });

            migrationBuilder.CreateTable(
                name: "InstrumentMonthPerformance",
                schema: "padb",
                columns: table => new
                {
                    InstrumentID = table.Column<int>(type: "int", nullable: false),
                    PeriodStart = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    MonthPerformance = table.Column<decimal>(type: "decimal(24,16)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentMonthPerformance", x => new { x.InstrumentID, x.PeriodStart, x.PeriodEnd });
                    table.ForeignKey(
                        name: "FK_InstrumentMonthPerformance_InstrumentID",
                        column: x => x.InstrumentID,
                        principalSchema: "padb",
                        principalTable: "Instrument",
                        principalColumn: "InstrumentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstrumentMonthPerformance_PeriodEnd",
                        column: x => x.PeriodEnd,
                        principalSchema: "padb",
                        principalTable: "DateInfo",
                        principalColumn: "Bankday");
                    table.ForeignKey(
                        name: "FK_InstrumentMonthPerformance_PeriodStart",
                        column: x => x.PeriodStart,
                        principalSchema: "padb",
                        principalTable: "DateInfo",
                        principalColumn: "Bankday");
                });

            migrationBuilder.CreateTable(
                name: "InstrumentPrice",
                schema: "padb",
                columns: table => new
                {
                    InstrumentID = table.Column<int>(type: "int", nullable: false),
                    Bankday = table.Column<DateOnly>(type: "date", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentPrice", x => new { x.InstrumentID, x.Bankday });
                    table.ForeignKey(
                        name: "FK_InstrumentPrice_Bankday",
                        column: x => x.Bankday,
                        principalSchema: "padb",
                        principalTable: "DateInfo",
                        principalColumn: "Bankday");
                    table.ForeignKey(
                        name: "FK_InstrumentPrice_InstrumentID",
                        column: x => x.InstrumentID,
                        principalSchema: "padb",
                        principalTable: "Instrument",
                        principalColumn: "InstrumentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                schema: "padb",
                columns: table => new
                {
                    PositionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortfolioID = table.Column<int>(type: "int", nullable: true),
                    InstrumentID = table.Column<int>(type: "int", nullable: true),
                    Bankday = table.Column<DateOnly>(type: "date", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", nullable: true),
                    Proportion = table.Column<decimal>(type: "decimal(5,4)", nullable: true),
                    Nominal = table.Column<decimal>(type: "decimal(19,4)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.PositionID);
                    table.ForeignKey(
                        name: "FK_Position_Bankday",
                        column: x => x.Bankday,
                        principalSchema: "padb",
                        principalTable: "DateInfo",
                        principalColumn: "Bankday");
                    table.ForeignKey(
                        name: "FK_Position_InstrumentID",
                        column: x => x.InstrumentID,
                        principalSchema: "padb",
                        principalTable: "Instrument",
                        principalColumn: "InstrumentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Position_PortfolioID",
                        column: x => x.PortfolioID,
                        principalSchema: "padb",
                        principalTable: "Portfolio",
                        principalColumn: "PortfolioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                schema: "padb",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortfolioID = table.Column<int>(type: "int", nullable: true),
                    InstrumentID = table.Column<int>(type: "int", nullable: true),
                    Bankday = table.Column<DateOnly>(type: "date", nullable: true),
                    TransactionTypeID = table.Column<int>(type: "int", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(19,4)", nullable: true),
                    Proportion = table.Column<decimal>(type: "decimal(5,4)", nullable: true),
                    Nominal = table.Column<decimal>(type: "decimal(19,4)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK_Transaction_Bankday",
                        column: x => x.Bankday,
                        principalSchema: "padb",
                        principalTable: "DateInfo",
                        principalColumn: "Bankday");
                    table.ForeignKey(
                        name: "FK_Transaction_InstrumentID",
                        column: x => x.InstrumentID,
                        principalSchema: "padb",
                        principalTable: "Instrument",
                        principalColumn: "InstrumentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_PortfolioID",
                        column: x => x.PortfolioID,
                        principalSchema: "padb",
                        principalTable: "Portfolio",
                        principalColumn: "PortfolioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_TransactionTypeID",
                        column: x => x.TransactionTypeID,
                        principalSchema: "padb",
                        principalTable: "TransactionType",
                        principalColumn: "TransactionTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PositionValue",
                schema: "padb",
                columns: table => new
                {
                    PositionID = table.Column<int>(type: "int", nullable: false),
                    Bankday = table.Column<DateOnly>(type: "date", nullable: false),
                    PositionValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionValue", x => new { x.PositionID, x.Bankday });
                    table.ForeignKey(
                        name: "FK_PositionValue_Bankday",
                        column: x => x.Bankday,
                        principalSchema: "padb",
                        principalTable: "DateInfo",
                        principalColumn: "Bankday");
                    table.ForeignKey(
                        name: "FK_PositionValue_PositionID",
                        column: x => x.PositionID,
                        principalSchema: "padb",
                        principalTable: "Position",
                        principalColumn: "PositionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Benchmark_BenchmarkID",
                schema: "padb",
                table: "Benchmark",
                column: "BenchmarkID");

            migrationBuilder.CreateIndex(
                name: "IX_Instrument_InstrumentTypeID",
                schema: "padb",
                table: "Instrument",
                column: "InstrumentTypeID");

            migrationBuilder.CreateIndex(
                name: "UQ_Instrument_InstrumentName",
                schema: "padb",
                table: "Instrument",
                column: "InstrumentName",
                unique: true,
                filter: "[InstrumentName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentDayPerformance_Bankday",
                schema: "padb",
                table: "InstrumentDayPerformance",
                column: "Bankday");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentHalfYearPerformance_PeriodEnd",
                schema: "padb",
                table: "InstrumentHalfYearPerformance",
                column: "PeriodEnd");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentHalfYearPerformance_PeriodStart",
                schema: "padb",
                table: "InstrumentHalfYearPerformance",
                column: "PeriodStart");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentMonthPerformance_PeriodEnd",
                schema: "padb",
                table: "InstrumentMonthPerformance",
                column: "PeriodEnd");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentMonthPerformance_PeriodStart",
                schema: "padb",
                table: "InstrumentMonthPerformance",
                column: "PeriodStart");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentPrice_Bankday",
                schema: "padb",
                table: "InstrumentPrice",
                column: "Bankday");

            migrationBuilder.CreateIndex(
                name: "UQ_KeyFigureInfo_KeyFigureName",
                schema: "padb",
                table: "KeyFigureInfo",
                column: "KeyFigureName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KeyFigureValue_KeyFigureID",
                schema: "padb",
                table: "KeyFigureValue",
                column: "KeyFigureID");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_UserID",
                schema: "padb",
                table: "Portfolio",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ_Portfolio_PortfolioName",
                schema: "padb",
                table: "Portfolio",
                column: "PortfolioName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioCumulativeDayPerformance_Bankday",
                schema: "padb",
                table: "PortfolioCumulativeDayPerformance",
                column: "Bankday");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioDayPerformance_Bankday",
                schema: "padb",
                table: "PortfolioDayPerformance",
                column: "Bankday");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioHalfYearPerformance_PeriodEnd",
                schema: "padb",
                table: "PortfolioHalfYearPerformance",
                column: "PeriodEnd");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioHalfYearPerformance_PeriodStart",
                schema: "padb",
                table: "PortfolioHalfYearPerformance",
                column: "PeriodStart");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioMonthPerformance_PeriodEnd",
                schema: "padb",
                table: "PortfolioMonthPerformance",
                column: "PeriodEnd");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioMonthPerformance_PeriodStart",
                schema: "padb",
                table: "PortfolioMonthPerformance",
                column: "PeriodStart");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioValue_Bankday",
                schema: "padb",
                table: "PortfolioValue",
                column: "Bankday");

            migrationBuilder.CreateIndex(
                name: "IX_Position_Bankday",
                schema: "padb",
                table: "Position",
                column: "Bankday");

            migrationBuilder.CreateIndex(
                name: "IX_Position_InstrumentID",
                schema: "padb",
                table: "Position",
                column: "InstrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Position_PortfolioID",
                schema: "padb",
                table: "Position",
                column: "PortfolioID");

            migrationBuilder.CreateIndex(
                name: "IX_PositionValue_Bankday",
                schema: "padb",
                table: "PositionValue",
                column: "Bankday");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Bankday",
                schema: "padb",
                table: "Transaction",
                column: "Bankday");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_InstrumentID",
                schema: "padb",
                table: "Transaction",
                column: "InstrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PortfolioID",
                schema: "padb",
                table: "Transaction",
                column: "PortfolioID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TransactionTypeID",
                schema: "padb",
                table: "Transaction",
                column: "TransactionTypeID");

            migrationBuilder.CreateIndex(
                name: "UQ_TransactionType_TransactionTypeName",
                schema: "padb",
                table: "TransactionType",
                column: "TransactionTypeName",
                unique: true,
                filter: "[TransactionTypeName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Benchmark",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "InstrumentDayPerformance",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "InstrumentHalfYearPerformance",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "InstrumentMonthPerformance",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "InstrumentPrice",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "KeyFigureValue",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "PortfolioCumulativeDayPerformance",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "PortfolioDayPerformance",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "PortfolioHalfYearPerformance",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "PortfolioMonthPerformance",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "PortfolioValue",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "PositionValue",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "Staging",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "Transaction",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "KeyFigureInfo",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "Position",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "TransactionType",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "DateInfo",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "Instrument",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "Portfolio",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "InstrumentType",
                schema: "padb");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
