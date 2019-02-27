namespace Lykke.Service.MarketInstruments.Domain
{
    /// <summary>
    /// Represents an asset setting on the exchange.
    /// </summary>
    public class Asset
    {
        /// <summary>
        /// The identifier of the asset on the exchange.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The common name of asset.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The name of exchange.
        /// </summary>
        public string Exchange { get; set; }

        /// <summary>
        /// The accuracy of the asset.
        /// </summary>
        public int Accuracy { get; set; }

        /// <summary>
        /// Indicates the type of asset.
        /// </summary>
        public AssetType Type { get; set; }
    }
}
