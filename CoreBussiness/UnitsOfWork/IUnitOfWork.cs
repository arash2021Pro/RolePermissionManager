using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoreBussiness.UnitsOfWork;

public interface IUnitOfWork
{
    DbSet<TEntity> Set<TEntity>() where TEntity:class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken  = new CancellationToken() );
    int SaveChanges(CancellationToken cancellationToken  = new CancellationToken());
    IQueryable<TEntity> FromSqlRaw<TEntity>(string sql, params object[] parameters) where TEntity : class;
    IQueryable<TEntity> FromSqlInterpolated<TEntity>(FormattableString sql) where TEntity : class;
    Task<int> ExecuteRemoveAsync<TEntity>(TEntity? entity, CancellationToken cancellationToken = default) where TEntity : class;

    Task<int> ExecuteAddAsync<TEntity>(TEntity? entity, CancellationToken cancellationToken = default)
        where TEntity : class;
    Task<IDbContextTransaction> BeginTransaction();
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default);
    void CommitTransaction(IDbContextTransaction transaction);
    void RollbackTransaction(IDbContextTransaction transaction);
    Task RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken);
}