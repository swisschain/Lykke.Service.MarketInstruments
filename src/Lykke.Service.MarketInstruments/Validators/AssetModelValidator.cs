using FluentValidation;
using JetBrains.Annotations;
using Lykke.Service.MarketInstruments.Client.Models.Assets;

namespace Lykke.Service.MarketInstruments.Validators
{
    [UsedImplicitly]
    public class AssetModelValidator : AbstractValidator<AssetModel>
    {
        public AssetModelValidator()
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

            RuleFor(o => o.Accuracy)
                .InclusiveBetween(1, 8)
                .WithMessage("Accuracy should be between 1 and 8");

            RuleFor(o => o.Type)
                .NotEqual(AssetType.None)
                .WithMessage("Type required");
        }
    }
}
