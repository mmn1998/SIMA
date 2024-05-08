using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.IssueManagement.IssueTypes;
using SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Args;
using SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.IssueManagement.IssueTypes;

public class IssueTypeCommandHandler : ICommandHandler<CreateIssueTypeCommand, Result<long>>, ICommandHandler<ModifyIssueTypeCommand, Result<long>>
    , ICommandHandler<DeleteIssueTypeCommand, Result<long>>
{
    private readonly IIssueTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIssueTypeDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public IssueTypeCommandHandler(IIssueTypeRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IIssueTypeDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateIssueTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateIssueTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await IssueType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyIssueTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyIssueTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteIssueTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}