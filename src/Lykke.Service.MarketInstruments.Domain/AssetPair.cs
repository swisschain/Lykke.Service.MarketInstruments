namespace Lykke.Service.MarketInstruments.Domain
{
    /// <summary>
    /// Represents an asset pair on the exchange.
    /// </summary>
    public class AssetPair
    {
        /// <summary>
        /// The identifier of the asset pair on the exchange.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The common name of asset pair.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The name of exchange.
        /// </summary>
        public string Exchange { get; set; }

        /// <summary>
        /// The name of base asset.
        /// </summary>
        public string BaseAsset { get; set; }

        /// <summary>
        /// The name of quote asset.
        /// </summary>
        public string QuoteAsset { get; set; }

        /// <summary>
        /// The price accuracy.
        /// </summary>
        public int PriceAccuracy { get; set; }

        /// <summary>
        /// The volume accuracy.
        /// </summary>
        public int VolumeAccuracy { get; set; }

        /// <summary>
        /// The minimal volume.
        /// </summary>
        public decimal MinVolume { get; set; }
    }
}
