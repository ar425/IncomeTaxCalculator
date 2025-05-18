using IncomeTaxApi.Data.Contexts;
using IncomeTaxApi.Data.Models;

namespace IncomeTaxApi.Data.Repositories
{

    public interface ITaxBandRepository : IRepositoryBase<TaxBand>
    {
        
    }
    
    public class TaxBandRepository: RepositoryBase<TaxBand>, ITaxBandRepository
    {
        public TaxBandRepository(IIncomeTaxCalculatorContext context) : base(context)
        {
        }
    }
}