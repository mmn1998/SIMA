using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.Risks;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Args;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.RiskManagers.Risks
{
    public class RiskCommandHandler : ICommandHandler<CreateRiskCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRiskRepository _riskRepository;
        private readonly ISimaIdentity _simaIdentity;

        public RiskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper , IRiskRepository riskRepository , ISimaIdentity simaIdentity)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _riskRepository = riskRepository;
            _simaIdentity = simaIdentity;
        }
        public async Task<Result<long>> Handle(CreateRiskCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateRiskArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await Risk.Create(arg);

            //Add EffectAsset
            var effectAssetArg =  _mapper.Map<CreateEffectedAssetArgs>(request);
            effectAssetArg.CreatedBy = _simaIdentity.UserId;
            await entity.AddEffectAsset(effectAssetArg);


            //Add CorrectiveAction
            var correctiveArg = _mapper.Map<List<CreateCorrectiveActionArg>>(request.CorrectiveActions);
            await entity.AddCorrectiveAction(correctiveArg);

            //Add PreventiveAction

            var preventiveArg = _mapper.Map<List<CreatePreventiveActionArg>>(request.PreventiveActions);
            await entity.AddPreventiveAction(preventiveArg);

            //Add issue

            var relatedIssueArg = _mapper.Map<CreateRiskRelatedIssueArg>(request);
            relatedIssueArg.CreatedBy = _simaIdentity.UserId;
            await entity.AddRiskRelatedIssue(relatedIssueArg);
                

            await _unitOfWork.SaveChangesAsync();

            return entity.Id.Value;





        }
    }
}
