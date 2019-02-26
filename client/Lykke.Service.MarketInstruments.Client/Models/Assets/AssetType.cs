using JetBrains.Annotations;

namespace Lykke.Service.MarketInstruments.Client.Models.Assets
{
    /// <summary>
    /// Specifies the type of asset.
    /// </summary>
    [PublicAPI]
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
