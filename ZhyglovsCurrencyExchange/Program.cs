using Microsoft.EntityFrameworkCore;
using ZhyglovsCurrencyExchange.BusinessLayer.Interfaces;
using ZhyglovsCurrencyExchange.BusinessLayer.Services;
using ZhyglovsCurrencyExchange.DataLayer;
using ZhyglovsCurrencyExchange.Extensions;

namespace ZhyglovsCurrencyExchange
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //add dbcontext
            builder.Services.AddDbContext<CurrencyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Add services to the container.
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<ICurrencyService, CurrencyService>();
            builder.Services.AddHttpClient<IOpenExchangeRatesService, OpenExchangeRatesService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseGlobalExceptionHandler(builder.Environment);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}