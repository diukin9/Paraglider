﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Paraglider.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WPItemDescs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConfigurationItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    TimeStampType = table.Column<int>(name: "TimeStamp_Type", type: "integer", nullable: false),
                    ExactTimeHour = table.Column<int>(name: "ExactTime_Hour", type: "integer", nullable: true),
                    ExactTimeMinute = table.Column<int>(name: "ExactTime_Minute", type: "integer", nullable: true),
                    ExactTimeSecond = table.Column<int>(name: "ExactTime_Second", type: "integer", nullable: true),
                    IntervalEndHour = table.Column<int>(name: "IntervalEnd_Hour", type: "integer", nullable: true),
                    IntervalEndMinute = table.Column<int>(name: "IntervalEnd_Minute", type: "integer", nullable: true),
                    IntervalEndSecond = table.Column<int>(name: "IntervalEnd_Second", type: "integer", nullable: true),
                    IntervalStartHour = table.Column<int>(name: "IntervalStart_Hour", type: "integer", nullable: true),
                    IntervalStartMinute = table.Column<int>(name: "IntervalStart_Minute", type: "integer", nullable: true),
                    IntervalStartSecond = table.Column<int>(name: "IntervalStart_Second", type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WPItemDescs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medias_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Sum = table.Column<decimal>(type: "numeric", nullable: true),
                    WPItemDescId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_WPItemDescs_WPItemDescId",
                        column: x => x.WPItemDescId,
                        principalTable: "WPItemDescs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
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
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
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
                name: "ExternalInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExternalId = table.Column<string>(type: "text", nullable: false),
                    ExternalProvider = table.Column<int>(type: "integer", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalInfo", x => x.Id);
                    table.UniqueConstraint("AK_ExternalInfo_ExternalProvider_ExternalId", x => new { x.ExternalProvider, x.ExternalId });
                    table.ForeignKey(
                        name: "FK_ExternalInfo_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WeddingPlannings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeddingPlannings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeddingPlannings_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BanquetHalls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ExternalInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeddingPlanningId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanquetHalls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BanquetHalls_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BanquetHalls_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BanquetHalls_ExternalInfo_ExternalInfoId",
                        column: x => x.ExternalInfoId,
                        principalTable: "ExternalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BanquetHalls_WeddingPlannings_WeddingPlanningId",
                        column: x => x.WeddingPlanningId,
                        principalTable: "WeddingPlannings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    WeddingPlanningId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_WeddingPlannings_WeddingPlanningId",
                        column: x => x.WeddingPlanningId,
                        principalTable: "WeddingPlannings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Caterings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ExternalInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeddingPlanningId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caterings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caterings_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Caterings_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Caterings_ExternalInfo_ExternalInfoId",
                        column: x => x.ExternalInfoId,
                        principalTable: "ExternalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Caterings_WeddingPlannings_WeddingPlanningId",
                        column: x => x.WeddingPlanningId,
                        principalTable: "WeddingPlannings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Confectioners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ExternalInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeddingPlanningId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Confectioners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Confectioners_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Confectioners_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Confectioners_ExternalInfo_ExternalInfoId",
                        column: x => x.ExternalInfoId,
                        principalTable: "ExternalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Confectioners_WeddingPlannings_WeddingPlanningId",
                        column: x => x.WeddingPlanningId,
                        principalTable: "WeddingPlannings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Decorators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ExternalInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeddingPlanningId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decorators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Decorators_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Decorators_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Decorators_ExternalInfo_ExternalInfoId",
                        column: x => x.ExternalInfoId,
                        principalTable: "ExternalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Decorators_WeddingPlannings_WeddingPlanningId",
                        column: x => x.WeddingPlanningId,
                        principalTable: "WeddingPlannings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Djs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ExternalInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeddingPlanningId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Djs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Djs_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Djs_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Djs_ExternalInfo_ExternalInfoId",
                        column: x => x.ExternalInfoId,
                        principalTable: "ExternalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Djs_WeddingPlannings_WeddingPlanningId",
                        column: x => x.WeddingPlanningId,
                        principalTable: "WeddingPlannings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Limousines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ManufactureYear = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MinRentLength = table.Column<TimeSpan>(type: "interval", nullable: true),
                    Capacity = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ExternalInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeddingPlanningId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Limousines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Limousines_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Limousines_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Limousines_ExternalInfo_ExternalInfoId",
                        column: x => x.ExternalInfoId,
                        principalTable: "ExternalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Limousines_WeddingPlannings_WeddingPlanningId",
                        column: x => x.WeddingPlanningId,
                        principalTable: "WeddingPlannings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Photographers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ExternalInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeddingPlanningId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photographers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photographers_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Photographers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Photographers_ExternalInfo_ExternalInfoId",
                        column: x => x.ExternalInfoId,
                        principalTable: "ExternalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Photographers_WeddingPlannings_WeddingPlanningId",
                        column: x => x.WeddingPlanningId,
                        principalTable: "WeddingPlannings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PhotoStudios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ExternalInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeddingPlanningId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoStudios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoStudios_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotoStudios_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotoStudios_ExternalInfo_ExternalInfoId",
                        column: x => x.ExternalInfoId,
                        principalTable: "ExternalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotoStudios_WeddingPlannings_WeddingPlanningId",
                        column: x => x.WeddingPlanningId,
                        principalTable: "WeddingPlannings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Registrars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ExternalInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeddingPlanningId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registrars_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrars_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrars_ExternalInfo_ExternalInfoId",
                        column: x => x.ExternalInfoId,
                        principalTable: "ExternalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registrars_WeddingPlannings_WeddingPlanningId",
                        column: x => x.WeddingPlanningId,
                        principalTable: "WeddingPlannings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stylists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ExternalInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeddingPlanningId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stylists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stylists_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stylists_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stylists_ExternalInfo_ExternalInfoId",
                        column: x => x.ExternalInfoId,
                        principalTable: "ExternalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stylists_WeddingPlannings_WeddingPlanningId",
                        column: x => x.WeddingPlanningId,
                        principalTable: "WeddingPlannings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Toastmasters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ExternalInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeddingPlanningId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toastmasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Toastmasters_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Toastmasters_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Toastmasters_ExternalInfo_ExternalInfoId",
                        column: x => x.ExternalInfoId,
                        principalTable: "ExternalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Toastmasters_WeddingPlannings_WeddingPlanningId",
                        column: x => x.WeddingPlanningId,
                        principalTable: "WeddingPlannings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Videographers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ExternalInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeddingPlanningId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videographers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videographers_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Videographers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Videographers_ExternalInfo_ExternalInfoId",
                        column: x => x.ExternalInfoId,
                        principalTable: "ExternalInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Videographers_WeddingPlannings_WeddingPlanningId",
                        column: x => x.WeddingPlanningId,
                        principalTable: "WeddingPlannings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Premises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    RentalPricePerPerson = table.Column<decimal>(type: "numeric", nullable: true),
                    RentalPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    MaxCapacity = table.Column<int>(type: "integer", nullable: true),
                    MinCapacity = table.Column<int>(type: "integer", nullable: true),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    MinimalPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    BanquetHallId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Premises_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Premises_BanquetHalls_BanquetHallId",
                        column: x => x.BanquetHallId,
                        principalTable: "BanquetHalls",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false),
                    AvatarUrl = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    Evaluation = table.Column<double>(type: "double precision", nullable: false),
                    BanquetHallId = table.Column<Guid>(type: "uuid", nullable: true),
                    CateringId = table.Column<Guid>(type: "uuid", nullable: true),
                    ConfectionerId = table.Column<Guid>(type: "uuid", nullable: true),
                    DecoratorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DjId = table.Column<Guid>(type: "uuid", nullable: true),
                    LimousineId = table.Column<Guid>(type: "uuid", nullable: true),
                    PhotoStudioId = table.Column<Guid>(type: "uuid", nullable: true),
                    PhotographerId = table.Column<Guid>(type: "uuid", nullable: true),
                    RegistrarId = table.Column<Guid>(type: "uuid", nullable: true),
                    StylistId = table.Column<Guid>(type: "uuid", nullable: true),
                    ToastmasterId = table.Column<Guid>(type: "uuid", nullable: true),
                    VideographerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_BanquetHalls_BanquetHallId",
                        column: x => x.BanquetHallId,
                        principalTable: "BanquetHalls",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Caterings_CateringId",
                        column: x => x.CateringId,
                        principalTable: "Caterings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Confectioners_ConfectionerId",
                        column: x => x.ConfectionerId,
                        principalTable: "Confectioners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Decorators_DecoratorId",
                        column: x => x.DecoratorId,
                        principalTable: "Decorators",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Djs_DjId",
                        column: x => x.DjId,
                        principalTable: "Djs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Limousines_LimousineId",
                        column: x => x.LimousineId,
                        principalTable: "Limousines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_PhotoStudios_PhotoStudioId",
                        column: x => x.PhotoStudioId,
                        principalTable: "PhotoStudios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Photographers_PhotographerId",
                        column: x => x.PhotographerId,
                        principalTable: "Photographers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Registrars_RegistrarId",
                        column: x => x.RegistrarId,
                        principalTable: "Registrars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Stylists_StylistId",
                        column: x => x.StylistId,
                        principalTable: "Stylists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Toastmasters_ToastmasterId",
                        column: x => x.ToastmasterId,
                        principalTable: "Toastmasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Videographers_VideographerId",
                        column: x => x.VideographerId,
                        principalTable: "Videographers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    MaxPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    MinPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    CateringId = table.Column<Guid>(type: "uuid", nullable: true),
                    ConfectionerId = table.Column<Guid>(type: "uuid", nullable: true),
                    DecoratorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DjId = table.Column<Guid>(type: "uuid", nullable: true),
                    PhotoStudioId = table.Column<Guid>(type: "uuid", nullable: true),
                    PhotographerId = table.Column<Guid>(type: "uuid", nullable: true),
                    RegistrarId = table.Column<Guid>(type: "uuid", nullable: true),
                    StylistId = table.Column<Guid>(type: "uuid", nullable: true),
                    ToastmasterId = table.Column<Guid>(type: "uuid", nullable: true),
                    VideographerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Caterings_CateringId",
                        column: x => x.CateringId,
                        principalTable: "Caterings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Confectioners_ConfectionerId",
                        column: x => x.ConfectionerId,
                        principalTable: "Confectioners",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Decorators_DecoratorId",
                        column: x => x.DecoratorId,
                        principalTable: "Decorators",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Djs_DjId",
                        column: x => x.DjId,
                        principalTable: "Djs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_PhotoStudios_PhotoStudioId",
                        column: x => x.PhotoStudioId,
                        principalTable: "PhotoStudios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Photographers_PhotographerId",
                        column: x => x.PhotographerId,
                        principalTable: "Photographers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Registrars_RegistrarId",
                        column: x => x.RegistrarId,
                        principalTable: "Registrars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Stylists_StylistId",
                        column: x => x.StylistId,
                        principalTable: "Stylists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Toastmasters_ToastmasterId",
                        column: x => x.ToastmasterId,
                        principalTable: "Toastmasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Videographers_VideographerId",
                        column: x => x.VideographerId,
                        principalTable: "Videographers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                name: "IX_AspNetUsers_CityId",
                table: "AspNetUsers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BanquetHalls_AlbumId",
                table: "BanquetHalls",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_BanquetHalls_CityId",
                table: "BanquetHalls",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_BanquetHalls_ExternalInfoId",
                table: "BanquetHalls",
                column: "ExternalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_BanquetHalls_WeddingPlanningId",
                table: "BanquetHalls",
                column: "WeddingPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_WeddingPlanningId",
                table: "Categories",
                column: "WeddingPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_Caterings_AlbumId",
                table: "Caterings",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Caterings_CityId",
                table: "Caterings",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Caterings_ExternalInfoId",
                table: "Caterings",
                column: "ExternalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Caterings_WeddingPlanningId",
                table: "Caterings",
                column: "WeddingPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_Confectioners_AlbumId",
                table: "Confectioners",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Confectioners_CityId",
                table: "Confectioners",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Confectioners_ExternalInfoId",
                table: "Confectioners",
                column: "ExternalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Confectioners_WeddingPlanningId",
                table: "Confectioners",
                column: "WeddingPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_Decorators_AlbumId",
                table: "Decorators",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Decorators_CityId",
                table: "Decorators",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Decorators_ExternalInfoId",
                table: "Decorators",
                column: "ExternalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Decorators_WeddingPlanningId",
                table: "Decorators",
                column: "WeddingPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_Djs_AlbumId",
                table: "Djs",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Djs_CityId",
                table: "Djs",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Djs_ExternalInfoId",
                table: "Djs",
                column: "ExternalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Djs_WeddingPlanningId",
                table: "Djs",
                column: "WeddingPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalInfo_ApplicationUserId",
                table: "ExternalInfo",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Limousines_AlbumId",
                table: "Limousines",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Limousines_CityId",
                table: "Limousines",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Limousines_ExternalInfoId",
                table: "Limousines",
                column: "ExternalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Limousines_WeddingPlanningId",
                table: "Limousines",
                column: "WeddingPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_Medias_AlbumId",
                table: "Medias",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_WPItemDescId",
                table: "Payments",
                column: "WPItemDescId");

            migrationBuilder.CreateIndex(
                name: "IX_Photographers_AlbumId",
                table: "Photographers",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Photographers_CityId",
                table: "Photographers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Photographers_ExternalInfoId",
                table: "Photographers",
                column: "ExternalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Photographers_WeddingPlanningId",
                table: "Photographers",
                column: "WeddingPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoStudios_AlbumId",
                table: "PhotoStudios",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoStudios_CityId",
                table: "PhotoStudios",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoStudios_ExternalInfoId",
                table: "PhotoStudios",
                column: "ExternalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoStudios_WeddingPlanningId",
                table: "PhotoStudios",
                column: "WeddingPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_Premises_AlbumId",
                table: "Premises",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Premises_BanquetHallId",
                table: "Premises",
                column: "BanquetHallId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrars_AlbumId",
                table: "Registrars",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrars_CityId",
                table: "Registrars",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrars_ExternalInfoId",
                table: "Registrars",
                column: "ExternalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrars_WeddingPlanningId",
                table: "Registrars",
                column: "WeddingPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BanquetHallId",
                table: "Reviews",
                column: "BanquetHallId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CateringId",
                table: "Reviews",
                column: "CateringId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ConfectionerId",
                table: "Reviews",
                column: "ConfectionerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_DecoratorId",
                table: "Reviews",
                column: "DecoratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_DjId",
                table: "Reviews",
                column: "DjId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_LimousineId",
                table: "Reviews",
                column: "LimousineId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PhotographerId",
                table: "Reviews",
                column: "PhotographerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PhotoStudioId",
                table: "Reviews",
                column: "PhotoStudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RegistrarId",
                table: "Reviews",
                column: "RegistrarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_StylistId",
                table: "Reviews",
                column: "StylistId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ToastmasterId",
                table: "Reviews",
                column: "ToastmasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_VideographerId",
                table: "Reviews",
                column: "VideographerId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_CateringId",
                table: "Services",
                column: "CateringId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ConfectionerId",
                table: "Services",
                column: "ConfectionerId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_DecoratorId",
                table: "Services",
                column: "DecoratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_DjId",
                table: "Services",
                column: "DjId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_PhotographerId",
                table: "Services",
                column: "PhotographerId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_PhotoStudioId",
                table: "Services",
                column: "PhotoStudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_RegistrarId",
                table: "Services",
                column: "RegistrarId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_StylistId",
                table: "Services",
                column: "StylistId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ToastmasterId",
                table: "Services",
                column: "ToastmasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_VideographerId",
                table: "Services",
                column: "VideographerId");

            migrationBuilder.CreateIndex(
                name: "IX_Stylists_AlbumId",
                table: "Stylists",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Stylists_CityId",
                table: "Stylists",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Stylists_ExternalInfoId",
                table: "Stylists",
                column: "ExternalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Stylists_WeddingPlanningId",
                table: "Stylists",
                column: "WeddingPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_Toastmasters_AlbumId",
                table: "Toastmasters",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Toastmasters_CityId",
                table: "Toastmasters",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Toastmasters_ExternalInfoId",
                table: "Toastmasters",
                column: "ExternalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Toastmasters_WeddingPlanningId",
                table: "Toastmasters",
                column: "WeddingPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_Videographers_AlbumId",
                table: "Videographers",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Videographers_CityId",
                table: "Videographers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Videographers_ExternalInfoId",
                table: "Videographers",
                column: "ExternalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Videographers_WeddingPlanningId",
                table: "Videographers",
                column: "WeddingPlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_WeddingPlannings_ApplicationUserId",
                table: "WeddingPlannings",
                column: "ApplicationUserId");
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
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Premises");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "WPItemDescs");

            migrationBuilder.DropTable(
                name: "BanquetHalls");

            migrationBuilder.DropTable(
                name: "Limousines");

            migrationBuilder.DropTable(
                name: "Caterings");

            migrationBuilder.DropTable(
                name: "Confectioners");

            migrationBuilder.DropTable(
                name: "Decorators");

            migrationBuilder.DropTable(
                name: "Djs");

            migrationBuilder.DropTable(
                name: "PhotoStudios");

            migrationBuilder.DropTable(
                name: "Photographers");

            migrationBuilder.DropTable(
                name: "Registrars");

            migrationBuilder.DropTable(
                name: "Stylists");

            migrationBuilder.DropTable(
                name: "Toastmasters");

            migrationBuilder.DropTable(
                name: "Videographers");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "ExternalInfo");

            migrationBuilder.DropTable(
                name: "WeddingPlannings");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}