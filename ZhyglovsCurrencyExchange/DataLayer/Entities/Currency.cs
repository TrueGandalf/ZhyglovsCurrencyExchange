namespace ZhyglovsCurrencyExchange.DataLayer.Entities;

public class Currency
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string? Name { get; set; }
    public string? Country { get; set; }
    public decimal Rate { get; set; }
}
