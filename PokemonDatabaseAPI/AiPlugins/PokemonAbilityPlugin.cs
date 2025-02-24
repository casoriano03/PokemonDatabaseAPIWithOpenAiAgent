using Microsoft.SemanticKernel;
using PokemonDatabaseAPI.Interfaces;
using PokemonDatabaseAPI.Model;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace PokemonDatabaseAPI.AiPlugins
{
    public class PokemonAbilityPlugin
    {
        private readonly IPokemonDbContext _pokemonDbContext;

        public PokemonAbilityPlugin(IPokemonDbContext pokemonDbContext)
        {
            _pokemonDbContext = pokemonDbContext;
        }

        [KernelFunction("get_pokemon_abilities")]
        [Description("Gets all pokemon abilities registered in the database")]
        [return: Description("List of pokemon abilities")]
        public async Task<List<PokemonAbility>> GetPokemonAbilities(Kernel kernel)
        {
            try
            {
                var pokemonAbilities = await _pokemonDbContext.PokemonAbilities.ToListAsync();
                return pokemonAbilities;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Pokemon: {ex.Message}");
                return new List<PokemonAbility>();
            }
        }


        [KernelFunction("add_pokemon_abilities")]
        [Description("Adds a pokemon ability to the database")]
        [return: Description("Result of the operation")]
        public async Task<IActionResult> AddPokemon(Kernel kernel, [Description("The pokemon ability to add")] PokemonAbility pokemonAbility)
        {
            try
            {
                var existingAbility = await _pokemonDbContext.PokemonAbilities
                    .FirstOrDefaultAsync(a => a.AbilityName == pokemonAbility.AbilityName);

                if (existingAbility == null)
                {
                    await _pokemonDbContext.PokemonAbilities.AddAsync(pokemonAbility);
                    await _pokemonDbContext.SaveChangesAsync();
                    return new OkObjectResult(new { message = "Pokemon ability added successfully.", pokemonAbility });
                }
                else
                {
                    return new OkObjectResult(new { message = "Pokemon ability already exists."});
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Pokemon Ability: {ex.Message}");
                return new StatusCodeResult(500);
            }
        }
    }
}
