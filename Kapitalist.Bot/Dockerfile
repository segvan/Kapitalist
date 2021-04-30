FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
ENV DOTNET_ENVIRONMENT docker

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY Kapitalist.Bot/Kapitalist.Bot.csproj ./Kapitalist.Bot/Kapitalist.Bot.csproj
COPY Kapitalist.Common/Kapitalist.Common.csproj ./Kapitalist.Common/Kapitalist.Common.csproj
COPY Kapitalist.Bot.sln ./Kapitalist.Bot.sln
RUN dotnet restore
COPY Kapitalist.Bot/ ./Kapitalist.Bot/
COPY Kapitalist.Common/ ./Kapitalist.Common/
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kapitalist.Bot.dll"]