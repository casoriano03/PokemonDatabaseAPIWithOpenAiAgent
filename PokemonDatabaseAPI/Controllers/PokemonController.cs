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
    public class PokemonController(IPokemonDbContext pokemonDbContext) : ControllerBase
    {
        [HttpGet("GetAllPokemons")]
        public async Task<IActionResult> GetAllPokemons()
        {
            try
            {
                return Ok(await pokemonDbContext.Pokemons
                    .Include(p => p.PokemonAbility)
                    .Include(p => p.PokemonType1)
                    .Include(p => p.PokemonType2)
                    .ToListAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("GetPokemon")]
        public async Task<IActionResult> GetPokemon(int id)
        {
            try
            {
                var pokemon = await pokemonDbContext.Pokemons
                    //.Include(p=>p.PokemonStats)
                    .Include(p => p.PokemonAbility)
                    .Include(p => p.PokemonType1)
                    .Include(p => p.PokemonType2)
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (pokemon == null) return BadRequest("No pokemon found with the id provided.");

                return Ok(pokemon);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddPokemon")]
        public async Task<IActionResult> AddPokemon(PokemonDto addPokemonDto)
        {
            try
            {
                var newPokemon = new Pokemon
                {
                    PokedexEntryNumber = addPokemonDto.PokedexEntryNumber,
                    PokemonName = addPokemonDto.PokemonName,
                    PokemonDescription = addPokemonDto.PokemonDescription,
                    PokemonImgLink = addPokemonDto.PokemonImgLink,
                    PokemonAbilityId = addPokemonDto.PokemonAbilityId,
                    PokemonType1Id = addPokemonDto.PokemonType1Id,
                    PokemonType2Id = addPokemonDto.PokemonType2Id
                };

                await pokemonDbContext.Pokemons.AddAsync(newPokemon);
                await pokemonDbContext.SaveChangesAsync();
                return Ok($"New pokemon {addPokemonDto.PokemonName} has added successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("EditPokemon")]
        public async Task<IActionResult> EditPokemon(int id, PokemonDto editPokemonDto)
        {
            try
            {
                var pokemon = await pokemonDbContext.Pokemons.FindAsync(id);
                if (pokemon == null) return BadRequest("No pokemon found with the id provided.");

                pokemon.PokedexEntryNumber = editPokemonDto.PokedexEntryNumber;
                pokemon.PokemonName = editPokemonDto.PokemonName;
                pokemon.PokemonDescription = editPokemonDto.PokemonDescription;
                pokemon.PokemonImgLink = editPokemonDto.PokemonImgLink;
                pokemon.PokemonAbilityId = editPokemonDto.PokemonAbilityId;
                pokemon.PokemonType1Id = editPokemonDto.PokemonType1Id;
                pokemon.PokemonType2Id = editPokemonDto.PokemonType2Id;

                await pokemonDbContext.SaveChangesAsync();
                return Ok($"Pokemon with id number {id} has been updated.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeletePokemon")]
        public async Task<IActionResult> DeletePokemon(int id)
        {
            try
            {
                var pokemon = await pokemonDbContext.Pokemons.FindAsync(id);
                if (pokemon == null) return BadRequest("No pokemon found with the id provided.");

                pokemonDbContext.Pokemons.Remove(pokemon);
                await pokemonDbContext.SaveChangesAsync();
                return Ok($"Pokemon with id {id} has been deleted.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
