using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.AssetAndConfigurations.DataProcedures;

public class CreateDataProcedureCommand : ICommand<Result<long>>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string? VersionNumber { get; set; }
    public string? Description { get; set; }
    public string? ReleaseDate { get; set; }
    public string? IsInternalApi { get; set; }
    public long DataProcedureTypeId { get; set; }
    public long DatabaseId { get; set; }
    public ICollection<CreateDataProcedureSupportTeamCommand>? DataProcedureSupportTeamList { get; set; }
    public ICollection<CreateDataProcedureConfigurationItemCommand>? ConfigurationItemDataProcedureList { get; set; }
    public ICollection<CreateDataProcedureDocumentCommand>? DataProcedureDocumentList { get; set; }
    public ICollection<CreateDataProcedureInputParamCommand>? DataDataProcedureInputParamList { get; set; }
    public ICollection<CreateDataProcedureOutputParamCommand>? DataProcedureOutputParamList { get; set; }
}