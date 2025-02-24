namespace PokemonDatabaseAPI.Interfaces;

public interface IAiAgentService
{
    Task<string?> AskAiAgent(string userInput, string authToken);
}