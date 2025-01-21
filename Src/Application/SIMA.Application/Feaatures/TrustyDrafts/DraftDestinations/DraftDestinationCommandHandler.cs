using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftDestinations;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.Contrcts;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.TrustyDrafts.DraftDestinations
{
    public class DraftDestinationCommandHandler : ICommandHandler<CreateDraftDestinationCommand, Result<long>>,
    ICommandHandler<ModifyDraftDestinationCommand, Result<long>>, ICommandHandler<DeleteDraftDestinationCommand, Result<long>>
    {
        private readonly IDraftDestinationRepository _repository;
        private readonly IDraftDestinationDomainService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISimaIdentity _simaIdentity;
        private readonly IMapper _mapper;

        public DraftDestinationCommandHandler(IDraftDestinationRepository repository, IDraftDestinationDomainService service,
            IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
        {
            _repository = repository;
            _service = service;
            _unitOfWork = unitOfWork;
            _simaIdentity = simaIdentity;
            _mapper = mapper;
        }
        public async Task<Result<long>> Handle(CreateDraftDestinationCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateDraftDestinationArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await DraftDestination.Create(arg, _service);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(arg.Id);
        }

        public async Task<Result<long>> Handle(ModifyDraftDestinationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(new(request.Id));
            var arg = _mapper.Map<ModifyDraftDestinationArg>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(arg.Id);
        }

        public async Task<Result<long>> Handle(DeleteDraftDestinationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(new(request.Id));
            long userId = _simaIdentity.UserId; entity.Delete(userId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
    }
}
