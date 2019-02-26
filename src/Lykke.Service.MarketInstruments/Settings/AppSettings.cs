using JetBrains.Annotations;
using Lykke.Sdk.Settings;
using Lykke.Service.MarketInstruments.Settings.ServiceSettings;

namespace Lykke.Service.MarketInstruments.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public MarketInstrumentsSettings MarketInstrumentsService { get; set; }
    }
}
