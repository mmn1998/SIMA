using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Entities;

public class ApiAuthenticationMethod : Entity, IAggregateRoot
{
    private ApiAuthenticationMethod() { }
    private ApiAuthenticationMethod(CreateApiAuthenticationMethodArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ApiAuthenticationMethod> Create(CreateApiAuthenticationMethodArg arg, IApiAuthenticationMethodDomainService service)
    {
        await CreateGuards(arg, service);
        return new ApiAuthenticationMethod(arg);
    }
    public async Task Modify(ModifyApiAuthenticationMethodArg arg, IApiAuthenticationMethodDomainService service)
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
    private static async Task CreateGuards(CreateApiAuthenticationMethodArg arg, IApiAuthenticationMethodDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyApiAuthenticationMethodArg arg, IApiAuthenticationMethodDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }


    #endregion
    public ApiAuthenticationMethodId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    
    private List<Api> _apis = new();
    public ICollection<Api> Apis => _apis;
}
