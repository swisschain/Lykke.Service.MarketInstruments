namespace Lykke.Service.MarketInstruments.Domain
{
    /// <summary>
    /// Specifies the type of asset.
    /// </summary>
    public enum AssetType
    {
        /// <summary>
        /// Unspecified type.
        /// </summary>
        None,

        /// <summary>
        /// Indicates that the asset is FIAT currency.
        /// </summary>
        Fiat,

        /// <summary>
        /// Indicates that the asset is crypto currency.
        /// </summary>
        Crypto
    }
}
