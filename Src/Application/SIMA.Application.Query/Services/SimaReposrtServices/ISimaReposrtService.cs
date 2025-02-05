namespace SIMA.Application.Query.Services.SimaReposrtServices;

public interface ISimaReposrtService
{
    byte[] ExportToCsv<T>(IEnumerable<T> data);
    byte[] ExportToExcel<T>(IEnumerable<T> data);
}