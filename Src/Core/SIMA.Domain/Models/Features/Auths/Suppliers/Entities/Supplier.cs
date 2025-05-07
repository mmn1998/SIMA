using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.SupplierRanks.Entities;
using SIMA.Domain.Models.Features.Auths.SupplierRanks.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Suppliers.Args;
using SIMA.Domain.Models.Features.Auths.Suppliers.Contracts;
using SIMA.Domain.Models.Features.Auths.Suppliers.Exceptions;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Args;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Args;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Suppliers.Entities;

public class Supplier : Entity, IAggregateRoot
{
    private Supplier() { }
    private Supplier(CreateSupplierArg arg)
    {
        Id = new(arg.Id);
        SupplierRankId = new(arg.SupplierRankId);
        Name = arg.Name;
        Code = arg.Code;
        IsInBlackList = arg.IsInBlackList;
        SuccessOrderCountinTheYear = arg.SuccessOrderCountinTheYear;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        NationalId = arg.NationalId;
        NationalCode = arg.NationalCode;
    }


    #region AddSubTable
    public async Task AddAccountList(List<CreateSupplierAccountListArg> request, long supplierId)
    {
        supplierId.NullCheck();

        var previousEntity = _supplierAccountLists.Where(x => x.SupplierId == new SupplierId(supplierId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addEntity = request.Where(x => !previousEntity.Any(c => c.Id.Value == x.Id)).ToList();
        var deleteEntity = previousEntity.Where(x => !request.Any(c => c.Id == x.Id.Value)).ToList();


        foreach (var item in addEntity)
        {
            var entity = _supplierAccountLists.Where(x => (x.SupplierId == new SupplierId(supplierId) && x.Id == new SupplierAccountListId((long)item.Id)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                entity.ChangeStatus(ActiveStatusEnum.Active, (long)request[0].CreatedBy);
            }
            else
            {
                entity = await SupplierAccountList.Create(item);
                _supplierAccountLists.Add(entity);
            }
        }

        foreach (var item in deleteEntity)
        {
            item.Delete((long)request[0].CreatedBy);
        }
    }
    public async Task AddAddressBook(List<CreateSupplierAddressBookArg> request, long supplierId)
    {
        supplierId.NullCheck();

        var previousEntity = _supplierAddressBooks.Where(x => x.SupplierId == new SupplierId(supplierId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addEntity = request.Where(x => !previousEntity.Any(c => c.Id.Value == x.Id)).ToList();
        var deleteEntity = previousEntity.Where(x => !request.Any(c => c.Id == x.Id.Value)).ToList();


        foreach (var item in addEntity)
        {
            var entity = _supplierAddressBooks.Where(x => (x.SupplierId == new SupplierId(supplierId) && x.Id == new SupplierAddressBookId((long)item.Id)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                entity.ChangeStatus(ActiveStatusEnum.Active, (long)request[0].CreatedBy);
            }
            else
            {
                entity = await SupplierAddressBook.Create(item);
                _supplierAddressBooks.Add(entity);
            }
        }

        foreach (var item in deleteEntity)
        {
            item.Delete((long)request[0].CreatedBy);
        }
    }
    public async Task AddPhoneBook(List<CreateSupplierPhoneBookArg> request, long supplierId)
    {
        supplierId.NullCheck();

        var previousEntity = _supplierPhoneBooks.Where(x => x.SupplierId == new SupplierId(supplierId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addEntity = request.Where(x => !previousEntity.Any(c => c.Id.Value == x.Id)).ToList();
        var deleteEntity = previousEntity.Where(x => !request.Any(c => c.Id == x.Id.Value)).ToList();


        foreach (var item in addEntity)
        {
            var entity = _supplierPhoneBooks.Where(x => (x.SupplierId == new SupplierId(supplierId) && x.Id == new SupplierPhoneBookId((long)item.Id)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                entity.ChangeStatus(ActiveStatusEnum.Active, (long)request[0].CreatedBy);
            }
            else
            {
                entity = await SupplierPhoneBook.Create(item);
                _supplierPhoneBooks.Add(entity);
            }
        }

        foreach (var item in deleteEntity)
        {
            item.Delete((long)request[0].CreatedBy);
        }
    }

    #endregion

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
        IsInBlackList = arg.IsInBlackList;
        SuccessOrderCountinTheYear = arg.SuccessOrderCountinTheYear;
        NationalCode = arg.NationalCode;
        NationalId = arg.NationalId;
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
        if (!string.IsNullOrEmpty(arg.NationalCode))
        {
            var validationResult = arg.NationalCode.IsNationalID();
            if (!validationResult) throw SupplierExceptions.NationalCodeException;
        }
        if (!string.IsNullOrEmpty(arg.NationalId))
            if (arg.NationalId.Length > 11 & arg.NationalId.Length < 10)
                throw SupplierExceptions.NationalIdException;
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifySupplierArg arg, ISupplierDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        if (!string.IsNullOrEmpty(arg.NationalCode))
        {
            var validationResult = arg.NationalCode.IsNationalID();
            if (!validationResult) throw SupplierExceptions.NationalCodeException;
        }
        if (!string.IsNullOrEmpty(arg.NationalId))
            if (arg.NationalId.Length > 11 & arg.NationalId.Length < 10)
                throw SupplierExceptions.NationalIdException;
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public SupplierId Id { get; private set; }
    public SupplierRankId SupplierRankId { get; private set; }
    public virtual SupplierRank SupplierRank { get; private set; }
    public string? Code { get; private set; }
    public string? Name { get; private set; }
    public string? NationalCode { get; private set; }
    public string? NationalId { get; private set; }
    public string? IsInBlackList { get; private set; }
    public int SuccessOrderCountinTheYear { get; private set; }
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
        DeleteSupplierDocuments(userId);
    }
    public void AddSupplierDocuments(List<CreateSupplierDocumentArg> args)
    {
        foreach (var arg in args)
        {
            var entity = SupplierDocument.Create(arg);
            _supplierDocuments.Add(entity);
        }
    }
    public void DeleteSupplierDocuments(long userId)
    {
        foreach (var item in _supplierDocuments)
        {
            item.Delete(userId);
        }
    }
    public void ModifySupplierDocuments(List<CreateSupplierDocumentArg> args)
    {
        var activeEntities = _supplierDocuments.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.DocumentId == x.DocumentId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.DocumentId.Value == x.DocumentId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _supplierDocuments.FirstOrDefault(x => x.DocumentId.Value == arg.DocumentId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy.Value);
            }
            else
            {
                entity = SupplierDocument.Create(arg);
                _supplierDocuments.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy.Value);
        }
    }
    private List<SupplierBlackListHistory> _supplierBlackListHistories = new();
    public ICollection<SupplierBlackListHistory> SupplierBlackListHistories => _supplierBlackListHistories;
    private List<CandidatedSupplier> _candidatedSuppliers = new();
    public ICollection<CandidatedSupplier> CandidatedSuppliers => _candidatedSuppliers;
    private List<Asset> _assets = new();
    public ICollection<Asset> Assets => _assets;
    private List<ConfigurationItem> _configurationItems = new();
    public ICollection<ConfigurationItem> ConfigurationItems => _configurationItems;
    private List<ConfigurationItem> _licensedConfigurationItems = new();
    public ICollection<ConfigurationItem> LicensedConfigurationItems => _licensedConfigurationItems;
    private List<SupplierDocument> _supplierDocuments = new();
    public ICollection<SupplierDocument> SupplierDocuments => _supplierDocuments;

    private List<GoodsCategorySupplier> _goodsCategorySuppliers = new();
    public ICollection<GoodsCategorySupplier> GoodsCategorySuppliers => _goodsCategorySuppliers;

    private List<SupplierAddressBook> _supplierAddressBooks = new();
    public ICollection<SupplierAddressBook> SupplierAddressBooks => _supplierAddressBooks;

    private List<SupplierAccountList> _supplierAccountLists = new();
    public ICollection<SupplierAccountList> SupplierAccountLists => _supplierAccountLists;

    private List<SupplierPhoneBook> _supplierPhoneBooks = new();
    public ICollection<SupplierPhoneBook> SupplierPhoneBooks => _supplierPhoneBooks;
}
