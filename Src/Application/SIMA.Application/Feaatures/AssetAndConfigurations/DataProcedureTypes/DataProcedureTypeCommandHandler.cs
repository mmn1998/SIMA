using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.DataProcedureTypes;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedureTypes.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.DataProcedureTypes;

public class DataProcedureTypeCommandHandler : ICommandHandler<CreateDataProcedureTypeCommand, Result<long>>,
    ICommandHandler<ModifyDataProcedureTypeCommand, Result<long>>, ICommandHandler<DeleteDataProcedureTypeCommand, Result<long>>
{
    private readonly IDataProcedureTypeRepository _repository;
    private readonly IDataProcedureTypeDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public DataProcedureTypeCommandHandler(IDataProcedureTypeRepository repository, IDataProcedureTypeDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateDataProcedureTypeCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateDataProcedureTypeArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await DataProcedureType.Create(arg, _service);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(ModifyDataProcedureTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyDataProcedureTypeArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteDataProcedureTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}