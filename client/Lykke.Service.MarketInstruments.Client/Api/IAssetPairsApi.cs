using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.MarketInstruments.Client.Models.AssetPairs;
using Refit;

namespace Lykke.Service.MarketInstruments.Client.Api
{
    /// <summary>
    /// Provides methods for working with asset pairs.
    /// </summary>
    [PublicAPI]
    public interface IAssetPairsApi
    {
        /// <summary>
        /// Returns all asset pairs.
        /// </summary>
        /// <returns>A collection of asset pairs.</returns>
        [Get("/api/AssetPairs")]
        Task<IReadOnlyList<AssetPairModel>> GetAsync();

        /// <summary>
        /// Adds new asset pair.
        /// </summary>
        /// <param name="model">The model that describes asset pair.</param>
        /// <param name="userId">The identifier of user witch performed operation.</param>
        [Post("/api/AssetPairs")]
        Task AddAsync([Body] AssetPairModel model, string userId);

        /// <summary>
        /// Updates existing asset pair.
        /// </summary>
        /// <param name="model">The model that describes asset pair.</param>
        /// <param name="userId">The identifier of user witch performed operation.</param>
        [Put("/api/AssetPairs")]
        Task UpdateAsync([Body] AssetPairModel model, string userId);

        /// <summary>
        /// Deletes asset pair.
        /// </summary>
        /// <param name="name">The name of the asset pair.</param>
        /// <param name="exchange">The name of exchange.</param>
        /// <param name="userId">The identifier of user witch performed operation.</param>
        [Delete("/api/AssetPairs")]
        Task DeleteAsync(string name, string exchange, string userId);
    }
}
