FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

COPY . .
WORKDIR /src/Template.Project.Api

RUN dotnet build "Template.Project.Api.csproj" --configfile /NuGet.Config -c Release -o /app/build
RUN dotnet publish "Template.Project.Api.csproj" --configfile /NuGet.Config -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

WORKDIR /app
EXPOSE 80
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Template.Project.Api.dll"]