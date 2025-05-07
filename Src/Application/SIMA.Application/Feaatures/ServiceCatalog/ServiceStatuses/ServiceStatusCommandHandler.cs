using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.ServiceCatalog.ServiceStatuses;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.ServiceCatalog.ServiceStatuses
{
    public class ServiceStatusCommandHandler : ICommandHandler<CreateServiceStatusCommand, Result<long>>,
    ICommandHandler<ModifyServiceStatusCommand, Result<long>>, ICommandHandler<DeleteServiceStatusCommand, Result<long>>
    {
        private readonly IServiceStatusRepository _repository;
        private readonly IServiceStatusDomainService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISimaIdentity _simaIdentity;
        private readonly IMapper _mapper;

        public ServiceStatusCommandHandler(IServiceStatusRepository repository, IServiceStatusDomainService service,
            IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
        {
            _repository = repository;
            _service = service;
            _unitOfWork = unitOfWork;
            _simaIdentity = simaIdentity;
            _mapper = mapper;
        }
        public async Task<Result<long>> Handle(CreateServiceStatusCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateServiceStatusArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await ServiceStatus.Create(arg, _service);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }

        public async Task<Result<long>> Handle(ModifyServiceStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(new ServiceStatusId(request.Id));
            var arg = _mapper.Map<ModifyServiceStatusArg>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(arg.Id);
        }

        public async Task<Result<long>> Handle(DeleteServiceStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(new ServiceStatusId(request.Id));
            long userId = _simaIdentity.UserId; entity.Delete(userId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
    }
}
