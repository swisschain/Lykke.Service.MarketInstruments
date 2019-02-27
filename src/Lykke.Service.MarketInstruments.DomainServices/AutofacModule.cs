using Autofac;
using JetBrains.Annotations;
using Lykke.Service.MarketInstruments.Domain.Services;

namespace Lykke.Service.MarketInstruments.DomainServices
{
    [UsedImplicitly]
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InstrumentService>()
                .As<IInstrumentService>()
                .SingleInstance();
        }
    }
}
