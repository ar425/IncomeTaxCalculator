using IncomeTaxApi.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace IncomeTaxApi.Data.Repositories
{
    /// <summary>
    /// Non-generic marker interface used by <see cref="UnitOfWork"/>
    /// </summary>
    public interface IRepositoryBase
    {
    }

    /// <summary>
    /// Repository base class containing functionality required for this application
    /// </summary>
    public interface IRepositoryBase<TEntity> : IRepositoryBase where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
    }

    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected RepositoryBase(IIncomeTaxCalculatorContext context)
        {
            Context = context;
        }

        private IIncomeTaxCalculatorContext Context { get; }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await ((DbContext)Context).Set<TEntity>().ToListAsync();
        }
    }
}