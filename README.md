# ZhyglovsCurrencyExchange

In general it is better to have at least three projects: \
WebApi (Controllers, responses)\
Core (Business logic, dtos)\
Data (Db connection, context, migrations, entities)\
Tests\
also we can have separate Infranstructure layer,\
etc.\
But for brevity here we have only one project,\
that's why Migrations folder is automatically placed in top-level folder\
and not in DataLayer folder,\
and we have a lot of dependencies in this one and only project. \

Scripts: \
cd .\ZhyglovsCurrencyExchange\
dotnet ef migrations add InitialCreate\
dotnet ef database update \
