using System.Reflection;
using CoreBussiness.BaseEntity;
using CoreBussiness.BussinessEntity.Permissions;
using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.UnitsOfWork;
using CoreStorage.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoreStorage.AppContext;

public class ApplicationContext:DbContext,IUnitOfWork
{
    public ApplicationContext(DbContextOptions<ApplicationContext>options):base(options)
    {
        
    }
        
    public DbSet<User>Users { get; set; }
    public DbSet<Role>Roles { get; set; }
    public DbSet<Permission>Permissions { get; set; }



    public override DbSet<TEntity> Set<TEntity>()
    {
        return base.Set<TEntity>();
    }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return base.SaveChangesAsync(cancellationToken);
    }
    
    public  IQueryable<TEntity> FromSqlRaw<TEntity>(string sql, params object[] parameters) where TEntity : class
    {   
        return base.Set<TEntity>().FromSqlRaw(sql, parameters);
    }

    public IQueryable<TEntity> FromSqlInterpolated<TEntity>(FormattableString sql) where TEntity : class
    {
        return base.Set<TEntity>().FromSqlInterpolated(sql);
    }
    
    public int SaveChanges(CancellationToken cancellationToken = new CancellationToken())
    {
        return base.SaveChanges();
    }
    
    public Task<IDbContextTransaction> BeginTransaction()
    {
        return base.Database.BeginTransactionAsync();

    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
       return await base.Database.BeginTransactionAsync(cancellationToken);
    
    }

    public void CommitTransaction(IDbContextTransaction transaction)
    {
        transaction.Commit();
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default)
    {
        await transaction.CommitAsync(cancellationToken);
    }

    public void RollbackTransaction(IDbContextTransaction transaction)
    {
        transaction.Rollback();
    }

    public async Task RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default)
    {
        await transaction.RollbackAsync(cancellationToken);
    }
    
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(UserConfiguration))!);
        
            
        var entities = modelBuilder
            .Model
            .GetEntityTypes()
            .Select(x => x.ClrType)
            .Where(x => x.BaseType == typeof(Core))
            .ToList();

        foreach (var type in entities)
        {
            var method = SetGlobalQueryMethod.MakeGenericMethod(type);
            method.Invoke(this, new object[] {modelBuilder});
        }
    }

    public static readonly MethodInfo SetGlobalQueryMethod = typeof(ApplicationContext)
        .GetMethods(BindingFlags.Public | BindingFlags.Instance)
        .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");

    
    
    public void SetGlobalQuery<T>(ModelBuilder builder) where T : Core
    {
        builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
    }


    private void changeEntitiesStates()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is Core && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                ((Core)entityEntry.Entity).CreationTimeOffset = DateTimeOffset.Now;
            }
            else if (entityEntry.State == EntityState.Modified)
            {
                ((Core)entityEntry.Entity).ModificationTime = DateTimeOffset.Now;
            }
        }
    }
    
    public class BloggingContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=InstaManager;Trusted_Connection=True;TrustServerCertificate=True;");

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
    
}