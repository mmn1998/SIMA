using SIMA.Domain.Models.Features.Auths.UserTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;

public class ServiceUser : Entity
{
    private ServiceUser()
    {
    }
    private ServiceUser(CreateServiceUserArg arg)
    {
        Id = new ServiceUserId(arg.Id);
        ServiceId = new ServiceId(arg.ServiceId);
        ServiceUserTypeId = new UserTypeId(arg.ServiceUserTypeId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static ServiceUser Create(CreateServiceUserArg arg)
    {
        return new ServiceUser(arg);

    }
    public ServiceUserId Id { get; private set; }
    public virtual Service Service { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public virtual UserType ServiceUserType { get; private set; }
    public UserTypeId ServiceUserTypeId { get; private set; }
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
    }
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}
