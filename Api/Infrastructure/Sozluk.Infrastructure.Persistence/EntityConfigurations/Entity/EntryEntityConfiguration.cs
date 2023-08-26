using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sozluk.Api.Domain.Models;
using Sozluk.Infrastructure.Persistence.Context;

namespace Sozluk.Infrastructure.Persistence.EntityConfigurations.Entity
{
	public abstract class EntryEntityConfiguration:BaseEntityConfiguration<Entry>
	{
        public override void Configure(EntityTypeBuilder<Entry> builder)
        {
            base.Configure(builder);
            builder.ToTable("entry", SozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(i => i.CreatedBy)
                .WithMany(i => i.Entries)
                .HasForeignKey(i => i.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}

