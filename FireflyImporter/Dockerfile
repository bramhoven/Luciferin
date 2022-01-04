#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["FireflyImporter/FireflyImporter.csproj", "FireflyImporter/"]
COPY ["FireflyImporter.BusinessLayer/FireflyImporter.BusinessLayer.csproj", "FireflyImporter.BusinessLayer/"]
RUN dotnet restore "FireflyImporter/FireflyImporter.csproj"
COPY . .
WORKDIR "/src/FireflyImporter"
RUN dotnet build "FireflyImporter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FireflyImporter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FireflyImporter.dll"]