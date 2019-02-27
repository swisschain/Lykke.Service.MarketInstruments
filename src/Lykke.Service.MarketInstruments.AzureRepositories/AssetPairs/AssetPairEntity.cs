using JetBrains.Annotations;
using Lykke.AzureStorage.Tables;
using Lykke.AzureStorage.Tables.Entity.Annotation;
using Lykke.AzureStorage.Tables.Entity.ValueTypesMerging;

namespace Lykke.Service.MarketInstruments.AzureRepositories.AssetPairs
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ValueTypeMergingStrategy(ValueTypeMergingStrategy.UpdateIfDirty)]
    public class AssetPairEntity : AzureTableEntity
    {
        private int _priceAccuracy;
        private int _volumeAccuracy;
        private decimal _minVolume;

        public AssetPairEntity()
        {
        }

        public AssetPairEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Exchange { get; set; }

        public string BaseAsset { get; set; }

        public string QuoteAsset { get; set; }

        public int PriceAccuracy
        {
            get => _priceAccuracy;
            set
            {
                if (_priceAccuracy != value)
                {
                    _priceAccuracy = value;
                    MarkValueTypePropertyAsDirty();
                }
            }
        }

        public int VolumeAccuracy
        {
            get => _volumeAccuracy;
            set
            {
                if (_volumeAccuracy != value)
                {
                    _volumeAccuracy = value;
                    MarkValueTypePropertyAsDirty();
                }
            }
        }

        public decimal MinVolume
        {
            get => _minVolume;
            set
            {
                if (_minVolume != value)
                {
                    _minVolume = value;
                    MarkValueTypePropertyAsDirty();
                }
            }
        }
    }
}
