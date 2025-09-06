// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Net;

Console.WriteLine("Hello, I'm learning AI");

string filePath = Path.GetFullPath("appsettings.json");
var config = new ConfigurationBuilder()
    .AddJsonFile(filePath)
    .Build();
// Create a kernel with Azure OpenAI chat completion
var deploymentName = config["DeploymentName"];
var endpoint = config["Endpoint"];

var builder = Kernel.CreateBuilder();
builder.AddOpenAIChatCompletion("Phi-3.5-mini-instruct-cuda-gpu", new Uri(" http://127.0.0.1:51560/"), null);
var kernel = builder.Build();
var chat = kernel.GetRequiredService<IChatCompletionService>();

// Start a chat loop
Console.WriteLine("Chat with the Semantic Kernel! Type 'exit' to quit.");
var history = new ChatHistory();
while (true)
{
    Console.Write("You: ");
    var userInput = Console.ReadLine();
    if (string.Equals(userInput, "exit", StringComparison.OrdinalIgnoreCase)) break;
    history.AddUserMessage(userInput);
    var response = await chat.GetChatMessageContentAsync(history);
    Console.WriteLine($"Bot: {response}");
    history.AddAssistantMessage(response.ToString());
}