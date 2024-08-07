using AutoMapper;
using Newtonsoft.Json;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.WorkFlowEngine.Progress;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Interface;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.WorkFlowEngine.Progress;

public class ProgressCommandHandler : ICommandHandler<ModifyProgressCommand, Result<long>>
{
    private readonly IProgressRepository _progressRepository;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProgressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
        IProgressRepository progressRepository, ISimaIdentity simaIdentity)
    {
        _progressRepository = progressRepository;
        _simaIdentity = simaIdentity;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<long>> Handle(ModifyProgressCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _progressRepository.GetById(request.Id);
            var arg = _mapper.Map<ChangeStatusArg>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            entity.ChangeStatus(arg);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
