using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.Assets;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetAssignedStaffs.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.Assets;

public class AssetCommandHandler : ICommandHandler<CreateAssetCommand, Result<long>>, ICommandHandler<ModifyAssetCommand, Result<long>>,
    ICommandHandler<DeleteAssetCommand, Result<long>>
{
    private readonly IAssetRepository _repository;
    private readonly IAssetDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public AssetCommandHandler(IAssetRepository repository, IAssetDomainService service,
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
        try
        {
            var arg = _mapper.Map<CreateAssetArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await Asset.Create(arg, _service);


            if (request.AssetCustomFeildOptionList is not null)
            {
                var args = _mapper.Map<List<CreateAssetCustomFieldValueArg>>(request.AssetCustomFeildOptionList);
                foreach (var item in args)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                    item.AssetId = arg.Id;
                }
                entity.AddAssetCustomFieldValueAsset(args);
            }



            if (request.AssetAssignedStaffList is not null)
            {
                var args = _mapper.Map<List<CreateAssetAssignedStaffArg>>(request.AssetAssignedStaffList);
                foreach (var item in args)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                    item.AssetId = arg.Id;
                }
                entity.AddAssignedStaffs(args);
            }


            if (request.ServiceAssetList is not null)
            {
                var args = _mapper.Map<List<CreateServiceAssetArg>>(request.ServiceAssetList);
                foreach (var item in args)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                    item.AssetId = arg.Id;

                }
                entity.AddAssetService(args);
            }


            if (request.AssetDocumentList is not null)
            {
                var args = _mapper.Map<List<CreateAssetDocumentArg>>(request.AssetDocumentList);
                foreach (var item in args)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                    item.AssetId = arg.Id;
                }
                entity.AddAssetDocument(args);
            }

            if (request.ConfigurationItemAssetList is not null)
            {
                var args = _mapper.Map<List<CreateConfigurationItemAssetArg>>(request.ConfigurationItemAssetList);
                foreach (var item in args)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                    item.AssetId = arg.Id;
                }
                entity.AddConfigurationItemAsset(args);
            }

            if (request.ComplexAssetList is not null)
            {
                var args = _mapper.Map<List<CreateComplexAssetArg>>(request.ComplexAssetList);
                foreach (var item in args)
                {
                    item.CreatedBy = _simaIdentity.UserId;
                    item.AssetId = arg.Id;
                }
                entity.AddComplexAssetAsset(args);
            }

            #region AssetIssues

            var assetIssueArg = _mapper.Map<CreateAssetIssueArg>(arg);
            assetIssueArg.AssetId = arg.Id;
            entity.AddAssetIssues(new List<CreateAssetIssueArg>

                {
                    assetIssueArg
                });
            #endregion


            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(arg.Id);
        }
        catch(Exception ex)
        {
            throw;
        }
       
    }

    public async Task<Result<long>> Handle(ModifyAssetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyAssetArg>(request);
        
            
        if (request.AssetCustomFeildOptionList is not null)
        {
            var args = _mapper.Map<List<CreateAssetCustomFieldValueArg>>(request.AssetCustomFeildOptionList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.AssetId = arg.Id;
            }
            entity.ModifyCreateAssetCustomFieldValues(args);
        }
        
        
        
        if (request.AssetAssignedStaffList is not null)
        {
            var args = _mapper.Map<List<CreateAssetAssignedStaffArg>>(request.AssetAssignedStaffList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.AssetId = arg.Id;
            }
            entity.ModifyAssignedStaffs(args);
        }
        
        
        if (request.ServiceAssetList is not null)
        {
            var args = _mapper.Map<List<CreateServiceAssetArg>>(request.ServiceAssetList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.AssetId = arg.Id;
                
            }
            entity.ModifyServiceAssets(args);
        }
        
        
        if (request.AssetDocumentList is not null)
        {
            var args = _mapper.Map<List<CreateAssetDocumentArg>>(request.AssetDocumentList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.AssetId = arg.Id;
            }
            entity.ModifyAssetDocuments(args);
        }
        
     
        if (request.ConfigurationItemAssetList is not null)
        {
            var args = _mapper.Map<List<CreateConfigurationItemAssetArg>>(request.ConfigurationItemAssetList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.AssetId = arg.Id;
            }
            entity.ModifyConfigurationItemAssets(args);
        }

        if (request.ComplexAssetList is not null)
        {
            var args = _mapper.Map<List<CreateComplexAssetArg>>(request.ComplexAssetList);
            foreach (var item in args)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.AssetId = arg.Id;
            }
            entity.ModifyComplexAssets(args);
        }
        
        
        
        await entity.Modify(arg, _service);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var userId = _simaIdentity.UserId;
        entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

}
