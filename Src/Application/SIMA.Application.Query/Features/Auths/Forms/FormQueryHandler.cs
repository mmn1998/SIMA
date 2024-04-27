using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using SIMA.Application.Query.Contract.Features.Auths.Forms;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Persistance.Read.Repositories.Features.Auths.Forms;
using System.Globalization;

namespace SIMA.Application.Query.Features.Auths.Forms;

public class FormQueryHandler : IQueryHandler<GetFormQuery, Result<GetFormQueryResult>>, IQueryHandler<GetAllFormQuery, Result<IEnumerable<GetFormQueryResult>>>
{
    private readonly IFormQueryRepository _repository;
    public FormQueryHandler(IFormQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetFormQueryResult>> Handle(GetFormQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.FindById(request.Id);
        if (result.JsonContent is not null && !string.IsNullOrEmpty(result.JsonContent))
        {
            result.JsonContent = await ProcessJsonContent(result.JsonContent);
        }
        return Result.Ok(result);
    }

    public async Task<Result<IEnumerable<GetFormQueryResult>>> Handle(GetAllFormQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }



    private async Task<string> ProcessJsonContent(string jsonContent)
    {
        var allComponents = JsonConvert.DeserializeObject<FormWrapper>(jsonContent).Components;
        foreach (var component in allComponents)
        {
            if (string.Equals(component.Type, FormInputType.select.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                var viewName = component.Properties?.SourceData;
                if (!string.IsNullOrEmpty(viewName))
                {
                    var dropDownData = await _repository.FetchFromView(viewName);
                    component.ValuesKey = JsonConvert.SerializeObject(dropDownData);
                }
            }
        }
        string result = JsonConvert.SerializeObject(allComponents);
        return result;
    }
}
public enum FormInputType
{
select,
Textfield,
Number,
button
}
public partial class FormWrapper
{
public Component[] Components { get; set; }
public string Type { get; set; }
public string Id { get; set; }
public Exporter Exporter { get; set; }
public long SchemaVersion { get; set; }
}

public partial class Component
{
public string Label { get; set; }

public string Type { get; set; }
public Layout Layout { get; set; }
public string Id { get; set; }
public string Key { get; set; }
public Properties Properties { get; set; }
public string ValuesKey { get; set; }
public string Action { get; set; }
}

public partial class Layout
{
public string Row { get; set; }
public long Columns { get; set; }
}

public partial class Properties
{
    // dropDown data
public string? SourceData { get; set; }
    // custome actions on buttons
public string? Sp { get; set; }
public string? TargetId { get; set; }
public string? ProgressId { get; set; }
}

public partial class Exporter
{
public string Name { get; set; }
public string Version { get; set; }
}

//public partial class Welcome5
//{
//    public static Welcome5 FromJson(string json) => JsonConvert.DeserializeObject<Welcome5>(json, CodeBeautify.Converter.Settings);
//}

//public static class Serialize
//{
//    public static string ToJson(this Welcome5 self) => JsonConvert.SerializeObject(self, CodeBeautify.Converter.Settings);
//}

internal static class Converter
{
public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
{
    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
    DateParseHandling = DateParseHandling.None,
    Converters =
        {
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        },
};
}
