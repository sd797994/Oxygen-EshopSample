﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trade.Infrastructure.EfDataAccess;

namespace Trade.Infrastructure.Migrations
{
    [DbContext(typeof(TradeContext))]
    partial class TradeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Trade.Infrastructure.PersistenceObject.Trade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid>("CreateUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Trade");
                });

            modelBuilder.Entity("Trade.Infrastructure.PersistenceObject.TradeRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreateTime");

                    b.Property<Guid>("CreateUserId");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("TradeId");

                    b.Property<DateTime>("TradeTime");

                    b.Property<int>("TradeType");

                    b.HasKey("Id");

                    b.ToTable("TradeRecord");
                });
#pragma warning restore 612, 618
        }
    }
}
