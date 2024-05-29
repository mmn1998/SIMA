using SIMA.Domain.Models.Features.RiskManagement.RiskPossibillities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Entities;

namespace SIMA.Domain.Models.Features.RiskManagement.Threats.Args
{
    public class CreateThreatArg
    {
        public string Code { get;  set; }
        public long RiskId { get;  set; }
        public long RiskPossibilityId { get;  set; }
        public long ThreatTypeId { get;  set; }
        public long ActiveStatusId { get;  set; }
        public DateTime? CreatedAt { get;  set; }
        public long? CreatedBy { get;  set; }
    }
}
