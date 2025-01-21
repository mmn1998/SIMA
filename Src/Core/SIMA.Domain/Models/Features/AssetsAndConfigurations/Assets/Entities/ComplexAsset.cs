using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities
{
    public class ComplexAsset
    {
        private ComplexAsset() { }
       
        public ComplexAssetId Id { get; private set; }
        public AssetId AssetId { get; private set; }
        public virtual Asset Asset { get; private set; }
        public AssetId ParentAssetId { get; private set; }
        public virtual Asset ParentAsset { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
