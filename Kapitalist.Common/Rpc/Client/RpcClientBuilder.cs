using System;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kapitalist.Common.Rpc.Client
{
    public class RpcClientBuilder
    {
        private readonly IServiceCollection serviceCollection;
        private readonly IConfigurationSection configurationSection;

        public RpcClientBuilder(IServiceCollection serviceCollection, IConfigurationSection configurationSection)
        {
            this.serviceCollection = serviceCollection;
            this.configurationSection = configurationSection;
        }

        public RpcClientBuilder AddClient<TClient>() where TClient : ClientBase<TClient>
        {
            var clientType = typeof(TClient);
            var clientName = clientType.Name;
            var clientConfigurationSection = configurationSection.GetSection(clientName);

            if (!clientConfigurationSection.Exists())
            {
                throw new Exception($"Cannot find configuration section for rpcClient:{clientName}");
            }

            var host = clientConfigurationSection.GetValue<string>("Host");
            var port = clientConfigurationSection.GetValue<int>("Port");

            serviceCollection.AddSingleton<TClient>(serviceProvider =>
            {
                var channel = GrpcChannel.ForAddress($"http://{host}:{port}");
                var instance = Activator.CreateInstance(clientType, channel) as TClient;
                return instance;
            });

            return this;
        }
    }
}