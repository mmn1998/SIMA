using SIMA.Domain.Models.Features.Auths.BusinessEntities.Args;
using SIMA.Domain.Models.Features.Auths.BusinessEntities.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Companies.Args;
using SIMA.Domain.Models.Features.Auths.Companies.Entities;
using SIMA.Domain.Models.Features.Auths.Companies.Interfaces;
using SIMA.Domain.Models.Features.Auths.Companies.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.BusinessEntities.Entities
{
    public class BusinessEntity :Entity
    {
        private BusinessEntity()
        {

        }
        private BusinessEntity(CreateBusinessEntityArg arg)
        {
            Id = new BusinessEntityId(IdHelper.GenerateUniqueId());
            Name = arg.Name;
            EnglishName = arg.EnglishName;
            Color = arg.Color;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;

        }
        public static async Task<BusinessEntity> Create(CreateBusinessEntityArg arg, ICompanyService service)
        {
            return new BusinessEntity(arg);
        }

        public async Task Modify(ModifyBusinessEntityArg arg, ICompanyService service)
        {
            Name = arg.Name;
            EnglishName = arg.EnglishName;
            Color = arg.Color;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }
        
        public void Delete(long userId)
        {
            ModifiedBy = userId;
            ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            ActiveStatusId = (long)ActiveStatusEnum.Delete;
        }

        public BusinessEntityId Id { get; private set; }
        public string? Name { get; private set; }
        public string? EnglishName { get; private set; }
        public string? Color { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
