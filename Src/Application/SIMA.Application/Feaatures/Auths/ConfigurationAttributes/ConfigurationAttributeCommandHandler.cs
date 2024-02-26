using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.ConfigurationAttributes;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.ConfigurationAttributes;

public class ConfigurationAttributeCommandHandler : ICommandHandler<DeleteConfigurationAttributeByIdCommand, Result<long>>
{
    private readonly IConfigurationAttributeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ConfigurationAttributeCommandHandler(IConfigurationAttributeRepository repository,
        IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(DeleteConfigurationAttributeByIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
}
