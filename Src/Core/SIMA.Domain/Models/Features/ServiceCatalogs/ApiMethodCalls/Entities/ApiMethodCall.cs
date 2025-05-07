using SIMA.Domain.Models.Features.ServiceCatalogs.ApiMethodCalls.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiMethodCalls.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiMethodCalls.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ApiMethodCalls.Entities;

public class ApiMethodCall : Entity, IAggregateRoot
{
    private ApiMethodCall() { }
    private ApiMethodCall(CreateApiMethodCallArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ApiMethodCall> Create(CreateApiMethodCallArg arg, IApiMethodCallDomainService service)
    {
        await CreateGuards(arg, service);
        return new ApiMethodCall(arg);
    }
    public async Task Modify(ModifyApiMethodCallArg arg, IApiMethodCallDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateApiMethodCallArg arg, IApiMethodCallDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyApiMethodCallArg arg, IApiMethodCallDomainService service)
    {

    }
    #endregion
    public ApiMethodCallId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<Api> _apis = new();
    public ICollection<Api> Apis => _apis;
}
