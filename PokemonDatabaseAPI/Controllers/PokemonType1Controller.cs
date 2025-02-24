using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonDatabaseAPI.Data;
using PokemonDatabaseAPI.Interfaces;
using PokemonDatabaseAPI.Model;

namespace PokemonDatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonType1Controller(IPokemonDbContext pokemonDbContext) : ControllerBase
    {
        [HttpGet("GetAllPokemonTypes1")]
        public async Task<IActionResult> GetAllPokemonTypes1()
        {
            try
            {
                var pokemonTypes = await pokemonDbContext.PokemonTypes1.ToListAsync();
                return Ok(pokemonTypes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        [HttpGet("GetPokemonType1")]
        public async Task<IActionResult> GetPokemonType1(int id)
        {
            try
            {
                var pokemonType1 = await pokemonDbContext.PokemonTypes1.FindAsync(id);
                return Ok(pokemonType1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddPokemonType1")]
        public async Task<IActionResult> AddPokemonType1(string newType1)
        {
            try
            {
                if (string.IsNullOrEmpty(newType1)) return BadRequest("New Type name cannot be empty.");
                var newType = new PokemonType1
                {
                    TypeName = newType1
                };

                await pokemonDbContext.PokemonTypes1.AddAsync(newType);
                await pokemonDbContext.SaveChangesAsync();
                return Ok($"Type1 {newType} has been successfully added.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("EditPokemonType1")]
        public async Task<IActionResult> EditPokemonType1(int id, string newType1Name)
        {
            try
            {
                var pokemonType1 = await pokemonDbContext.PokemonTypes1.FindAsync(id);
                if (pokemonType1 == null) return BadRequest("No type1 found with the id provided.");
                if (string.IsNullOrEmpty(newType1Name)) return BadRequest("New Type name cannot be empty.");

                pokemonType1.TypeName = newType1Name;
                await pokemonDbContext.SaveChangesAsync();
                return Ok($"Success! Ability with id {id} has changed to {newType1Name}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeletePokemonType1")]
        public async Task<IActionResult> DeletePokemonType1(int id)
        {
            try
            {
                var pokemonType1 = await pokemonDbContext.PokemonTypes1.FindAsync(id);
                if (pokemonType1 == null) return BadRequest("No type1 found with the id provided.");
                pokemonDbContext.PokemonTypes1.Remove(pokemonType1);
                await pokemonDbContext.SaveChangesAsync();
                return Ok($"Pokemon Type1 with id {id} has been deleted");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
