using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.DMS.WorkFlowDocumentTypes;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Args;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Entities;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.DMS.WorkFlowDocumentTypes;

public class WorkFlowDocumentTypesCommandHandler : ICommandHandler<CreateWorkflowDocumentTypeCommand, Result<long>>,
    ICommandHandler<ModifyWorkflowDocumentTypeCommand, Result<long>>, ICommandHandler<DeleteWorkflowDocumentTypeCommand, Result<long>>
{
    private readonly IWorkflowDocumentTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWorkflowDocumentTypeDomainService _domainService;
    private readonly ISimaIdentity _simaIdentity;

    public WorkFlowDocumentTypesCommandHandler(IWorkflowDocumentTypeRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IWorkflowDocumentTypeDomainService domainService, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _domainService = domainService;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateWorkflowDocumentTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateWorkFlowDocumentTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await WorkflowDocumentType.Create(arg, _domainService);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyWorkflowDocumentTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyWorkFlowDocumentTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _domainService);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteWorkflowDocumentTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
