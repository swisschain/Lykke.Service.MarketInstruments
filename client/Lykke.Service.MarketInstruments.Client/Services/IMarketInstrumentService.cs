using System.Collections.Generic;
using JetBrains.Annotations;
using Lykke.Service.MarketInstruments.Client.Models.AssetPairs;
using Lykke.Service.MarketInstruments.Client.Models.Assets;

namespace Lykke.Service.MarketInstruments.Client.Services
{
    /// <summary>
    /// Provides methods to get read only collections of assets and asset pairs.
    /// </summary>
    [PublicAPI]
    public interface IMarketInstrumentService
    {
        /// <summary>
        /// Returns the asset by <paramref name="name"/> and <paramref name="exchange"/>.
        /// </summary>
        /// <param name="name">The name of the asset.</param>
        /// <param name="exchange">The name of the exchange</param>
        /// <returns>The asset if exists, otherwise <c>null</c>.</returns>
        AssetModel GetAsset(string name, string exchange);

        /// <summary>
        /// Returns a collection of all assets.
        /// </summary>
        /// <returns>A collection of all assets.</returns>
        IReadOnlyList<AssetModel> GetAssets();

        /// <summary>
        /// Returns the asset pair by <paramref name="name"/> and <paramref name="exchange"/>.
        /// </summary>
        /// <param name="name">The name of the asset pair.</param>
        /// <param name="exchange">The name of the exchange</param>
        /// <returns>The asset pair if exists, otherwise <c>null</c>.</returns>
        AssetPairModel GetAssetPair(string name, string exchange);

        /// <summary>
        /// Returns a collection of all asset pairs.
        /// </summary>
        /// <returns>A collection of all asset pairs.</returns>
        IReadOnlyList<AssetPairModel> GetAssetPairs();
    }
}
