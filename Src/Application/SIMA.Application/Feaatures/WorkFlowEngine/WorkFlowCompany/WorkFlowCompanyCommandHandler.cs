using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlowCompany;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.Interface;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.WorkFlowEngine.WorkFlowCompany;

public class WorkFlowCompanyCommandHandler : ICommandHandler<CreateWorkFlowCompanyCommand, Result<long>>, ICommandHandler<DeleteWorkFlowCompanyCommand, Result<long>>, ICommandHandler<ModifyWorkFlowCompanyCommand, Result<long>>
{
    private readonly IWorkFlowCompanyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISimaIdentity _simaIdentity;

    public WorkFlowCompanyCommandHandler(IWorkFlowCompanyRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _simaIdentity = simaIdentity;
    }

    public async Task<Result<long>> Handle(CreateWorkFlowCompanyCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateWorkFlowCompanyArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.Entities.WorkFlowCompany.New(arg);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(ModifyWorkFlowCompanyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyWorkFlowCompanyArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(DeleteWorkFlowCompanyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}
