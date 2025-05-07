using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.Frequencies;

public class CreateFrequencyCommand: ICommand<Result<long>>
{  
    public string? Code { get; set; }
    public string Name { get; set; }
    public float NumericValue { get; set; }
    public string ValueTitle { get; set; }
    
}