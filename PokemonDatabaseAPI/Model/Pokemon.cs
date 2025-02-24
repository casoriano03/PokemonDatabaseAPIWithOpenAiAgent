﻿using System.ComponentModel.DataAnnotations;

namespace PokemonDatabaseAPI.Model
{
    public class Pokemon
    {
        public int Id { get; set; }
        [Range(1,1000, ErrorMessage = "Pokedex Entry Number must be between 1 and 1000.")]
        public required int PokedexEntryNumber { get; set; }
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Pokemon Name must be between 1 and 50 characters")]
        public required string PokemonName { get; set; }
        [StringLength(300, MinimumLength = 10, ErrorMessage = "Pokemon Description must be between 10 and 300 characters")]
        public required string PokemonDescription { get; set; }
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Pokemon Image Link must be between 10 and 100 characters")]
        public required string PokemonImgLink { get; set; }
        //public PokemonStats? PokemonStats { get; set; }
        [Range(1, 350, ErrorMessage = "Pokemon Ability Id must be between 1 and 350.")]
        public int? PokemonAbilityId { get; set; }
        public PokemonAbility? PokemonAbility { get; set; }
        [Range(1, 20, ErrorMessage = "Pokemon Type1 Id must be between 1 and 20.")]
        public int? PokemonType1Id { get; set; }
        public PokemonType1? PokemonType1 { get; set; }
        [Range(1, 20, ErrorMessage = "Pokemon Type2 Id must be between 1 and 20.")]
        public int? PokemonType2Id { get; set; }
        public PokemonType2? PokemonType2 { get; set; }
    }
}
