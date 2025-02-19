using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.Risks;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Args;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Interfaces;
using SIMA.Domain.Models.Features.RiskManagement.ServiceRiskImpacts.Args;
using SIMA.Domain.Models.Features.RiskManagement.Threats.Args;
using SIMA.Domain.Models.Features.RiskManagement.Vulnerabilities.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.RiskManagers.Risks;

public class RiskCommandHandler : ICommandHandler<CreateRiskCommand, Result<long>>,
    ICommandHandler<ModifyRiskCommand, Result<long>>, ICommandHandler<DeleteRiskCommand, Result<long>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IRiskRepository _repository;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IRiskDomainService _domainService;

    public RiskCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IRiskRepository repository,
        ISimaIdentity simaIdentity, IRiskDomainService domainService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = repository;
        _simaIdentity = simaIdentity;
        _domainService = domainService;
    }
    public async Task<Result<long>> Handle(CreateRiskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var arg = _mapper.Map<CreateRiskArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var lastRequest = await _repository.GetLast();

            if (lastRequest is not null)
                arg.Code = (Convert.ToInt32(lastRequest.Code) + 1).ToString();
            else
                arg.Code = "101";
            var entity = await Risk.Create(arg, _domainService);
            var userId = _simaIdentity.UserId;

            if (request.PreventiveActionList is not null)
            {
                var args = _mapper.Map<List<CreatePreventiveActionArg>>(request.PreventiveActionList);
                foreach (var item in args)
                {
                    item.RiskId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddPreventiveActions(args);
            }
            if (request.StaffList is not null)
            {
                var args = _mapper.Map<List<CreateRiskStaffArg>>(request.StaffList);
                foreach (var item in args)
                {
                    item.RiskId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddStaffs(args);
            }
            if (request.CorrectiveActionList is not null)
            {
                var args = _mapper.Map<List<CreateCorrectiveActionArg>>(request.CorrectiveActionList);
                foreach (var item in args)
                {
                    item.RiskId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddCorrectiveActions(args);
            }
            if (request.EffectedAssetList is not null)
            {
                foreach (var item in request.EffectedAssetList)
                {
                    var effectedAssetArg = _mapper.Map<CreateEffectedAssetArgs>(item);
                    effectedAssetArg.CreatedBy = userId;
                    effectedAssetArg.RiskId = arg.Id;
                    var vulnerabilityArgs = _mapper.Map<List<CreateVulnerabilityArg>>(item.VulnerabilityList);
                    entity.AddEffectAsset(effectedAssetArg, vulnerabilityArgs);
                }
            }
            if (request.ServiceRiskImpactList is not null)
            {
                foreach (var item in request.ServiceRiskImpactList)
                {
                    var serviceRiskArg = _mapper.Map<CreateServiceRiskArg>(item);
                    serviceRiskArg.CreatedBy = userId;
                    serviceRiskArg.RiskId = arg.Id;
                    var serviceImpacts = _mapper.Map<List<CreateServiceRiskImpactArg>>(item.RiskImpactList);
                    entity.AddServiceRiskImpact(serviceRiskArg, serviceImpacts);
                }
            }
            if (request.ThreatList is not null)
            {
                var args = _mapper.Map<List<CreateThreatArg>>(request.ThreatList);
                foreach (var item in args)
                {
                    item.RiskId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddThreats(args);
            }
            
            if (request.ServiceAssignedStavesList is not null)
            {
                var args = _mapper.Map<List<CreateRiskStaffArg>>(request.ServiceAssignedStavesList);
                foreach (var item in args)
                {
                    item.StaffId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddStaffs(args);
            }
            ////Add issue

            var relatedIssueArg = _mapper.Map<CreateRiskRelatedIssueArg>(request);
            relatedIssueArg.CreatedBy = _simaIdentity.UserId;
            entity.AddRiskRelatedIssue(relatedIssueArg);

            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok(entity.Id.Value);
        }
        catch (Exception ex)
        {

            throw;
        }
       
    }

    public async Task<Result<long>> Handle(ModifyRiskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _repository.GetById(new(request.Id));


            var arg = _mapper.Map<ModifyRiskArg>(request);

            await entity.Modify(arg, _domainService);
            var userId = _simaIdentity.UserId;
            if (request.PreventiveActionList is not null)
            {
                var args = _mapper.Map<List<CreatePreventiveActionArg>>(request.PreventiveActionList);
                foreach (var item in args)
                {
                    item.RiskId = request.Id;
                    item.CreatedBy = userId;
                }
                entity.ModifyPreventiveActions(args);
            }
            if (request.StaffList is not null)
            {
                var args = _mapper.Map<List<CreateRiskStaffArg>>(request.StaffList);
                foreach (var item in args)
                {
                    item.RiskId = request.Id;
                    item.CreatedBy = userId;
                }
                entity.ModifyStaffs(args);
            }
            if (request.CorrectiveActionList is not null)
            {
                var args = _mapper.Map<List<CreateCorrectiveActionArg>>(request.CorrectiveActionList);
                foreach (var item in args)
                {
                    item.RiskId = request.Id;
                    item.CreatedBy = userId;
                }
                entity.ModifyCorrectiveActions(args);
            }
            if (request.EffectedAssetList is not null)
            {
                var args = _mapper.Map<List<ModifyEffectedAssetArgs>>(request.EffectedAssetList);

                foreach (var item in args)
                {
                    item.CreatedBy = userId;

                    foreach (var item2 in item.VulnerabilityList)
                    {
                        item2.CreatedBy = userId;
                    }
                }
                entity.ModifyEffectAssets(args);
            }
            if (request.ServiceRiskImpactList is not null)
            {
                var args = _mapper.Map<List<ModifyServiceRiskArg>>(request.ServiceRiskImpactList);

                foreach (var item in args)
                {
                    item.CreatedBy = userId;
                }
                entity.ModifyServiceRiskImpacts(args);
            }
            if (request.ThreatList is not null)
            {
                var args = _mapper.Map<List<CreateThreatArg>>(request.ThreatList);
                foreach (var item in args)
                {
                    item.RiskId = request.Id;
                    item.CreatedBy = userId;
                }
                entity.ModifyThreats(args);
            }
            if (request.ServiceAssignedStavesList is not null)
            {
                var args = _mapper.Map<List<CreateRiskStaffArg>>(request.ServiceAssignedStavesList);
                foreach (var item in args)
                {
                    item.StaffId = request.Id;
                    item.CreatedBy = userId;
                }
                entity.ModifyStaffs(args);
            }

            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
        catch (Exception ex)
        {

            throw;
        }
       
    }

    public async Task<Result<long>> Handle(DeleteRiskCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        entity.Delete(request.Id);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
