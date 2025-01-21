using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BCP.BusinesImpactAnalysises;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Contracts;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BCP.BusinesImpactAnalysises;

public class BusinessImpactAnalysisCommandHandler : ICommandHandler<CreateBusinessImpactAnalysisCommand, Result<long>>,
    ICommandHandler<ModifyBusinessImpactAnalysisCommand, Result<long>>, ICommandHandler<DeleteBusinessImpactAnalysisCommand, Result<long>>
{
    private readonly IBusinessImpactAnalysisRepository _repository;
    private readonly IBusinessImpactAnalysisDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public BusinessImpactAnalysisCommandHandler(IBusinessImpactAnalysisRepository repository, IBusinessImpactAnalysisDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateBusinessImpactAnalysisCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var arg = _mapper.Map<CreateBusinessImpactAnalysisArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await BusinessImpactAnalysis.Create(arg, _service);
            #region MyRegion

            #endregion
            if (request.BusinessImpactAnalysisAssetList is not null)
            {
                var args = _mapper.Map<List<CreateBusinessImpactAnalysisAssetArg>>(request.BusinessImpactAnalysisAssetList);
                foreach (var item in args)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                    item.BusinessImpactAnalysisId = arg.Id;
                }
                entity.AddBusinessImpactAnalysisAssets(args);
            }
            //if (request.BusinessImpactAnalysisStaffList is not null)
            //{
            //    var args = _mapper.Map<List<CreateBusinessImpactAnalysisStaffArg>>(request.BusinessImpactAnalysisStaffList);
            //    foreach (var item in args)
            //    {
            //        item.CreatedBy = _simaIdentity.UserId;
            //        item.BusinessImpactAnalysisId = arg.Id;
            //    }
            //    entity.AddBusinessImpactAnalysisStaffs(args);
            //}
            if (request.BusinessImpactAnalysisDocumentList is not null)
            {
                var args = _mapper.Map<List<CreateBusinessImpactAnalysisDocumentArg>>(request.BusinessImpactAnalysisDocumentList);
                foreach (var item in args)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                    item.BusinessImpactAnalysisId = arg.Id;
                }
                entity.AddBusinessImpactAnalysisDocuments(args);
            }
            if (request.BusinessImpactAnalysisDisasterOriginList is not null)
            {
                var args = _mapper.Map<List<CreateBusinessImpactAnalysisDisasterOriginArg>>(request.BusinessImpactAnalysisDisasterOriginList);
                foreach (var item in args)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                    item.BusinessImpactAnalysisId = arg.Id;
                }
                entity.AddBusinessImpactAnalysisDisasterOrigins(args);
            }
            var relatedIssueArg = _mapper.Map<CreateBusinessImpactAnalysisIssueArg>(arg);
            relatedIssueArg.BusinessImpactAnalysisId = arg.Id;
            relatedIssueArg.CreatedBy = _simaIdentity.UserId;
            entity.AddBusinessImpactAnalysisIssues(relatedIssueArg);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(arg.Id);
        }
        catch (Exception e)
        {

            throw;
        }
    }

    public async Task<Result<long>> Handle(ModifyBusinessImpactAnalysisCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyBusinessImpactAnalysisArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        if (request.BusinessImpactAnalysisAssetList is not null)
        {
            var args = _mapper.Map<List<CreateBusinessImpactAnalysisAssetArg>>(request.BusinessImpactAnalysisAssetList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.BusinessImpactAnalysisId = arg.Id;
            }
            entity.ModifyBusinessImpactAnalysisAssets(args);
        }
        //if (request.BusinessImpactAnalysisStaffList is not null)
        //{
        //    var args = _mapper.Map<List<CreateBusinessImpactAnalysisStaffArg>>(request.BusinessImpactAnalysisStaffList);
        //    foreach (var item in args)
        //    {
        //        item.CreatedBy = _simaIdentity.UserId;
        //        item.BusinessImpactAnalysisId = arg.Id;
        //    }
        //    entity.ModifyBusinessImpactAnalysisStaffs(args);
        //}
        if (request.BusinessImpactAnalysisDocumentList is not null)
        {
            var args = _mapper.Map<List<CreateBusinessImpactAnalysisDocumentArg>>(request.BusinessImpactAnalysisDocumentList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.BusinessImpactAnalysisId = arg.Id;
            }
            entity.ModifyBusinessImpactAnalysisDocuments(args);
        }
        if (request.BusinessImpactAnalysisDisasterOriginList is not null)
        {
            var args = _mapper.Map<List<CreateBusinessImpactAnalysisDisasterOriginArg>>(request.BusinessImpactAnalysisDisasterOriginList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.BusinessImpactAnalysisId = arg.Id;
            }
            entity.ModifyBusinessImpactAnalysisDisasterOrigins(args);
        }
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteBusinessImpactAnalysisCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId;
        entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}