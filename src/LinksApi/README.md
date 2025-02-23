# LinksApi

The **LinksApi** library project contains the core business logic for URL shortening/retrieval. It provides services and data models for client applications.

## Directory Structure

```
└──📁 src
    └──📁 LinksApi
        ├──📁 Data                              # Data-Access Tier interfaces and classes
        │   ├── I{T}Repository.cs               # Repository interfaces
        │   └── {T}Repository.cs                # Repository implementations
        ├──📁 Exceptions                        # Directory for custom service exceptions
        ├──📁 Requests                          # Directory for service reqeust models
        ├──📁 Responses                         # Directory for service response models
        ├── I{T}Service.cs                      # Service interfaces
        ├── {T}Service.cs                       # Service implementations
        ├── ServiceCollectionExtensions.cs      # Extension methods for registering services in DI
        └── {T}ConfigurationOptions.cs          # Configuration options for the service
```

## Library Design Guidelines

- Everything in the `/Data` directory **must not** be shared externally. The **Data-Access Tier** is an implementation detail that serves **Service-Tier** objects.
- Service implementations **must not** be share externally. Use `ServiceCollectionExtensions` to register `{T}Service` as an implementation of `I{T}Service`.
- Configuration **must** be provided through a `{Custom}ConfigurationOptions.cs`. This allows for stronger messaging around configuration issues.