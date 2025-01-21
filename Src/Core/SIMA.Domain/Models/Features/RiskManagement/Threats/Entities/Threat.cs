using SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Threats.Args;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.Threats.Entities
{
    public class Threat : Entity
    {
        private Threat()
        {

        }
        private Threat(CreateThreatArg arg)
        {
            Id = new ThreatId(arg.Id);
            Description = arg.Description;
            RiskId = new(arg.RiskId);
            RiskPossibilityId = new(arg.RiskPossibilityId);
            ThreatTypeId = new(arg.ThreatTypeId);
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static Threat Create(CreateThreatArg arg)
        {
            return new Threat(arg);
        }
        public void Delete(long userId)
        {
            ModifiedBy = userId;
            ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            ActiveStatusId = (long)ActiveStatusEnum.Delete;
        }
        public void Active(long userId)
        {
            ModifiedBy = userId;
            ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            ActiveStatusId = (long)ActiveStatusEnum.Active;
        }
        public ThreatId Id { get; set; }
        public string Description { get; private set; }
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
