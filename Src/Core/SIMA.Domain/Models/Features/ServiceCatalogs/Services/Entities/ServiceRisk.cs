using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ServiceRiskImpacts.Args;
using SIMA.Domain.Models.Features.RiskManagement.ServiceRiskImpacts.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

public class ServiceRisk : Entity
{
    private ServiceRisk()
    {

    }
    private ServiceRisk(CreateServiceRiskArg arg)
    {
        Id = new ServiceRiskId(arg.Id);
        ServiceId = new ServiceId(arg.ServiceId);
        RiskId = new RiskId(arg.RiskId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static ServiceRisk Create(CreateServiceRiskArg arg)
    {
        return new ServiceRisk(arg);
    }
    public ServiceRiskId Id { get; private set; }
    public virtual Service Service { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public virtual Risk Risk { get; private set; }
    public RiskId RiskId { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        DeleteServiceRiskImpacts(userId);
        
    }
    public void DeleteServiceRiskImpacts(long userId)
    {
        foreach (var item in _serviceRiskImpacts)
        {
            item.Delete(userId);
        }
    }
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
    public void AddServiceRiskImpacts(List<CreateServiceRiskImpactArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ServiceRiskImpact.Create(arg);
            _serviceRiskImpacts.Add(entity);
        }
    }
    private List<ServiceRiskImpact> _serviceRiskImpacts = new();
    public ICollection<ServiceRiskImpact> ServiceRiskImpacts => _serviceRiskImpacts;
}
