using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.BusinessContinuityPlans;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Entities;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.BusinessContinuityPlans
{
    public class BusinessContinuityPlanCommandHandler :
    ICommandHandler<CreateBusinessContinuityPlanCommand, Result<long>>,
    ICommandHandler<ModifyBusinessContinuityPlanCommand, Result<long>>,
    ICommandHandler<DeleteBusinessContinuityPlanCommand, Result<long>>,
    ICommandHandler<DeleteBusinessContinuityPlanVersioningCommand, Result<long>>
    {
        private readonly IBusinessContinuityPlanRepository _repository;
        private readonly IBusinessContinuityPlanDomainService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISimaIdentity _simaIdentity;
        private readonly IMapper _mapper;

        public BusinessContinuityPlanCommandHandler(IBusinessContinuityPlanRepository repository, IBusinessContinuityPlanDomainService service,
            IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
        {
            _repository = repository;
            _service = service;
            _unitOfWork = unitOfWork;
            _simaIdentity = simaIdentity;
            _mapper = mapper;
        }
        public async Task<Result<long>> Handle(CreateBusinessContinuityPlanCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateBusinessContinuityPlanArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = BusinessContinuityPlan.CreateEmpty();

            var businessContinuityPlanIssue = new CreateBusinessContinuityPlanIssueArg()
            {
                CreatedBy = _simaIdentity.UserId,
                CreatedAt = DateTime.Now,
                IssueId = arg.IssueId,
                ActiveStatusId = (long)ActiveStatusEnum.Active
            };

            var businessContinuityPlanVersioning = _mapper.Map<CreateBusinessContinuityPlanVersioningArg>(request);
            businessContinuityPlanVersioning.CreatedBy = _simaIdentity.UserId;

            if (request.BusinessContinuityPlanId > 0 || request.BusinessContinuityPlanId is not null)
            {
                entity = await _repository.GetById(new((long)request.BusinessContinuityPlanId));
                entity.Update(_simaIdentity.UserId);
                entity.AddBusinessContinuityPlanVersioning(businessContinuityPlanVersioning);
                entity.AddBusinessContinuityPlanIssue(businessContinuityPlanIssue, entity.Id.Value, entity.Title);
            }
            else
            {
                entity = await BusinessContinuityPlan.Create(arg, _service);
                businessContinuityPlanVersioning.BusinessContinuityPlanId = entity.Id.Value;
                entity.AddBusinessContinuityPlanVersioning(businessContinuityPlanVersioning);
                entity.AddBusinessContinuityPlanIssue(businessContinuityPlanIssue, entity.Id.Value, arg.Title);
                await _repository.Add(entity);
            }

            if (request.BusinessContinuityPlanStratgyList is not null)
            {
                var businessContinuityPlanStratgy = _mapper.Map<List<CreateBusinessContinuityPlanStratgyArg>>(request.BusinessContinuityPlanStratgyList);
                foreach (var item in businessContinuityPlanStratgy)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddBusinessContinuityPlanStratgy(businessContinuityPlanStratgy, businessContinuityPlanVersioning.Id);
            }

            if (request.BusinessContinuityPlanServiceList is not null)
            {
                var businessContinuityPlanService = _mapper.Map<List<CreateBusinessContinuityPlanServiceArg>>(request.BusinessContinuityPlanServiceList);
                foreach (var item in businessContinuityPlanService)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddBusinessContinuityPlanService(businessContinuityPlanService, businessContinuityPlanVersioning.Id);
            }

            if (request.BusinessContinuityPlanRiskList is not null)
            {
                var businessContinuityPlanRisk = _mapper.Map<List<CreateBusinessContinuityPlanRiskArg>>(request.BusinessContinuityPlanRiskList);
                foreach (var item in businessContinuityPlanRisk)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddBusinessContinuityPlanRisk(businessContinuityPlanRisk, businessContinuityPlanVersioning.Id);
            }

            if (request.BusinessContinuityPlanCriticalActivityList is not null)
            {
                var businessContinuityPlanCriticalActivity = _mapper.Map<List<CreateBusinessContinuityPlanCriticalActivityArg>>(request.BusinessContinuityPlanCriticalActivityList);
                foreach (var item in businessContinuityPlanCriticalActivity)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddBusinessContinuityPlanCriticalActivity(businessContinuityPlanCriticalActivity, businessContinuityPlanVersioning.Id);
            }

            if (request.BusinessContinuityPlanRelatedStaffList is not null)
            {
                var businessContinuityPlanRelatedStaff = _mapper.Map<List<CreateBusinessContinuityPlanRelatedStaffArg>>(request.BusinessContinuityPlanRelatedStaffList);
                foreach (var item in businessContinuityPlanRelatedStaff)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddBusinessContinuityPlanRelatedStaff(businessContinuityPlanRelatedStaff, businessContinuityPlanVersioning.Id);
            }

            if (request.BusinessContinuityPlanResponsibleList is not null)
            {
                var businessContinuityPlanResponsible = _mapper.Map<List<CreateBusinessContinuityPlanResponsibleArg>>(request.BusinessContinuityPlanResponsibleList);
                foreach (var item in businessContinuityPlanResponsible)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddBusinessContinuityPlanResponsible(businessContinuityPlanResponsible, businessContinuityPlanVersioning.Id);
            }

            if (request.BusinessContinuityPlanAssumptionList is not null)
            {
                var businessContinuityPlanAssumption = _mapper.Map<List<CreateBusinessContinuityPlanAssumptionArg>>(request.BusinessContinuityPlanAssumptionList);
                foreach (var item in businessContinuityPlanAssumption)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddBusinessContinuityPlanAssumption(businessContinuityPlanAssumption, businessContinuityPlanVersioning.Id);
            }

            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(arg.Id);
        }

        public async Task<Result<long>> Handle(ModifyBusinessContinuityPlanCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(new BusinessContinuityPlanId(request.Id));
            var version = await _repository.GetBusinessContinuityPlanVersioningById(new BusinessContinuityPlanId(request.Id));
            var arg = _mapper.Map<ModifyBusinessContinuityPlanArg>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg, _service);

            if (request.BusinessContinuityPlanStratgyList is not null || request.BusinessContinuityPlanStratgyList.Count > 0)
            {
                var businessContinuityPlanStratgy = _mapper.Map<List<CreateBusinessContinuityPlanStratgyArg>>(request.BusinessContinuityPlanStratgyList);
                foreach (var item in businessContinuityPlanStratgy)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddBusinessContinuityPlanStratgy(businessContinuityPlanStratgy, version.Id.Value);
            }

            if (request.BusinessContinuityPlanServiceList is not null || request.BusinessContinuityPlanServiceList.Count > 0)
            {
                var businessContinuityPlanService = _mapper.Map<List<CreateBusinessContinuityPlanServiceArg>>(request.BusinessContinuityPlanServiceList);
                foreach (var item in businessContinuityPlanService)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddBusinessContinuityPlanService(businessContinuityPlanService, version.Id.Value);
            }

            if (request.BusinessContinuityPlanRiskList is not null || request.BusinessContinuityPlanRiskList.Count > 0)
            {
                var businessContinuityPlanRisk = _mapper.Map<List<CreateBusinessContinuityPlanRiskArg>>(request.BusinessContinuityPlanRiskList);
                foreach (var item in businessContinuityPlanRisk)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddBusinessContinuityPlanRisk(businessContinuityPlanRisk, version.Id.Value);
            }

            if (request.BusinessContinuityPlanCriticalActivityList is not null || request.BusinessContinuityPlanCriticalActivityList.Count > 0)
            {
                var businessContinuityPlanCriticalActivity = _mapper.Map<List<CreateBusinessContinuityPlanCriticalActivityArg>>(request.BusinessContinuityPlanCriticalActivityList);
                foreach (var item in businessContinuityPlanCriticalActivity)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddBusinessContinuityPlanCriticalActivity(businessContinuityPlanCriticalActivity, version.Id.Value);
            }

            if (request.BusinessContinuityPlanRelatedStaffList is not null || request.BusinessContinuityPlanRelatedStaffList.Count > 0)
            {
                var businessContinuityPlanRelatedStaff = _mapper.Map<List<CreateBusinessContinuityPlanRelatedStaffArg>>(request.BusinessContinuityPlanRelatedStaffList);
                foreach (var item in businessContinuityPlanRelatedStaff)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddBusinessContinuityPlanRelatedStaff(businessContinuityPlanRelatedStaff, version.Id.Value);
            }

            if (request.BusinessContinuityPlanResponsibleList is not null || request.BusinessContinuityPlanResponsibleList.Count > 0)
            {
                var businessContinuityPlanResponsible = _mapper.Map<List<CreateBusinessContinuityPlanResponsibleArg>>(request.BusinessContinuityPlanResponsibleList);
                foreach (var item in businessContinuityPlanResponsible)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddBusinessContinuityPlanResponsible(businessContinuityPlanResponsible, version.Id.Value);
            }

            if (request.BusinessContinuityPlanAssumptionList is not null || request.BusinessContinuityPlanAssumptionList.Count > 0)
            {
                var businessContinuityPlanAssumption = _mapper.Map<List<CreateBusinessContinuityPlanAssumptionArg>>(request.BusinessContinuityPlanAssumptionList);
                foreach (var item in businessContinuityPlanAssumption)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddBusinessContinuityPlanAssumption(businessContinuityPlanAssumption, version.Id.Value);
            }

            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }

        public async Task<Result<long>> Handle(DeleteBusinessContinuityPlanCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(new BusinessContinuityPlanId(request.Id));
            long userId = _simaIdentity.UserId; entity.Delete(userId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }

        public async Task<Result<long>> Handle(DeleteBusinessContinuityPlanVersioningCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(new BusinessContinuityPlanId(request.Id));
            long userId = _simaIdentity.UserId;
            entity.DeleteVersion(userId , request.BusinessContinuityPlanVersioningId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
    }
}
