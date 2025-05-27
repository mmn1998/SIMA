using AutoMapper;
using SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItemCustomFields;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Contracts;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using Sima.Framework.Core.Repository;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Args;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.ConfigurationItemCustomFields
{
    public class ConfigurationItemCustomFieldCommandHandler : ICommandHandler<CreateConfigurationItemCustomFieldCommand, Result<long>>,
    ICommandHandler<ModifyConfigurationItemCustomFieldCommand, Result<long>>, ICommandHandler<DeleteConfigurationItemCustomFieldCommand, Result<long>>
    {
        private readonly IConfigurationItemCustomFieldRepository _repository;
        private readonly IConfigurationItemCustomFieldDomainService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISimaIdentity _simaIdentity;
        private readonly IMapper _mapper;

        public ConfigurationItemCustomFieldCommandHandler(IConfigurationItemCustomFieldRepository repository, IConfigurationItemCustomFieldDomainService service,
            IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
        {
            _repository = repository;
            _service = service;
            _unitOfWork = unitOfWork;
            _simaIdentity = simaIdentity;
            _mapper = mapper;
        }
        public async Task<Result<long>> Handle(CreateConfigurationItemCustomFieldCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var arg = _mapper.Map<CreateConfigurationItemCustomFieldArg>(request);
                arg.CreatedBy = _simaIdentity.UserId;
                var entity = ConfigurationItemCustomField.Create(arg, _service);

                if (request.AssetCustomFieldOption is not null)
                {
                    var args = _mapper.Map<List<CreateConfigurationItemCustomFieldOptionArg>>(request.AssetCustomFieldOption);
                    foreach (var item in args)
                    {
                        item.CreatedBy = _simaIdentity.UserId;
                        item.ConfigurationItemCustomFieldId = arg.Id;
                    }
                    entity.AddConfigurationItemCustomFieldOption(args);
                }

                await _repository.Add(entity);
                await _unitOfWork.SaveChangesAsync();
                return Result.Ok(arg.Id);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<Result<long>> Handle(ModifyConfigurationItemCustomFieldCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(new(request.Id));
            var arg = _mapper.Map<ModifyConfigurationItemCustomFieldArg>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            entity.Modify(arg, _service);

            if (request.AssetCustomFieldOption is not null)
            {
                var args = _mapper.Map<List<CreateConfigurationItemCustomFieldOptionArg>>(request.AssetCustomFieldOption);
                foreach (var item in args)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                    item.ConfigurationItemCustomFieldId = arg.Id;
                }
                entity.AddConfigurationItemCustomFieldOption(args);
            }

            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(arg.Id);
        }

        public async Task<Result<long>> Handle(DeleteConfigurationItemCustomFieldCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(new(request.Id));
            long userId = _simaIdentity.UserId; entity.Delete(userId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
    }
}
