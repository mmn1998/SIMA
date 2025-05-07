using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.DMS.DocumentExtensions;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.Args;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using System.Globalization;

namespace SIMA.Application.Feaatures.DMS.DocumentsExtensions;

public class DocumentExtensionHandler : ICommandHandler<CreateDocumentExtensionCommand, Result<long>>, ICommandHandler<ModifyDocumentExtensionCommand, Result<long>>,
    ICommandHandler<DeleteDocumentExtensionCommand, Result<long>>
{
    private readonly IDocumentExtensionRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDocumentExtensionDomainService _domainService;
    private readonly IMapper _mapper;
    private readonly ISimaIdentity _simaIdentity;

    public DocumentExtensionHandler(IDocumentExtensionRepository repository, IUnitOfWork unitOfWork,
        IDocumentExtensionDomainService domainService, IMapper mapper, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _domainService = domainService;
        _mapper = mapper;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateDocumentExtensionCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateDocumentExtensionArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await DocumentExtension.Create(arg, _domainService);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyDocumentExtensionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyDocumentExtensionArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _domainService);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteDocumentExtensionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
