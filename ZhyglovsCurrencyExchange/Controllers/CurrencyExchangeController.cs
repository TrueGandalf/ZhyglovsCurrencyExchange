using Microsoft.AspNetCore.Mvc;
using ZhyglovsCurrencyExchange.BusinessLayer.Interfaces;

namespace ZhyglovsCurrencyExchange.Controllers;

[Route("api/v1/exchange")]
public class CurrencyExchangeController : ControllerBase
{
    private readonly IOpenExchangeRatesService _exchangeRatesService;

    public CurrencyExchangeController(IOpenExchangeRatesService exchangeRatesService)
    {
        _exchangeRatesService = exchangeRatesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetExchangeRate()
    {
        var response = await _exchangeRatesService.GetLatestRatesAsync();
        return Ok(response);
    }
    
    [HttpGet("mocked")]
    public async Task<IActionResult> GetMockedExchangeRate()
    {
        var response = await _exchangeRatesService.GetLatestRatesMockedAsync();
        return Ok(response);
    }
}
