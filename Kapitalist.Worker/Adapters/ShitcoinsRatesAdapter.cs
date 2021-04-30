using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kapitalist.Common.ApiModels;
using Kapitalist.Common.Enums;
using Kapitalist.Common.Extensions;
using Kapitalist.Common.Services;
using Marvin.StreamExtensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Kapitalist.Worker.Adapters
{
    public class ShitcoinsRatesAdapter : IRatesAdapter
    {
        public string Name => "shitcoins.club";

        private const string ServiceUrl = "https://shitcoins.club";
        private const string CurrenciesEndpoint = "/currency/getEnabledCurrenciesValues.json";
        private readonly HttpClient httpClient;
        private readonly WorkerSettings workerSettings;
        private readonly IDateTimeService dateTimeService;

        public ShitcoinsRatesAdapter(IHttpClientFactory httpClientFactory, IOptions<WorkerSettings> workerSettings,
            IDateTimeService dateTimeService)
        {
            this.dateTimeService = dateTimeService;
            this.workerSettings = workerSettings.Value;
            httpClient = httpClientFactory.CreateGenericClient();
            httpClient.BaseAddress = new Uri(ServiceUrl);
        }

        public async Task<RatesSnapshot> Get(CancellationToken cancellationToken)
        {
            var result = new RatesSnapshot
            {
                Source = httpClient?.BaseAddress?.Host?.ToLower() ?? Name,
                RateType = RateType.CryptoCurrency
            };

            var response = await CreateRequestAsync(cancellationToken);

            result.TimeStamp = dateTimeService.UtcNow();
            result.CurrencyValues = ParseResponseAsync(response, cancellationToken);

            return result;
        }

        private async Task<Model> CreateRequestAsync(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, CurrenciesEndpoint);

            using var response = await httpClient.SendAsync(request, cancellationToken);
            await using Stream stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            Model result = await stream.ReadAndDeserializeFromJsonAsync<Model>();
            return result;
        }

        private Rate[] ParseResponseAsync(Model response, CancellationToken cancellationToken)
        {
            if (response?.IsError == true)
            {
                // ReSharper disable once MethodSupportsCancellation
                throw new HttpRequestException(JsonConvert.SerializeObject(response));
            }

            var currencies = response?.Message?.FirstOrDefault();
            if (currencies == null)
            {
                return new Rate[0];
            }


            return currencies
                .Where(x => workerSettings.SupportedCurrencies.Any(y =>
                    y.Equals(x.ToCurrency, StringComparison.OrdinalIgnoreCase)))
                .Select(x => new Rate
                {
                    From = x.FromCurrency,
                    To = x.ToCurrency,
                    RateAsk = x.RateAsk,
                    RateBid = x.RateBid
                })
                .ToArray();
        }

        private class Model
        {
            public bool IsError { get; set; }

            public IEnumerable<Rate[]> Message { get; set; }

            public class Rate
            {
                public string FromCurrency { get; set; }

                public string ToCurrency { get; set; }

                public decimal RateAsk { get; set; }

                public decimal RateBid { get; set; }
            }
        }
    }
}