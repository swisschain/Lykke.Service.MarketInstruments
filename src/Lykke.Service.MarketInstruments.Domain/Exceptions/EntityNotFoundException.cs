using System;

namespace Lykke.Service.MarketInstruments.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
            : base("Entity not found")
        {
        }
    }
}
