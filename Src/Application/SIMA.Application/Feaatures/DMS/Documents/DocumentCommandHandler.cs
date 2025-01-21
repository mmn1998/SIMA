using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.DMS.Documents;
using SIMA.Domain.Models.Features.DMS.Documents.Args;
using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using SIMA.Resources;

namespace SIMA.Application.Feaatures.DMS.Documents;

public class DocumentCommandHandler : ICommandHandler<CreateDocumentCommand, Result<long>>,
    ICommandHandler<MultiCreateDocumentCommand, Result<List<long>>>
    , ICommandHandler<ModifyDocumentCommand, Result<long>>
    , ICommandHandler<DeleteDocumentCommand, Result<long>>
{
    private readonly IDocumentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IDocumentDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public DocumentCommandHandler(IDocumentRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IDocumentDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateDocumentArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        arg.CompanyId = _simaIdentity.CompanyId;
        var entity = await Document.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyDocumentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyDocumentArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        arg.CompanyId = _simaIdentity.CompanyId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId;
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<List<long>>> Handle(MultiCreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var args = new List<CreateDocumentArg>();
        args = await Mappers.DocumentMapper.Map(request.Documents, _simaIdentity.UserId, _simaIdentity.CompanyId);
        List<long> documentIds = new List<long>();
        foreach (var arg in args)
        {
            var entity = await Document.Create(arg, _service);
            documentIds.Add(entity.Id.Value);
            await _repository.Add(entity);
        }
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(documentIds);
    }
}
