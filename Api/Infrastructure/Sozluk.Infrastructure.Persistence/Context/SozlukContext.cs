using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Sozluk.Api.Domain.Models;

namespace Sozluk.Infrastructure.Persistence.Context
{
    public class SozlukContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "dbo";

        public SozlukContext()
        {

        }
        public SozlukContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<User> Users { get; set; }
        DbSet<Entry> Entries { get; set; }
        DbSet<EntryVote> EntryVotes { get; set; }
        DbSet<EntryFavorite> EntryFavorites { get; set; }
        DbSet<EntryComment> EntryComments { get; set; }
        DbSet<EntryCommentVote> EntryCommentVotes { get; set; }
        DbSet<EntryCommentFavorite> EntryCommentFavorites { get; set; }
        DbSet<EmailConfirmation> EmailConfirmations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=SozlukDB.db");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            OnBeforeSave();
            return base.SaveChanges();

        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        public void OnBeforeSave()
        {
            var addedEntities = ChangeTracker.Entries()
                .Where(i => i.State == EntityState.Added)
                .Select(i => (BaseEntity)i.Entity);

            PrepareAddedEntites(addedEntities);
        }

        public void PrepareAddedEntites(IEnumerable<BaseEntity> baseEntities)
        {
            foreach (var entity in baseEntities)
            {
                if (entity.CreateDate==DateTime.MinValue)
                entity.CreateDate = DateTime.Now;
            }
        }
    }
}

