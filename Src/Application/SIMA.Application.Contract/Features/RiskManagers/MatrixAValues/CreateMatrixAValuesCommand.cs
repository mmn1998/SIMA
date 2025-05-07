using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.MatrixAValues;

public class CreateMatrixAValuesCommand: ICommand<Result<long>>
{
    public string Code { get; set; }
    public float NumericValue { get; set; }
    public string ValueTitle { get; set; }
    public string Color { get; set; }
}