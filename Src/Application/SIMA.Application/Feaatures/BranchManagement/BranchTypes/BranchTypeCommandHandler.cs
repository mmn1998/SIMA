using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BranchManagement.BranchTypes;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Args;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BranchManagement.BranchTypes;

public class BranchTypeCommandHandler : ICommandHandler<CreateBranchTypeCommand, Result<long>>, ICommandHandler<ModifyBranchTypeCommand, Result<long>>, ICommandHandler<DeleteBranchTypeCommand, Result<long>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBranchTypeRepository _repository;
    private readonly IBranchTypeDomainService _domainService;
    private readonly ISimaIdentity _simaIdentity;

    public BranchTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IBranchTypeRepository repository, IBranchTypeDomainService domainService,
        ISimaIdentity simaIdentity)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _repository = repository;
        _domainService = domainService;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateBranchTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateBranchTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await BranchType.Create(arg, _domainService);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(ModifyBranchTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyBranchTypeArg>(request);
        arg.ModifyBy = _simaIdentity.UserId;
        await entity.Modify(arg, _domainService);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(DeleteBranchTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}
