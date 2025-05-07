namespace SIMA.Application.Query.Services.SimaReposrtServices;

public interface ISimaReportService
{
    byte[] ExportToCsv<T>(IEnumerable<T> data);
    byte[] ExportToExcel<T>(IEnumerable<T> data);
    string GenerateFileName(string cartableName);
    
}