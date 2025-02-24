using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonDatabaseAPI.Interfaces;
using PokemonDatabaseAPI.Model;
using PokemonDatabaseAPI.Model.Dto;

namespace PokemonDatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonStatController(IPokemonDbContext pokemonDbContext) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllPokemonStats")]
        public async Task<IActionResult> GetAllPokemonStats()
        {
            try
            {
                var stats = await pokemonDbContext.PokemonStats.ToListAsync();
                return Ok(stats);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("GetPokemonStat")]
        public async Task<IActionResult> GetPokemonStat(int pokemonId)
        {
            try
            {
                var stat = await pokemonDbContext.PokemonStats.FirstOrDefaultAsync((p) => p.PokemonId == pokemonId);
                return Ok(stat);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddPokemonStat")]
        public async Task<IActionResult> AddPokemonStat(PokemonStatsDto pokemonStatsDto)
        {
            try
            {
                var newStat = new PokemonStats()
                {
                    Hp = pokemonStatsDto.Hp,
                    Atk = pokemonStatsDto.Atk,
                    Def = pokemonStatsDto.Def,
                    Spd = pokemonStatsDto.Spd,
                    SAtk = pokemonStatsDto.SAtk,
                    SDef = pokemonStatsDto.SDef,
                    PokemonId = pokemonStatsDto.PokemonId,
                };

                await pokemonDbContext.PokemonStats.AddAsync(newStat);
                await pokemonDbContext.SaveChangesAsync();
                return Ok($"Stats for Pokemon with Id {pokemonStatsDto.PokemonId} has been added.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("EditPokemonStat")]
        public async Task<IActionResult> EditPokemonStat(int id, PokemonStatsDto pokemonStatsDto)
        {
            try
            {
                var stat = await pokemonDbContext.PokemonStats.FindAsync(id);
                if (stat == null) return BadRequest("No Stat found with the id provided.");

                stat.Hp = pokemonStatsDto.Hp;
                stat.Atk = pokemonStatsDto.Atk;
                stat.Def = pokemonStatsDto.Def;
                stat.Spd = pokemonStatsDto.Spd;
                stat.SAtk = pokemonStatsDto.SAtk;
                stat.SDef = pokemonStatsDto.SDef;

                await pokemonDbContext.SaveChangesAsync();
                return Ok($"Stats for Pokemon with Id {pokemonStatsDto.PokemonId} has been updated");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeletePokemonStat")]
        public async Task<IActionResult> DeletePokemonStat(int id)
        {
            try
            {
                var stat = await pokemonDbContext.PokemonStats.FindAsync(id);
                if (stat == null) return BadRequest("No Stat found with the id provided.");
                pokemonDbContext.PokemonStats.Remove(stat);
                await pokemonDbContext.SaveChangesAsync();
                return Ok($"Stats for Pokemon Id {stat.PokemonId} has been deleted.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
