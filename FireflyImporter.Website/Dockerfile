#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["FireflyImporter.Website/FireflyImporter.Website.csproj", "FireflyImporter.Website/"]
COPY ["FireflyImporter.BusinessLayer/FireflyImporter.BusinessLayer.csproj", "FireflyImporter.BusinessLayer/"]
RUN dotnet restore "FireflyImporter.Website/FireflyImporter.Website.csproj"
COPY . .
WORKDIR "/src/FireflyImporter.Website"
RUN dotnet build "FireflyImporter.Website.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FireflyImporter.Website.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FireflyImporter.Website.dll"]