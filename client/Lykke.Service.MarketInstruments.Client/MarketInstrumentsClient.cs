using Lykke.HttpClientGenerator;

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
        }
    }
}
