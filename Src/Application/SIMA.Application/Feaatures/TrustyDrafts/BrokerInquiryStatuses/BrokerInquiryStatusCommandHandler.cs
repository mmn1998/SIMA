using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.BrokerInquiryStatuses;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.Contarcts;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.BrokerInquiryStatuses;

public class BrokerInquiryStatusCommandHandler : ICommandHandler<CreateBrokerInquiryStatusCommand, Result<long>>,
ICommandHandler<ModifyBrokerInquiryStatusCommand, Result<long>>, ICommandHandler<DeleteBrokerInquiryStatusCommand, Result<long>>
{
    private readonly IBrokerInquiryStatusRepository _repository;
    private readonly IBrokerInquiryStatusDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public BrokerInquiryStatusCommandHandler(IBrokerInquiryStatusRepository repository, IBrokerInquiryStatusDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateBrokerInquiryStatusCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateBrokerInquiryStatusArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await BrokerInquiryStatus.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyBrokerInquiryStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyBrokerInquiryStatusArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteBrokerInquiryStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}