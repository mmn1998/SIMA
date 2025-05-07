using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.Auths.Forms.Interfaces;

public interface IFormRepository : IRepository<Form>
{
    Task<Form> GetById(long Id);
}
