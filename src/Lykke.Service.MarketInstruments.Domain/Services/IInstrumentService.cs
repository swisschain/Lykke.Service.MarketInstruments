using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.MarketInstruments.Domain.Services
{
    public interface IInstrumentService
    {
        Task<IReadOnlyList<Asset>> GetAssetsAsync();

        Task<Asset> GetAssetAsync(string name, string exchange);

        Task AddAssetAsync(Asset asset, string userId);

        Task UpdateAssetAsync(Asset asset, string userId);

        Task DeleteAssetAsync(string name, string exchange, string userId);

        Task<IReadOnlyList<AssetPair>> GetAssetPairsAsync();

        Task<AssetPair> GetAssetPairAsync(string name, string exchange);

        Task AddAssetPairAsync(AssetPair assetPair, string userId);

        Task UpdateAssetPairAsync(AssetPair assetPair, string userId);

        Task DeleteAssetPairAsync(string name, string exchange, string userId);
    }
}
