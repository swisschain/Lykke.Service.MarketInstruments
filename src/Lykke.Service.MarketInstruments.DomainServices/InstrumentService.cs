using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Service.MarketInstruments.Domain;
using Lykke.Service.MarketInstruments.Domain.Exceptions;
using Lykke.Service.MarketInstruments.Domain.Repositories;
using Lykke.Service.MarketInstruments.Domain.Services;
using Lykke.Service.MarketInstruments.DomainServices.Extensions;

namespace Lykke.Service.MarketInstruments.DomainServices
{
    public class InstrumentService : IInstrumentService
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetPairRepository _assetPairRepository;
        private readonly ILog _log;

        private readonly InMemoryCache<Asset> _assetsCache;
        private readonly InMemoryCache<AssetPair> _assetPairsCache;

        public InstrumentService(
            IAssetRepository assetRepository,
            IAssetPairRepository assetPairRepository,
            ILogFactory logFactory)
        {
            _assetRepository = assetRepository;
            _assetPairRepository = assetPairRepository;
            _log = logFactory.CreateLog(this);

            _assetsCache = new InMemoryCache<Asset>(GetKey, false);
            _assetPairsCache = new InMemoryCache<AssetPair>(GetKey, false);
        }

        public async Task<IReadOnlyList<Asset>> GetAssetsAsync()
        {
            IReadOnlyList<Asset> assetsSettings = _assetsCache.GetAll();

            if (assetsSettings == null)
            {
                assetsSettings = await _assetRepository.GetAllAsync();

                _assetsCache.Initialize(assetsSettings);
            }

            return assetsSettings;
        }

        public async Task<Asset> GetAssetAsync(string name, string exchange)
        {
            IReadOnlyCollection<Asset> assetsSettings = await GetAssetsAsync();

            return assetsSettings.SingleOrDefault(o => o.Name == name && o.Exchange == exchange);
        }

        public async Task AddAssetAsync(Asset asset, string userId)
        {
            Asset existingAsset = await GetAssetAsync(asset.Name, asset.Exchange);

            if (existingAsset != null)
                throw new EntityAlreadyExistsException();

            await _assetRepository.InsertAsync(asset);

            _assetsCache.Set(asset);

            _log.InfoWithDetails("Asset added", new {asset, userId});
        }

        public async Task UpdateAssetAsync(Asset asset, string userId)
        {
            Asset existingAsset = await GetAssetAsync(asset.Name, asset.Exchange);

            if (existingAsset == null)
                throw new EntityNotFoundException();

            await _assetRepository.UpdateAsync(asset);

            _assetsCache.Set(asset);

            _log.InfoWithDetails("Asset updated", new {asset, userId});
        }

        public async Task DeleteAssetAsync(string name, string exchange, string userId)
        {
            Asset existingAsset = await GetAssetAsync(name, exchange);

            if (existingAsset == null)
                throw new EntityNotFoundException();

            IReadOnlyCollection<AssetPair> assetPairsSettings = await GetAssetPairsAsync();

            if (assetPairsSettings.Any(o => o.Exchange == exchange && (o.BaseAsset == name || o.QuoteAsset == name)))
                throw new FailedOperationException("Asset is used by asset pair.");

            await _assetRepository.DeleteAsync(name, exchange);

            _assetsCache.Remove(GetKey(name, exchange));

            _log.InfoWithDetails("Asset deleted", new {existingAsset, userId});
        }

        public async Task<IReadOnlyList<AssetPair>> GetAssetPairsAsync()
        {
            IReadOnlyList<AssetPair> assetPairs = _assetPairsCache.GetAll();

            if (assetPairs == null)
            {
                assetPairs = await _assetPairRepository.GetAllAsync();

                _assetPairsCache.Initialize(assetPairs);
            }

            return assetPairs;
        }

        public async Task<AssetPair> GetAssetPairAsync(string name, string exchange)
        {
            IReadOnlyCollection<AssetPair> assetPairsSettings = await GetAssetPairsAsync();

            return assetPairsSettings.SingleOrDefault(o => o.Name == name && o.Exchange == exchange);
        }

        public async Task AddAssetPairAsync(AssetPair assetPair, string userId)
        {
            AssetPair existingAssetPair = await GetAssetPairAsync(assetPair.Name, assetPair.Exchange);

            if (existingAssetPair != null)
                throw new EntityAlreadyExistsException();

            await ValidateAssetPairAsync(assetPair);

            await _assetPairRepository.InsertAsync(assetPair);

            _assetPairsCache.Set(assetPair);

            _log.InfoWithDetails("Asset pair added", new {assetPair, userId});
        }

        public async Task UpdateAssetPairAsync(AssetPair assetPair, string userId)
        {
            AssetPair existingAssetPair = await GetAssetPairAsync(assetPair.Name, assetPair.Exchange);

            if (existingAssetPair == null)
                throw new EntityNotFoundException();

            await ValidateAssetPairAsync(assetPair);

            await _assetPairRepository.UpdateAsync(assetPair);

            _assetPairsCache.Set(assetPair);

            _log.InfoWithDetails("Asset pair updated", new {assetPair, userId});
        }

        public async Task DeleteAssetPairAsync(string name, string exchange, string userId)
        {
            AssetPair existingAssetPair = await GetAssetPairAsync(name, exchange);

            if (existingAssetPair == null)
                throw new EntityNotFoundException();

            await _assetPairRepository.DeleteAsync(name, exchange);

            _assetPairsCache.Remove(GetKey(name, exchange));

            _log.InfoWithDetails("Asset pair deleted", new {existingAssetPair, userId});
        }

        private async Task ValidateAssetPairAsync(AssetPair assetPair)
        {
            Asset baseAsset = await GetAssetAsync(assetPair.BaseAsset, assetPair.Exchange);

            if (baseAsset == null)
                throw new FailedOperationException("Base asset not found");

            Asset quoteAsset = await GetAssetAsync(assetPair.QuoteAsset, assetPair.Exchange);

            if (quoteAsset == null)
                throw new FailedOperationException("Quote asset not found");
        }

        private static string GetKey(Asset asset)
            => GetKey(asset.Name, asset.Exchange);

        private static string GetKey(AssetPair assetPair)
            => GetKey(assetPair.Name, assetPair.Exchange);

        private static string GetKey(string instrument, string exchange)
            => $"{instrument}_{exchange}";
    }
}
