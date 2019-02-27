using Lykke.HttpClientGenerator;
using Lykke.Service.MarketInstruments.Client.Api;

namespace Lykke.Service.MarketInstruments.Client
{
    /// <inheritdoc/>
    public class MarketInstrumentsClient : IMarketInstrumentsClient
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MarketInstrumentsClient"/> with <param name="httpClientGenerator"></param>.
        /// </summary> 
        public MarketInstrumentsClient(IHttpClientGenerator httpClientGenerator)
        {
            Assets = httpClientGenerator.Generate<IAssetsApi>();
            AssetPairs = httpClientGenerator.Generate<IAssetPairsApi>();
        }

        /// <inheritdoc/>
        public IAssetsApi Assets { get; }

        /// <inheritdoc/>
        public IAssetPairsApi AssetPairs { get; }
    }
}
