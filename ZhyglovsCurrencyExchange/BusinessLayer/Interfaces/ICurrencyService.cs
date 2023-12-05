using ZhyglovsCurrencyExchange.BusinessLayer.Dtos;

namespace ZhyglovsCurrencyExchange.BusinessLayer.Interfaces
{
    public interface ICurrencyService
    {
        public Task<decimal> Convert(decimal amount, string fromCurrency, string toCurrency);
        // add methods to get, add, update, delete currencies
        public Task<CurrencyDto> GetCurrency(int id);
        public Task<IEnumerable<CurrencyDto>> GetCurrencies();
        public Task<IEnumerable<CurrencyDto>> SyncCurrencies();
        public Task<CurrencyDto> AddCurrency(CurrencyDto currency);
        public Task<CurrencyDto> UpdateCurrency(CurrencyDto currency);
        public Task<CurrencyDto> DeleteCurrency(int id);

    }
}
