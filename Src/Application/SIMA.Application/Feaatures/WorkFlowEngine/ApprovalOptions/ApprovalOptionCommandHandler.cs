using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.WorkFlowEngine.ApprovalOptions;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Interface;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.WorkFlowEngine.ApprovalOptions
{
    public class ApprovalOptionCommandHandler :ICommandHandler<CreateApprovalOptionCommand, Result<long>>, ICommandHandler<ModifyApprovalOptionCommand, Result<long>>
    , ICommandHandler<DeleteApprovalOptionCommand, Result<long>>
    {
        private readonly IApprovalOptionRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IApprovalOptionDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public ApprovalOptionCommandHandler(IApprovalOptionRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IApprovalOptionDomainService service, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateApprovalOptionCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateApprovalOptionArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await ApprovalOption.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyApprovalOptionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyApprovalOptionArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteApprovalOptionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
}
