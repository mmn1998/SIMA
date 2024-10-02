using SIMA.Domain.Models.Features.Auths.Domains.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Auths.Forms.Entities
{
    public class DomainForm : Entity 
    {
        private DomainForm() { }

        public DomainFormId Id { get; private set; }
        public FormId FormId { get; private set; }
        public virtual Form Form { get; private set; }
        public virtual Domains.Entities.Domain Domain { get; private set; }
        public DomainId DomainId { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
