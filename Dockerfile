# syntax=docker/dockerfile:1

# Build stage
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
COPY . /source
WORKDIR /source/src/LinksApi.Web

# Allow architecture-specific builds 
# Placed here to cache previous steps across architectures
ARG TARGETARCH

# Build with cached NuGet packages
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish -a $TARGETARCH --use-current-runtime --self-contained false -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app

# Ensure app binds to the correct port
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Copy built app from build stage
COPY --from=build /app .

# Run as a non-root user 
# Ref: https://devblogs.microsoft.com/dotnet/securing-containers-with-rootless/
USER $APP_UID

# Define runtime's main process
ENTRYPOINT ["dotnet", "LinksApi.Web.dll"]
