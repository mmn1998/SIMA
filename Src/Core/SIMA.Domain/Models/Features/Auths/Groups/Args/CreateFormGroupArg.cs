namespace SIMA.Domain.Models.Features.Auths.Groups.Args
{
    public class CreateFormGroupArg 
    {
        public long FormId { get; set; }
        public long? GroupId { get; set; }
        public long ActiveStatusId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
