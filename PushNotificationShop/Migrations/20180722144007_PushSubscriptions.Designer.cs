﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using PushNotificationShop.Entities.Context;
using System;

namespace PushNotificationShop.Migrations
{
    [DbContext(typeof(PushNotificationShopDbContext))]
    [Migration("20180722144007_PushSubscriptions")]
    partial class PushSubscriptions
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PushNotificationShop.Entities.Item", b =>
                {
                    b.Property<int>("Objectid")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cost");

                    b.Property<string>("Name");

                    b.HasKey("Objectid");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("PushNotificationShop.Entities.PushSubscription", b =>
                {
                    b.Property<int>("ObjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Auth");

                    b.Property<string>("EndPoint");

                    b.Property<string>("P256dh");

                    b.HasKey("ObjectId");

                    b.ToTable("PushSubscriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
