using AutoMapper;
using Microsoft.AspNetCore.Http;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.DMS.Documents;
using SIMA.Domain.Models.Features.DMS.Documents.Args;
using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.DMS.Documents;

public class DocumentCommandHandler : ICommandHandler<CreateDocumentCommand, Result<long>>, ICommandHandler<ModifyDocumentCommand, Result<long>>
    , ICommandHandler<DeleteDocumentCommand, Result<long>>
{
    private readonly IDocumentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IDocumentDomainService _service;

    public DocumentCommandHandler(IDocumentRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IDocumentDomainService service)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
    }
    public async Task<Result<long>> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateDocumentArg>(request);
        var entity = await Document.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyDocumentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyDocumentArg>(request);
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Deactive();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
    
}
