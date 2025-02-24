using Microsoft.SemanticKernel;
using PokemonDatabaseAPI.Interfaces;
using PokemonDatabaseAPI.Model;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace PokemonDatabaseAPI.AiPlugins
{
    public class PokemonType2Plugin
    {
        private readonly IPokemonDbContext _pokemonDbContext;

        public PokemonType2Plugin(IPokemonDbContext pokemonDbContext)
        {
            _pokemonDbContext = pokemonDbContext;
        }

        [KernelFunction("get_pokemon_type2")]
        [Description("Gets all pokemon type2 registered in the database")]
        [return: Description("List of pokemon type2")]
        public async Task<List<PokemonType2>> GetPokemonType2(Kernel kernel)
        {
            try
            {
                var pokemonType2 = await _pokemonDbContext.PokemonTypes2.ToListAsync();

                return pokemonType2;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Pokemon: {ex.Message}");
                return new List<PokemonType2>();
            }
        }
    }
}
