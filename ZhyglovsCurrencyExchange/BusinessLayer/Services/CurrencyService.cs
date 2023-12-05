using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZhyglovsCurrencyExchange.BusinessLayer.Dtos;
using ZhyglovsCurrencyExchange.BusinessLayer.Interfaces;
using ZhyglovsCurrencyExchange.DataLayer;
using ZhyglovsCurrencyExchange.DataLayer.Entities;

namespace ZhyglovsCurrencyExchange.BusinessLayer.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IMapper _mapper;
        private readonly CurrencyDbContext _context;
        private readonly IOpenExchangeRatesService _openExchangeRatesService;

        public CurrencyService(
            IMapper mapper,
            CurrencyDbContext context,
            IOpenExchangeRatesService openExchangeRatesService)
        {
            _mapper = mapper;
            _context = context;
            _openExchangeRatesService = openExchangeRatesService;
        }

        public async Task<decimal> Convert(decimal amount, string fromCurrencyCode, string toCurrencyCode)
        {
            var fromCurrencyEntity = await _context.Currencies.FirstOrDefaultAsync(c => c.Code == fromCurrencyCode);
            var toCurrencyEntity = await _context.Currencies.FirstOrDefaultAsync(c => c.Code == toCurrencyCode);

            if (fromCurrencyEntity == null || toCurrencyEntity == null)
            {
                throw new ArgumentException("Currency not found");
            }

            return amount * toCurrencyEntity.Rate / fromCurrencyEntity.Rate;
        }

        // add methods to get, add, update, delete currencies
        public async Task<CurrencyDto> GetCurrency(int id)
        {
            var currencyEntity = await _context.Currencies.FirstOrDefaultAsync(c => c.Id == id);
            if (currencyEntity == null)
            {
                throw new ArgumentException("Currency not found");
            }

            // autoMapper example
            return _mapper.Map<CurrencyDto>(currencyEntity);
        }

        public async Task<IEnumerable<CurrencyDto>> GetCurrencies()
        {
            var currencyEntities = await _context.Currencies.ToListAsync();
            return currencyEntities.Select(c => new CurrencyDto
            {
                Id = c.Id,
                Code = c.Code,
                Name = c.Name,
                Rate = c.Rate
            });
        }

        public async Task<IEnumerable<CurrencyDto>> SyncCurrencies()
        {
            var currencyResponse = await _openExchangeRatesService.GetLatestRatesMockedAsync();

            var oldCurrencies = await _context.Currencies.ToListAsync();

            var newCurrencies = currencyResponse.Rates.Select(rate => new CurrencyDto
            {
                Code = rate.Key,
                Rate = rate.Value,
            });

            var uncreatedCurrencies = newCurrencies.Where(
                c => !oldCurrencies.Select(o => o.Code).Contains(c.Code)).ToList();

            var newEntities = _mapper.Map<List<Currency>>(uncreatedCurrencies);

            _context.Currencies.AddRange(newEntities);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var a = 1;
            }

            return _mapper.Map<List<CurrencyDto>>(newEntities);
        }

        public async Task<CurrencyDto> AddCurrency(CurrencyDto currency)
        {
            var currencyEntity = new Currency
            {
                Code = currency.Code,
                Name = currency.Name,
                Rate = currency.Rate
            };

            _context.Currencies.Add(currencyEntity);
            await _context.SaveChangesAsync();

            return new CurrencyDto
            {
                Id = currencyEntity.Id,
                Code = currencyEntity.Code,
                Name = currencyEntity.Name,
                Rate = currencyEntity.Rate
            };
        }

        public async Task<CurrencyDto> UpdateCurrency(CurrencyDto currency)
        {
            var currencyEntity = await _context.Currencies.FirstOrDefaultAsync(c => c.Id == currency.Id);
            if (currencyEntity == null)
            {
                throw new ArgumentException("Currency not found");
            }

            currencyEntity.Code = currency.Code;
            currencyEntity.Name = currency.Name;
            currencyEntity.Rate = currency.Rate;
            await _context.SaveChangesAsync();
            return new CurrencyDto
            {
                Id = currencyEntity.Id,
                Code = currencyEntity.Code,
                Name = currencyEntity.Name,
                Rate = currencyEntity.Rate
            };
        }

        public async Task<CurrencyDto> DeleteCurrency(int id)
        {
            var currencyEntity = await _context.Currencies.FirstOrDefaultAsync(c => c.Id == id);
            if (currencyEntity == null)
            {
                throw new ArgumentException("Currency not found");
            }

            var removed = _context.Currencies.Remove(currencyEntity);
            await _context.SaveChangesAsync();

            // create currencyDto from currencyEntity using automapper
            // use automapper to map currencyEntity to currencyDto
            return new CurrencyDto
            {
                Id = currencyEntity.Id,
                Code = currencyEntity.Code,
                Name = currencyEntity.Name,
                Rate = currencyEntity.Rate
            };
        }
    }
}
