using AutoMapper;
using ZhyglovsCurrencyExchange.BusinessLayer.Dtos;
using ZhyglovsCurrencyExchange.DataLayer.Entities;

namespace ZhyglovsCurrencyExchange.BusinessLayer.Profilers;

public class CurrencyProfile : Profile
{
    public CurrencyProfile()
    {
        // Define your mapping here for entities to DTOs
        CreateMap<Currency, CurrencyDto>();
        // Reverse mapping: DTOs to entities
        CreateMap<CurrencyDto, Currency>();
    }
}
