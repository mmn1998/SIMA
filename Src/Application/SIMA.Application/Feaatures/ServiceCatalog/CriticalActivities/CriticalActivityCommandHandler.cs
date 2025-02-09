using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.ServiceCatalog.CriticalActivities;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.ServiceCatalog.CriticalActivities;

public class CriticalActivityCommandHandler : ICommandHandler<CreateCriticalActivityCommand, Result<long>>, ICommandHandler<ModifyCriticalActivityCommand,
    Result<long>>, ICommandHandler<DeleteCriticalActivityCommand, Result<long>>
{
    private readonly ICriticalActivityRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICriticalActivityDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public CriticalActivityCommandHandler(ICriticalActivityRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, ICriticalActivityDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateCriticalActivityCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateCriticalActivityArg>(request);
        var userId = _simaIdentity.UserId;
        arg.CreatedBy = userId;

        arg.Code = await CalculateCode();
        var entity = await CriticalActivity.Create(arg, _service);

        #region IsFullTimeExecution
        ///<summary>
        /// if IsFullTimeExecution is setted to 1 then 7 records will be added to database foreach day of week and its from 00:00 to 23:59
        ///</summary>
        if (string.Equals(request.IsFullTimeExecution, "1", StringComparison.InvariantCultureIgnoreCase))
        {
            var list = GetRecordsForFullTimeExecution(userId: userId, criticalActivityId: arg.Id);
            request.ExecutionPlanList = null;
            entity.AddCriticalActivityExrecutionPlans(list);
        }
        #endregion

        if (request.RelatedServiceList is not null)
        {
            var args = _mapper.Map<List<CreateCriticalActivityServicesArg>>(request.RelatedServiceList);
            foreach (var item in args)
            {
                item.CreatedBy = userId;
                item.CriticalActivityId = arg.Id;
            }
            entity.AddCriticalActivityServices(args);
        }
        if (request.RiskList is not null)
        {
            var args = _mapper.Map<List<CreateCriticalActivityRiskArg>>(request.RiskList);
            foreach (var item in args)
            {
                item.CreatedBy = userId;
                item.CriticalActivityId = arg.Id;
            }
            entity.AddCriticalActivityRisks(args);
        }
        if (request.AssetList is not null)
        {
            var args = _mapper.Map<List<CreateCriticalActivityAssetArg>>(request.AssetList);
            foreach (var item in args)
            {
                item.CreatedBy = userId;
                item.CriticalActivityId = arg.Id;
            }
            entity.AddCriticalActivityAssets(args);
        }
        if (request.ConfigurationItemList is not null)
        {
            var args = _mapper.Map<List<CreateCriticalActivityConfigurationItemArg>>(request.ConfigurationItemList);
            foreach (var item in args)
            {
                item.CreatedBy = userId;
                item.CriticalActivityId = arg.Id;
            }
            entity.AddCriticalActivityConfigurationItems(args);
        }
        if (request.AssignedStaffList is not null)
        {
            var args = _mapper.Map<List<CreateCriticalActivityAssignedStaffArg>>(request.AssignedStaffList);
            foreach (var item in args)
            {
                item.CreatedBy = userId;
                item.CriticalActivityId = arg.Id;
            }
            entity.AddAssignedStaffs(args);
        }
        if (request.ExecutionPlanList is not null)
        {
            var args = new List<CreateCriticalActivityExecutionPlanArg>();
            foreach (var item in request.ExecutionPlanList)
            {
                for (int i = item.WeekDayStart; i < item.WeekDayEnd; i++)
                {
                    var serviceEndTime = item.ServiceAvalibilityEndTime.ToTimeOnly() ?? throw SimaResultException.NullException;
                    var serviceStartTime = item.ServiceAvalibilityStartTime.ToTimeOnly() ?? throw SimaResultException.NullException;
                    var newArg = new CreateCriticalActivityExecutionPlanArg
                    {
                        ActiveStatusId = (long)ActiveStatusEnum.Active,
                        CreatedAt = DateTime.Now,
                        CreatedBy = userId,
                        CriticalActivityId = arg.Id,
                        Id = IdHelper.GenerateUniqueId(),
                        WeekDay = i,
                        ServiceAvalibilityEndTime = serviceEndTime,
                        ServiceAvalibilityStartTime = serviceStartTime
                    };
                    args.Add(newArg);
                }
            }
            entity.AddCriticalActivityExrecutionPlans(args);
        }
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyCriticalActivityCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _repository.GetById(new(request.Id));
            var arg = _mapper.Map<ModifyCriticalActivityArg>(request);
            var userId = _simaIdentity.UserId;

            #region IsFullTimeExecution
            ///<summary>
            /// if IsFullTimeExecution is setted to 1 then 7 records will be added to database foreach day of week and its from 00:00 to 23:59
            ///</summary>
            if (string.Equals(request.IsFullTimeExecution, "1", StringComparison.InvariantCultureIgnoreCase))
            {
                var list = GetRecordsForFullTimeExecution(userId: userId, criticalActivityId: arg.Id);
                request.ExecutionPlanList = null;
                entity.ModifyCriticalActivityExecutionPlans(list);
            }
            #endregion

            if (request.RelatedServiceList is not null)
            {
                var args = _mapper.Map<List<CreateCriticalActivityServicesArg>>(request.RelatedServiceList);
                foreach (var item in args)
                {
                    item.CreatedBy = userId;
                    item.CriticalActivityId = arg.Id;
                }
                entity.ModifyCriticalActivityServices(args);
            }
            if (request.RiskList is not null)
            {
                var args = _mapper.Map<List<CreateCriticalActivityRiskArg>>(request.RiskList);
                foreach (var item in args)
                {
                    item.CreatedBy = userId;
                    item.CriticalActivityId = arg.Id;
                }
                entity.ModifyCriticalActivityRisks(args);
            }
            if (request.AssetList is not null)
            {
                var args = _mapper.Map<List<CreateCriticalActivityAssetArg>>(request.AssetList);
                foreach (var item in args)
                {
                    item.CreatedBy = userId;
                    item.CriticalActivityId = arg.Id;
                }
                entity.ModifyCriticalActivityAssets(args);
            }
            if (request.ConfigurationItemList is not null)
            {
                var args = _mapper.Map<List<CreateCriticalActivityConfigurationItemArg>>(request.ConfigurationItemList);
                foreach (var item in args)
                {
                    item.CreatedBy = userId;
                    item.CriticalActivityId = arg.Id;
                }
                entity.ModifyCriticalConfigurationItems(args);
            }
            if (request.AssignedStaffList is not null)
            {
                var args = _mapper.Map<List<CreateCriticalActivityAssignedStaffArg>>(request.AssignedStaffList);
                foreach (var item in args)
                {
                    item.CreatedBy = userId;
                    item.CriticalActivityId = arg.Id;
                }
                entity.ModifyAssignedStaffs(args);
            }
            if (request.ExecutionPlanList is not null)
            {
                var args = new List<CreateCriticalActivityExecutionPlanArg>();
                foreach (var item in request.ExecutionPlanList)
                {
                    for (int i = item.WeekDayStart; i < item.WeekDayEnd; i++)
                    {
                        var serviceEndTime = item.ServiceAvalibilityEndTime.ToTimeOnly() ?? throw SimaResultException.NullException;
                        var serviceStartTime = item.ServiceAvalibilityStartTime.ToTimeOnly() ?? throw SimaResultException.NullException;
                        var newArg = new CreateCriticalActivityExecutionPlanArg
                        {
                            ActiveStatusId = (long)ActiveStatusEnum.Active,
                            CreatedAt = DateTime.Now,
                            CreatedBy = userId,
                            CriticalActivityId = arg.Id,
                            Id = IdHelper.GenerateUniqueId(),
                            WeekDay = i,
                            ServiceAvalibilityEndTime = serviceEndTime,
                            ServiceAvalibilityStartTime = serviceStartTime
                        };
                        args.Add(newArg);
                    }
                }
                entity.ModifyCriticalActivityExecutionPlans(args);
            }
            await entity.Modify(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
        catch (Exception ex)
        {

            throw;
        }
       
    }

    public async Task<Result<long>> Handle(DeleteCriticalActivityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        entity.Delete(_simaIdentity.UserId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
    private List<CreateCriticalActivityExecutionPlanArg> GetRecordsForFullTimeExecution(long userId, long criticalActivityId)
    {
        var list = new List<CreateCriticalActivityExecutionPlanArg>();
        for (int i = 1; i < 8; i++)
        {
            var item = new CreateCriticalActivityExecutionPlanArg
            {
                WeekDay = i,
                CreatedBy = userId,
                CreatedAt = DateTime.Now,
                CriticalActivityId = criticalActivityId,
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
        if (!string.IsNullOrEmpty(code) && code.StartsWith("CR") && code.Length == 5)
        {
            var temp = code.Substring(2, 3);
            counter = (Convert.ToInt32(temp) + 1).ToString("000");
        }
        return $"CR{counter}";
    }
}
