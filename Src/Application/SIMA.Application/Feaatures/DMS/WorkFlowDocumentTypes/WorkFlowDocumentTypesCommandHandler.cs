using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.DMS.WorkFlowDocumentTypes;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Args;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Entities;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.DMS.WorkFlowDocumentTypes;

public class WorkFlowDocumentTypesCommandHandler : ICommandHandler<CreateWorkflowDocumentTypeCommand, Result<long>>,
    ICommandHandler<ModifyWorkflowDocumentTypeCommand, Result<long>>, ICommandHandler<DeleteWorkflowDocumentTypeCommand, Result<long>>
{
    private readonly IWorkflowDocumentTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWorkflowDocumentTypeDomainService _domainService;

    public WorkFlowDocumentTypesCommandHandler(IWorkflowDocumentTypeRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IWorkflowDocumentTypeDomainService domainService)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _domainService = domainService;
    }
    public async Task<Result<long>> Handle(CreateWorkflowDocumentTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateWorkFlowDocumentTypeArg>(request);
        var entity = await WorkflowDocumentType.Create(arg, _domainService);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyWorkflowDocumentTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyWorkFlowDocumentTypeArg>(request);
        await entity.Modify(arg, _domainService);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteWorkflowDocumentTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Deactive();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
