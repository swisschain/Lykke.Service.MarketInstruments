using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Lykke.Service.MarketInstruments.Client.Models.AssetPairs;
using Lykke.Service.MarketInstruments.Client.Models.Assets;
using Microsoft.Extensions.Caching.Memory;

namespace Lykke.Service.MarketInstruments.Client.Services
{
    internal class MarketInstrumentService : IMarketInstrumentService, IStartable
    {
        private const string AllAssetsKey = "AssetsKeys";
        private const string AllAssetPairsKey = "AssetPairsKeys";

        private readonly IMarketInstrumentsClient _marketInstrumentsClient;
        private readonly IMemoryCache _cache;

        public MarketInstrumentService(IMarketInstrumentsClient marketInstrumentsClient, IMemoryCache cache)
        {
            _marketInstrumentsClient = marketInstrumentsClient;
            _cache = cache;
        }

        public AssetModel GetAsset(string name, string exchange)
        {
            try
            {
                _cache.TryGetValue(GetAssetKey(name, exchange), out AssetModel value);
                return value;
            }
            catch (InvalidCastException)
            {
                return null;
            }
        }

        public IReadOnlyList<AssetModel> GetAssets()
        {
            var ids = _cache.Get<ConcurrentBag<(string, string)>>(AllAssetsKey);
            return ids.Select(tuple => _cache.Get<AssetModel>(GetAssetKey(tuple.Item1, tuple.Item2))).ToList();
        }

        public AssetPairModel GetAssetPair(string name, string exchange)
        {
            try
            {
                _cache.TryGetValue(GetAssetPairKey(name, exchange), out AssetPairModel value);
                return value;
            }
            catch (InvalidCastException)
            {
                return null;
            }
        }

        public IReadOnlyList<AssetPairModel> GetAssetPairs()
        {
            var ids = _cache.Get<ConcurrentBag<(string, string)>>(AllAssetPairsKey);
            return ids.Select(tuple => _cache.Get<AssetPairModel>(GetAssetPairKey(tuple.Item1, tuple.Item2))).ToList();
        }

        public void Start()
        {
            IReadOnlyList<AssetModel> assets = _marketInstrumentsClient.Assets.GetAsync()
                .GetAwaiter()
                .GetResult();

            IReadOnlyList<AssetPairModel> assetPairs = _marketInstrumentsClient.AssetPairs.GetAsync()
                .GetAwaiter()
                .GetResult();

            foreach (AssetModel asset in assets)
                _cache.Set(GetAssetKey(asset.Name, asset.Exchange), asset);

            _cache.Set(AllAssetsKey,
                new ConcurrentBag<(string, string)>(assets.Select(asset => (asset.Name, asset.Exchange))));

            foreach (AssetPairModel assetPair in assetPairs)
                _cache.Set(GetAssetPairKey(assetPair.Name, assetPair.Exchange), assetPair);

            _cache.Set(AllAssetPairsKey,
                new ConcurrentBag<(string, string)>(
                    assetPairs.Select(assetPair => (assetPair.Name, assetPair.Exchange))));
        }

        private static string GetKey(AssetModel asset)
            => GetAssetKey(asset.Name, asset.Exchange);

        private static string GetKey(AssetPairModel assetPair)
            => GetAssetPairKey(assetPair.Name, assetPair.Exchange);

        private static string GetAssetKey(string name, string exchange)
            => $"Assets:{exchange}:{name}";

        private static string GetAssetPairKey(string name, string exchange)
            => $"AssetPairs:{exchange}:{name}";
    }
}
