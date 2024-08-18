using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.UserTypes;
using SIMA.Domain.Models.Features.Auths.UserTypes.Args;
using SIMA.Domain.Models.Features.Auths.UserTypes.Entities;
using SIMA.Domain.Models.Features.Auths.UserTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.ServiceCatalog.ServiceUserTypes;

public class UserTypeCommandHandler : ICommandHandler<CreateUserTypeCommand, Result<long>>,
    ICommandHandler<ModifyUserTypeCommand, Result<long>>, ICommandHandler<DeleteUserTypeCommand, Result<long>>
{
    private readonly IUserTypeRepository _repository;
    private readonly IUserTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public UserTypeCommandHandler(IUserTypeRepository repository, IUserTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateUserTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateUserTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await UserType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyUserTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new UserTypeId(request.Id));
        var arg = _mapper.Map<ModifyUserTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteUserTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new UserTypeId(request.Id));
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}