using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BranchManagement.Branches;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Args;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BranchManagement.Branches;

public class BranchCommandHandler : ICommandHandler<CreateBranchCommand, Result<long>>, ICommandHandler<ModifyBranchCommand, Result<long>>,
    ICommandHandler<DeleteBranchCommand, Result<long>>
{
    private readonly IBranchRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IBranchDomainService _domainService;

    public BranchCommandHandler(IBranchRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IBranchDomainService domainService)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _domainService = domainService;
    }
    public async Task<Result<long>> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateBranchArg>(request);
        var entity = await Branch.Create(arg, _domainService);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyBranchCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyBranchArg>(request);
        await entity.Modify(arg, _domainService);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}
