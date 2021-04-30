FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
ENV DOTNET_ENVIRONMENT docker

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY Kapitalist.Worker/Kapitalist.Worker.csproj ./Kapitalist.Worker/Kapitalist.Worker.csproj
COPY Kapitalist.Common/Kapitalist.Common.csproj ./Kapitalist.Common/Kapitalist.Common.csproj
COPY Kapitalist.Worker.sln ./Kapitalist.Worker.sln
RUN dotnet restore
COPY Kapitalist.Worker/ ./Kapitalist.Worker/
COPY Kapitalist.Common/ ./Kapitalist.Common/
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kapitalist.Worker.dll"]