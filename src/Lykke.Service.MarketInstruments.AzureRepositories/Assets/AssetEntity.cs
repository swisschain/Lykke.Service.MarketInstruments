using JetBrains.Annotations;
using Lykke.AzureStorage.Tables;
using Lykke.AzureStorage.Tables.Entity.Annotation;
using Lykke.AzureStorage.Tables.Entity.ValueTypesMerging;
using Lykke.Service.MarketInstruments.Domain;

namespace Lykke.Service.MarketInstruments.AzureRepositories.Assets
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [ValueTypeMergingStrategy(ValueTypeMergingStrategy.UpdateIfDirty)]
    public class AssetEntity : AzureTableEntity
    {
        private int _accuracy;
        private AssetType _type;

        public AssetEntity()
        {
        }

        public AssetEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Exchange { get; set; }

        public int Accuracy
        {
            get => _accuracy;
            set
            {
                if (_accuracy != value)
                {
                    _accuracy = value;
                    MarkValueTypePropertyAsDirty();
                }
            }
        }

        public AssetType Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    MarkValueTypePropertyAsDirty();
                }
            }
        }
    }
}
