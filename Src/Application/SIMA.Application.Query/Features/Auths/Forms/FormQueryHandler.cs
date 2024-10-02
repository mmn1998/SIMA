using Newtonsoft.Json;
using SIMA.Application.Query.Contract.Features.Auths.Forms;
using SIMA.Framework.Common.Helper.FormMaker;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.Forms;

namespace SIMA.Application.Query.Features.Auths.Forms;

public class FormQueryHandler : IQueryHandler<GetFormQuery, Result<GetFormQueryResult>>,
    IQueryHandler<GetAllFormQuery, Result<IEnumerable<GetFormQueryResult>>>,
    IQueryHandler<GetAllFormFieldsQuery, Result<IEnumerable<GetFormFieldsQueryResult>>>,
    IQueryHandler<GetFormByDomainQuery, Result<IEnumerable<GetFormQueryResult>>>
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

    public async Task<Result<IEnumerable<GetFormFieldsQueryResult>>> Handle(GetAllFormFieldsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllFormFields(request);
    }

    public async Task<Result<IEnumerable<GetFormQueryResult>>> Handle(GetFormByDomainQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetFormByDomainId(request.DomainId);
    }

    private async Task<string> ProcessJsonContent(string jsonContent)
    {
        var form = JsonConvert.DeserializeObject<FormWrapper>(jsonContent);
        var allComponents = form.components;
        if (allComponents is not null)
        {
            foreach (var component in allComponents)
            {
                if (string.Equals(component.type, FormInputType.select.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    var viewName = component.properties?.sourceData;
                    if (!string.IsNullOrEmpty(viewName))
                    {
                        var dropDownData = await _repository.FetchFromView(viewName);
                        component.valuesKey = JsonConvert.SerializeObject(dropDownData);
                    }
                }
            }
        }
        form.components = allComponents;
        string result = JsonConvert.SerializeObject(form);
        return result;
    }
}