using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BranchManagement.Customers;
using SIMA.Domain.Models.Features.BranchManagement.Customers.Args;
using SIMA.Domain.Models.Features.BranchManagement.Customers.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.Customers.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BranchManagement.Customers;

public class CustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Result<long>>,
    ICommandHandler<ModifyCustomerCommand, Result<long>>, ICommandHandler<DeleteCustomerCommand, Result<long>>
{
    private readonly ICustomerRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;
    private readonly ICustomerDomainService _service ;

    public CustomerCommandHandler(ICustomerRepository repository,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper , ICustomerDomainService service)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
        _service = service;
    }
    public async Task<Result<long>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateCustomerArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Customer.Create(arg , _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyCustomerArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}