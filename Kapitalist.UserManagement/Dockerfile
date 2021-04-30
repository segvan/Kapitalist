FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80/tcp
ENV ASPNETCORE_URLS http://+:80
ENV ASPNETCORE_ENVIRONMENT docker

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY Kapitalist.UserManagement/Kapitalist.UserManagement.csproj ./Kapitalist.UserManagement/Kapitalist.UserManagement.csproj
COPY Kapitalist.Common/Kapitalist.Common.csproj ./Kapitalist.Common/Kapitalist.Common.csproj
COPY Kapitalist.UserManagement.sln ./Kapitalist.UserManagement.sln
RUN dotnet restore
COPY Kapitalist.UserManagement/ ./Kapitalist.UserManagement/
COPY Kapitalist.Common/ ./Kapitalist.Common/
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kapitalist.UserManagement.dll"]