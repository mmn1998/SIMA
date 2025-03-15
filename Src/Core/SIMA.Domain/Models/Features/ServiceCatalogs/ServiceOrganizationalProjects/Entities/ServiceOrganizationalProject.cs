using System.Text;
using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceOrganizationalProjects.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ServiceOrganizationalProjects.Entities;

public class ServiceOrganizationalProject : Entity
{
    public ServiceOrganizationalProject()
    {

    }

    public ServiceOrganizationalProject(CreateServiceOrganizationalProjectArg arg)
    {
        Id = new(arg.Id);
        ServiceId = new(arg.ServiceId);
        OrganizationalProjectId = new(arg.OrganizationalProjectId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ServiceOrganizationalProject Create(CreateServiceOrganizationalProjectArg arg)
    {
        return new ServiceOrganizationalProject(arg);
    }

    //public async Task Modify(ModifyServiceOrganizationalProjectArg arg)
    //{
    //    ServiceId = arg.ServiceId.HasValue ? new ServiceId(arg.ServiceId.Value) : null;
    //    OrganizationalProjectId = arg.OrganizationalProjectId.HasValue ? new OrganizationalProjectId(arg.OrganizationalProjectId.Value) : null;
    //    ActiveStatusId = arg.ActiveStatusId;
    //}

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
    public ServiceOrganizationalProjectId Id { get; private set; }
    public ServiceId? ServiceId { get; private set; }
    public OrganizationalProjectId? OrganizationalProjectId { get; private set; }
    public virtual Service Service { get; private set; }
    public virtual OrganizationalProject OrganizationalProject { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}