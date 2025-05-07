using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftCurrencyOrigins;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftCurrencyOrigins.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftCurrencyOrigins.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftCurrencyOrigins.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.DraftCurrencyOrigins;

public class DraftCurrencyOriginCommandHandler : ICommandHandler<CreateDraftCurrencyOriginCommand, Result<long>>,
    ICommandHandler<ModifyDraftCurrencyOriginCommand, Result<long>>, ICommandHandler<DeleteDraftCurrencyOriginCommand, Result<long>>
{
    private readonly IDraftCurrencyOriginRepository _repository;
    private readonly IDraftCurrencyOriginDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public DraftCurrencyOriginCommandHandler(IDraftCurrencyOriginRepository repository, IDraftCurrencyOriginDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateDraftCurrencyOriginCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateDraftCurrencyOriginArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await DraftCurrencyOrigin.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyDraftCurrencyOriginCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyDraftCurrencyOriginArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteDraftCurrencyOriginCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}