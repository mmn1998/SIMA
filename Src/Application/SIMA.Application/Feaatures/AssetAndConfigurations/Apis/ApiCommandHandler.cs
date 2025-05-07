using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.AssetAndConfigurations.Apis;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Contracts;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Args;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.AssetAndConfigurations.Apis;

public class ApiCommandHandler : ICommandHandler<CreateApiCommand, Result<long>>,
    ICommandHandler<ModifyApiCommand, Result<long>>, ICommandHandler<DeleteApiCommand, Result<long>>
{
    private readonly IApiRepository _repository;
    private readonly IApiDomainService _service;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IMapper _mapper;

    public ApiCommandHandler(IApiRepository repository, IApiDomainService service,
        IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _unitOfWork = unitOfWork;
        _simaIdentity = simaIdentity;
        _mapper = mapper;
    }
    public async Task<Result<long>> Handle(CreateApiCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var arg = _mapper.Map<CreateApiArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await Api.Create(arg, _service);
            if (request.ApiDocumentList is not null && request.ApiDocumentList.Count > 0)
            {
                var documentArg = _mapper.Map<List<CreateApiDocumentArg>>(request.ApiDocumentList);
                foreach (var item in documentArg)
                {
                    item.ApiId = entity.Id.Value;
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddApiDocument(documentArg, entity.Id.Value, _simaIdentity.UserId);

            }
            if (request.ApiSupportTeamList is not null && request.ApiSupportTeamList.Count > 0)
            {
                var supportTeamtArg = _mapper.Map<List<CreateApiSupportTeamArg>>(request.ApiSupportTeamList);
                foreach (var item in supportTeamtArg)
                {
                    item.ApiId = entity.Id.Value;
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddApiSupportTeam(supportTeamtArg);

            }
            if (request.ConfigurationItemApiList is not null && request.ConfigurationItemApiList.Count > 0)
            {
                var configurationItemApiArg = _mapper.Map<List<CreateConfigurationItemApiArg>>(request.ConfigurationItemApiList);
                foreach (var item in configurationItemApiArg)
                {
                    item.ApiId = entity.Id.Value;
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddConfigurationItemApi(configurationItemApiArg);
            }
            if (request.ApiRequestHeaderParamList is not null && request.ApiRequestHeaderParamList.Count > 0)
            {
                var apiRequestHeaderParamArg = _mapper.Map<List<CreateApiRequestHeaderParamArg>>(request.ApiRequestHeaderParamList);
                foreach (var item in apiRequestHeaderParamArg)
                {
                    item.ApiId = entity.Id.Value;
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddApiRequestHeaderParam(apiRequestHeaderParamArg);
            }
            if (request.ApiRequestBodyParamList is not null && request.ApiRequestBodyParamList.Count > 0)
            {
                var apiRequestBodyParamArg = _mapper.Map<List<CreateApiRequestBodyParamArg>>(request.ApiRequestBodyParamList);
                foreach (var item in apiRequestBodyParamArg)
                {
                    item.ApiId = entity.Id.Value;
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddApiRequestBodyParam(apiRequestBodyParamArg);
            }
            if (request.ApiResponseBodyParamList is not null && request.ApiResponseBodyParamList.Count > 0)
            {
                var apiResponseBodyParamArg = _mapper.Map<List<CreateApiResponseBodyParamArg>>(request.ApiResponseBodyParamList);
                foreach (var item in apiResponseBodyParamArg)
                {
                    item.ApiId = entity.Id.Value;
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddApiResponseBodyParam(apiResponseBodyParamArg);
            }
            if (request.ApiResponseHeaderParamList is not null && request.ApiResponseHeaderParamList.Count > 0)
            {
                var apiResponseHeaderParamArg = _mapper.Map<List<CreateApiResponseHeaderParamArg>>(request.ApiResponseHeaderParamList);
                foreach (var item in apiResponseHeaderParamArg)
                {
                    item.ApiId = entity.Id.Value;
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddApiResponseHeaderParam(apiResponseHeaderParamArg);
            }
            if (request.ApiRequestUrlParamList is not null && request.ApiRequestUrlParamList.Count > 0)
            {
                var apiRequestUrlParamArg = _mapper.Map<List<CreateApiRequestUrlParamArg>>(request.ApiRequestUrlParamList);
                foreach (var item in apiRequestUrlParamArg)
                {
                    item.ApiId = entity.Id.Value;
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddApiRequestUrlParam(apiRequestUrlParamArg);
            }
            if (request.ApiRequestQueryStringParamList is not null && request.ApiRequestQueryStringParamList.Count > 0)
            {
                var apiRequestQueryStringParam = _mapper.Map<List<CreateApiRequestQueryStringParamArg>>(request.ApiRequestQueryStringParamList);
                foreach (var item in apiRequestQueryStringParam)
                {
                    item.ApiId = entity.Id.Value;
                    item.CreatedBy = _simaIdentity.UserId;
                }
                entity.AddApiRequestQueryStringParam(apiRequestQueryStringParam);
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

    public async Task<Result<long>> Handle(ModifyApiCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        var arg = _mapper.Map<ModifyApiArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        if (request.ApiDocumentList is not null && request.ApiDocumentList.Count > 0)
        {
            var documentArg = _mapper.Map<List<CreateApiDocumentArg>>(request.ApiDocumentList);
            foreach (var item in documentArg)
            {
                item.ApiId = entity.Id.Value;
                item.CreatedBy = _simaIdentity.UserId;
            }
            entity.AddApiDocument(documentArg, entity.Id.Value, _simaIdentity.UserId);

        }
        if (request.ApiSupportTeamList is not null && request.ApiSupportTeamList.Count > 0)
        {
            var supportTeamtArg = _mapper.Map<List<CreateApiSupportTeamArg>>(request.ApiSupportTeamList);
            foreach (var item in supportTeamtArg)
            {
                item.ApiId = entity.Id.Value;
                item.CreatedBy = _simaIdentity.UserId;
            }
            entity.ModifyApiSupportTeam(supportTeamtArg);

        }
        if (request.ConfigurationItemApiList is not null && request.ConfigurationItemApiList.Count > 0)
        {
            var configurationItemApiArg = _mapper.Map<List<CreateConfigurationItemApiArg>>(request.ConfigurationItemApiList);
            foreach (var item in configurationItemApiArg)
            {
                item.ApiId = entity.Id.Value;
                item.CreatedBy = _simaIdentity.UserId;
            }
            entity.ModifyConfigurationItemApi(configurationItemApiArg);
        }
        if (request.ApiRequestHeaderParamList is not null && request.ApiRequestHeaderParamList.Count > 0)
        {
            var apiRequestHeaderParamArg = _mapper.Map<List<CreateApiRequestHeaderParamArg>>(request.ApiRequestHeaderParamList);
            foreach (var item in apiRequestHeaderParamArg)
            {
                item.ApiId = entity.Id.Value;
                item.CreatedBy = _simaIdentity.UserId;
            }
            entity.ModifyApiRequestHeaderParam(apiRequestHeaderParamArg);
        }
        if (request.ApiRequestBodyParamList is not null && request.ApiRequestBodyParamList.Count > 0)
        {
            var apiRequestBodyParamArg = _mapper.Map<List<CreateApiRequestBodyParamArg>>(request.ApiRequestBodyParamList);
            foreach (var item in apiRequestBodyParamArg)
            {
                item.ApiId = entity.Id.Value;
                item.CreatedBy = _simaIdentity.UserId;
            }
            entity.ModifyApiRequestBodyParam(apiRequestBodyParamArg);
        }
        if (request.ApiResponseBodyParamList is not null && request.ApiResponseBodyParamList.Count > 0)
        {
            var apiResponseBodyParamArg = _mapper.Map<List<CreateApiResponseBodyParamArg>>(request.ApiResponseBodyParamList);
            foreach (var item in apiResponseBodyParamArg)
            {
                item.ApiId = entity.Id.Value;
                item.CreatedBy = _simaIdentity.UserId;
            }
            entity.ModifyApiResponseBodyParam(apiResponseBodyParamArg);
        }
        if (request.ApiResponseHeaderParamList is not null && request.ApiResponseHeaderParamList.Count > 0)
        {
            var apiResponseHeaderParamArg = _mapper.Map<List<CreateApiResponseHeaderParamArg>>(request.ApiResponseHeaderParamList);
            foreach (var item in apiResponseHeaderParamArg)
            {
                item.ApiId = entity.Id.Value;
                item.CreatedBy = _simaIdentity.UserId;
            }
            entity.ModifyApiResponseHeaderParam(apiResponseHeaderParamArg);
        }
        if (request.ApiRequestUrlParamList is not null && request.ApiRequestUrlParamList.Count > 0)
        {
            var apiRequestUrlParamArg = _mapper.Map<List<CreateApiRequestUrlParamArg>>(request.ApiRequestUrlParamList);
            foreach (var item in apiRequestUrlParamArg)
            {
                item.ApiId = entity.Id.Value;
                item.CreatedBy = _simaIdentity.UserId;
            }
            entity.ModifyApiRequestUrlParam(apiRequestUrlParamArg);
        }
        if (request.ApiRequestQueryStringParamList is not null && request.ApiRequestQueryStringParamList.Count > 0)
        {
            var apiRequestQueryStringParam = _mapper.Map<List<CreateApiRequestQueryStringParamArg>>(request.ApiRequestQueryStringParamList);
            foreach (var item in apiRequestQueryStringParam)
            {
                item.ApiId = entity.Id.Value;
                item.CreatedBy = _simaIdentity.UserId;
            }
            entity.ModifyApiRequestQueryStringParam(apiRequestQueryStringParam);
        }

        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(DeleteApiCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(new(request.Id));
        long userId = _simaIdentity.UserId;
        entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
}