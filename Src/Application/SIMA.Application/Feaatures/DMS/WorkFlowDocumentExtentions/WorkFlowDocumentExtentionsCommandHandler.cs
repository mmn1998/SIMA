using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.DMS.WorkFlowDocumentExtentions;
using SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Args;
using SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Entities;
using SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;


namespace SIMA.Application.Feaatures.DMS.WorkFlowDocumentExtentions;

public class WorkFlowDocumentExtentionsCommandHandler : ICommandHandler<CreateWorkFlowDocumentExtentionCommand, Result<long>>,
    ICommandHandler<ModifyWorkFlowDocumentExtentionCommand, Result<long>>, ICommandHandler<DeleteWorkFlowDocumentExtentionCommand, Result<long>>
{
    private readonly IWorkflowDocumentExtensionRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWorkflowDocumentExtensionDomainService _domainService;
    private readonly ISimaIdentity _simaIdentity;

    public WorkFlowDocumentExtentionsCommandHandler(IWorkflowDocumentExtensionRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IWorkflowDocumentExtensionDomainService domainService, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _domainService = domainService;
        _simaIdentity = simaIdentity;
    }

    public async Task<Result<long>> Handle(CreateWorkFlowDocumentExtentionCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateWorkflowDocumentExtensionArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await WorkflowDocumentExtension.Create(arg, _domainService);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyWorkFlowDocumentExtentionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyWorkFlowDocumentExtensionArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _domainService);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteWorkFlowDocumentExtentionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
