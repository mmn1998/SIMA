using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Entities;

public class ApiType : Entity, IAggregateRoot
{
    private ApiType() { }
    private ApiType(CreateApiTypeArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ApiType> Create(CreateApiTypeArg arg, IApiTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new ApiType(arg);
    }
    public async Task Modify(ModifyApiTypeArg arg, IApiTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateApiTypeArg arg, IApiTypeDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyApiTypeArg arg, IApiTypeDomainService service)
    {

    }
    #endregion
    public ApiTypeId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<Api> _apis = new();
    public ICollection<Api> Apis => _apis;
}