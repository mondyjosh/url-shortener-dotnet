# build copies all project files and restores NuGet packages
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG TARGETARCH
WORKDIR /src

# Copy project file and restore as distinct layers
COPY --link LinksApi.Web/*.csproj LinksApi.Web/
COPY --link LinksApi/*.csproj LinksApi/
# RUN dotnet restore -a $TARGETARCH LinksApi/LinksApi.csproj
RUN dotnet restore -a $TARGETARCH LinksApi.Web/LinksApi.Web.csproj

# Copy source code and publish app
COPY --link LinksApi.Web/ LinksApi.Web/
COPY --link LinksApi/ LinksApi/

# test-build builds the xUnit test project
FROM build AS test-build

COPY --link ../test/LinksApi/*.csproj test/LinksApi/
WORKDIR /test/LinksApi
RUN dotnet restore

COPY --link ../test .
RUN dotnet build --no-restore

# test-entrypoint exposes tests as the default executable for the stage
FROM test-build AS test
ENTRYPOINT ["dotnet", "test", "--no-build", "--logger:trx"]

# publish builds and publishes complexapp
FROM build AS publish
WORKDIR /src/LinksApi.Web
RUN dotnet publish -a $TARGETARCH --no-restore -o /app

# final is the final runtime stage for running the app
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --link --from=publish /app .
ENTRYPOINT ["dotnet", "LinksApi.Web.dll"]
