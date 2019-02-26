using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lykke.Service.MarketInstruments.Domain.Repositories
{
    public interface IAssetRepository
    {
        Task<IReadOnlyList<Asset>> GetAllAsync();

        Task InsertAsync(Asset asset);

        Task UpdateAsync(Asset asset);

        Task DeleteAsync(string name, string exchange);
    }
}
