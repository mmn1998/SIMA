using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.BrokerSecondLevelAddressBooks;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.BrokerSecondLevelAddressBooks;

public class BrokerSecondLevelAddressBookCommandHandler : ICommandHandler<CreateBrokerSecondLevelAddressBookCommand, Result<long>>,
    ICommandHandler<ModifyBrokerSecondLevelAddressBookCommand, Result<long>>, ICommandHandler<DeleteBrokerSecondLevelAddressBookCommand, Result<long>>
{
    private readonly IBrokerSecondLevelAddressBookRepository _repository;
    private readonly IBrokerSecondLevelAddressBookDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public BrokerSecondLevelAddressBookCommandHandler(IBrokerSecondLevelAddressBookRepository repository, IBrokerSecondLevelAddressBookDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateBrokerSecondLevelAddressBookCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateBrokerSecondLevelAddressBookArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = BrokerSecondLevelAddressBook.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyBrokerSecondLevelAddressBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyBrokerSecondLevelAddressBookArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteBrokerSecondLevelAddressBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}