using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.ImportanceDegrees;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Args;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Contracts;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Entities;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.ImportanceDegrees;

public class ImportanceDegreeCommandHandler : ICommandHandler<CreateImportanceDegreeCommand, Result<long>>,
    ICommandHandler<ModifyImportanceDegreeCommand, Result<long>>, ICommandHandler<DeleteImportanceDegreeCommand, Result<long>>
{
    private readonly IImportanceDegreeRepository _repository;
    private readonly IImportanceDegreeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ImportanceDegreeCommandHandler(IImportanceDegreeRepository repository, IImportanceDegreeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateImportanceDegreeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateImportanceDegreeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ImportanceDegree.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyImportanceDegreeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ImportanceDegreeId(request.Id));
        var arg = _mapper.Map<ModifyImportanceDegreeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteImportanceDegreeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new ImportanceDegreeId(request.Id));
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}