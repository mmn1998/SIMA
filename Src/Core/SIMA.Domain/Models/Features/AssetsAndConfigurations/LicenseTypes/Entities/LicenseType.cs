using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Contracts;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Entities;

public class LicenseType : Entity
{
    private LicenseType() { }
    private LicenseType(CreateLicenseTypeArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<LicenseType> Create(CreateLicenseTypeArg arg, ILicenseTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new LicenseType(arg);
    }
    public async Task Modify(ModifyLicenseTypeArg arg, ILicenseTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateLicenseTypeArg arg, ILicenseTypeDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyLicenseTypeArg arg, ILicenseTypeDomainService service)
    {

    }
    #endregion
    public LicenseTypeId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}