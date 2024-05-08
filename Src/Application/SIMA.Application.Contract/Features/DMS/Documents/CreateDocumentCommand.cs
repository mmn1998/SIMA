using Microsoft.AspNetCore.Http;
using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.DMS.Documents;

public class CreateDocumentCommand : ICommand<Result<long>>
{
    public string Name { get; set; }
    public string DocumentFile { get; set; }
    public long? MainAggregateId { get; set; }
    public long? SourceId { get; set; }
    public long? AttachStepId { get; set; }
    public long? DocumentTypeId { get; set; }
    public long? FileExtensionId { get; set; }
}