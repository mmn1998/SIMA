using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.DMS.DocumentTypes;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.Args;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.DMS.DocumentTypes;

public class DocumentTypeCommandHandler : ICommandHandler<CreateDocumentTypeCommand, Result<long>>, ICommandHandler<ModifyDocumentTypeCommand, Result<long>>,
    ICommandHandler<DeleteDocumentTypeCommand, Result<long>>
{
    private readonly IDocumentTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDocumentTypeDomainService _domainService;
    private readonly IMapper _mapper;
    private readonly ISimaIdentity _simaIdentity;

    public DocumentTypeCommandHandler(IDocumentTypeRepository repository, IUnitOfWork unitOfWork,
        IDocumentTypeDomainService domainService, IMapper mapper, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _domainService = domainService;
        _mapper = mapper;
        _simaIdentity = simaIdentity;
    }

    public async Task<Result<long>> Handle(CreateDocumentTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateDocumentTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await DocumentType.Create(arg, _domainService);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(ModifyDocumentTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyDocumentTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _domainService);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
    public async Task<Result<long>> Handle(DeleteDocumentTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);

    }
}
