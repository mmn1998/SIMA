using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.ServiceCatalog.ApiAuthenticationMethods;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.ServiceCatalog.ApiAuthenticationMethods
{
    public class ApiAuthenticationMethodCommandHandler : ICommandHandler<CreateApiAuthenticationMethodCommand, Result<long>>,
    ICommandHandler<ModifyApiAuthenticationMethodCommand, Result<long>>, ICommandHandler<DeleteApiAuthenticationMethodCommand, Result<long>>
    {
        private readonly IApiAuthenticationMethodRepository _repository;
        private readonly IApiAuthenticationMethodDomainService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISimaIdentity _simaIdentity;
        private readonly IMapper _mapper;

        public ApiAuthenticationMethodCommandHandler(IApiAuthenticationMethodRepository repository, IApiAuthenticationMethodDomainService service,
            IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
        {
            _repository = repository;
            _service = service;
            _unitOfWork = unitOfWork;
            _simaIdentity = simaIdentity;
            _mapper = mapper;
        }
        public async Task<Result<long>> Handle(CreateApiAuthenticationMethodCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateApiAuthenticationMethodArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await ApiAuthenticationMethod.Create(arg, _service);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }

        public async Task<Result<long>> Handle(ModifyApiAuthenticationMethodCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(new ApiAuthenticationMethodId(request.Id));
            var arg = _mapper.Map<ModifyApiAuthenticationMethodArg>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(arg.Id);
        }

        public async Task<Result<long>> Handle(DeleteApiAuthenticationMethodCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(new ApiAuthenticationMethodId(request.Id));
            long userId = _simaIdentity.UserId; entity.Delete(userId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
    }
}
