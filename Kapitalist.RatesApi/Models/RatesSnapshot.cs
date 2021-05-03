using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kapitalist.Common.Enums;

namespace Kapitalist.RatesApi.Models
{
    public class RatesSnapshot
    {
        [Key] 
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)] 
        public string Source { get; set; }

        [EnumDataType(typeof(RateType))]
        public RateType RateType { get; set; }

        public DateTime TimeStamp { get; set; }

        [Required]
        [MinLength(1)]
        public IEnumerable<Rate> CurrencyValues { get; set; }
    }
}