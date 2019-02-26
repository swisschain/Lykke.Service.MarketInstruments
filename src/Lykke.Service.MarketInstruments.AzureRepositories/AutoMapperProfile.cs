using AutoMapper;
using JetBrains.Annotations;
using Lykke.Service.MarketInstruments.AzureRepositories.AssetPairs;
using Lykke.Service.MarketInstruments.AzureRepositories.Assets;
using Lykke.Service.MarketInstruments.Domain;

namespace Lykke.Service.MarketInstruments.AzureRepositories
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Asset, AssetEntity>(MemberList.Source);
            CreateMap<AssetEntity, Asset>(MemberList.Destination);

            CreateMap<AssetPair, AssetPairEntity>(MemberList.Source);
            CreateMap<AssetPairEntity, AssetPair>(MemberList.Destination);
        }
    }
}
