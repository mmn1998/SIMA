using AutoMapper;
using SIMA.Application.Contract.Features.RiskManagers.MatrixAs;
using SIMA.Application.Contract.Features.RiskManagers.MatrixAValues;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Args;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Args;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;

namespace SIMA.Application.Feaatures.RiskManagers.MatrixAValues;

public class MatrixAValueCommandHandler: ICommandHandler<CreateMatrixAValuesCommand, Result<long>>, ICommandHandler<ModifyMatrixAValuesCommand, Result<long>>
    , ICommandHandler<DeleteMatrixAValuesCommand, Result<long>>

{
    
    private readonly IMatrixAValueRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMatrixAValueDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public MatrixAValueCommandHandler(IMatrixAValueRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IMatrixAValueDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }

    public async Task<Result<long>> Handle(CreateMatrixAValuesCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateMatrixAValueArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await MatrixAValue.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyMatrixAValuesCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyMatrixAValueArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteMatrixAValuesCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}