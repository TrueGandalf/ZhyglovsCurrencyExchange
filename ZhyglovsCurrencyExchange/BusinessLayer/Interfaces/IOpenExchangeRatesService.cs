using ZhyglovsCurrencyExchange.BusinessLayer.ThirdPartyResponses;

namespace ZhyglovsCurrencyExchange.BusinessLayer.Interfaces;

public interface IOpenExchangeRatesService
{
    Task<ExchangeRatesResponse> GetLatestRatesAsync();
    Task<ExchangeRatesResponse> GetLatestRatesMockedAsync();
}
