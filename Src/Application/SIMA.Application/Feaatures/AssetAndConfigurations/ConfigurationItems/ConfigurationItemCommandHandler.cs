using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItems;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.ConfigurationItems;

public class ConfigurationItemCommandHandler : ICommandHandler<CreateConfigurationItemCommand, Result<long>>,
    ICommandHandler<ModifyConfigurationItemCommand, Result<long>>,
    ICommandHandler<DeleteConfigurationItemCommand, Result<long>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfigurationItemRepository _repository;
    private readonly IMapper _mapper;
    private readonly IConfigurationItemDomainService _service;
    private readonly ISimaIdentity _simaIdentity;

    public ConfigurationItemCommandHandler(IUnitOfWork unitOfWork, IConfigurationItemRepository repository,
        IMapper mapper, IConfigurationItemDomainService service, ISimaIdentity simaIdentity)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _mapper = mapper;
        _service = service;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateConfigurationItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = _simaIdentity.UserId;
            var arg = _mapper.Map<CreateConfigurationItemArg>(request);
            arg.CreatedBy = userId;
            var entity = await ConfigurationItem.Create(arg, _service);
            if (request.ConfigurationItemCustomFieldValueList is not null && request.ConfigurationItemCustomFieldValueList.Count > 0)
            {
                var relatedArgs = _mapper.Map<List<CreateConfigurationItemCustomFieldValueArg>>(request.ConfigurationItemCustomFieldValueList);
                foreach (var item in relatedArgs)
                {
                    item.ConfigurationItemId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddConfigurationItemCustomFieldValues(relatedArgs);
            }
            if (request.ConfigurationItemSupportTeamList is not null && request.ConfigurationItemSupportTeamList.Count > 0)
            {
                var relatedArgs = _mapper.Map<List<CreateConfigurationItemSupportTeamArg>>(request.ConfigurationItemSupportTeamList);
                foreach (var item in relatedArgs)
                {
                    item.ConfigurationItemId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddConfigurationItemSupportTeams(relatedArgs);
            }
            if (request.ConfigurationItemAccessInfoList is not null && request.ConfigurationItemAccessInfoList.Count > 0)
            {
                var relatedArgs = _mapper.Map<List<CreateConfigurationItemAccessInfoArg>>(request.ConfigurationItemAccessInfoList);
                foreach (var item in relatedArgs)
                {
                    item.ConfigurationItemId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddConfigurationItemAccessInfos(relatedArgs);
            }
            if (request.ConfigurationItemBackupScheduleList is not null && request.ConfigurationItemBackupScheduleList.Count > 0)
            {
                var relatedArgs = _mapper.Map<List<CreateConfigurationItemBackupScheduleArg>>(request.ConfigurationItemBackupScheduleList);
                foreach (var item in relatedArgs)
                {
                    item.ConfigurationItemId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddConfigurationItemBackupSchedules(relatedArgs);
            }
            if (request.ConfigurationItemApiList is not null && request.ConfigurationItemApiList.Count > 0)
            {
                var relatedArgs = _mapper.Map<List<CreateConfigurationItemApiArg>>(request.ConfigurationItemApiList);
                foreach (var item in relatedArgs)
                {
                    item.ConfigurationItemId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddConfigurationItemApis(relatedArgs);
            }
            if (request.ConfigurationItemDataProcedureList is not null && request.ConfigurationItemDataProcedureList.Count > 0)
            {
                var relatedArgs = _mapper.Map<List<CreateConfigurationItemDataProcedureArg>>(request.ConfigurationItemDataProcedureList);
                foreach (var item in relatedArgs)
                {
                    item.ConfigurationItemId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddConfigurationItemDataProcedures(relatedArgs);
            }
            if (request.ServiceConfigurationItemList is not null && request.ServiceConfigurationItemList.Count > 0)
            {
                var relatedArgs = _mapper.Map<List<CreateServiceConfigurationItemArg>>(request.ServiceConfigurationItemList);
                foreach (var item in relatedArgs)
                {
                    item.ConfigurationItemId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddServiceConfigurationItems(relatedArgs);
            }
            if (request.ConfigurationItemDocumentList is not null && request.ConfigurationItemDocumentList.Count > 0)
            {
                var relatedArgs = _mapper.Map<List<CreateConfigurationItemDocumentArg>>(request.ConfigurationItemDocumentList);
                foreach (var item in relatedArgs)
                {
                    item.ConfigurationItemId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddConfigurationItemDocuments(relatedArgs);
            }
            if (request.ConfigurationItemAssetList is not null && request.ConfigurationItemAssetList.Count > 0)
            {
                var relatedArgs = _mapper.Map<List<CreateConfigurationItemAssetArg>>(request.ConfigurationItemAssetList);
                foreach (var item in relatedArgs)
                {
                    item.ConfigurationItemId = arg.Id;
                    item.CreatedBy = userId;
                }
                entity.AddConfigurationItemAssets(relatedArgs);
            }


            #region ConfigurationItemIssues

            var configurationItemIssueArg = _mapper.Map<CreateConfigurationItemIssueArg>(arg);
            configurationItemIssueArg.ConfigurationItemId = arg.Id;
            entity.AddConfigurationIteIssues(new List<CreateConfigurationItemIssueArg>

                {
                    configurationItemIssueArg
                });

            #endregion

            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(arg.Id);
        }
        catch (Exception ex)
        {

            throw;
        }
        
    }

    public async Task<Result<long>> Handle(ModifyConfigurationItemCommand request, CancellationToken cancellationToken)
    {
        var userId = _simaIdentity.UserId;
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyConfigurationItemArg>(request);
        arg.ModifiedBy = userId;
        await entity.Modify(arg, _service);
        if (request.ConfigurationItemCustomFieldValueList is not null && request.ConfigurationItemCustomFieldValueList.Count > 0)
        {
            var relatedArgs = _mapper.Map<List<CreateConfigurationItemCustomFieldValueArg>>(request.ConfigurationItemCustomFieldValueList);
            foreach (var item in relatedArgs)
            {
                item.ConfigurationItemId = arg.Id;
                item.CreatedBy = userId;
            }
            entity.ModifyConfigurationItemCustomFieldValues(relatedArgs);
        }
        if (request.ConfigurationItemSupportTeamList is not null && request.ConfigurationItemSupportTeamList.Count > 0)
        {
            var relatedArgs = _mapper.Map<List<CreateConfigurationItemSupportTeamArg>>(request.ConfigurationItemSupportTeamList);
            foreach (var item in relatedArgs)
            {
                item.ConfigurationItemId = arg.Id;
                item.CreatedBy = userId;
            }
            entity.ModifyConfigurationItemSupportTeams(relatedArgs);
        }
        if (request.ConfigurationItemAccessInfoList is not null && request.ConfigurationItemAccessInfoList.Count > 0)
        {
            var relatedArgs = _mapper.Map<List<CreateConfigurationItemAccessInfoArg>>(request.ConfigurationItemAccessInfoList);
            foreach (var item in relatedArgs)
            {
                item.ConfigurationItemId = arg.Id;
                item.CreatedBy = userId;
            }
            entity.ModifyConfigurationItemAccessInfos(relatedArgs);
        }
        if (request.ConfigurationItemBackupScheduleList is not null && request.ConfigurationItemBackupScheduleList.Count > 0)
        {
            var relatedArgs = _mapper.Map<List<CreateConfigurationItemBackupScheduleArg>>(request.ConfigurationItemBackupScheduleList);
            foreach (var item in relatedArgs)
            {
                item.ConfigurationItemId = arg.Id;
                item.CreatedBy = userId;
            }
            entity.ModifyConfigurationItemBackupSchedules(relatedArgs);
        }
        if (request.ConfigurationItemApiList is not null && request.ConfigurationItemApiList.Count > 0)
        {
            var relatedArgs = _mapper.Map<List<CreateConfigurationItemApiArg>>(request.ConfigurationItemApiList);
            foreach (var item in relatedArgs)
            {
                item.ConfigurationItemId = arg.Id;
                item.CreatedBy = userId;
            }
            entity.ModifyConfigurationItemApis(relatedArgs);
        }
        if (request.ConfigurationItemDataProcedureList is not null && request.ConfigurationItemDataProcedureList.Count > 0)
        {
            var relatedArgs = _mapper.Map<List<CreateConfigurationItemDataProcedureArg>>(request.ConfigurationItemDataProcedureList);
            foreach (var item in relatedArgs)
            {
                item.ConfigurationItemId = arg.Id;
                item.CreatedBy = userId;
            }
            entity.ModifyConfigurationItemDataProcedures(relatedArgs);
        }
        if (request.ServiceConfigurationItemList is not null && request.ServiceConfigurationItemList.Count > 0)
        {
            var relatedArgs = _mapper.Map<List<CreateServiceConfigurationItemArg>>(request.ServiceConfigurationItemList);
            foreach (var item in relatedArgs)
            {
                item.ConfigurationItemId = arg.Id;
                item.CreatedBy = userId;
            }
            entity.ModifyServiceConfigurationItems(relatedArgs);
        }
        if (request.ConfigurationItemDocumentList is not null && request.ConfigurationItemDocumentList.Count > 0)
        {
            var relatedArgs = _mapper.Map<List<CreateConfigurationItemDocumentArg>>(request.ConfigurationItemDocumentList);
            foreach (var item in relatedArgs)
            {
                item.ConfigurationItemId = arg.Id;
                item.CreatedBy = userId;
            }
            entity.ModifyConfigurationItemDocuments(relatedArgs);
        }
        if (request.ConfigurationItemAssetList is not null && request.ConfigurationItemAssetList.Count > 0)
        {
            var relatedArgs = _mapper.Map<List<CreateConfigurationItemAssetArg>>(request.ConfigurationItemAssetList);
            foreach (var item in relatedArgs)
            {
                item.ConfigurationItemId = arg.Id;
                item.CreatedBy = userId;
            }
            entity.ModifyConfigurationItemAssets(relatedArgs);
        }
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(arg.Id);
    }

    public async Task<Result<long>> Handle(DeleteConfigurationItemCommand request, CancellationToken cancellationToken)
    {
        var userId = _simaIdentity.UserId;
        var entity = await _repository.GetById(new(request.Id));
        entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}
