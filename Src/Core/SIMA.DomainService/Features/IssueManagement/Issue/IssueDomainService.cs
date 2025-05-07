using SIMA.Domain.Models.Features.IssueManagement.Issues.Interfaces;
using SIMA.Persistance.Read.Repositories.Features.IssueManagement.Issues;

namespace SIMA.DomainService.Features.IssueManagement.Issue
{
    public class IssueDomainService : IIssueDomainService
    {
        private readonly IIssueQueryRepository _issueRepository;

        public IssueDomainService(IIssueQueryRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        public async Task<bool> CheckDueDate(DateTime dueDate)
        {
            bool result = true;

            if (DateTime.Now >= dueDate)
                result = false;

            return result;

        }

        public async Task<bool> IsCodeUnique(string code, long id)
        {
            return await _issueRepository.IsCodeUnique(code, id);
        }
    }
}
