using FluentValidation;

namespace StockPerformance.API.Validators
{
    public class StockSymbolValidator : AbstractValidator<string>
    {
        public StockSymbolValidator()
        {
            RuleFor( symbol => symbol ).NotEmpty().WithMessage( "You must enter a stocking symbol" );
        }
    }
}