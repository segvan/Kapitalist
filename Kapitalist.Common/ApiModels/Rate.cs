using System;
using FluentValidation;

namespace Kapitalist.Common.ApiModels
{
    public class Rate
    {
        public string From { get; set; }

        public string To { get; set; }

        public decimal RateAsk { get; set; }

        public decimal RateBid { get; set; }
    }

    public class RateValidator : AbstractValidator<Rate>
    {
        public RateValidator()
        {
            RuleFor(x => x.From).NotEmpty().MaximumLength(10);
            RuleFor(x => x.To).NotEmpty().MaximumLength(10);
            RuleFor(x => x.RateAsk).ExclusiveBetween(0, Decimal.MaxValue);
            RuleFor(x => x.RateBid).ExclusiveBetween(0, Decimal.MaxValue);
        }
    }
}