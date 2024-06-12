using AutoMapper;
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
            //var v1 = new List<ProgressStoreProcedureArg>();
            //foreach (var item in request.StoreProcedures)
            //{
            //    var v2 = _mapper.Map<ProgressStoreProcedureArg>(item);
            //    foreach (var item2 in item.Params)
            //    {
            //        var v3 = _mapper.Map<List<ProgressStoreProcedureParamArg>>(item2);
            //        v2.ProgressStoreProcedureParams = v3;
            //    }
            //    v1.Add(v2);
            //}
            //arg.ProgressStoreProcedures = v1;
            //foreach (var procedure in arg.ProgressStoreProcedures)
            //{
            //    procedure.ProgressStoreProcedureParams = _mapper.Map<List<ProgressStoreProcedureParamArg>>(procedure.ProgressStoreProcedureParams);
            //}
            //foreach (var item in request.StoreProcedures)
            //{
            //    foreach (var item2 in item.Params)
            //    {
            //        foreach (var item3 in arg.ProgressStoreProcedures)
            //        {
            //            item3.ProgressStoreProcedureParams = _mapper.Map<List<ProgressStoreProcedureParamArg>>(item.Params);
            //        }
            //        break;
            //    }
            //}
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
