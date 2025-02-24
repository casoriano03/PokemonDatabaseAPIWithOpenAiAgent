using Microsoft.SemanticKernel;
using PokemonDatabaseAPI.Model;
using System.ComponentModel;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonDatabaseAPI.Interfaces;
using PokemonDatabaseAPI.Model.Dto;

namespace PokemonDatabaseAPI.AiPlugins
{
    public class PokemonPlugin
    {
        private readonly IPokemonDbContext _pokemonDbContext;

        public PokemonPlugin(IPokemonDbContext pokemonDbContext)
        {
            _pokemonDbContext = pokemonDbContext;
        }

        [KernelFunction("get_pokemons")]
        [Description("Gets all pokemons registered in the database")]
        [return: Description("List of pokemons")]
        public async Task<List<Pokemon>> GetPokemons(Kernel kernel)
        {
            try
            {
                var pokemons = await _pokemonDbContext.Pokemons
                    .Include(p => p.PokemonAbility)
                    .Include(p => p.PokemonType1)
                    .Include(p => p.PokemonType2)
                    .ToListAsync();
                return pokemons;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Pokemon: {ex.Message}");
                return new List<Pokemon>();
            }
        }

        [KernelFunction("add_pokemon")]
        [Description("Adds a pokemon to the database")]
        [return: Description("Result of the operation")]
        public async Task<IActionResult> AddPokemon(Kernel kernel, [Description("The pokemon to add")] AiPokemonDto aiPokemonDto)
        {
            try
            {
                var existingPokemon = await _pokemonDbContext.Pokemons
                    .FirstOrDefaultAsync(p => p.PokedexEntryNumber == aiPokemonDto.PokedexEntryNumber);

                var existingAbility = await _pokemonDbContext.PokemonAbilities
                    .FirstOrDefaultAsync(a => a.AbilityName == aiPokemonDto.PokemonAbilityName);

                var existingType1 = await _pokemonDbContext.PokemonTypes1
                    .FirstOrDefaultAsync(t => t.TypeName == aiPokemonDto.PokemonType1Name);

                var existingType2 = await _pokemonDbContext.PokemonTypes2
                    .FirstOrDefaultAsync(t => t.TypeName == aiPokemonDto.PokemonType2Name);

                var type2Null = await _pokemonDbContext.PokemonTypes2
                    .FirstOrDefaultAsync(t => t.TypeName == null);

                if (existingPokemon != null) {
                    return new OkObjectResult(new { message = "Pokemon already exists." });
                }

                if (existingAbility == null)
                {
                    var newPokemonAbility = new PokemonAbility()
                    {
                        AbilityName = aiPokemonDto.PokemonAbilityName,
                        AbilityDescription = aiPokemonDto.PokemonAbilityDescription
                    };
                    await _pokemonDbContext.PokemonAbilities.AddAsync(newPokemonAbility);
                    await _pokemonDbContext.SaveChangesAsync();
                    existingAbility = newPokemonAbility;
                }

                int? type2Id = existingType2?.Id;

                Pokemon newPokemon = new Pokemon()
                {
                    PokedexEntryNumber = aiPokemonDto.PokedexEntryNumber,
                    PokemonName = aiPokemonDto.PokemonName,
                    PokemonDescription = aiPokemonDto.PokemonDescription,
                    PokemonImgLink = aiPokemonDto.PokemonImgLink,
                    PokemonAbilityId = existingAbility.Id,
                    PokemonType1Id = existingType1.Id,
                    PokemonType2Id = type2Id
                };
               
                await _pokemonDbContext.Pokemons.AddAsync(newPokemon);
                await _pokemonDbContext.SaveChangesAsync();

                var pokemonEntity = await _pokemonDbContext.Pokemons
                    .FirstOrDefaultAsync(a => a.PokedexEntryNumber == aiPokemonDto.PokedexEntryNumber);
                if (pokemonEntity == null)
                {
                    return new OkObjectResult(new { message = "Pokemon already does not exists." });
                }

                PokemonStats newStats = new PokemonStats()
                {
                    Hp = aiPokemonDto.Hp,
                    Atk = aiPokemonDto.Atk,
                    Def = aiPokemonDto.Def,
                    Spd = aiPokemonDto.Spd,
                    SAtk = aiPokemonDto.SAtk,
                    SDef = aiPokemonDto.SDef,
                    PokemonId = pokemonEntity.Id
                };

                await _pokemonDbContext.PokemonStats.AddAsync(newStats);
                await _pokemonDbContext.SaveChangesAsync();

                return new OkObjectResult(new { message = "Pokemon added successfully." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Pokemon: {ex.Message}");
                return new StatusCodeResult(500);
            }
        }
    }
}
