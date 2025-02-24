using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Services;
using PokemonDatabaseAPI.Interfaces;
using PokemonDatabaseAPI.Model;
using PokemonDatabaseAPI.Model.Dto;

namespace PokemonDatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiAgentController(IAiAgentService aiAgentService) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost("chat")]
        public async Task<IActionResult> AskAiAgent(AiChatMessageDto aiChatMessageDto)
        {
            try
            {
                if (string.IsNullOrEmpty(aiChatMessageDto.Message))
                {
                    return BadRequest("Message cannot be empty.");
                }

                var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return Unauthorized("Authorization token is missing or invalid.");
                }

                string authToken = authHeader.Substring("Bearer ".Length).Trim();

                var aiResponse = await aiAgentService.AskAiAgent(aiChatMessageDto.Message, authToken);

                return Ok(new { response = aiResponse });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
