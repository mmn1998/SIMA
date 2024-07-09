using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Domain.Models.Features.Logistics.Goodses.Args;
using SIMA.Domain.Models.Features.Logistics.SupplierRanks.Entities;
using SIMA.Domain.Models.Features.Logistics.SupplierRanks.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Suppliers.Args;
using SIMA.Domain.Models.Features.Logistics.Suppliers.Contracts;
using SIMA.Domain.Models.Features.Logistics.Suppliers.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SIMA.Domain.Models.Features.Logistics.Suppliers.Entities;

public class Supplier : Entity, IAggregateRoot
{
    private Supplier() { }
    private Supplier(CreateSupplierArg arg)
    {
        Id = new(arg.Id);
        SupplierRankId = new(arg.SupplierRankId);
        Name = arg.Name;
        Code = arg.Code;
        PhoneNumber = arg.PhoneNumber;
        MobileNumber = arg.MobileNumber;
        FaxNumber = arg.FaxNumber;
        PostalCode = arg.PostalCode;
        Address = arg.Address;
        IsInBlackList = arg.IsInBlackList;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        
    }
    public static async Task<Supplier> Create(CreateSupplierArg arg, ISupplierDomainService service)
    {
        await CreateGuards(arg, service);
        return new Supplier(arg);
    }
    public async Task Modify(ModifySupplierArg arg, ISupplierDomainService service)
    {
        await ModifyGuards(arg, service);
        SupplierRankId = new(arg.SupplierRankId);
        Name = arg.Name;
        Code = arg.Code;
        PhoneNumber = arg.PhoneNumber;
        MobileNumber = arg.MobileNumber;
        FaxNumber = arg.FaxNumber;
        PostalCode = arg.PostalCode;
        Address = arg.Address;
        IsInBlackList = arg.IsInBlackList;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateSupplierArg arg, ISupplierDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifySupplierArg arg, ISupplierDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public SupplierId Id { get; private set; }
    public SupplierRankId SupplierRankId { get; private set; }
    public virtual SupplierRank SupplierRank { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? MobileNumber { get; private set; }
    public string? FaxNumber { get; private set; }
    public string? PostalCode { get; private set; }
    public string? Address { get; private set; }
    public string? IsInBlackList { get; private set; }
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
    private List<SupplierBlackListHistory> _supplierBlackListHistories = new();
    public ICollection<SupplierBlackListHistory> SupplierBlackListHistories => _supplierBlackListHistories;
    private List<CandidatedSupplier> _candidatedSuppliers = new();
    public ICollection<CandidatedSupplier> CandidatedSuppliers => _candidatedSuppliers;
}
