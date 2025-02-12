using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.BiaValues;
using SIMA.Domain.Models.Features.BCP.BiaValues.Args;
using SIMA.Domain.Models.Features.BCP.BiaValues.Contracts;
using SIMA.Domain.Models.Features.BCP.BiaValues.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.BiaValues;

public class BiaValueCommandHandler : ICommandHandler<CreateBiaValueCommand, Result<long>>,
    ICommandHandler<ModifyBiaValueCommand, Result<long>>, ICommandHandler<DeleteBiaValueCommand, Result<long>>
{
    private readonly IBiaValueRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public BiaValueCommandHandler(IBiaValueRepository repository,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateBiaValueCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateBiaValueArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = BiaValue.Create(arg);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyBiaValueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyBiaValueArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        entity.Modify(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteBiaValueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}