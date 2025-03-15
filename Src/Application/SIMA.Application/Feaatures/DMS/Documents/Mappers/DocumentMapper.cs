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
using SIMA.Resources;
using System.Text;

namespace SIMA.Application.Feaatures.DMS.Documents.Mappers;

public class DocumentMapper : Profile
{
    private static IFileService _fileService;
    private static IServiceProvider _serviceProvider;
    private static string _root;

    public DocumentMapper(ISimaIdentity simaIdentity, IWebHostEnvironment webHost, IFileService fileService, IServiceProvider serviceProvider)
    {
        try
        {

            CreateMap<CreateDocumentCommand, CreateDocumentArg>().ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.Code, act => act.MapFrom(source => IdHelper.GenerateUniqueId().ToString()))
                .ForMember(dest => dest.FileAddress, act =>
                    act.MapFrom(source =>
                        UploadFileInFileServer(source.DocumentFile, source.Name, source.FileExtensionId, fileService, webHost.ContentRootPath).GetAwaiter().GetResult()
                                ))
                        .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<ModifyDocumentCommand, ModifyDocumentArg>()
                //.ForMember(dest => dest.ModifiedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.Code, act => act.MapFrom(source => IdHelper.GenerateUniqueId().ToString()))
                    .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
        }
        catch (SimaException e)
        {

            throw;
        }
        _fileService = fileService;
        _serviceProvider = serviceProvider;
        _root = webHost.ContentRootPath;
    }
    public static async Task<List<CreateDocumentArg>> Map(List<CreateDocumentCommand> args, long userId, long companyId)
    {
        var result = new List<CreateDocumentArg>();
        foreach (var arg in args)
        {
            var item = new CreateDocumentArg();
            item.Name = arg.Name;
            item.Code = IdHelper.GenerateUniqueId().ToString();
            item.FileAddress = await UploadFileInFileServer(arg.DocumentFile, arg.Name, arg.FileExtensionId, _fileService, _root);
            item.AttachStepId = arg.AttachStepId;
            item.SourceId = arg.SourceId;
            item.CreatedBy = userId;
            item.CompanyId = companyId;
            item.DocumentTypeId = arg.DocumentTypeId.HasValue ? arg.DocumentTypeId.Value : 0;
            item.ActiveStatusId = (long)ActiveStatusEnum.Active;
            item.FileExtensionId = arg.FileExtensionId.HasValue ? arg.FileExtensionId.Value : 0;
            item.MainAggregateId = arg.MainAggregateId;
            result.Add(item);
        }
        return result;
    }

    private static async Task<string> UploadFileInFileServer(string base64, string fileName, long? extensionTypeId, IFileService fileService, string rootPath)
    {
        var fileContent = fileService.GetBytesFromBase64(base64);
        var mimeType = fileService.GetMimeType(fileContent);

        if (fileContent is null || fileContent.Length == 0)
        {
            throw new SimaResultException(CodeMessges._400Code, Messages.FileContentNullError);
        }
        var filePath = await fileService.Upload(fileContent, fileName, rootPath);
        try
        {
            await FileValidations(filePath, extensionTypeId,mimeType);
        }
        catch (SimaException)
        {
            fileService.DeleteFile(filePath);
            throw;
        }
        return filePath;
    }
    #region FileValidations
    private static async Task FileValidations(string filePath, long? extensionTypeId,string? mimeType)
    {
        var fileExtension = Path.GetExtension(filePath).Replace(".", "");
        if (mimeType != fileExtension.GetContentType())
            throw new SimaResultException(CodeMessges._400Code, Messages.MimeTypeNotValid);
        if (extensionTypeId == null)
        {
            throw new SimaResultException(CodeMessges._400Code, Messages.FileTypeNotSelect);
        }
        using (var scope = _serviceProvider.CreateScope())
        {
            var service = scope.ServiceProvider.GetRequiredService<IDocumentDomainService>();
            var extensionName = await service.GetDocumentExtension(extensionTypeId.Value);
            if (!string.Equals(extensionName, fileExtension, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SimaResultException(CodeMessges._100042Code, Messages.FileTypeError);
            }
        }
    }
    #endregion
}
