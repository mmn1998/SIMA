using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.SecurityCommitees.Meetings
{
    public class CreateMeetingCommands : ICommand<Result<long>>
    {
        public long MeetingTurn { get; set; }
        public string Description { get; set; }
        public string Lable { get; set; }
        public List<MeetingReasonsCommand> Reasons { get; set; }
        public List<MeetingDocumentCommand> MeetingsDocument { get; set; }
        public List<CreateSubjectCommand> NewSubject { get; set; }
        public List<ExistsSubjectcommand> ExistsSubjects { get; set; }
    }
   
    public class MeetingDocumentCommand
    {
        public long DocumentId { get; set; }
    }

    public class MeetingReasonsCommand
    {
        public long ReasonId { get; set; }
    }
    public class ExistsSubjectcommand
    {
        public long SubjectId { get; set; }
    }

}
