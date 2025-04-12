// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel;

var builder = Host.CreateApplicationBuilder(args);

// AddOllamaChatCompletion
// There are some overload methods
// We can pass a custom HttpClient to AddOllamaChatCompletion
#pragma warning disable SKEXP0070
builder.Services.AddOllamaChatCompletion(
    modelId: "",
    endpoint: new Uri("")
    );

builder.Services.AddTransient((serviceProvider) =>
{
    return new Kernel(serviceProvider);
});
Console.WriteLine("Hello, World!");
