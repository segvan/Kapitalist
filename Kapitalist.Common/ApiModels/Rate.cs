namespace Kapitalist.Common.ApiModels
{
    public class Rate
    {
        public string From { get; set; }
        
        public string To { get; set; }
        
        public decimal RateAsk { get; set; }
        
        public decimal RateBid { get; set; }
    }
}