using System;
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
}