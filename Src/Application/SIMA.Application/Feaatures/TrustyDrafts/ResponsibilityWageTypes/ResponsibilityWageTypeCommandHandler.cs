using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.ResponsibilityWageTypes;
using SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.ResponsibilityWageTypes;

public class ResponsibilityWageTypeCommandHandler : ICommandHandler<CreateResponsibilityWageTypeCommand, Result<long>>,
    ICommandHandler<ModifyResponsibilityWageTypeCommand, Result<long>>, ICommandHandler<DeleteResponsibilityWageTypeCommand, Result<long>>
{
    private readonly IResponsibilityWageTypeRepository _repository;
    private readonly IResponsibilityWageTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ResponsibilityWageTypeCommandHandler(IResponsibilityWageTypeRepository repository, IResponsibilityWageTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateResponsibilityWageTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateResponsibilityWageTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ResponsibilityWageType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyResponsibilityWageTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyResponsibilityWageTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteResponsibilityWageTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}