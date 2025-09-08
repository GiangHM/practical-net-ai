using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using practice_sk_plugins_foundrylocal;


string filePath = Path.GetFullPath("appsettings.json");
var config = new ConfigurationBuilder()
    .AddJsonFile(filePath)
    .Build();

// Create a kernel with Azure OpenAI chat completion
// modelId = deploymentName
var modelId = // "gpt-4o-minigpt-4o-mini";
 config["DeploymentName"] ?? "Phi-3.5-mini-instruct-generic-cpu";
var endpoint = //"https://models.inference.ai.azure.com"; 
config["Endpoint"] ?? "";

// Create a kernel and chat completion service
var builder = Kernel.CreateBuilder();
builder.AddOpenAIChatCompletion(modelId, new Uri(endpoint), null);
var kernel = builder.Build();

// Add plugins to kernel
kernel.Plugins.AddFromType<FlightBookingPlugin>("FlightBookingPlugin");

KernelFunction searchFlights = kernel.Plugins.GetFunction("FlightBookingPlugin", "search_flights");

OpenAIPromptExecutionSettings settings = new()
{
    //MaxTokens = 1000,
    //Temperature = 0.7,
    FunctionChoiceBehavior = FunctionChoiceBehavior.Required(functions: [searchFlights])
};
// Start a chat loop
var history = new ChatHistory();
history.AddSystemMessage("You're flight booking assistant");

AddUserMessage("Find me a flight to Tokyo on the 2026-01-19");
await GetReply();
GetInput();
await GetReply();


void GetInput()
{
    Console.Write("User: ");
    string input = Console.ReadLine()!;
    history.AddUserMessage(input);
}

async Task GetReply()
{
    var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

    ChatMessageContent reply = await chatCompletionService.GetChatMessageContentAsync(
        history,
        executionSettings: settings,
        kernel: kernel
    );
    Console.WriteLine("Assistant: " + reply.ToString());
    history.AddAssistantMessage(reply.ToString());
}

void AddUserMessage(string msg)
{
    Console.WriteLine("User: " + msg);
    history.AddUserMessage(msg);
}
