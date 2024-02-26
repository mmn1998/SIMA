using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.PhoneTypes;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.Args;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.Entities;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.Interfaces;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.Repositories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.PhoneTypes;

public class PhoneTypeCommandHandler : ICommandHandler<DeletePhoneTypeCommand, Result<long>>,
    ICommandHandler<CreatePhoneTypeCommand, Result<long>>, ICommandHandler<ModifyPhoneTypeCommand, Result<long>>
{
    private readonly IPhoneTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPhoneTypeService _service;

    public PhoneTypeCommandHandler(IPhoneTypeRepository repository,
        IUnitOfWork unitOfWork, IMapper mapper, IPhoneTypeService service)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
    }
    public async Task<Result<long>> Handle(CreatePhoneTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreatePhoneTypeArg>(request);
        var entity = await PhoneType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(ModifyPhoneTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyPhoneTypeArg>(request);
        entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(DeletePhoneTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}
