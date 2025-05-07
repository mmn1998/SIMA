using SIMA.Domain.Models.Features.Auths.Companies.Entities;
using SIMA.Framework.Core.Repository;
using System.Threading.Tasks;

namespace SIMA.Domain.Models.Features.Auths.Companies
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company> GetById(long id);
    }

}
