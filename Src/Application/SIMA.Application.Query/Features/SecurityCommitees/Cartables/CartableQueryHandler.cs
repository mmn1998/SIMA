using Newtonsoft.Json;
using SIMA.Application.Query.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.Cartables;
using SIMA.Application.Query.Features.Auths.Forms;
using SIMA.Framework.Common.Helper.FormMaker;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.Forms;
using SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.Cartables;

namespace SIMA.Application.Query.Features.SecurityCommitees.Cartables;

public class CartableQueryHandler : IQueryHandler<GetAllCartableQuery, Result<IEnumerable<GetAllCartableQueryResult>>>,
    IQueryHandler<GetCartableQuery, Result<GetCartableQueryResult>>
{
    private readonly ICartableQueryRepository _repository;
    private readonly IFormQueryRepository _formQueryRepository;

    public CartableQueryHandler(ICartableQueryRepository repository, IFormQueryRepository formQueryRepository)
    {
        _repository = repository;
        _formQueryRepository = formQueryRepository;
    }
    public async Task<Result<IEnumerable<GetAllCartableQueryResult>>> Handle(GetAllCartableQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request);
    }

    public async Task<Result<GetCartableQueryResult>> Handle(GetCartableQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetDetail(request);

        if (result.JsonContent is not null && !string.IsNullOrEmpty(result.JsonContent))
        {
            result.JsonContent = await ProcessJsonContent(result.JsonContent, result.RelatedProgresses);
        }
        return Result.Ok(result);
    }
    private async Task<string> ProcessJsonContent(string jsonContent, List<GetRelatedProgressQueryResult> dbButtons)
    {
        var allComponents = JsonConvert.DeserializeObject<FormWrapper>(jsonContent).components;

        var SelectList = allComponents.Where(it => it.type == FormInputType.select.ToString()).ToList();
        var ButtonList = allComponents.Where(it => it.type == FormInputType.button.ToString()).ToList();


        foreach (var select in SelectList)
        {
            var viewName = select.properties?.sourceData;
            if (!string.IsNullOrEmpty(viewName))
            {
                var dropDownData = await _formQueryRepository.FetchFromView(viewName);
                select.valuesKey = JsonConvert.SerializeObject(dropDownData);
            }
        }
        foreach (var button in ButtonList)
        {
            foreach (var dbButton in dbButtons)
            {
                if (string.Equals(button.label, dbButton.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    button.properties.targetId = dbButton.TargetId.ToString();
                    button.properties.progressId = dbButton.ProgressId.ToString();
                }
            }
        }
        string result = JsonConvert.SerializeObject(allComponents);
        return result;
    }
}
