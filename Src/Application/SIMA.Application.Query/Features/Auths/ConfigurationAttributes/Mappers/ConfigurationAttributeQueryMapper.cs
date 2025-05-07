using AutoMapper;
using SIMA.Application.Query.Contract.Features.Auths.ConfigurationAttributes;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Entities;

namespace SIMA.Application.Query.Features.Auths.ConfigurationAttributes.Mappers;

internal class ConfigurationAttributeQueryMapper : Profile
{
    public ConfigurationAttributeQueryMapper()
    {
        CreateMap<ConfigurationAttribute, GetConfigurationAttributeQueryResult>();
    }
}
