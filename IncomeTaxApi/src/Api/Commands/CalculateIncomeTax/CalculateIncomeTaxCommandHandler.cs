using FluentValidation;
using IncomeTaxApi.Abstractions;
using IncomeTaxApi.Api.Dtos;
using IncomeTaxApi.Api.Services;
using IncomeTaxApi.Data.Models;

namespace IncomeTaxApi.Api.Commands.CalculateIncomeTax
{

    public class CalculateIncomeTaxCommandHandler : IRequestHandler<CalculateIncomeTaxCommand, TaxBreakdownDto>
    {
        private readonly ICalculateIncomeTaxService _calculateIncomeTaxService;
        private readonly CalculateIncomeTaxCommandValidator _validator;
        private readonly IConverter<ITaxBreakdown, TaxBreakdownDto> _converter;
        private readonly ILogger<CalculateIncomeTaxCommandHandler> _logger;

        public CalculateIncomeTaxCommandHandler(ICalculateIncomeTaxService calculateIncomeTaxService,
                                                IConverter<ITaxBreakdown, TaxBreakdownDto> converter,
                                                CalculateIncomeTaxCommandValidator validator,
                                                ILogger<CalculateIncomeTaxCommandHandler> logger)
        {
            _calculateIncomeTaxService = calculateIncomeTaxService;
            _converter = converter;
            _validator = validator;
            _logger = logger;
        }

        // Command handler for triggering domain logic
        // Utilizing the CQRS pattern to improve performance, scalability, and future security (when implemented)
        public async Task<TaxBreakdownDto> HandleAsync(CalculateIncomeTaxCommand command)
        {
            await _validator.ValidateAndThrowAsync(command);

            try
            {
                var taxCalculations =
                    await _calculateIncomeTaxService.CalculateIncomeTaxAsync(command.AnnualSalaryAmount);
                return _converter.Convert(taxCalculations);
            }
            catch (Exception e)
            {
                // For future work I would implement more tailored exceptions (as I have done for EntityNotFoundException)
                // so that it could be applied in other areas and passed to the UI for personalised error toasts where applicable
                _logger.LogError("Unexpected exception thrown when attempting to calculate income tax {E}", e);
                throw new InvalidOperationException("Unexpected exception thrown when attempting to calculate income tax");
            }
        }
    }
}