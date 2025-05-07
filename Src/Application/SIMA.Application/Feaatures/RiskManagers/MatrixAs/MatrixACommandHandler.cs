using AutoMapper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.RiskManagers.MatrixAs;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Args;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Entities;

namespace SIMA.Application.Feaatures.RiskManagers.MatrixAs;

public class MatrixACommandHandler : ICommandHandler<CreateMatrixACommand, Result<long>>, ICommandHandler<ModifyMatrixACommand, Result<long>>
, ICommandHandler<DeleteMatrixACommand, Result<long>>
{
    private readonly IMatrixARepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMatrixADomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public MatrixACommandHandler(IMatrixARepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IMatrixADomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateMatrixACommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateMatrixAArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await MatrixA.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyMatrixACommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyMatrixAArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteMatrixACommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}