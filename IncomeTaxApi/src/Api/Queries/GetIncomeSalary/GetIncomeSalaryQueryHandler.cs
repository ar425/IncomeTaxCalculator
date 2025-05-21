using IncomeTaxApi.Abstractions;
using IncomeTaxApi.Api.Dtos;

namespace IncomeTaxApi.Api.Queries.GetIncomeSalary;

// Thought about saving salary and then getting it with this query, but it might be a security breach
// I have left this in here as an example query handler
public class GetIncomeSalaryQueryHandler : IRequestHandler<GetIncomeSalaryQuery, SalaryDto>
{
    public Task<SalaryDto> HandleAsync(GetIncomeSalaryQuery query)
    {
        // Information would be retrieved from the database
        // and converted from the database object to the dto
        
        // The response received from this query handler will be read only as it is only fetching the data
        return Task.FromResult(new SalaryDto());
    }
}