using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.MarketInstruments.Domain.Repositories
{
    public interface IAssetPairRepository
    {
        Task<IReadOnlyList<AssetPair>> GetAllAsync();

        Task InsertAsync(AssetPair assetPair);

        Task UpdateAsync(AssetPair assetPair);

        Task DeleteAsync(string name, string exchange);
    }
}
