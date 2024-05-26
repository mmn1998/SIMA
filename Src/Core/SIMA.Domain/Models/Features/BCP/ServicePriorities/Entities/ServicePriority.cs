using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.ValueObjects;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.Args;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.Contracts;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.ServicePriorities.Entities;

public class ServicePriority : Entity, IAggregateRoot
{
    private ServicePriority()
    {

    }
    private ServicePriority(CreateServicePriorityArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        Ordering = arg.Ordering;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ServicePriority> Create(CreateServicePriorityArg arg, IServicePriorityDomainService service)
    {
        await CreateGuards(arg, service);
        return new ServicePriority(arg);
    }
    public async Task Modify(ModifyServicePriorityArg arg, IServicePriorityDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        Ordering = arg.Ordering;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateServicePriorityArg arg, IServicePriorityDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyServicePriorityArg arg, IServicePriorityDomainService service)
    {

    }
    #endregion
    public ServicePriorityId Id { get; set; }
    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public float Ordering { get; private set; }
    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<BusinessImpactAnalysis> _businessImpactAnalyses = new();
    public ICollection<BusinessImpactAnalysis> BusinessImpactAnalyses => _businessImpactAnalyses;
}
