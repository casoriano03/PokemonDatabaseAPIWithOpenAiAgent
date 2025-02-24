using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonDatabaseAPI.Data;
using PokemonDatabaseAPI.Interfaces;
using PokemonDatabaseAPI.Model;
using PokemonDatabaseAPI.Model.Dto;

namespace PokemonDatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonAbilityController(IPokemonDbContext pokemonDbContext) : ControllerBase
    {
        [HttpGet("GetAllPokemonAbilities")]
        public async Task<IActionResult> GetAllPokemonAbilities()
        {
            try
            {
                var pokemonAbilities = await pokemonDbContext.PokemonAbilities.ToListAsync();
                return Ok(pokemonAbilities);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("GetPokemonAbility")]
        public async Task<IActionResult> GetPokemonAbility(int id)
        {
            try
            {
                var pokemonAbility = await pokemonDbContext.PokemonAbilities.FindAsync(id);
                if (pokemonAbility == null) return BadRequest("No ability found with the id provided.");

                return Ok(pokemonAbility);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddPokemonAbility")]
        public async Task<IActionResult> AddPokemonAbility(PokemonAbilityDto pokemonAbilityDto)
        {
            try
            {
                var newAbility = new PokemonAbility
                {
                    AbilityName = pokemonAbilityDto.AbilityName,
                    AbilityDescription = pokemonAbilityDto.AbilityDescription
                };

                await pokemonDbContext.PokemonAbilities.AddAsync(newAbility);
                await pokemonDbContext.SaveChangesAsync();
                return Ok($"Ability {pokemonAbilityDto.AbilityName} has been successfully added.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("EditPokemonAbility")]
        public async Task<IActionResult> EditPokemonAbility(int id, PokemonAbilityDto pokemonAbilityDto)
        {
            try
            {
                var pokemonAbility = await pokemonDbContext.PokemonAbilities.FindAsync(id);
                pokemonAbility.AbilityName = pokemonAbilityDto.AbilityName;
                pokemonAbility.AbilityDescription = pokemonAbilityDto.AbilityDescription;
                await pokemonDbContext.SaveChangesAsync();
                return Ok($"Success! Ability with id {id} has changed to {pokemonAbilityDto.AbilityName}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeletePokemonAbility")]
        public async Task<IActionResult> DeletePokemonAbility(int id)
        {
            try
            {
                var pokemonAbility = await pokemonDbContext.PokemonAbilities.FindAsync(id);
                if (pokemonAbility == null) return BadRequest("No ability found with the id provided.");
                pokemonDbContext.PokemonAbilities.Remove(pokemonAbility);
                await pokemonDbContext.SaveChangesAsync();
                return Ok($"Pokemon Ability with id {id} has been deleted");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
