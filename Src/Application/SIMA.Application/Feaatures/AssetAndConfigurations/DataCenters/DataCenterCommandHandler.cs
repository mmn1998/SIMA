using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.DataCenters;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.DataCenters;

public class DataCenterCommandHandler : ICommandHandler<CreateDataCenterCommand, Result<long>>,
    ICommandHandler<ModifyDataCenterCommand, Result<long>>, ICommandHandler<DeleteDataCenterCommand, Result<long>>
{
    private readonly IDataCenterRepository _repository;
    private readonly IDataCenterDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public DataCenterCommandHandler(IDataCenterRepository repository, IDataCenterDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;_service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateDataCenterCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateDataCenterArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await DataCenter.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyDataCenterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyDataCenterArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteDataCenterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}