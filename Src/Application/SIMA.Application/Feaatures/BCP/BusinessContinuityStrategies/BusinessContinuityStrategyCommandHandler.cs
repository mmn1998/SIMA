using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.BusinessContinuityStrategies;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgySolutions.Args;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.BusinessContinuityStrategies;

public class BusinessContinuityStrategyCommandHandler : ICommandHandler<CreateBusinessContinuityStrategyCommand, Result<long>>,
    ICommandHandler<ModifyBusinessContinuityStrategyCommand, Result<long>>, ICommandHandler<DeleteBusinessContinuityStrategyCommand, Result<long>>
{
    private readonly IBusinessContinuityStategyRepository _repository;
    private readonly IBusinessContinuityStategyDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public BusinessContinuityStrategyCommandHandler(IBusinessContinuityStategyRepository repository, IBusinessContinuityStategyDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateBusinessContinuityStrategyCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateBusinessContinuityStategyArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        
        var entity = await BusinessContinuityStrategy.Create(arg, _service);
        #region RelatedEntities

        if (request.BusinessContinuityStrategyObjectiveList is not null)
        {
            var args = _mapper.Map<List<CreateBusinessContinuityStrategyObjectiveArg>>(request.BusinessContinuityStrategyObjectiveList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.BusinessContinuityStategyId = arg.Id;
            }
            await entity.AddBusinessContinuityStrategyObjectives(args, _service);
        }
        if (request.BusinessContinuityStrategySolutionList is not null)
        {
            var args = _mapper.Map<List<CreateBusinessContinuityStratgySolutionArg>>(request.BusinessContinuityStrategySolutionList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.BusinessContinuityStratgyId = arg.Id;
            }
            await entity.AddBusinessContinuityStrategySolutions(args, _service);
        }
        if (request.BusinessContinuityStrategyDocumentList is not null)
        {
            var args = _mapper.Map<List<CreateBusinessContinuityStrategyDocumentArg>>(request.BusinessContinuityStrategyDocumentList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.BusinessContinuityStategyId = arg.Id;
            }
            entity.AddBusinessContinuityStrategyDocuments(args);
        }
        //if (request.BusinessContinuityStrategyResponsibleList is not null)
        //{
        //    var args = _mapper.Map<List<CreateBusinessContinuityStratgyResponsibleArg>>(request.BusinessContinuityStrategyResponsibleList);
        //    foreach (var item in args)
        //    {
        //        item.CreatedBy = _simaIdentity.UserId;
        //        item.BusinessContinuityStrategyId = arg.Id;
        //    }
        //    entity.AddBusinessContinuityStrategyResponsibles(args);
        //}
        var relatedIssueArg = _mapper.Map<CreateBusinessContinuityStrategyIssueArg>(arg);
        relatedIssueArg.BusinessContinuityStategyId = arg.Id;
        relatedIssueArg.CreatedBy = _simaIdentity.UserId;
        entity.AddBusinessContinuityStrategyRelatedIssues(relatedIssueArg);
        #endregion
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyBusinessContinuityStrategyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyBusinessContinuityStategyArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        #region RelatedEntities

        if (request.BusinessContinuityStrategyObjectiveList is not null)
        {
            var args = _mapper.Map<List<CreateBusinessContinuityStrategyObjectiveArg>>(request.BusinessContinuityStrategyObjectiveList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.BusinessContinuityStategyId = request.Id;
            }
            await entity.ModifyBusinessContinuityStrategyObjectives(args, _service);
        }
        if (request.BusinessContinuityStrategySolutionList is not null)
        {
            var args = _mapper.Map<List<CreateBusinessContinuityStratgySolutionArg>>(request.BusinessContinuityStrategySolutionList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.BusinessContinuityStratgyId = request.Id;
            }
            await entity.ModifyBusinessContinuityStrategySolutions(args, _service);
        }
        if (request.BusinessContinuityStrategyDocumentList is not null)
        {
            var args = _mapper.Map<List<CreateBusinessContinuityStrategyDocumentArg>>(request.BusinessContinuityStrategyDocumentList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.BusinessContinuityStategyId = request.Id;
            }
            entity.ModifyBusinessContinuityStrategyDocuments(args);
        }
        //if (request.BusinessContinuityStrategyResponsibleList is not null)
        //{
        //    var args = _mapper.Map<List<CreateBusinessContinuityStratgyResponsibleArg>>(request.BusinessContinuityStrategyResponsibleList);
        //    foreach (var item in args)
        //    {
        //        item.CreatedBy = _simaIdentity.UserId;
        //        item.BusinessContinuityStrategyId = request.Id;
        //    }
        //    entity.ModifyBusinessContinuityStrategyResponsibles(args);
        //}
        #endregion
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteBusinessContinuityStrategyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId;
        entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}