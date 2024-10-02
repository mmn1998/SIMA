using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.ServiceCatalog.Services;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.ServiceCatalog.Services;

public class ServiceCommandHandler : ICommandHandler<CreateServiceCommand, Result<long>>,
    ICommandHandler<ModifyServiceCommand, Result<long>>, ICommandHandler<DeleteServiceCommand, Result<long>>
{
    private readonly IServiceRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceDomainService _service;
    private readonly IMapper _mapper;
    private readonly ISimaIdentity _simaIdentity;

    public ServiceCommandHandler(IServiceRepository repository, IUnitOfWork unitOfWork
        , IServiceDomainService service, IMapper mapper, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _service = service;
        _mapper = mapper;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateServiceArg>(request);
        var entity = await Service.Create(arg, _service);
        if (request.CustomerTypeList is not null)
        {
            var args = _mapper.Map<List<CreateServiceCustomerArg>>(request.CustomerTypeList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.AddServiceCustomers(args);
        }
        if (request.UserTypeList is not null)
        {
            var args = _mapper.Map<List<CreateServiceUserArg>>(request.UserTypeList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.AddServiceUsers(args);
        }
        if (request.ChannelList is not null)
        {
            var args = _mapper.Map<List<CreateServiceChannelArg>>(request.ChannelList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.AddServiceChannels(args);
        }
        if (request.PrerequisiteServiceList is not null)
        {
            var args = _mapper.Map<List<CreatePreRequisiteServicesArg>>(request.PrerequisiteServiceList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.AddServicePrerequisite(args);
        }
        if (request.ProviderList is not null)
        {
            var args = _mapper.Map<List<CreateServiceProviderArg>>(request.ProviderList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.AddServiceProviders(args);
        }
        if (request.RiskList is not null)
        {
            var args = _mapper.Map<List<CreateServiceRiskArg>>(request.RiskList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.AddServiceRisks(args);
        }
        if (request.AssetList is not null)
        {
            var args = _mapper.Map<List<CreateServiceAssetArg>>(request.AssetList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.AddServiceAssets(args);
        }
        if (request.ConfigurationItemList is not null)
        {
            var args = _mapper.Map<List<CreateServiceConfigurationItemArg>>(request.ConfigurationItemList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.AddServiceConfigurationItems(args);
        }
        if (request.ServiceAssignedStaffList is not null)
        {
            var args = _mapper.Map<List<CreateServiceAssignedStaffArg>>(request.ServiceAssignedStaffList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.AddServiceAssignedStaffs(args);
        }
        if (request.ServiceAvalibilityList is not null)
        {
            var args = _mapper.Map<List<CreateServiceAvalibilityArg>>(request.ServiceAvalibilityList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.AddServiceAvalibilities(args);
        }
        #region ServiceIssues
        var serviceIssueArg = _mapper.Map<CreateServiceRelatedIssueArg>(arg);
        entity.AddServiceIssues(new List<CreateServiceRelatedIssueArg>
        {
            serviceIssueArg
        });
        #endregion
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(ModifyServiceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyServiceArg>(request);
        await entity.Modify(arg, _service);
        if (request.CustomerTypeList is not null)
        {
            var args = _mapper.Map<List<CreateServiceCustomerArg>>(request.CustomerTypeList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.ModifyServiceCustomers(args);
        }
        if (request.UserTypeList is not null)
        {
            var args = _mapper.Map<List<CreateServiceUserArg>>(request.UserTypeList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.ModifyServiceUsers(args);
        }
        if (request.ChannelList is not null)
        {
            var args = _mapper.Map<List<CreateServiceChannelArg>>(request.ChannelList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.ModifyServiceChannels(args);
        }
        if (request.PrerequisiteServiceList is not null)
        {
            var args = _mapper.Map<List<CreatePreRequisiteServicesArg>>(request.PrerequisiteServiceList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.ModifyServicePrerequisite(args);
        }
        if (request.ProviderList is not null)
        {
            var args = _mapper.Map<List<CreateServiceProviderArg>>(request.ProviderList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.ModifyServiceProviders(args);
        }
        if (request.RiskList is not null)
        {
            var args = _mapper.Map<List<CreateServiceRiskArg>>(request.RiskList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.ModifyServiceRisks(args);
        }
        if (request.AssetList is not null)
        {
            var args = _mapper.Map<List<CreateServiceAssetArg>>(request.AssetList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.ModifyServiceAssets(args);
        }
        if (request.ConfigurationItemList is not null)
        {
            var args = _mapper.Map<List<CreateServiceConfigurationItemArg>>(request.ConfigurationItemList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.ModifyServiceConfigurationItems(args);
        }
        if (request.ServiceAssignedStaffList is not null)
        {
            var args = _mapper.Map<List<CreateServiceAssignedStaffArg>>(request.ServiceAssignedStaffList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.ModifyServiceAssignedStaffs(args);
        }
        if (request.ServiceAvalibilityList is not null)
        {
            var args = _mapper.Map<List<CreateServiceAvalibilityArg>>(request.ServiceAvalibilityList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.ServiceId = arg.Id;
            }
            entity.ModifyServiceAvalibilities(args);
        }
        var serviceIssueArg = _mapper.Map<CreateServiceRelatedIssueArg>(arg);
        #region ServiceIssues
        entity.ModifyServiceIssues(new List<CreateServiceRelatedIssueArg>
        {
            serviceIssueArg
        });
        #endregion
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        entity.Delete(userId: _simaIdentity.UserId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

}
