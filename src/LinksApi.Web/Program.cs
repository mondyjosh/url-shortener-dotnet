using System.Reflection;

using Microsoft.OpenApi.Models;

using LinksApi;
using Microsoft.AspNetCore.Diagnostics;
using LinksApi.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

builder.Services.SetupLinksApiServices();

// Lowercase routes, please
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add Swagger Generator
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Version = "v1",
            Title = "Links Web API",
            Description = "Web API for managing links.",
        }
    );

    // Via reflection, build an XML file name matching the name of the web API project
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline

// Normally, we'd only want to show SwaggerUI when app.Environment.IsDevelopment(),
// but this is a handy way to demo!
app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();

app.UseHttpsRedirection();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is ShortLinkNotFoundException)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new { error = exception.Message });
        }
    });
});

app.UseAuthorization();

app.MapControllers();

app.Run();
