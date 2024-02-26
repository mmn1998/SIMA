using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SIMA.Application.Contract.Features.DMS.Documents;
using SIMA.Domain.Models.Features.DMS.Documents.Args;
using SIMA.Domain.Models.Features.DMS.Documents.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Helper.FileHelper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.DMS.Documents.Mappers;

public class DocumentMapper : Profile
{
    private readonly IServiceProvider _serviceProvider;

    public DocumentMapper(ISimaIdentity simaIdentity, IWebHostEnvironment webHost, IFileService fileService, IServiceProvider serviceProvider)
    {
        CreateMap<CreateDocumentCommand, CreateDocumentArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.Code, act => act.MapFrom(source => IdHelper.GenerateUniqueId().ToString()))
                .ForMember(dest => dest.FileAddress, act =>
                    act.MapFrom(source =>
                        UploadFileInFileServer(source.DocumentFile, source.Name, source.FileExtensionId, fileService, webHost.ContentRootPath).GetAwaiter().GetResult()
                                ))
                        .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyDocumentCommand, ModifyDocumentArg>()
            .ForMember(dest => dest.ModifiedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.Code, act => act.MapFrom(source => IdHelper.GenerateUniqueId().ToString()))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
        _serviceProvider = serviceProvider;
    }

    private async Task<string> UploadFileInFileServer(string base64, string fileName, long? extensionTypeId, IFileService fileService, string rootPath)
    {
        try
        {
            var fileContent = fileService.GetBytesFromBase64(base64);
            if (fileContent is null || fileContent.Length == 0)
            {
                throw new SimaResultException("400", "فایل خالی است");
            }
            var filePath = await fileService.Upload(fileContent, fileName, rootPath);
            try
            {
                await FileValidations(filePath, extensionTypeId);
            }
            catch (Exception)
            {
                fileService.DeleteFile(filePath);
                throw;
            }
            return filePath;
        }
        catch (Exception)
        {
            throw new SimaResultException("500", "بارگذاری فایل با خطا مواجه شد.لطفا دوباره سعی کنید");
        }
    }
    #region FileValidations
    private async Task FileValidations(string filePath, long? extensionTypeId)
    {
        var fileExtension = Path.GetExtension(filePath).Replace(".", "");
        if (extensionTypeId == null)
        {
            throw new SimaResultException("400", "جنس فایل انتخاب نشده است");
        }
        using (var scope = _serviceProvider.CreateScope())
        {
            var service = scope.ServiceProvider.GetRequiredService<IDocumentDomainService>();
            var extensionName = await service.GetDocumentExtension(extensionTypeId.Value);
            if (!string.Equals(extensionName, fileExtension, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SimaResultException("400", "جنس فایل اشتباه است");
            }
        }
    }
    #endregion
}
