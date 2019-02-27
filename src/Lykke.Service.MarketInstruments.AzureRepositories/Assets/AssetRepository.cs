using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using Lykke.Service.MarketInstruments.Domain;
using Lykke.Service.MarketInstruments.Domain.Repositories;

namespace Lykke.Service.MarketInstruments.AzureRepositories.Assets
{
    public class AssetRepository : IAssetRepository
    {
        private readonly INoSQLTableStorage<AssetEntity> _storage;

        public AssetRepository(INoSQLTableStorage<AssetEntity> storage)
        {
            _storage = storage;
        }

        public async Task<IReadOnlyList<Asset>> GetAllAsync()
        {
            IList<AssetEntity> entities = await _storage.GetDataAsync();

            return Mapper.Map<List<Asset>>(entities);
        }

        public async Task InsertAsync(Asset asset)
        {
            var entity = new AssetEntity(GetPartitionKey(asset.Exchange), GetRowKey(asset.Name));

            Mapper.Map(asset, entity);

            await _storage.InsertAsync(entity);
        }

        public Task UpdateAsync(Asset asset)
        {
            return _storage.MergeAsync(GetPartitionKey(asset.Exchange), GetRowKey(asset.Name),
                entity =>
                {
                    Mapper.Map(asset, entity);
                    return entity;
                });
        }

        public Task DeleteAsync(string name, string exchange)
        {
            return _storage.DeleteAsync(GetPartitionKey(exchange), GetRowKey(name));
        }

        private static string GetPartitionKey(string exchange)
            => exchange.ToUpper();

        private static string GetRowKey(string name)
            => name.ToUpper();
    }
}
