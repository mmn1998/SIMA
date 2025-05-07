using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.ReconsilationTypes;
using SIMA.Domain.Models.Features.TrustyDrafts.ReconsilationTypes.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.ReconsilationTypes.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.ReconsilationTypes.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.ReconsilationTypes;

public class ReconsilationTypeCommandHandler : ICommandHandler<CreateReconsilationTypeCommand, Result<long>>,
    ICommandHandler<ModifyReconsilationTypeCommand, Result<long>>, ICommandHandler<DeleteReconsilationTypeCommand, Result<long>>
{
    private readonly IReconsilationTypeRepository _repository;
    private readonly IReconsilationTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ReconsilationTypeCommandHandler(IReconsilationTypeRepository repository, IReconsilationTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateReconsilationTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateReconsilationTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ReconsilationType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyReconsilationTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyReconsilationTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteReconsilationTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}