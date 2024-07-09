using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Entities;

public class ApiAuthentoicationMethod : Entity, IAggregateRoot
{
    private ApiAuthentoicationMethod() { }
    private ApiAuthentoicationMethod(CreateApiAuthentoicationMethodArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ApiAuthentoicationMethod> Create(CreateApiAuthentoicationMethodArg arg, IApiAuthentoicationMethodDomainService service)
    {
        await CreateGuards(arg, service);
        return new ApiAuthentoicationMethod(arg);
    }
    public async Task Modify(ModifyApiAuthentoicationMethodArg arg, IApiAuthentoicationMethodDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateApiAuthentoicationMethodArg arg, IApiAuthentoicationMethodDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyApiAuthentoicationMethodArg arg, IApiAuthentoicationMethodDomainService service)
    {

    }
    #endregion
    public ApiAuthentoicationMethodId Id { get; private set; }
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
