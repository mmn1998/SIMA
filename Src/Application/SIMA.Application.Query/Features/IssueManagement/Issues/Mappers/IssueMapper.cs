namespace SIMA.Application.Query.Features.IssueManagement.Issues.Mappers;

internal static class IssueMapper
{
    public static string ColorizeCurrentStep(this string? xml, string? bpmnId)
    {
        string xmlContent = string.Empty;

        if (string.IsNullOrEmpty(bpmnId) || string.IsNullOrEmpty(xml))
            xmlContent = string.Empty;

        else if (xml.Contains($"bpmnElement=\"{bpmnId}\""))
            xmlContent = xml.Replace($"bpmnElement=\"{bpmnId}\"", $"bpmnElement=\"{bpmnId}\" bioc:stroke=\"#e53935\" bioc:fill=\"#ffcdd2\" color:background-color=\"#ffcdd2\" color:border-color=\"#e53935\"");

        else
            xmlContent = xml;

        return xmlContent;
    }
}
