using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.AssetCustomFields;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.AssetCustomFields
{
    public class AssetCustomFieldCommandHandler : ICommandHandler<CreateAssetCustomFieldCommand, Result<long>>,
    ICommandHandler<ModifyAssetCustomFieldCommand, Result<long>>, ICommandHandler<DeleteAssetCustomFieldCommand, Result<long>>
    {
        private readonly IAssetCustomFieldRepository _repository;
        private readonly IAssetCustomFieldDomainService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISimaIdentity _simaIdentity;
        private readonly IMapper _mapper;

        public AssetCustomFieldCommandHandler(IAssetCustomFieldRepository repository, IAssetCustomFieldDomainService service,
            IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
        {
            _repository = repository;
            _service = service;
            _unitOfWork = unitOfWork;
            _simaIdentity = simaIdentity;
            _mapper = mapper;
        }
        public async Task<Result<long>> Handle(CreateAssetCustomFieldCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var arg = _mapper.Map<CreateAssetCustomFieldArg>(request);
                arg.CreatedBy = _simaIdentity.UserId;
                var entity = AssetCustomField.Create(arg, _service);

                if (request.AssetCustomFieldOption is not null)
                {
                    var args = _mapper.Map<List<CreateAssetCustomFieldOptionArg>>(request.AssetCustomFieldOption);
                    foreach (var item in args)
                    {
                        item.CreatedBy = _simaIdentity.UserId;
                        item.AssetCustomFieldId = arg.Id;
                    }
                    entity.AddAssetCustomFieldOption(args);
                }

                await _repository.Add(entity);
                await _unitOfWork.SaveChangesAsync();
                return Result.Ok(arg.Id);
            }
            catch (Exception ex )
            {

                throw;
            }
            
        }

        public async Task<Result<long>> Handle(ModifyAssetCustomFieldCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(new(request.Id));
            var arg = _mapper.Map<ModifyAssetCustomFieldArg>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            entity.Modify(arg, _service);

            if (request.AssetCustomFieldOption is not null)
            {
                var args = _mapper.Map<List<CreateAssetCustomFieldOptionArg>>(request.AssetCustomFieldOption);
                foreach (var item in args)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                    item.AssetCustomFieldId = arg.Id;
                }
                entity.AddAssetCustomFieldOption(args);
            }

            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(arg.Id);
        }

        public async Task<Result<long>> Handle(DeleteAssetCustomFieldCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(new(request.Id));
            long userId = _simaIdentity.UserId; entity.Delete(userId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
    }
}
