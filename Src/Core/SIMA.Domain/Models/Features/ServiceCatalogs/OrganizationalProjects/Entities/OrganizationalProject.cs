using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceOrganizationalProjects.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.OrganizationalProjects.Entities;

public class OrganizationalProject : Entity, IAggregateRoot
{
    private OrganizationalProject() { }
    private OrganizationalProject(CreateOrganizationalProjectArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<OrganizationalProject> Create(CreateOrganizationalProjectArg arg, IOrganizationalProjectDomainService service)
    {
        await CreateGuards(arg, service);
        return new OrganizationalProject(arg);
    }
    public async Task Modify(ModifyOrganizationalProjectArg arg, IOrganizationalProjectDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    #region Guards
    private static async Task CreateGuards(CreateOrganizationalProjectArg arg, IOrganizationalProjectDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyOrganizationalProjectArg arg, IOrganizationalProjectDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }


    #endregion
    public OrganizationalProjectId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<ServiceOrganizationalProject> _serviceOrganizationalProjects = new();
    public ICollection<ServiceOrganizationalProject> ServiceOrganizationalProjects => _serviceOrganizationalProjects;
}