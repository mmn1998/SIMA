using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.IssueManagement.IssueWeightCategories;
using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Args;
using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.IssueManagement.IssueWeightCategories;

public class IssueWeightCategoryHandler : ICommandHandler<CreateIssueWeightCategoryCommand, Result<long>>, ICommandHandler<ModifyIssueWeightCategoryCommand, Result<long>>,
    ICommandHandler<DeleteIssueWeightCategoryCommand, Result<long>>
{
    private readonly IIssueWeightCategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIssueWeightCategoryDomainService _service;
    private readonly IMapper _mapper;

    public IssueWeightCategoryHandler(IIssueWeightCategoryRepository repository, IUnitOfWork unitOfWork,
        IIssueWeightCategoryDomainService service, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _service = service;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateIssueWeightCategoryCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateIssueWeightCategoryArg>(request);
        var entity = await IssueWeightCategory.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(ModifyIssueWeightCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyIssueWeightCategoryArg>(request.Id);
        entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
    public async Task<Result<long>> Handle(DeleteIssueWeightCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Deactive();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
