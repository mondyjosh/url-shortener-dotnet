# LinksApi

The **LinksApi** library project contains the core business logic for URL shortening/retrieval. It provides services and data models for client applications.

## Key Responsibilities

- Provides an abstraction layer over database access.
- Implements business rules for short link creation and retrieval.
- Encapsulates encoding logic (Base62).
- Defines clear input and output models for link operations.

## Directory Structure

```
└── src
    └── LinksApi
        ├── Data                                # Data-Access Tier interfaces and classes
        │   ├── I{T}Repository.cs               # Repository interface
        │   └── {T}Repository.cs                # Repository implementation
        ├── Exceptions                          # Directory for custom service exceptions
        ├── Requests                            # Directory for service reqeust models
        ├── Responses                           # Directory for service response models
        ├── I{T}Service.cs                      # Service interface
        ├── {T}Service.cs                       # Service implementation
        ├── ServiceCollectionExtensions.cs      # Extension methods for registering services in dependency injection
        └── {T}ConfigurationOptions.cs          # Configuration options for the service.
```

## Library Design Guidelines

- Everything in the `/Data` directory **must not** be shared externally. The Data-Access Tier is an implementation detail that serves Service-Tier objects.
- Service implementations **must not** be share externally. Use `ServiceCollectionExtensions` to register `{T}Service` as an implementation of `I{T}Service`.
- Configuration **must** be provided through a `{Custom}ConfigurationOptions.cs`. This allows for stronger messaging around configuration issues.