namespace ZhyglovsCurrencyExchange.BusinessLayer.Dtos;

public class CurrencyDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string? Name { get; set; }
    //public string Country { get; set; } we have it only in Currency Entity
    public decimal Rate { get; set; }
    public string SomeDescription { get; set; } = "default test description";
}
