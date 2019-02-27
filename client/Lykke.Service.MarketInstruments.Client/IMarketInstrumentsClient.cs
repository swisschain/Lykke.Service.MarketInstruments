using JetBrains.Annotations;
using Lykke.Service.MarketInstruments.Client.Api;

namespace Lykke.Service.MarketInstruments.Client
{
    /// <summary>
    /// Market instruments service client interface.
    /// </summary>
    [PublicAPI]
    public interface IMarketInstrumentsClient
    {
        /// <summary>
        /// Assets API.
        /// </summary>
        IAssetsApi Assets { get; }

        /// <summary>
        /// Asset pairs API.
        /// </summary>
        IAssetPairsApi AssetPairs { get; }
    }
}
