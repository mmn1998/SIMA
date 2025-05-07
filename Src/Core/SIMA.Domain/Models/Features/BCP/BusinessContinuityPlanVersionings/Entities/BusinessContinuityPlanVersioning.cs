//using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
//using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;
//using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
//using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
//using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Args;
//using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Entities;
//using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Args;
//using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.ValueObjects;
//using SIMA.Domain.Models.Features.BCP.Scenarios.Entities;
//using SIMA.Framework.Common.Helper;
//using SIMA.Framework.Core.Entities;
//using System.Text;

//namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;

//public class BusinessContinuityPlanVersioning : Entity
//{
//    private BusinessContinuityPlanVersioning()
//    {

//    }
//    private BusinessContinuityPlanVersioning(CreateBusinessContinuityPlanVersioningArg arg)
//    {
//        Id = new(arg.Id);
//        BusinessContinuityPlanId = new(arg.BusinessContinuityPlanId);
//        VersionNumber = arg.VersionNumber;
//        ReleaseDate = arg.ReleaseDate;
//        ActiveStatusId = arg.ActiveStatusId;
//        CreatedAt = arg.CreatedAt;
//        CreatedBy = arg.CreatedBy;
//    }
//    public static BusinessContinuityPlanVersioning Create(CreateBusinessContinuityPlanVersioningArg arg)
//    {
//        return new BusinessContinuityPlanVersioning(arg);
//    }
//    public void Modify(ModifyBusinessContinuityPlanVersioningArg arg)
//    {
//        BusinessContinuityPlanId = new(arg.BusinessContinuityPlanId);
//        VersionNumber = arg.VersionNumber;
//        ReleaseDate = arg.ReleaseDate;
//        ActiveStatusId = arg.ActiveStatusId;
//        ModifiedBy = arg.ModifiedBy;
//        ModifiedAt = arg.ModifiedAt;
//    }
//    public void Delete(long userId)
//    {
//        ModifiedBy = userId;
//        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
//        ActiveStatusId = (long)ActiveStatusEnum.Delete;
//    }


//    public void AddBusinessContinuityPlanStratgy(List<CreateBusinessContinuityPlanStratgyArg> request, long planVersioningId)
//    {
//        planVersioningId.NullCheck();

//        var previousEntuty = _businessContinuityPlanStratgy.Where(x => x.BusinessContinuityPlanVersioningId == new BusinessContinuityPlanVersioningId(planVersioningId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

//        var addEntity = request.Where(x => !previousEntuty.Any(c => c.BusinessContinuityStratgyId.Value == x.BusinessContinuityStratgyId)).ToList();
//        var deleteEntity = previousEntuty.Where(x => !request.Any(c => c.BusinessContinuityStratgyId == x.BusinessContinuityStratgyId.Value)).ToList();

//        foreach (var item in addEntity)
//        {
//            var entity = _businessContinuityPlanStratgy.Where(x => (x.BusinessContinuityStratgyId == new BusinessContinuityStrategyId(item.BusinessContinuityStratgyId) && x.BusinessContinuityPlanVersioningId == new BusinessContinuityPlanVersioningId(planVersioningId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
//            if (entity is not null)
//            {
//                entity.ChangeStatus(ActiveStatusEnum.Active);
//            }
//            else
//            {
//                item.BusinessContinuityPlanVersioningId = planVersioningId;
//                entity = BusinessContinuityPlanStratgy.Create(item);
//                _businessContinuityPlanStratgy.Add(entity);
//            }
//        }

//        foreach (var role in deleteEntity)
//        {
//            role.Delete((long)request[0].CreatedBy);
//        }
//    }
//    public void AddBusinessContinuityPlanService(List<CreateBusinessContinuityPlanServiceArg> request, long planVersioningId)
//    {

//        planVersioningId.NullCheck();

//        var previousEntuty = _businessContinuityPlanServices.Where(x => x.BusinessContinuityPlanVersioningId == new BusinessContinuityPlanVersioningId(planVersioningId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

//        var addEntity = request.Where(x => !previousEntuty.Any(c => c.ServiceId.Value == x.ServiceId)).ToList();
//        var deleteEntity = previousEntuty.Where(x => !request.Any(c => c.ServiceId == x.ServiceId.Value)).ToList();

//        foreach (var item in addEntity)
//        {
//            var entity = _businessContinuityPlanServices.Where(x => (x.ServiceId == new ServiceId(item.ServiceId) && x.BusinessContinuityPlanVersioningId == new BusinessContinuityPlanVersioningId(planVersioningId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
//            if (entity is not null)
//            {
//                entity.ChangeStatus(ActiveStatusEnum.Active);
//            }
//            else
//            {
//                item.BusinessContinuityPlanVersioningId = planVersioningId;
//                entity = BusinessContinuityPlanService.Create(item);
//                _businessContinuityPlanServices.Add(entity);
//            }
//        }

//        foreach (var role in deleteEntity)
//        {
//            role.Delete((long)request[0].CreatedBy);
//        }

//    }
//    public void AddBusinessContinuityPlanRisk(List<CreateBusinessContinuityPlanRiskArg> request, long planVersioningId)
//    {
//        planVersioningId.NullCheck();

//        var previousEntuty = _businessContinuityPlanRisk.Where(x => x.BusinessContinuityPlanVersioningId == new BusinessContinuityPlanVersioningId(planVersioningId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

//        var addEntity = request.Where(x => !previousEntuty.Any(c => c.RiskId.Value == x.RiskId)).ToList();
//        var deleteEntity = previousEntuty.Where(x => !request.Any(c => c.RiskId == x.RiskId.Value)).ToList();

//        foreach (var item in addEntity)
//        {
//            var entity = _businessContinuityPlanRisk.Where(x => (x.RiskId == new RiskId(item.RiskId) && x.BusinessContinuityPlanVersioningId == new BusinessContinuityPlanVersioningId(planVersioningId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
//            if (entity is not null)
//            {
//                entity.ChangeStatus(ActiveStatusEnum.Active);
//            }
//            else
//            {
//                item.BusinessContinuityPlanVersioningId = planVersioningId;
//                entity = BusinessContinuityPlanRisk.Create(item);
//                _businessContinuityPlanRisk.Add(entity);
//            }
//        }

//        foreach (var role in deleteEntity)
//        {
//            role.Delete((long)request[0].CreatedBy);
//        }
//    }
//    public void AddBusinessContinuityPlanStaff(List<CreateBusinessContinuityPlanRelatedStaffArg> request, long planVersioningId)
//    {
//        planVersioningId.NullCheck();

//        var previousEntuty = _BusinessContinuityPlanRelatedStaff.Where(x => x.BusinessContinuityPlanVersioningId == new BusinessContinuityPlanVersioningId(planVersioningId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

//        var addEntity = request.Where(x => !previousEntuty.Any(c => c.StaffId.Value == x.StaffId)).ToList();
//        var deleteEntity = previousEntuty.Where(x => !request.Any(c => c.StaffId == x.StaffId.Value)).ToList();

//        foreach (var item in addEntity)
//        {
//            var entity = _BusinessContinuityPlanRelatedStaff.Where(x => (x.StaffId == new StaffId(item.StaffId) && x.BusinessContinuityPlanVersioningId == new BusinessContinuityPlanVersioningId(planVersioningId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
//            if (entity is not null)
//            {
//                entity.ChangeStatus(ActiveStatusEnum.Active);
//            }
//            else
//            {
//                item.BusinessContinuityPlanVersioningId = planVersioningId;
//                entity = BusinessContinuityPlans.Entities.BusinessContinuityPlanRelatedStaff.Create(item);
//                _BusinessContinuityPlanRelatedStaff.Add(entity);
//            }
//        }

//        foreach (var role in deleteEntity)
//        {
//            role.Delete((long)request[0].CreatedBy);
//        }
//    }
//    public void AddBusinessContinuityPlanCriticalActivity(List<CreateBusinessContinuityPlanCriticalActivityArg> request, long planVersioningId)
//    {
//        planVersioningId.NullCheck();

//        var previousEntuty = _businessContinuityPlanCriticalActivities.Where(x => x.BusinessContinuityPlanVersioningId == new BusinessContinuityPlanVersioningId(planVersioningId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

//        var addEntity = request.Where(x => !previousEntuty.Any(c => c.CriticalActivityId.Value == x.CriticalActivityId)).ToList();
//        var deleteEntity = previousEntuty.Where(x => !request.Any(c => c.CriticalActivityId == x.CriticalActivityId.Value)).ToList();

//        foreach (var item in addEntity)
//        {
//            var entity = _businessContinuityPlanCriticalActivities.Where(x => (x.CriticalActivityId == new CriticalActivityId(item.CriticalActivityId) && x.BusinessContinuityPlanVersioningId == new BusinessContinuityPlanVersioningId(planVersioningId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
//            if (entity is not null)
//            {
//                entity.ChangeStatus(ActiveStatusEnum.Active);
//            }
//            else
//            {
//                item.BusinessContinuityPlanVersioningId = planVersioningId;
//                entity = BusinessContinuityPlans.Entities.BusinessContinuityPlanCriticalActivity.Create(item);
//                _businessContinuityPlanCriticalActivities.Add(entity);
//            }
//        }

//        foreach (var role in deleteEntity)
//        {
//            role.Delete((long)request[0].CreatedBy);
//        }
//    }
//    public void AddBusinessContinuityPlanResponsible(List<CreateBusinessContinuityPlanResponsibleArg> request, long planVersioningId)
//    {
//        planVersioningId.NullCheck();

//        var previousEntuty = _businessContinuityPlanResponsible.Where(x => x.BusinessContinuityPlanVersioningId == new BusinessContinuityPlanVersioningId(planVersioningId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);
        
//        var addEntity = request.Where(x => !previousEntuty.Any(c => c.PlanResponsibilityId.Value == x.PlanResponsibilityId)).ToList();
//        var deleteEntity = previousEntuty.Where(x => !request.Any(c => c.PlanResponsibilityId == x.PlanResponsibilityId.Value)).ToList();

//        foreach (var item in addEntity)
//        {
//            var entity = _businessContinuityPlanResponsible.Where(x => (x.PlanResponsibilityId == new PlanResponsibilityId(item.PlanResponsibilityId) && x.BusinessContinuityPlanVersioningId == new BusinessContinuityPlanVersioningId(planVersioningId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
//            if (entity is not null)
//            {
//                entity.ChangeStatus(ActiveStatusEnum.Active);
//            }
//            else
//            {
//                item.BusinessContinuityPlanVersioningId = planVersioningId;
//                entity = BusinessContinuityPlanResponsible.Create(item);
//                _businessContinuityPlanResponsible.Add(entity);
//            }
//        }

//        foreach (var role in deleteEntity)
//        {
//            role.Delete((long)request[0].CreatedBy);
//        }
//    }
//    public void AddBusinessContinuityPlanAssumption(List<CreateBusinessContinuityPlanAssumptionArg> request, long planVersioningId)
//    {
//        planVersioningId.NullCheck();

//        var previousEntuty = _businessContinuityPlanAssumption.Where(x => x.BusinessContinuityPlanVersioningId == new BusinessContinuityPlanVersioningId(planVersioningId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

//        var addEntity = request.Where(x => !previousEntuty.Any(c => c.Code == x.Code)).ToList();
//        var deleteEntity = previousEntuty.Where(x => !request.Any(c => c.Code == x.Code)).ToList();

//        foreach (var item in addEntity)
//        {
//            var entity = _businessContinuityPlanAssumption.Where(x => (x.Code == item.Code && x.BusinessContinuityPlanVersioningId == new BusinessContinuityPlanVersioningId(planVersioningId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
//            if (entity is not null)
//            {
//                entity.ChangeStatus(ActiveStatusEnum.Active);
//            }
//            else
//            {
//                item.BusinessContinuityPlanVersioningId = planVersioningId;
//                entity = BusinessContinuityPlanAssumption.Create(item);
//                _businessContinuityPlanAssumption.Add(entity);
//            }
//        }

//        foreach (var role in deleteEntity)
//        {
//            role.Delete((long)request[0].CreatedBy);
//        }
//    }
//    public BusinessContinuityPlanVersioningId Id { get; set; }
//    public BusinessContinuityPlanId BusinessContinuityPlanId { get; private set; }
//    public virtual BusinessContinuityPlan BusinessContinuityPlan { get; private set; }
//    public string VersionNumber { get; set; }
//    public DateTime ReleaseDate { get; private set; }
//    public long ActiveStatusId { get; private set; }
//    public DateTime? CreatedAt { get; private set; }
//    public long? CreatedBy { get; private set; }
//    public byte[]? ModifiedAt { get; private set; }
//    public long? ModifiedBy { get; private set; }

//    private List<BusinessContinuityPlanResponsible> _businessContinuityPlanResponsible = new();
//    public ICollection<BusinessContinuityPlanResponsible> BusinessContinuityPlanResponsibles => _businessContinuityPlanResponsible;

//    private List<ScenarioBusinessContinuityPlanVersioning> _scenarioBusinessContinuityPlanVersionings = new();
//    public ICollection<ScenarioBusinessContinuityPlanVersioning> ScenarioBusinessContinuityPlanVersionings => _scenarioBusinessContinuityPlanVersionings;

//    private List<BusinessContinuityPlanAssumption> _businessContinuityPlanAssumption = new();
//    public ICollection<BusinessContinuityPlanAssumption> BusinessContinuityPlanAssumptions => _businessContinuityPlanAssumption;

//    private List<BusinessContinuityPlanRisk> _businessContinuityPlanRisk = new();
//    public ICollection<BusinessContinuityPlanRisk> BusinessContinuityPlanRisks => _businessContinuityPlanRisk;

//    private List<BusinessContinuityPlanService> _businessContinuityPlanServices = new();
//    public ICollection<BusinessContinuityPlanService> BusinessContinuityPlanServices => _businessContinuityPlanServices;

//    private List<BusinessContinuityPlanStratgy> _businessContinuityPlanStratgy = new();
//    public ICollection<BusinessContinuityPlanStratgy> BusinessContinuityPlanStratgies => _businessContinuityPlanStratgy;

//    private List<BusinessContinuityPlanCriticalActivity> _businessContinuityPlanCriticalActivities = new();
//    public ICollection<BusinessContinuityPlanCriticalActivity> BusinessContinuityPlanCriticalActivities => _businessContinuityPlanCriticalActivities;

//    private List<BusinessContinuityPlanRelatedStaff> _BusinessContinuityPlanRelatedStaff = new();
//    public ICollection<BusinessContinuityPlanRelatedStaff> BusinessContinuityPlanRelatedStaff => _BusinessContinuityPlanRelatedStaff;


//}
