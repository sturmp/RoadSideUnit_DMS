﻿// <auto-generated />
using System;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SNMPManager.Core.Enumerations;
using SNMPManager.Persistence;

namespace SNMPManager.WebAPI.Migrations
{
    [DbContext(typeof(ManagerContext))]
    [Migration("20190406120019_UserModified")]
    partial class UserModified
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:Enum:log_type", "db,security,apicall,snmp")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("SNMPManager.Core.Entities.ManagerLog", b =>
                {
                    b.Property<DateTime>("TimeStamp");

                    b.Property<LogType>("Type");

                    b.Property<string>("Message");

                    b.HasKey("TimeStamp", "Type");

                    b.ToTable("ManagerLogs");
                });

            modelBuilder.Entity("SNMPManager.Core.Entities.ManagerSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Timeout");

                    b.HasKey("Id");

                    b.ToTable("ManagerSettings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Timeout = 2000
                        });
                });

            modelBuilder.Entity("SNMPManager.Core.Entities.RSU", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("FirmwareVersion")
                        .HasMaxLength(32);

                    b.Property<IPAddress>("IP")
                        .IsRequired();

                    b.Property<double>("Latitude");

                    b.Property<string>("LocationDescription")
                        .HasMaxLength(140);

                    b.Property<double>("Longitude");

                    b.Property<string>("MIBVersion")
                        .HasMaxLength(32);

                    b.Property<string>("Manufacturer")
                        .HasMaxLength(32);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<IPAddress>("NotificationIP")
                        .IsRequired();

                    b.Property<int>("NotificationPort");

                    b.Property<int>("Port");

                    b.HasKey("Id");

                    b.ToTable("RSUs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Active = true,
                            FirmwareVersion = "",
                            IP = System.Net.IPAddress.Parse("172.168.45.27"),
                            Latitude = 17.449999999999999,
                            LocationDescription = "",
                            Longitude = 24.120000000000001,
                            MIBVersion = "",
                            Manufacturer = "Commsignia",
                            Name = "TestRSU",
                            NotificationIP = System.Net.IPAddress.Parse("186.56.123.84"),
                            NotificationPort = 161,
                            Port = 162
                        },
                        new
                        {
                            Id = 2,
                            Active = true,
                            FirmwareVersion = "",
                            IP = System.Net.IPAddress.Parse("112.111.45.89"),
                            Latitude = 19.449999999999999,
                            LocationDescription = "",
                            Longitude = 45.119999999999997,
                            MIBVersion = "",
                            Manufacturer = "Commsignia",
                            Name = "RSUuu",
                            NotificationIP = System.Net.IPAddress.Parse("186.56.123.84"),
                            NotificationPort = 161,
                            Port = 162
                        },
                        new
                        {
                            Id = 3,
                            Active = true,
                            FirmwareVersion = "",
                            IP = System.Net.IPAddress.Parse("127.0.0.1"),
                            Latitude = 13.449999999999999,
                            LocationDescription = "",
                            Longitude = 32.119999999999997,
                            MIBVersion = "",
                            Manufacturer = "Commsignia",
                            Name = "RSUjavaagent",
                            NotificationIP = System.Net.IPAddress.Parse("127.0.0.1"),
                            NotificationPort = 162,
                            Port = 161
                        });
                });

            modelBuilder.Entity("SNMPManager.Core.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Monitor"
                        });
                });

            modelBuilder.Entity("SNMPManager.Core.Entities.TrapLog", b =>
                {
                    b.Property<DateTime>("TimeStamp");

                    b.Property<LogType>("Type");

                    b.Property<int>("SourceRSU");

                    b.Property<string>("Message");

                    b.HasKey("TimeStamp", "Type", "SourceRSU");

                    b.ToTable("TrapLogs");
                });

            modelBuilder.Entity("SNMPManager.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("RoleId");

                    b.Property<string>("SNMPv3Auth");

                    b.Property<string>("SNMPv3Priv");

                    b.Property<string>("Token");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("UserName");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleId = 1,
                            SNMPv3Auth = "authpass012",
                            SNMPv3Priv = "privpass012",
                            Token = "Adminpass01",
                            UserName = "admin"
                        },
                        new
                        {
                            Id = 2,
                            RoleId = 2,
                            SNMPv3Auth = "authpass012",
                            SNMPv3Priv = "privpass012",
                            Token = "Monitorpass01",
                            UserName = "monitor"
                        });
                });

            modelBuilder.Entity("SNMPManager.Core.Entities.User", b =>
                {
                    b.HasOne("SNMPManager.Core.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });
#pragma warning restore 612, 618
        }
    }
}