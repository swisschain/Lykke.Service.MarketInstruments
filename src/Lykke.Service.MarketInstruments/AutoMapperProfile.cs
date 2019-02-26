using AutoMapper;
using JetBrains.Annotations;
using Lykke.Service.MarketInstruments.Client.Models.AssetPairs;
using Lykke.Service.MarketInstruments.Client.Models.Assets;
using Lykke.Service.MarketInstruments.Domain;

namespace Lykke.Service.MarketInstruments
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Asset, AssetModel>(MemberList.Source);
            CreateMap<AssetModel, Asset>(MemberList.Destination);

            CreateMap<AssetPair, AssetPairModel>(MemberList.Source);
            CreateMap<AssetPairModel, AssetPair>(MemberList.Destination);
        }
    }
}
