using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftTypes;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.DraftTypes;

public class DraftTypeCommandHandler : ICommandHandler<CreateDraftTypeCommand, Result<long>>,
    ICommandHandler<ModifyDraftTypeCommand, Result<long>>, ICommandHandler<DeleteDraftTypeCommand, Result<long>>
{
    private readonly IDraftTypeRepository _repository;
    private readonly IDraftTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public DraftTypeCommandHandler(IDraftTypeRepository repository, IDraftTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateDraftTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateDraftTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await DraftType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyDraftTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyDraftTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteDraftTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}