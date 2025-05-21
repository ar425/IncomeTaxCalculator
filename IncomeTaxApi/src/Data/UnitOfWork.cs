using IncomeTaxApi.Data.Repositories;

namespace IncomeTaxApi.Data
{
    /// <summary>
    /// Wrapper class that helps avoid injecting and using the DbContext directly,
    /// as well as giving flexibility with unit tests
    /// Currently only containing functions that this application uses
    /// but can be expanded
    /// </summary>
    public interface IUnitOfWork
    {
        TRepository GetRepository<TRepository>();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IEnumerable<IRepositoryBase> _repositories;

        public UnitOfWork(IEnumerable<IRepositoryBase> repositories)
        {
            _repositories = repositories;
        }
        
        public TRepository GetRepository<TRepository>()
        {
            return _repositories.OfType<TRepository>().Single();
        }
    }
}