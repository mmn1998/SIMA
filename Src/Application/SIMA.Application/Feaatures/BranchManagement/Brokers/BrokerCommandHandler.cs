using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.BranchManagement.Brokers;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Args;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.BranchManagement.Brokers;

public class BrokerCommandHandler : ICommandHandler<CreateBrokerCommand, Result<long>>, ICommandHandler<ModifyBrokerCommand, Result<long>>
    , ICommandHandler<DeleteBrokerCommand, Result<long>>
{
    private readonly IBrokerRepository _repository;
    private readonly IBrokerService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISimaIdentity _simaIdentity;

    public BrokerCommandHandler(IBrokerRepository repository, IBrokerService service,
        IUnitOfWork unitOfWork, IMapper mapper, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateBrokerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var arg = _mapper.Map<CreateBrokerArg>(request);
            var userId = _simaIdentity.UserId;
            arg.CreatedBy = userId;
            var entity = await Broker.Create(arg, _service);
            if (request.BrokerAddressBookList is not null)
            {
                var args = _mapper.Map<List<CreateBrokerAddressBookArg>>(request.BrokerAddressBookList);
                foreach (var related in args)
                {
                    related.BrokerId = arg.Id;
                    related.CreatedBy = userId;
                }
                entity.AddBrokerAddressBooks(args);
            }
            if (request.BrokerAccountBookList is not null)
            {
                var args = _mapper.Map<List<CreateBrokerAccountBookArg>>(request.BrokerAccountBookList);
                foreach (var related in args)
                {
                    related.BrokerId = arg.Id;
                    related.CreatedBy = userId;
                }
                entity.AddBrokerAccountBooks(args);
            }
            if (request.BrokerPhoneBookList is not null)
            {
                var args = _mapper.Map<List<CreateBrokerPhoneBookArg>>(request.BrokerPhoneBookList);
                foreach (var related in args)
                {
                    related.BrokerId = arg.Id;
                    related.CreatedBy = userId;
                }
                entity.AddBrokerPhoneBooks(args);
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

    public async Task<Result<long>> Handle(ModifyBrokerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _repository.GetById(new(request.Id));
            var arg = _mapper.Map<ModifyBrokerArg>(request);
            var userId = _simaIdentity.UserId;
            arg.ModifiedBy = userId;
            await entity.Modify(arg, _service);
            if (request.BrokerAddressBookList is not null)
            {
                var args = _mapper.Map<List<CreateBrokerAddressBookArg>>(request.BrokerAddressBookList);
                foreach (var related in args)
                {
                    related.BrokerId = arg.Id;
                    related.CreatedBy = userId;
                }
                entity.ModifyBrokerAddressBooks(args , entity.Id.Value);
            }
            if (request.BrokerAccountBookList is not null)
            {
                var args = _mapper.Map<List<CreateBrokerAccountBookArg>>(request.BrokerAccountBookList);
                foreach (var related in args)
                {
                    related.BrokerId = arg.Id;
                    related.CreatedBy = userId;
                }
                entity.ModifyBrokerAccountBooks(args , entity.Id.Value);
            }
            if (request.BrokerPhoneBookList is not null)
            {
                var args = _mapper.Map<List<CreateBrokerPhoneBookArg>>(request.BrokerPhoneBookList);
                foreach (var related in args)
                {
                    related.BrokerId = arg.Id;
                    related.CreatedBy = userId;
                }
                entity.ModifyBrokerPhoneBooks(args, entity.Id.Value);
            }
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
        catch (Exception ex)
        {

            throw;
        }
        
    }

    public async Task<Result<long>> Handle(DeleteBrokerCommand request, CancellationToken cancellationToken)
    {
        var brokerId = new BrokerId(request.Id);
        var entity = await _repository.GetById(brokerId);
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
