using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ZhyglovsCurrencyExchange.DataLayer.Entities;

namespace ZhyglovsCurrencyExchange.DataLayer.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        // Set the table name (optional, by default EF uses the class name as table name)
        builder.ToTable("Currencies");

        // Set the primary key
        builder.HasKey(c => c.Id);

        // Set the properties with the relevant column types and constraints
        builder.Property(c => c.Code)
            .IsRequired() // Make Code a required field
            .HasMaxLength(3); // Assuming currency code is ISO 4217, which is 3 letters.

        builder.Property(c => c.Name)
            //.IsRequired()
            .HasMaxLength(50); // Set a max length for the name.

        builder.Property(c => c.Country)
            .HasMaxLength(50); // Optional: Set a max length for the country.
                               // Not calling IsRequired() implies that Country is an optional field
        
        builder.Property(c => c.Rate)
            .IsRequired();
    }
}
