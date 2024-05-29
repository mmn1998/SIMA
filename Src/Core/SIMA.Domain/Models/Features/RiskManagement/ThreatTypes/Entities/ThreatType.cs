using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Threats.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Entities
{
    public class ThreatType : Entity
    {
        private ThreatType()
        {

        }
        private ThreatType(CreateThreatTypeArg arg)
        {
            Id = new ThreatTypeId(IdHelper.GenerateUniqueId());
            Name = arg.Name;
            Code = arg.Code;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<ThreatType> Create(CreateThreatTypeArg arg)
        {
            return new ThreatType(arg);
        }
        public async Task Modify(ModifyThreatTypeArg arg)
        {
            Name = arg.Name;
            Code = arg.Code;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }
        public void Delete()
        {
            ActiveStatusId = (int)ActiveStatusEnum.Delete;
        }
        public ThreatTypeId Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

        private List<Threat> _threats = new();
        public ICollection<Threat> Threats => _threats;
    }
}
