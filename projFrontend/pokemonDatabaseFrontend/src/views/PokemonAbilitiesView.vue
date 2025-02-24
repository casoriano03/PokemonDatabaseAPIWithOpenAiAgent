<script setup>
import axios from 'axios';
import { ref, onMounted } from 'vue';
import PokemonAbilities from '@/components/PokemonAbilities.vue';

const pokemonAbilities = ref([])

onMounted(async() =>{
    try{ 
        const responsePokemonAbilities = await axios.get(`https://localhost:7252/api/PokemonAbility/GetAllPokemonAbilities`);
        pokemonAbilities.value = responsePokemonAbilities.data;
    }catch(error){
        console.log(error)
    }
});
</script>

<template>
    <PokemonAbilities :abilities="pokemonAbilities"/>
</template>