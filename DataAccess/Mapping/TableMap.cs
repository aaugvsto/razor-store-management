using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace DataAccess.Mapping
{
    public class TableMap : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("TABLES");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.StoreId)
                .IsRequired();

            builder.HasIndex(e => e.StoreId);

            builder.Property(e => e.Number)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.SeatsNumber)
                .IsRequired();

            builder.Property(e => e.IsAvailable)
                .IsRequired();

            builder.HasOne(e => e.Store)
                .WithMany(s => s.Tables)
                .HasForeignKey(e => e.StoreId);
        }
    }
}
