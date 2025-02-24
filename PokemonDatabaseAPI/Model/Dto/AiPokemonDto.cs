using System.ComponentModel.DataAnnotations;

namespace PokemonDatabaseAPI.Model.Dto
{
    public class AiPokemonDto
    {
        [Range(1, 1000, ErrorMessage = "Pokedex Entry Number must be between 1 and 1000.")]
        public required int PokedexEntryNumber { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Pokemon Name must be between 2 and 50 characters")]
        public required string PokemonName { get; set; }

        [StringLength(300, MinimumLength = 10, ErrorMessage = "Pokemon Description must be between 10 and 300 characters")]
        public required string PokemonDescription { get; set; }
        [StringLength(300, MinimumLength = 10, ErrorMessage = "Pokemon Image Link must be between 10 and 100 characters")]
        public required string PokemonImgLink { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Pokemon Ability must be between 3 and 100 characters.")]
        public required string PokemonAbilityName { get; set; }
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Pokemon AbilityDescription must be between 3 and 300 characters.")]
        public required string PokemonAbilityDescription { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Pokemon Type1Name must be between 3 and 100 characters.")]
        public required string PokemonType1Name { get; set; }
        public string? PokemonType2Name { get; set; }

        [Range(1, 800, ErrorMessage = "Hp must be between 1 and 800.")]
        public required int Hp { get; set; }
        [Range(1, 800, ErrorMessage = "Atk must be between 1 and 800.")]
        public required int Atk { get; set; }
        [Range(1, 800, ErrorMessage = "Def must be between 1 and 800.")]
        public required int Def { get; set; }
        [Range(1, 800, ErrorMessage = "Spd must be between 1 and 800.")]
        public required int Spd { get; set; }
        [Range(1, 800, ErrorMessage = "SAtk must be between 1 and 800.")]
        public required int SAtk { get; set; }
        [Range(1, 800, ErrorMessage = "SDef must be between 1 and 800.")]
        public required int SDef { get; set; }
    }
}
