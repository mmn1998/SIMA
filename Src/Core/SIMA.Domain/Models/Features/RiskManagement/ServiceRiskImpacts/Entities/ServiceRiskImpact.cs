using SIMA.Domain.Models.Features.RiskManagement.ImpactScales.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ServiceRiskImpacts.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.ServiceRiskImpacts.Entities;

public class ServiceRiskImpact : Entity
{
    private ServiceRiskImpact()
    {

    }
    private ServiceRiskImpact(CreateServiceRiskImpactArg arg)
    {
        Id = new(arg.Id);
        ServiceRiskId = new(arg.ServiceRiskId);
        ImpactScaleId = new(arg.ImpactScaleId);
        RiskImpactId = new(arg.RiskImpactId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static ServiceRiskImpact Create(CreateServiceRiskImpactArg arg)
    {
        return new ServiceRiskImpact(arg);
    }
    public void Modify(ModifyServiceRiskImpactArg arg)
    {
        ServiceRiskId = new(arg.ServiceRiskId);
        ImpactScaleId = new(arg.ImpactScaleId);
        RiskImpactId = new(arg.RiskImpactId);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }

    public ServiceRiskImpactId Id { get; set; }
    public ServiceRiskId ServiceRiskId { get; private set; }
    public virtual ServiceRisk ServiceRisk { get; private set; }
    public ImpactScaleId ImpactScaleId { get; private set; }
    public virtual ImpactScale ImpactScale { get; private set; }
    public RiskImpactId RiskImpactId { get; private set; }
    public virtual RiskImpact RiskImpact { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
