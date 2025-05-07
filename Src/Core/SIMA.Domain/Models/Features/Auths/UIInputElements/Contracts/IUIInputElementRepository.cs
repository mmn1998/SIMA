using SIMA.Domain.Models.Features.Auths.UIInputElements.Entities;
using SIMA.Domain.Models.Features.Auths.UIInputElements.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.UIInputElements.Contracts;

public interface IUIInputElementRepository : IRepository<UIInputElement>
{
    Task<UIInputElement> GetById(UIInputElementId id);
}
