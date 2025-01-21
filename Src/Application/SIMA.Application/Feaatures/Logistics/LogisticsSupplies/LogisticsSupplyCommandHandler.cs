using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Logistics.LogisticsSupplies;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Interfaces;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Args;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Contracts;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Logistics.LogisticsSupplies;

public class LogisticsSupplyCommandHandler : ICommandHandler<CreateLogisticsSupplyCommand, Result<long>>, ICommandHandler<ModifyLogisticsSupplyCommand, Result<long>>, ICommandHandler<DeleteLogisticsSupplyCommand, Result<long>>
{
    private readonly ILogisticsSupplyRepository _repository;
    private readonly IIssueRepository _issueRepository;
    private readonly ILogisticsSupplyDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public LogisticsSupplyCommandHandler(ILogisticsSupplyRepository repository, ILogisticsSupplyDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper, IIssueRepository issueRepository)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _issueRepository = issueRepository;
        _mapper = mapper;
    }

    public async Task<Result<long>> Handle(CreateLogisticsSupplyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var arg = _mapper.Map<CreateLogisticsSupplyArg>(request);
            if (!string.IsNullOrEmpty(request.IssueInforamation.DueDate))
                arg.DueDate = request.IssueInforamation.DueDate.ToMiladiDate();
            arg.CreatedBy = _simaIdentity.UserId;
            #region GenerateCode

            var lastRequest = await _repository.GetLastLogisticsSupply();

            if (lastRequest is not null)
                arg.Code = (Convert.ToInt32(lastRequest.Code) + 1).ToString();
            else
                arg.Code = "101";

            #endregion
            var entity = await LogisticsSupply.Create(arg, _service);

            if (request.DocumentList is not null)
            {
                var docs = _mapper.Map<List<CreateLogisticsSupplyDocumentArg>>(request.DocumentList);
                foreach (var doc in docs)
                {
                    doc.CreatedBy = _simaIdentity.UserId;
                    doc.LogisticsSupplyId = arg.Id;
                }
                entity.AddLogisticsSupplyDocuments(docs);
            }
            if (request.LogisticsRequestGoodsList is not null)
            {
                var docs = _mapper.Map<List<CreateLogisticsSupplyGoodsArg>>(request.LogisticsRequestGoodsList);
                foreach (var doc in docs)
                {
                    doc.CreatedBy = _simaIdentity.UserId;
                    doc.LogisticsSupplyId = arg.Id;
                }
                entity.AddLogisticsSupplyGoods(docs);
            }

            await _repository.Add(entity);

            await _unitOfWork.SaveChangesAsync();

            return Result.Ok(entity.Id.Value);
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    public async Task<Result<long>> Handle(ModifyLogisticsSupplyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var issueEntity = await _issueRepository.GetIssueBySourceId(request.Id, MainAggregateEnums.LogisticsSupply);
        var arg = _mapper.Map<ModifyLogisticsSupplyArg>(request);
        if (!string.IsNullOrEmpty(request.IssueInforamation.DueDate))
            arg.DueDate = request.IssueInforamation.DueDate.ToMiladiDate() ?? DateTime.Now;
        arg.IssueId = issueEntity.Id.Value;
        arg.Code = entity.Code;
        arg.ModifiedBy = _simaIdentity.UserId;
        if (request.DocumentList is not null)
        {
            var docs = _mapper.Map<List<CreateLogisticsSupplyDocumentArg>>(request.DocumentList);
            foreach (var doc in docs)
            {
                doc.CreatedBy = _simaIdentity.UserId;
                doc.LogisticsSupplyId = arg.Id;
            }
            entity.ModifyLogisticsSupplyDocuments(docs);
        }
        if (request.LogisticsRequestGoodsList is not null)
        {
            var docs = _mapper.Map<List<CreateLogisticsSupplyGoodsArg>>(request.LogisticsRequestGoodsList);
            foreach (var doc in docs)
            {
                doc.CreatedBy = _simaIdentity.UserId;
                doc.LogisticsSupplyId = arg.Id;
            }
            entity.ModifyLogisticsSupplyGoods(docs);
        }
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok(entity.Id.Value);

    }

    public async Task<Result<long>> Handle(DeleteLogisticsSupplyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var issueEntity = await _issueRepository.GetIssueBySourceId(request.Id, MainAggregateEnums.LogisticsSupply);

        entity.Delete(_simaIdentity.UserId, issueEntity.Id.Value);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok(entity.Id.Value);
    }
}
