using SIMA.Domain.Models.Features.RiskManagement.ImpactScales.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ServiceRiskImpacts.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.ServiceRiskImpacts.Entities
{
    public class ServiceRiskImpact : Entity
    {
        private ServiceRiskImpact()
        {

        }
        private ServiceRiskImpact(CreateServiceRiskImpactArg arg)
        {
            Id = new ServiceRiskImpactId(IdHelper.GenerateUniqueId());
            ImpactScaleId = new ImpactScaleId(arg.ImpactScaleId);
            RiskImpactId = new RiskImpactId(arg.RiskImpactId);
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<ServiceRiskImpact> Create(CreateServiceRiskImpactArg arg)
        {
            return new ServiceRiskImpact(arg);
        }
        public async Task Modify(ModifyServiceRiskImpactArg arg)
        {
            ImpactScaleId = new ImpactScaleId(arg.ImpactScaleId);
            RiskImpactId = new RiskImpactId(arg.RiskImpactId);
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }
        public void Delete(long userId)
        {
            ModifiedBy = userId;
            ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            ActiveStatusId = (long)ActiveStatusEnum.Delete;
        }

        public ServiceRiskImpactId Id { get; set; }
        //public ServiceRiskId ServiceRiskId { get; private set; }
        //public virtual ServiceRisk ServiceRisk { get; private set; }
        public ImpactScaleId ImpactScaleId { get; private set; }
        public virtual ImpactScale ImpactScale { get; private set; }
        public RiskImpactId RiskImpactId { get; private set; }
        public virtual RiskImpact RiskImpact { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
