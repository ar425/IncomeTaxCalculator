using IncomeTaxApi.Abstractions;
using IncomeTaxApi.Api.Commands.CalculateIncomeTax;
using IncomeTaxApi.Api.Dtos;
using IncomeTaxApi.Api.Queries.GetIncomeSalary;
using Microsoft.AspNetCore.Mvc;

namespace IncomeTaxApi.Api.Controllers
{
    // Controller to handle all tax calculation based http requests
    // if for example we added user authentication, those http requests would be handled in a separate controller
    // to keep the code clean, more maintainable, and scalable
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly IRequestHandler<CalculateIncomeTaxCommand, TaxBreakdownDto> _calculateIncomeTaxCommandHandler;
        // example query handler
        private readonly IRequestHandler<GetIncomeSalaryQuery, SalaryDto> _getIncomeSalaryQueryHandler;

        public TaxController(IRequestHandler<CalculateIncomeTaxCommand, TaxBreakdownDto> calculateIncomeTaxCommandHandler,
                             IRequestHandler<GetIncomeSalaryQuery, SalaryDto> getIncomeSalaryQueryHandler)
        {
            _calculateIncomeTaxCommandHandler = calculateIncomeTaxCommandHandler;
            _getIncomeSalaryQueryHandler = getIncomeSalaryQueryHandler;
        }

        [HttpPost("calculate")]
        public async Task<ActionResult<TaxBreakdownDto>> CalculateTax([FromBody] CalculateIncomeTaxCommand command)
        {
            var result = await _calculateIncomeTaxCommandHandler.HandleAsync(command);

            return Ok(result);
        }
        
        // example get query
        [HttpGet]
        public async Task<ActionResult<SalaryDto>> GetSalary()
        {
            var query = new GetIncomeSalaryQuery();
            var result = await _getIncomeSalaryQueryHandler.HandleAsync(query);

            return Ok(result);
        }
    }
}