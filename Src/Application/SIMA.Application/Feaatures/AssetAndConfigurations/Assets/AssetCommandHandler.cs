using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.Assets;
using SIMA.Application.Contract.Features.Auths.AccessTypes;
using SIMA.Domain.Models.Features.Auths.AccessTypes.Args;
using SIMA.Domain.Models.Features.Auths.AccessTypes.Contracts;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.Assets
{
    public class AssetCommandHandler : ICommandHandler<CreateAssetCommand, Result<long>>
    {
        private readonly IAccessTypeRepository _repository;
        private readonly IAccessTypeDomainService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISimaIdentity _simaIdentity;
        private readonly IMapper _mapper;

        public AssetCommandHandler(IAccessTypeRepository repository, IAccessTypeDomainService service,
            IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
        {
            _repository = repository;
            _service = service;
            _unitOfWork = unitOfWork;
            _simaIdentity = simaIdentity;
            _mapper = mapper;
        }
        public async Task<Result<long>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            long x = 1;
            //var arg = _mapper.Map<CreateAccessTypeArg>(request);
            //arg.CreatedBy = _simaIdentity.UserId;
            //var entity = await AccessType.Create(arg, _service);
            //await _repository.Add(entity);
            //await _unitOfWork.SaveChangesAsync();
            return Result.Ok(x);
        }
    }
}
