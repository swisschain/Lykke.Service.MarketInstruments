using Autofac;
using AzureStorage.Tables;
using JetBrains.Annotations;
using Lykke.Common.Log;
using Lykke.Service.MarketInstruments.AzureRepositories.AssetPairs;
using Lykke.Service.MarketInstruments.AzureRepositories.Assets;
using Lykke.Service.MarketInstruments.Domain.Repositories;
using Lykke.SettingsReader;

namespace Lykke.Service.MarketInstruments.AzureRepositories
{
    [UsedImplicitly]
    public class AutofacModule : Module
    {
        private readonly IReloadingManager<string> _connectionString;

        public AutofacModule(IReloadingManager<string> connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(container => new AssetRepository(
                    AzureTableStorage<AssetEntity>.Create(_connectionString,
                        "Assets", container.Resolve<ILogFactory>())))
                .As<IAssetRepository>()
                .SingleInstance();

            builder.Register(container => new AssetPairRepository(
                    AzureTableStorage<AssetPairEntity>.Create(_connectionString,
                        "AssetPairs", container.Resolve<ILogFactory>())))
                .As<IAssetPairRepository>()
                .SingleInstance();
        }
    }
}
