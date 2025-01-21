using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.RequestValors;
using SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.RequestValors;

public class RequestValorCommandHandler : ICommandHandler<CreateRequestValorCommand, Result<long>>,
    ICommandHandler<ModifyRequestValorCommand, Result<long>>, ICommandHandler<DeleteRequestValorCommand, Result<long>>
{
    private readonly IRequestValorRepository _repository;
    private readonly IRequestValorDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public RequestValorCommandHandler(IRequestValorRepository repository, IRequestValorDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateRequestValorCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateRequestValorArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await RequestValor.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyRequestValorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyRequestValorArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteRequestValorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}