using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.MarketInstruments.Client
{
    /// <summary>
    /// Market instruments service client settings.
    /// </summary>
    [PublicAPI]
    public class MarketInstrumentsServiceClientSettings
    {
        /// <summary>
        /// Service url.
        /// </summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl { get; set; }
    }
}
