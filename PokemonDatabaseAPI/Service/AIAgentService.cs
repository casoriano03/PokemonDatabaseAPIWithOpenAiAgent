using Microsoft.Identity.Client;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using PokemonDatabaseAPI.Interfaces;

namespace PokemonDatabaseAPI.Service
{
    public class AiAgentService(Kernel kernel) : IAiAgentService
    {
        private readonly IChatCompletionService _chatService = kernel.GetRequiredService<IChatCompletionService>();
        private readonly ChatHistory _chatMessages = new ChatHistory("You are nurse joy from the pokemon series.");
        public async Task<string?> AskAiAgent(string userInput, string authToken)
        {
            try
            {
                _chatMessages.AddUserMessage(userInput);

                var response = _chatService.GetStreamingChatMessageContentsAsync(_chatMessages,
                    executionSettings: new OpenAIPromptExecutionSettings()
                    {
                        ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
                    },
                    kernel: kernel);

                string fullMessage = "";
                await foreach (var message in response)
                {
                    fullMessage += message.Content;
                }
                return fullMessage;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
