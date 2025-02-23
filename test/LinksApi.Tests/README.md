# LinksApi.Tests

The **LinksApi.Tests** project contains unit tests for the **LinksApi** library.

## Directory Structure

```
└── test
    └── LinksApi.Tests
        ├── GlobalUsings.cs
        ├── LinksApi.Tests.csproj
        └── LinksServiceTests.cs    # Test files are named after the service under test
```

Test files should be named after the service under test.

## Running Tests

To run tests, use your IDE of choice to run this project's tests, or run the following command using the .NET CLI:

```bash
  dotnet test
```

To run tests with detailed output:

```bash
  dotnet test --logger "console;verbosity=detailed"
```

## Test Framework & Dependencies

- **[xUnit](https://xunit.net/)**: Used for unit/integration tests.
- **[Moq](https://github.com/devlooped/moq)**: Used to mock dependencies.


