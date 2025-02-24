using Microsoft.SemanticKernel;
using PokemonDatabaseAPI.Interfaces;
using PokemonDatabaseAPI.Model;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace PokemonDatabaseAPI.AiPlugins
{
    public class PokemonType1Plugin
    {
        private readonly IPokemonDbContext _pokemonDbContext;

        public PokemonType1Plugin(IPokemonDbContext pokemonDbContext)
        {
            _pokemonDbContext = pokemonDbContext;
        }

        [KernelFunction("get_pokemon_type1")]
        [Description("Gets all pokemon type1 registered in the database")]
        [return: Description("List of pokemon type1")]
        public async Task<List<PokemonType1>> GetPokemonType1(Kernel kernel)
        {
            try
            {
                var pokemonType1 = await _pokemonDbContext.PokemonTypes1.ToListAsync();
                   
                return pokemonType1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Pokemon: {ex.Message}");
                return new List<PokemonType1>();
            }
        }
    }
}
