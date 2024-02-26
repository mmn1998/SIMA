using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BranchManagement.Brokers;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Args;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BranchManagement.Brokers;

public class BrokerCommandHandler : ICommandHandler<CreateBrokerCommand, Result<long>>, ICommandHandler<ModifyBrokerCommand, Result<long>>
    , ICommandHandler<DeleteBrokerCommand, Result<long>>
{
    private readonly IBrokerRepository _repository;
    private readonly IBrokerService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BrokerCommandHandler(IBrokerRepository repository, IBrokerService service,
        IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateBrokerCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateBrokerArg>(request);
        var entity = await Broker.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyBrokerCommand request, CancellationToken cancellationToken)
    {
        var brokerId = new BrokerId(request.Id);
        var entity = await _repository.GetById(brokerId);
        var arg = _mapper.Map<ModifyBrokerArg>(request);
        entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteBrokerCommand request, CancellationToken cancellationToken)
    {
        var brokerId = new BrokerId(request.Id);
        var entity = await _repository.GetById(brokerId);
        entity.Deactive();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
