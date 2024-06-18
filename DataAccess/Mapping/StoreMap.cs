using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;

namespace DataAccess.Mapping
{
    public class StoreMap : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("STORES");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.HasIndex(e => e.UserId);

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Phone)
                .IsRequired();

            builder.Property(e => e.ImageBase64)
                .IsRequired();

            builder.HasMany(e => e.Tables)
                .WithOne(e => e.Store)
                .HasForeignKey(e => e.StoreId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.User)
                .WithMany(e => e.Stores)
                .HasForeignKey(e => e.UserId);
        }
    }
}
