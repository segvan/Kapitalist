using System;
using FluentValidation;
using Kapitalist.Common.Enums;

namespace Kapitalist.Common.ApiModels
{
    public class RatesSnapshot
    {
        public string Source { get; set; }

        public RateType RateType { get; set; }

        public DateTime TimeStamp { get; set; }

        public Rate[] CurrencyValues { get; set; }
    }
    
    public class RatesSnapshotValidator : AbstractValidator<RatesSnapshot> {
        public RatesSnapshotValidator() {
            RuleFor(x => x.Source).NotEmpty().MaximumLength(100);
            RuleFor(x => x.RateType).IsInEnum();
            RuleFor(x => x.CurrencyValues).NotEmpty();
            RuleForEach(x => x.CurrencyValues).SetValidator(new RateValidator());
        }
    }
}