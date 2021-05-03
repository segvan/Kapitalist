using System;
using System.ComponentModel.DataAnnotations;

namespace Kapitalist.RatesApi.Models
{
    public class Rate
    {
        [Key] 
        public Guid Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string From { get; set; }
        
        [Required]
        [MaxLength(10)]
        public string To { get; set; }
        
        [Range(Double.Epsilon, Double.MaxValue, ErrorMessage = "Rate should be greater than zero.")]
        public decimal RateAsk { get; set; }
        
        [Range(Double.Epsilon, Double.MaxValue, ErrorMessage = "Rate should be greater than zero.")]
        public decimal RateBid { get; set; }
        
        public Guid RatesSnapshotId { get; set; }
    }
}