using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.ConfigurationAttributes;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.ConfigurationAttributes;

public class ConfigurationAttributeCommandHandler : ICommandHandler<DeleteConfigurationAttributeByIdCommand, Result<long>>
{
    private readonly IConfigurationAttributeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ISimaIdentity _simaIdentity;

    public ConfigurationAttributeCommandHandler(IConfigurationAttributeRepository repository,
        IUnitOfWork unitOfWork, IMapper mapper, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(DeleteConfigurationAttributeByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId;entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}
