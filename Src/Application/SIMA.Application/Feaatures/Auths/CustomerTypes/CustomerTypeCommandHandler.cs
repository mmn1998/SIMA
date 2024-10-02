using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.CustomerTypes;
using SIMA.Domain.Models.Features.Auths.CustomerTypes.Args;
using SIMA.Domain.Models.Features.Auths.CustomerTypes.Entities;
using SIMA.Domain.Models.Features.Auths.CustomerTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.CustomerTypes;

public class CustomerTypeCommandHandler : ICommandHandler<CreateCustomerTypeCommand, Result<long>>,
    ICommandHandler<ModifyCustomerTypeCommand, Result<long>>, ICommandHandler<DeleteCustomerTypeCommand, Result<long>>
{
    private readonly ICustomerTypeRepository _repository;
    private readonly ICustomerTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public CustomerTypeCommandHandler(ICustomerTypeRepository repository, ICustomerTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateCustomerTypeCommand request, CancellationToken cancellationToken)
    {
       
            var arg = _mapper.Map<CreateCustomerTypeArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await CustomerType.Create(arg, _service);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyCustomerTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new CustomerTypeId(request.Id));
        var arg = _mapper.Map<ModifyCustomerTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteCustomerTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new CustomerTypeId(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}