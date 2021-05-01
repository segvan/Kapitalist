FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY Kapitalist.RatesApi/Kapitalist.RatesApi.csproj ./Kapitalist.RatesApi/Kapitalist.RatesApi.csproj
COPY Kapitalist.Common/Kapitalist.Common.csproj ./Kapitalist.Common/Kapitalist.Common.csproj
COPY Kapitalist.RatesApi.sln ./Kapitalist.RatesApi.sln
RUN dotnet restore
COPY Kapitalist.RatesApi/ ./Kapitalist.RatesApi/
COPY Kapitalist.Common/ ./Kapitalist.Common/
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kapitalist.RatesApi.dll"]