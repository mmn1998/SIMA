namespace SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.Args
{
    public class ModifyCurrencyOprationTypeArg
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long? ActiveStatusId { get; set; }
        public byte[]? ModifyAt { get; set; }
        public long? ModifyBy { get; set; }
    }
}
