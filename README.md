# Bookleus

How to run locally

dotnet ef database update --project .\Infrastructure\Bookleus.Data\Bookleus.Data.csproj --startup-project .\Presentation\Bookleus.Web\Bookleus.Web.csproj --context DatabaseContext

dotnet ef database update --project .\Infrastructure\Bookleus.Identity\Bookleus.Identity.csproj --startup-project .\Presentation\Bookleus.Web\Bookleus.Web.csproj --context IdentityDbContext

dotnet run --project .\Presentation\Bookleus.Web\
