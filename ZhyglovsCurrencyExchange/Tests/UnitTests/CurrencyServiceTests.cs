using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;

using Xunit;
using ZhyglovsCurrencyExchange.BusinessLayer.Dtos;
using ZhyglovsCurrencyExchange.BusinessLayer.Interfaces;
using ZhyglovsCurrencyExchange.BusinessLayer.Services;
using ZhyglovsCurrencyExchange.DataLayer;
using ZhyglovsCurrencyExchange.DataLayer.Entities;

namespace ZhyglovsCurrencyExchange.Tests.UnitTests;



public class CurrencyServiceTests
{
    private readonly CurrencyService _service;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IOpenExchangeRatesService> _mockOpenExchangeRatesService;
    private readonly CurrencyDbContext _dbContext;

    public CurrencyServiceTests()
    {
        _mockMapper = new Mock<IMapper>();
        _mockOpenExchangeRatesService = new Mock<IOpenExchangeRatesService>();

        var options = new DbContextOptionsBuilder<CurrencyDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new CurrencyDbContext(options);

        _service = new CurrencyService(_mockMapper.Object, _dbContext, _mockOpenExchangeRatesService.Object);
    }

    [Fact]
    public async Task GetCurrency_ValidId_ReturnsExpectedCurrencyDto()
    {
        // Arrange
        var currencyEntity = new Currency { Id = 1, Code = "USD", Name = "US Dollar", Rate = 1.0m };
        await _dbContext.Currencies.AddAsync(currencyEntity);
        await _dbContext.SaveChangesAsync();

        var currencyDto = new CurrencyDto { Id = 1, Code = "USD", Name = "US Dollar", Rate = 1.0m };
        _mockMapper.Setup(m => m.Map<CurrencyDto>(It.IsAny<Currency>())).Returns(currencyDto);

        // Act
        var result = await _service.GetCurrency(1);

        // Assert
        result.Should().BeEquivalentTo(currencyDto);
    }

    [Fact]
    public async Task GetCurrency_InvalidId_ThrowsArgumentException()
    {
        // Arrange
        var currencyEntity = new Currency { Id = 1, Code = "USD", Name = "US Dollar", Rate = 1.0m };
        await _dbContext.Currencies.AddAsync(currencyEntity);
        await _dbContext.SaveChangesAsync();

        // Act
        Func<Task> act = async () => await _service.GetCurrency(99);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Currency not found");
    }

    // Other tests...
}
