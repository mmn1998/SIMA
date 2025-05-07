using SIMA.Framework.Common.Helper.FormMaker;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Diagrams;

public class GetRiskListDiagramResult
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public AffectedHistory AffectedHistory { get; set; }
    public UseVulnerability UseVulnerability { get; set; }
    public ConsequenceLevel ConsequenceLevel { get; set; }
    public TriggerStatus TriggerStatus { get; set; }
    public ScenarioHistory ScenarioHistory { get; set; }
    public Frequency Frequency { get; set; }
    public string IsNeedCobit { get; set; }
    public RiskType RiskType { get; set; }
    public string Description { get; set; }
    public string ActiveStatus { get; set; }
    public string CreatedAt { get; set; }

    
}


public class GetRiskListResultWrapper
{
    public List<GetRiskListDiagramResult>? Data { get; set; }
}

public class RiskType
{
    
    public long Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}

public class UseVulnerability
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string ValueTitle { get; set; }
    public string NumberValue { get; set; }
}

public class Frequency
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string ValueTitle { get; set; }
    public string NumberValue { get; set; }
}

public class ScenarioHistory
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string ValueTitle { get; set; }
    public string NumberValue { get; set; }
}

public class TriggerStatus
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string ValueTitle { get; set; }
    public string NumberValue { get; set; }
}

public class ConsequenceLevel
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string ValueTitle { get; set; }
    public string NumberValue { get; set; }
}

public class UseVulnerabilityId
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string ValueTitle { get; set; }
    public string NumberValue { get; set; }
}

public class AffectedHistory
{
    public long Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string ValueTitle { get; set; }
    public string NumberValue { get; set; }
}

