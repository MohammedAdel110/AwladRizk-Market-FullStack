using Microsoft.EntityFrameworkCore.Storage;
using AwladRizk.Domain.Interfaces;

namespace AwladRizk.Persistence;

/// <summary>
/// Unit of Work implementation wrapping DbContext.
/// Provides atomic SaveChanges and explicit transaction support.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly AwladRizkDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(AwladRizkDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }

    public async Task BeginTransactionAsync(CancellationToken ct = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitTransactionAsync(CancellationToken ct = default)
    {
        if (_transaction is null)
            throw new InvalidOperationException("No active transaction to commit.");

        try
        {
            await _context.SaveChangesAsync(ct);
            await _transaction.CommitAsync(ct);
        }
        catch
        {
            await RollbackTransactionAsync(ct);
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken ct = default)
    {
        if (_transaction is null) return;

        try
        {
            await _transaction.RollbackAsync(ct);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
