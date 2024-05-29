using SIMA.Domain.Models.Features.RiskManagement.Risks.Args;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Events;
using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Threats.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Entities
{
    public class Risk : Entity
    {
        private Risk()
        {
            
        }
        private Risk(CreateRiskArg arg)
        {
            Id = new RiskId(IdHelper.GenerateUniqueId());
            Code = arg.Code;
            Name = arg.Name;
            Description = arg.Description;
            RiskTypeId = new RiskTypeId(arg.RiskTypeId);
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static async Task<Risk> Create(CreateRiskArg arg)
        {
            return new Risk(arg);
        }
        public async Task Modify(ModifyRiskArg arg)
        {
            Code = arg.Code;
            Name = arg.Name;
            Description = arg.Description;
            RiskTypeId = new RiskTypeId(arg.RiskTypeId);
            ActiveStatusId = arg.ActiveStatusId;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
        }

        public async Task AddEffectAsset(CreateEffectedAssetArgs arg) 
        {
            var effectAsset = await EffectedAsset.Create(arg);
            _effectedAssets.Add(effectAsset);   
        }

        public async Task AddCorrectiveAction(List<CreateCorrectiveActionArg> args)
        {
            var corrective = args.Select(CorrectiveAction.Create);
            _correctiveActions.AddRange(corrective);
        }

        public async Task AddPreventiveAction(List<CreatePreventiveActionArg> args)
        {
            var preventives = args.Select(PreventiveAction.Create);
            _preventiveActions.AddRange(preventives);
        }

        public async Task AddRiskRelatedIssue(CreateRiskRelatedIssueArg arg)
        {
            var riskRelated = RiskRelatedIssue.Create(arg);
            _riskRelatedIssues.Add(riskRelated);

            AddDomainEvent(new RiskCreateEvents(arg.IssueId, MainAggregateEnums.CatalogService, Name, Id.Value));
        }

        public void Delete()
        {
            ActiveStatusId = (int)ActiveStatusEnum.Delete;
        }
        public RiskId Id { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }
        public RiskTypeId RiskTypeId { get; private set; }
        public virtual RiskType RiskType { get; private set; }
      
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

        private List<CorrectiveAction> _correctiveActions = new();
        public ICollection<CorrectiveAction> CorrectiveActions => _correctiveActions;

        private List<PreventiveAction> _preventiveActions = new();
        public ICollection<PreventiveAction> PreventiveActions => _preventiveActions;

        private List<EffectedAsset> _effectedAssets = new();
        public ICollection<EffectedAsset> EffectedAssets => _effectedAssets;

        private List<RiskRelatedIssue> _riskRelatedIssues = new();
        public ICollection<RiskRelatedIssue> RiskRelatedIssues => _riskRelatedIssues;

        private List<Threat> _threats = new();
        public ICollection<Threat> Threats => _threats;

    }
}
