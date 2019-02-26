using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.MarketInstruments.Client.Models.Assets;
using Refit;

namespace Lykke.Service.MarketInstruments.Client.Api
{
    /// <summary>
    /// Provides methods for working with assets.
    /// </summary>
    [PublicAPI]
    public interface IAssetsApi
    {
        /// <summary>
        /// Returns all assets.
        /// </summary>
        /// <returns>A collection of assets.</returns>
        [Get("/api/Assets")]
        Task<IReadOnlyList<AssetModel>> GetAsync();

        /// <summary>
        /// Adds new asset.
        /// </summary>
        /// <param name="model">The model that describes asset.</param>
        /// <param name="userId">The identifier of user witch performed operation.</param>
        [Post("/api/Assets")]
        Task AddAsync([Body] AssetModel model, string userId);

        /// <summary>
        /// Updates existing asset.
        /// </summary>
        /// <param name="model">The model that describes asset.</param>
        /// <param name="userId">The identifier of user witch performed operation.</param>
        [Put("/api/Assets")]
        Task UpdateAsync([Body] AssetModel model, string userId);

        /// <summary>
        /// Deletes asset.
        /// </summary>
        /// <param name="name">The name of the asset.</param>
        /// <param name="exchange">The name of exchange.</param>
        /// <param name="userId">The identifier of user witch performed operation.</param>
        [Delete("/api/Assets")]
        Task DeleteAsync(string name, string exchange, string userId);
    }
}
