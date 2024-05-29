using SIMA.Domain.Models.Features.RiskManagement.Threats.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskPossibillities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.RiskManagement.Threats.Entities
{
    public class Threat : Entity
    {
        private Threat()
        {

        }
        private Threat(CreateThreatArg arg)
        {
            Id = new ThreatId(IdHelper.GenerateUniqueId());
            Code = arg.Code;
            RiskId = new RiskId(arg.RiskId);
            RiskPossibilityId = new RiskPossibilityId(arg.RiskPossibilityId);
            ThreatTypeId = new ThreatTypeId(arg.ThreatTypeId);
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<Threat> Create(CreateThreatArg arg)
        {
            return new Threat(arg);
        }
        public async Task Modify(ModifyThreatArg arg)
        {
            Code = arg.Code;
            RiskId = new RiskId(arg.RiskId);
            RiskPossibilityId = new RiskPossibilityId(arg.RiskPossibilityId);
            ThreatTypeId = new ThreatTypeId(arg.ThreatTypeId);
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }
        public void Delete()
        {
            ActiveStatusId = (int)ActiveStatusEnum.Delete;
        }
        public ThreatId Id { get; set; }
        public string Code { get; private set; }
        public RiskId RiskId { get; private set; }
        public virtual Risk Risk { get; private set; }
        public RiskPossibilityId RiskPossibilityId { get; private set; }
        public virtual RiskPossibility RiskPossibility { get; private set; }
        public ThreatTypeId ThreatTypeId { get; private set; }
        public virtual ThreatType ThreatType { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
