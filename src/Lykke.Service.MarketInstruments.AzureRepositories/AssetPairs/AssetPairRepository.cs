using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AzureStorage;
using Lykke.Service.MarketInstruments.Domain;
using Lykke.Service.MarketInstruments.Domain.Repositories;

namespace Lykke.Service.MarketInstruments.AzureRepositories.AssetPairs
{
    public class AssetPairRepository : IAssetPairRepository
    {
        private readonly INoSQLTableStorage<AssetPairEntity> _storage;

        public AssetPairRepository(INoSQLTableStorage<AssetPairEntity> storage)
        {
            _storage = storage;
        }

        public async Task<IReadOnlyList<AssetPair>> GetAllAsync()
        {
            IList<AssetPairEntity> entities = await _storage.GetDataAsync();

            return Mapper.Map<List<AssetPair>>(entities);
        }

        public async Task InsertAsync(AssetPair assetPair)
        {
            var entity = new AssetPairEntity(GetPartitionKey(assetPair.Exchange), GetRowKey(assetPair.Name));

            Mapper.Map(assetPair, entity);

            await _storage.InsertAsync(entity);
        }

        public Task UpdateAsync(AssetPair assetPair)
        {
            return _storage.MergeAsync(GetPartitionKey(assetPair.Exchange), GetRowKey(assetPair.Name), entity =>
            {
                Mapper.Map(assetPair, entity);
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
