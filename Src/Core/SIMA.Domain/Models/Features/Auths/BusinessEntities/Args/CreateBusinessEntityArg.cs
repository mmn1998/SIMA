namespace SIMA.Domain.Models.Features.Auths.BusinessEntities.Args
{
    public class CreateBusinessEntityArg
    {
        public string? Name { get; set; }
        public string? EnglishName { get; set; }
        public string? Color { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
