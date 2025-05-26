using IncomeTaxApi.Data.Repositories;

namespace IncomeTaxApi.Data
{
    /// <summary>
    /// Wrapper class that helps avoid injecting and using the DbContext directly,
    /// as well as giving flexibility with unit tests
    /// Currently only containing a simple get repository function this application uses - typically
    /// IUnitOfWork is used for more complex actions in order to ensure that database transactions
    /// are finalized concurrently across multiple databases
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

        // Although not utilized in this project, this function would be needed in order
        // to finalize the transactions across databases
        // public async Task CompleteAsync(CancellationToken cancellationToken = default)
        // {
        //     using (var transaction = CreateTransaction())
        //     {
        //         transaction.Complete();
        //     }
        //
        //     foreach (var command in _deferredCommands)
        //     {
        //         await command.Compile().Invoke();
        //     }
        // }
        // public void OnCompleted(Expression<Func<Task>> command)
        // {
        //     _deferredCommands.Add(command);
        // }
    }
}