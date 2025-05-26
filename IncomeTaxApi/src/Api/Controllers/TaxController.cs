using IncomeTaxApi.Abstractions;
using IncomeTaxApi.Api.Commands.CalculateIncomeTax;
using IncomeTaxApi.Api.Dtos;
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

        public TaxController(IRequestHandler<CalculateIncomeTaxCommand, TaxBreakdownDto> calculateIncomeTaxCommandHandler)
        {
            _calculateIncomeTaxCommandHandler = calculateIncomeTaxCommandHandler;
        }

        [HttpPost("calculate")]
        public async Task<ActionResult<TaxBreakdownDto>> CalculateTax([FromBody] CalculateIncomeTaxCommand command)
        {
            var result = await _calculateIncomeTaxCommandHandler.HandleAsync(command);

            return Ok(result);
        }
    }
}