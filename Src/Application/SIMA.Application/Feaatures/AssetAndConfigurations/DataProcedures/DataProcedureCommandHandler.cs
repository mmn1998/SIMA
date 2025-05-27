using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.DataProcedures;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.DataProcedures;

public class DataProcedureCommandHandler : ICommandHandler<CreateDataProcedureCommand, Result<long>>,
    ICommandHandler<ModifyDataProcedureCommand, Result<long>>, ICommandHandler<DeleteDataProcedureCommand, Result<long>>
{
    private readonly IDataProcedureRepository _repository;
    private readonly IDataProcedureDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public DataProcedureCommandHandler(IDataProcedureRepository repository, IDataProcedureDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateDataProcedureCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var arg = _mapper.Map<CreateDataProcedureArg>(request);
            long userId = _simaIdentity.UserId;
            arg.CreatedBy = userId;
            var entity = await DataProcedure.Create(arg, _service);
            if (request.DataProcedureSupportTeamList is not null && request.DataProcedureSupportTeamList.Count > 0)
            {
                var relatedArgs = _mapper.Map<List<CreateDataProcedureSupportTeamArg>>(request.DataProcedureSupportTeamList);
                foreach (var item in relatedArgs)
                {
                    item.DataProcedureId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddDataProcedureSupportTeams(relatedArgs);
            }
            if (request.ConfigurationItemDataProcedureList is not null && request.ConfigurationItemDataProcedureList.Count > 0)
            {
                var relatedArgs = _mapper.Map<List<CreateConfigurationItemDataProcedureArg>>(request.ConfigurationItemDataProcedureList);
                foreach (var item in relatedArgs)
                {
                    item.DataProcedureId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddConfigurationItemDataProcedures(relatedArgs);
            }
            if (request.DataDataProcedureInputParamList is not null && request.DataDataProcedureInputParamList.Count > 0)
            {
                var relatedArgs = _mapper.Map<List<CreateDataProcedureInputParamArg>>(request.DataDataProcedureInputParamList);
                foreach (var item in relatedArgs)
                {
                    item.DataProcedureId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddDataProcedureInputParams(relatedArgs);
            }
            if (request.DataProcedureOutputParamList is not null && request.DataProcedureOutputParamList.Count > 0)
            {
                var relatedArgs = _mapper.Map<List<CreateDataProcedureOutputParamArg>>(request.DataProcedureOutputParamList);
                foreach (var item in relatedArgs)
                {
                    item.DataProcedureId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddDataProcedureOutputParams(relatedArgs);
            }
            if (request.DataProcedureDocumentList is not null && request.DataProcedureDocumentList.Count > 0)
            {
                var relatedArgs = _mapper.Map<List<CreateDataProcedureDocumentArg>>(request.DataProcedureDocumentList);
                foreach (var item in relatedArgs)
                {
                    item.DataProcedureId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddDataProcedureDocuments(relatedArgs);
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

    public async Task<Result<long>> Handle(ModifyDataProcedureCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyDataProcedureArg>(request);
        long userId = _simaIdentity.UserId;
        arg.ModifiedBy = userId;
        if (request.DataProcedureSupportTeamList is not null && request.DataProcedureSupportTeamList.Count > 0)
        {
            var relatedArgs = _mapper.Map<List<CreateDataProcedureSupportTeamArg>>(request.DataProcedureSupportTeamList);
            foreach (var item in relatedArgs)
            {
                item.DataProcedureId = arg.Id;
                item.CreatedBy = userId;
            }
            entity.ModifyDataProcedureSupportTeams(relatedArgs);
        }
        if (request.ConfigurationItemDataProcedureList is not null && request.ConfigurationItemDataProcedureList.Count > 0)
        {
            var relatedArgs = _mapper.Map<List<CreateConfigurationItemDataProcedureArg>>(request.ConfigurationItemDataProcedureList);
            foreach (var item in relatedArgs)
            {
                item.DataProcedureId = arg.Id;
                item.CreatedBy = userId;
            }
            entity.ModifyConfigurationItemDataProcedures(relatedArgs);
        }
        if (request.DataDataProcedureInputParamList is not null && request.DataDataProcedureInputParamList.Count > 0)
        {
            var relatedArgs = _mapper.Map<List<CreateDataProcedureInputParamArg>>(request.DataDataProcedureInputParamList);
            foreach (var item in relatedArgs)
            {
                item.DataProcedureId = arg.Id;
                item.CreatedBy = userId;
            }
            entity.ModifyDataProcedureInputParams(relatedArgs);
        }
        if (request.DataProcedureOutputParamList is not null && request.DataProcedureOutputParamList.Count > 0)
        {
            var relatedArgs = _mapper.Map<List<CreateDataProcedureOutputParamArg>>(request.DataProcedureOutputParamList);
            foreach (var item in relatedArgs)
            {
                item.DataProcedureId = arg.Id;
                item.CreatedBy = userId;
            }
            entity.ModifyDataProcedureOutputParams(relatedArgs);
        }
        if (request.DataProcedureDocumentList is not null && request.DataProcedureDocumentList.Count > 0)
        {
            var relatedArgs = _mapper.Map<List<CreateDataProcedureDocumentArg>>(request.DataProcedureDocumentList);
            foreach (var item in relatedArgs)
            {
                item.DataProcedureId = arg.Id;
                item.CreatedBy = userId;
            }
            entity.ModifyDataProcedureDocuments(relatedArgs);
        }
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteDataProcedureCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId;
        entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}