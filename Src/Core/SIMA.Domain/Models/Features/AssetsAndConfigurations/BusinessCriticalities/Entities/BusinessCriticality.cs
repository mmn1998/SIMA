using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Contracts;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Entities;

public class BusinessCriticality : Entity
{
    private BusinessCriticality() { }
    private BusinessCriticality(CreateBusinessCriticalityArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<BusinessCriticality> Create(CreateBusinessCriticalityArg arg, IBusinessCriticalityDomainService service)
    {
        await CreateGuards(arg, service);
        return new BusinessCriticality(arg);
    }
    public async Task Modify(ModifyBusinessCriticalityArg arg, IBusinessCriticalityDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateBusinessCriticalityArg arg, IBusinessCriticalityDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyBusinessCriticalityArg arg, IBusinessCriticalityDomainService service)
    {

    }
    #endregion
    public BusinessCriticalityId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    private List<Asset> _assets = new();
    public ICollection<Asset> Assets => _assets;
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
