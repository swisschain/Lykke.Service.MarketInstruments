using JetBrains.Annotations;
using Lykke.Service.MarketInstruments.Settings.ServiceSettings.Db;

namespace Lykke.Service.MarketInstruments.Settings.ServiceSettings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class MarketInstrumentsSettings
    {
        public DbSettings Db { get; set; }
    }
}
