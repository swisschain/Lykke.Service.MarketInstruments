using FluentValidation;
using JetBrains.Annotations;
using Lykke.Service.MarketInstruments.Client.Models.AssetPairs;

namespace Lykke.Service.MarketInstruments.Validators
{
    [UsedImplicitly]
    public class AssetPairModelValidator : AbstractValidator<AssetPairModel>
    {
        public AssetPairModelValidator()
        {
            RuleFor(o => o.Id)
                .NotEmpty()
                .WithMessage("Id required");

            RuleFor(o => o.Name)
                .NotEmpty()
                .WithMessage("Name required");

            RuleFor(o => o.Exchange)
                .NotEmpty()
                .WithMessage("Exchange required");

            RuleFor(o => o.BaseAsset)
                .NotEmpty()
                .WithMessage("Base asset required");

            RuleFor(o => o.QuoteAsset)
                .NotEmpty()
                .WithMessage("Quote asset required");

            RuleFor(o => o.PriceAccuracy)
                .InclusiveBetween(1, 8)
                .WithMessage("Price accuracy should be between 1 and 8");

            RuleFor(o => o.VolumeAccuracy)
                .InclusiveBetween(1, 8)
                .WithMessage("Volume accuracy should be between 1 and 8");

            RuleFor(o => o.MinVolume)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Min volume should be greater than or equal to zero");
        }
    }
}
