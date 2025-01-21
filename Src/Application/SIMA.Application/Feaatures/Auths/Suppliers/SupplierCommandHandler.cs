using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.Suppliers;
using SIMA.Domain.Models.Features.Auths.Suppliers.Args;
using SIMA.Domain.Models.Features.Auths.Suppliers.Contracts;
using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.Suppliers;

public class SupplierCommandHandler : ICommandHandler<CreateSupplierCommand, Result<long>>,
    ICommandHandler<ModifySupplierCommand, Result<long>>, ICommandHandler<DeleteSupplierCommand, Result<long>>
{
    private readonly ISupplierRepository _repository;
    private readonly ISupplierDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public SupplierCommandHandler(ISupplierRepository repository, ISupplierDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateSupplierArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Supplier.Create(arg, _service);

        #region Add Account List

        var AccountArgs = _mapper.Map<List<CreateSupplierAccountListArg>>(request.AccountList);
        AccountArgs.ForEach(x => x.CreatedBy = _simaIdentity.UserId);
        await entity.AddAccountList(AccountArgs, entity.Id.Value);

        #endregion

        #region Add Account List

        var AddressBookArgs = _mapper.Map<List<CreateSupplierAddressBookArg>>(request.AddressBooks);
        AddressBookArgs.ForEach(x => x.CreatedBy = _simaIdentity.UserId);
        await entity.AddAddressBook(AddressBookArgs, entity.Id.Value);

        #endregion

        #region Add Account List

        var PhoneBookArgs = _mapper.Map<List<CreateSupplierPhoneBookArg>>(request.PhoneBooks);
        PhoneBookArgs.ForEach(x => x.CreatedBy = _simaIdentity.UserId);
        await entity.AddPhoneBook(PhoneBookArgs, entity.Id.Value);

        #endregion


        #region AddSupplierDocuments
        if (request.DocumentList is not null)
        {
            var userId = _simaIdentity.UserId;
            var supplierId = entity.Id.Value;
            var documentArgs = _mapper.Map<List<CreateSupplierDocumentArg>>(request.DocumentList);
            foreach (var item in documentArgs)
            {
                item.SupplierId = supplierId;
                item.CreatedBy = userId;
            }
            entity.AddSupplierDocuments(documentArgs);
        }
        #endregion

        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifySupplierCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new SupplierId(request.Id));
        var arg = _mapper.Map<ModifySupplierArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;

        #region Add Account List

        var AccountArgs = _mapper.Map<List<CreateSupplierAccountListArg>>(request.AccountList);
        AccountArgs.ForEach(x => x.CreatedBy = _simaIdentity.UserId);
        await entity.AddAccountList(AccountArgs, entity.Id.Value);

        #endregion

        #region Add Account List

        var AddressBookArgs = _mapper.Map<List<CreateSupplierAddressBookArg>>(request.AddressBooks);
        AddressBookArgs.ForEach(x => x.CreatedBy = _simaIdentity.UserId);
        await entity.AddAddressBook(AddressBookArgs, entity.Id.Value);

        #endregion

        #region Add Account List

        var PhoneBookArgs = _mapper.Map<List<CreateSupplierPhoneBookArg>>(request.PhoneBooks);
        PhoneBookArgs.ForEach(x => x.CreatedBy = _simaIdentity.UserId);
        await entity.AddPhoneBook(PhoneBookArgs, entity.Id.Value);

        #endregion

        #region AddSupplierDocuments
        if (request.DocumentList is not null)
        {
            var userId = _simaIdentity.UserId;
            var supplierId = entity.Id.Value;
            var documentArgs = _mapper.Map<List<CreateSupplierDocumentArg>>(request.DocumentList);
            foreach (var item in documentArgs)
            {
                item.SupplierId = supplierId;
                item.CreatedBy = userId;
            }
            entity.ModifySupplierDocuments(documentArgs);
        }
        #endregion

        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new SupplierId(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}