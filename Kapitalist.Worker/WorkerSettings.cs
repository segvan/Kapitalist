namespace Kapitalist.Worker
{
    public class WorkerSettings
    {
        public string[] SupportedCurrencies { get; set; }
        
        public int JobFrequencySeconds { get; set; }
    }
}