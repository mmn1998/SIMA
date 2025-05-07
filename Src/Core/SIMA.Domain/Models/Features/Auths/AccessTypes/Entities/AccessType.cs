using SIMA.Domain.Models.Features.AccessManagement.AccessRequests.Entities;
using SIMA.Domain.Models.Features.Auths.AccessTypes.Args;
using SIMA.Domain.Models.Features.Auths.AccessTypes.Contracts;
using SIMA.Domain.Models.Features.Auths.AccessTypes.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.AccessTypes.Entities;

public class AccessType : Entity, IAggregateRoot
{
    private AccessType() { }
    private AccessType(CreateAccessTypeArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<AccessType> Create(CreateAccessTypeArg arg, IAccessTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new AccessType(arg);
    }
    public async Task Modify(ModifyAccessTypeArg arg, IAccessTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateAccessTypeArg arg, IAccessTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyAccessTypeArg arg, IAccessTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public AccessTypeId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public long? ActiveStatusId { get; set; }
    public long? CreatedBy { get; set; }
    public long? ModifiedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public ICollection<ProgressStoreProcedureParam>? ProgressStoreProcedureParams { get; set; }
    public ICollection<Api>? Apis { get; set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<AccessRequest> _accessRequests = new();
    public ICollection<AccessRequest> AccessRequests => _accessRequests;
}
