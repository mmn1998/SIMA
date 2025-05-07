namespace SIMA.Domain.Models.Features.Auths.BusinessEntities.Args
{
    public class ModifyBusinessEntityArg
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? EnglishName { get; set; }
        public string? Color { get; set; }
        public long ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
