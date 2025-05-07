using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.CancellationResaons;
using SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.CancellationResaons;

public class CancellationResaonCommandHandler : ICommandHandler<CreateCancellationResaonCommand, Result<long>>,
    ICommandHandler<ModifyCancellationResaonCommand, Result<long>>, ICommandHandler<DeleteCancellationResaonCommand, Result<long>>
{
    private readonly ICancellationResaonRepository _repository;
    private readonly ICancellationResaonDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public CancellationResaonCommandHandler(ICancellationResaonRepository repository, ICancellationResaonDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateCancellationResaonCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateCancellationResaonArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await CancellationResaon.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyCancellationResaonCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyCancellationResaonArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteCancellationResaonCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}