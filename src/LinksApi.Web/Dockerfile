FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/LinksApi.Web/LinksApi.Web.csproj", "src/LinksApi.Web/"]
RUN dotnet restore "src/LinksApi.Web/LinksApi.Web.csproj"
COPY . .
WORKDIR "/src/src/LinksApi.Web"
RUN dotnet build "LinksApi.Web.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "LinksApi.Web.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LinksApi.Web.dll"]
