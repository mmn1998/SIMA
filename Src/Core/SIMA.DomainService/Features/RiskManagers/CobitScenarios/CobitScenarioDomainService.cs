using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Contracts;
using System.Text.RegularExpressions;

namespace SIMA.DomainService.Features.RiskManagers.CobitScenarios;

public class CobitScenarioDomainService : ICobitScenarioDomainService
{
    public void ValidateAscii(string input)
    {
        // Regex to match any non-ASCII character
        Regex regex = new Regex(@"[^\x00-\x7F]");

        if (regex.IsMatch(input))
        {
            throw new Exception("The string contains non-ASCII characters.");
        }
    }
}
