//using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
//using SIMA.Framework.Core.Entities;

//namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

//public class ServiceRisk : Entity
//{
//    private ServiceRisk()
//    {

//    }
//    private ServiceRisk(CreateServiceRiskArg arg)
//    {
//        Id = new ServiceRiskId(arg.Id);
//        ServiceId = new ServiceId(arg.ServiceId);
//        ActiveStatusId = arg.ActiveStatusId;
//        CreatedAt = arg.CreatedAt;
//        CreatedBy = arg.CreatedBy;
//    }

//    public static async Task<ServiceRisk> Create(CreateServiceRiskArg arg)
//    {
//        return new ServiceRisk(arg);
//    }
//    public ServiceRiskId Id { get; private set; }
//    public virtual Service Service { get; private set; }
//    public ServiceId ServiceId { get; private set; }
//    //? risk
//    public long? ActiveStatusId { get; private set; }
//    public DateTime? CreatedAt { get; private set; }
//    public long? CreatedBy { get; private set; }
//    public byte[]? ModifiedAt { get; private set; }
//    public long? ModifiedBy { get; private set; }
//}
