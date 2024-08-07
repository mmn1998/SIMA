using AutoMapper;
using MediatR;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Logistics.LogisticRequests;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Interfaces;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Args;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Contracts;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Logistics.LogisticRequests;
public class LogisticRequestsCommandHandler : ICommandHandler<CreateLogisticRequestCommand, Result<long>>, ICommandHandler<ModifyLogisticsRequestCommand, Result<long>>, ICommandHandler<DeleteLogisticRequestCommand, Result<long>>
{
    private readonly ILogisticsRequestRepository _repository;
    private readonly IIssueRepository _issueRepository;
    private readonly ILogisticsRequestDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public LogisticRequestsCommandHandler(ILogisticsRequestRepository repository, ILogisticsRequestDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper, IIssueRepository issueRepository)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _issueRepository = issueRepository;
        _mapper = mapper;
    }

    public async Task<Result<long>> Handle(CreateLogisticRequestCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateLogisticsRequestArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        #region GenerateCode
        var lastRequest = await _repository.GetLastLogisticsRequest();
        if (lastRequest != null)
            arg.Code = (Convert.ToInt32(lastRequest.Code) + 1).ToString();
        else
            arg.Code = "101";
        #endregion
        var entity = await LogisticsRequest.Create(arg, _service);

        var goodsArg = _mapper.Map<List<CreateLogisticsRequestGoodsArg>>(request.GoodsList);
        await entity.AddRequestGoods(goodsArg, arg.Id, _simaIdentity.UserId, _service);

        if (request.Documents.Count > 0 && request.Documents is not null)
        {
            var docs = _mapper.Map<List<CreateLogisticsRequestDocumentArg>>(request.Documents);
            foreach (var doc in docs)
            {
                doc.CreatedBy = _simaIdentity.UserId;
            }
            await entity.AddLogisticsRequestDocument(docs, arg.Id, _simaIdentity.UserId);
        }

        await _repository.Add(entity);

        await _unitOfWork.SaveChangesAsync();

        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyLogisticsRequestCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var issueEntity = await _issueRepository.GetIssueBySourceId(request.Id, MainAggregateEnums.LogisticsRequest);
        var arg = _mapper.Map<ModifyLogisticsRequestArg>(request);
        arg.IssueId = issueEntity.Id.Value;
        arg.Code = entity.Code;
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);

        var goodsArg = _mapper.Map<List<CreateLogisticsRequestGoodsArg>>(request.GoodsList);

        foreach (var item in goodsArg)
        {
            item.CreatedBy = _simaIdentity.UserId;
        }
        await entity.AddRequestGoods(goodsArg, arg.Id, _simaIdentity.UserId, _service);

        var docs = _mapper.Map<List<CreateLogisticsRequestDocumentArg>>(request.Documents);
        await entity.AddLogisticsRequestDocument(docs, arg.Id, _simaIdentity.UserId);

        await _unitOfWork.SaveChangesAsync();

        return Result.Ok(entity.Id.Value);

    }

    public async Task<Result<long>> Handle(DeleteLogisticRequestCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var issueEntity = await _issueRepository.GetIssueBySourceId(request.Id, MainAggregateEnums.LogisticsRequest);

        await entity.DeleteRequestGoods(request.Id, _simaIdentity.UserId);
        await entity.DeleteLogisticsRequestDocument(request.Id, _simaIdentity.UserId);

        entity.Delete(issueEntity.Id.Value, _simaIdentity.UserId);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok(entity.Id.Value);
    }
}
