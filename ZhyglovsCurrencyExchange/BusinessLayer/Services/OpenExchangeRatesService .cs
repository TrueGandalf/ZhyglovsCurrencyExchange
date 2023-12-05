using System.Text.Json;
using ZhyglovsCurrencyExchange.BusinessLayer.Interfaces;
using ZhyglovsCurrencyExchange.BusinessLayer.ThirdPartyResponses;

namespace ZhyglovsCurrencyExchange.BusinessLayer.Services;

public class OpenExchangeRatesService : IOpenExchangeRatesService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "041f13216f7840fbbeb5d9828390ce51"; //it mustn't be here in a real project

    public OpenExchangeRatesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ExchangeRatesResponse> GetLatestRatesAsync()
    {
        var url = $"https://openexchangerates.org/api/latest.json?app_id={_apiKey}";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var x = response.Content;
        return await response.Content.ReadFromJsonAsync<ExchangeRatesResponse>();
    }

    public async Task<ExchangeRatesResponse> GetLatestRatesMockedWrongAsync()
    {
        string jsonString = @"{
            disclaimer: 'https://openexchangerates.org/terms/',
            license: 'https://openexchangerates.org/license/',
            timestamp: 1449877801,
            base: 'USD',
            rates:
                {
                AED: 3.672538,
                AFN: 66.809999,
                ALL: 125.716501,
                AMD: 484.902502,
                ANG: 1.788575,
                AOA: 135.295998,
                ARS: 9.750101,
                AUD: 1.390866,
            }}";

        return JsonSerializer.Deserialize<ExchangeRatesResponse>(jsonString);
    }

    public async Task<ExchangeRatesResponse> GetLatestRatesMockedAsync()
    {
        string jsonString = @"{
            ""disclaimer"": ""https://openexchangerates.org/terms/"",
            ""license"": ""https://openexchangerates.org/license/"",
            ""timestamp"": 1449877801,
            ""base"": ""USD"",
            ""rates"": {
                ""AED"": 3.672538,
                ""AFN"": 66.809999,
                ""ALL"": 125.716501,
                ""AMD"": 484.902502,
                ""ANG"": 1.788575,
                ""AOA"": 135.295998,
                ""ARS"": 9.750101,
                ""AUD"": 1.390866
            }
        }";

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return await Task.FromResult(JsonSerializer.Deserialize<ExchangeRatesResponse>(jsonString, options));
    }
}
