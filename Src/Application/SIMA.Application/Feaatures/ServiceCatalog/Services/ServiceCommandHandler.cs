using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.ServiceCatalog.Services;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
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
        try
        {
            var arg = _mapper.Map<CreateServiceArg>(request);
            var userId = _simaIdentity.UserId;
            //var lastRequest = await _repository.GetLastService();
            arg.CreatedBy = userId;

            arg.Code = await CalculateCode();
            var entity = await Service.Create(arg, _service);
            #region isFullTimeSercice

            ///<summary>
            /// if IsFullTimeExecution is setted to 1 then 7 records will be added to database foreach day of week and its from 00:00 to 23:59
            ///</summary>
            if (string.Equals(request.isFullTimeSercice, "1", StringComparison.InvariantCultureIgnoreCase))
            {
                var list = GetRecordsForFullTimeExecution(userId: userId, serviceId: arg.Id);
                request.ServiceAvalibilityList = null;
                entity.AddServiceAvalibilities(list);
            }
            #endregion
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
            if (request.ServiceAvalibilityList is not null)
            {
                var args = new List<CreateServiceAvalibilityArg>();
                foreach (var item in request.ServiceAvalibilityList)
                {
                    for (int i = item.WeekDayStart; i < item.WeekDayEnd; i++)
                    {
                        var serviceEndTime = item.ServiceAvalibilityEndTime.ToTimeOnly() ?? throw SimaResultException.NullException;
                        var serviceStartTime = item.ServiceAvalibilityStartTime.ToTimeOnly() ?? throw SimaResultException.NullException;
                        var newArg = new CreateServiceAvalibilityArg
                        {
                            ActiveStatusId = (long)ActiveStatusEnum.Active,
                            CreatedAt = DateTime.Now,
                            CreatedBy = userId,
                            ServiceId = arg.Id,
                            Id = IdHelper.GenerateUniqueId(),
                            WeekDay = i,
                            ServiceAvalibilityEndTime = serviceEndTime,
                            ServiceAvalibilityStartTime = serviceStartTime
                        };
                        args.Add(newArg);
                    }
                }
                entity.AddServiceAvalibilities(args);
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
            //if (request.ServiceAvalibilityList is not null)
            //{
            //    var args = _mapper.Map<List<CreateServiceAvalibilityArg>>(request.ServiceAvalibilityList);
            //    foreach (var item in args)
            //    {
            //        item.CreatedBy = _simaIdentity.UserId;
            //        item.ServiceId = arg.Id;
            //    }
            //    entity.AddServiceAvalibilities(args);
            //}
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
        catch(Exception ex)
        {
            throw;
        }
        
    }
    public async Task<Result<long>> Handle(ModifyServiceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyServiceArg>(request);
        await entity.Modify(arg, _service);
        var userId = _simaIdentity.UserId;
        #region isFullTimeSercice
        ///<summary>
        /// if IsFullTimeExecution is setted to 1 then 7 records will be added to database foreach day of week and its from 00:00 to 23:59
        ///</summary>
        if (string.Equals(request.isFullTimeSercice, "1", StringComparison.InvariantCultureIgnoreCase))
        {
            var list = GetRecordsForFullTimeExecution(userId: userId, serviceId: arg.Id);
            request.ServiceAvalibilityList = null;
            entity.ModifyServiceAvalibilities(list);
        }
        #endregion
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
            var args = new List<CreateServiceAvalibilityArg>();
            foreach (var item in request.ServiceAvalibilityList)
            {
                for (int i = item.WeekDayStart; i < item.WeekDayEnd; i++)
                {
                    var serviceEndTime = item.ServiceAvalibilityEndTime.ToTimeOnly() ?? throw SimaResultException.NullException;
                    var serviceStartTime = item.ServiceAvalibilityStartTime.ToTimeOnly() ?? throw SimaResultException.NullException;
                    var newArg = new CreateServiceAvalibilityArg
                    {
                        ActiveStatusId = (long)ActiveStatusEnum.Active,
                        CreatedAt = DateTime.Now,
                        CreatedBy = userId,
                        ServiceId = arg.Id,
                        Id = IdHelper.GenerateUniqueId(),
                        WeekDay = i,
                        ServiceAvalibilityEndTime = serviceEndTime,
                        ServiceAvalibilityStartTime = serviceStartTime
                    };
                    args.Add(newArg);
                }
            }
            entity.ModifyServiceAvalibilities(args);
        }
        var serviceIssueArg = _mapper.Map<CreateServiceRelatedIssueArg>(arg);
        #region ServiceIssues
        //entity.ModifyServiceIssues(new List<CreateServiceRelatedIssueArg>
        //{
        //    serviceIssueArg
        //});
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
    private List<CreateServiceAvalibilityArg> GetRecordsForFullTimeExecution(long userId, long serviceId)
    {
        var list = new List<CreateServiceAvalibilityArg>();
        for (int i = 1; i < 8; i++)
        {
            var item = new CreateServiceAvalibilityArg
            {
                WeekDay = i,
                CreatedBy = userId,
                CreatedAt = DateTime.Now,
                ServiceId = serviceId,
                Id = IdHelper.GenerateUniqueId(),
                ActiveStatusId = (long)ActiveStatusEnum.Active,
                ServiceAvalibilityEndTime = new TimeOnly(23, 59),
                ServiceAvalibilityStartTime = new TimeOnly(0, 0),
            };
            list.Add(item);
        }
        return list;
    }
    private async Task<string> CalculateCode()
    {
        var counter = "001";
        var code = await _service.GetLastCode();
        if (!string.IsNullOrEmpty(code) && code.StartsWith("SE") && code.Length == 5)
        {
            var temp = code.Substring(2, 3);
            counter = (Convert.ToInt32(temp) + 1).ToString("000");
        }
        return $"SE{counter}";
    }
}
