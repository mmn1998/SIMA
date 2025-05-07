using AutoMapper;
using SIMA.Application.Query.Contract.Features.Auths.Domains;

namespace SIMA.Application.Query.Features.Auths.Domains.Mappers;

internal class DomainQueryMapper : Profile
{
    public DomainQueryMapper()
    {
        CreateMap<Domain.Models.Features.Auths.Domains.Entities.Domain, GetDomainQueryResult>();
    }
}
