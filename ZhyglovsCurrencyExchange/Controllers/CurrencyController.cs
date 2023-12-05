using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ZhyglovsCurrencyExchange.BusinessLayer.Dtos;
using ZhyglovsCurrencyExchange.BusinessLayer.Interfaces;

namespace ZhyglovsCurrencyExchange.Controllers;

[Route("api/v1/currencies")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _currencyConversionService;

    public CurrencyController(ICurrencyService currencyConversionService)
    {
        _currencyConversionService = currencyConversionService;
    }

    [HttpGet("convert")]
    public async Task<IActionResult> Convert(
        // make [Required>0] decimal amount required to be more than 0
        [Required, Range(0, double.MaxValue)] decimal amount,
        // string fromCurrency,  requeired to be 3 characters long and not null
        [Required, StringLength(3, MinimumLength = 3)] string fromCurrencyCode,
        [Required, StringLength(3, MinimumLength = 3)] string toCurrencyCode)
    {
        var result = await _currencyConversionService.Convert(amount, fromCurrencyCode, toCurrencyCode);
        return Ok(result);
    }

    //write an endpoint that will get one currency from db
    [HttpGet("{id}")]
public async Task<IActionResult> GetCurrency(int id)
    {
        var currency = await _currencyConversionService.GetCurrency(id);
        return Ok(currency);
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrencies()
    {
        var currencies = await _currencyConversionService.GetCurrencies();
        return Ok(currencies);
    }

    [HttpGet("sync-aka-create-mocked-data")]
    public async Task<IActionResult> SyncCurrencies()
    {
        var currencies = await _currencyConversionService.SyncCurrencies();
        return Ok(currencies);
    }

    //write an endpoint that will add currency to db
    [HttpPost]
    public async Task<IActionResult> AddCurrency([FromBody] CurrencyDto currency)
    {
        var addedCurrency = await _currencyConversionService.AddCurrency(currency);
        return Ok(addedCurrency);
    }

    //write an endpoint that will update currency in db
    [HttpPut]
    public async Task<IActionResult> UpdateCurrency([FromBody] CurrencyDto currency)
    {
        var updatedCurrency = await _currencyConversionService.UpdateCurrency(currency);
        return Ok(updatedCurrency);
    }

    //write an endpoint that will delete currency from db
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCurrency(int id)
    {
        await _currencyConversionService.DeleteCurrency(id);
        return Ok();
    }
}
