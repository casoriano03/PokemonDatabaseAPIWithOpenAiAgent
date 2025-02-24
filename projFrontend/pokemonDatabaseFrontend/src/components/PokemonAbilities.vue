<script setup>

const props = defineProps({
    abilities:{
        type:Array,
        default:() => ({}),
    },
});
</script>

<template>
<div class="container mx-auto p-6">
    <h2 class="text-2xl font-bold text-center mb-4">Pokémon Abilities</h2>
    <div class="overflow-x-auto">
        <table class="min-w-full bg-white border border-gray-200 shadow-md rounded-lg">
            <thead>
                <tr class="bg-[#313167] text-white">
                    <th class="py-2 px-4 text-left">Ability</th>
                    <th class="py-2 px-4 text-left">Description</th>
                </tr>
            </thead>
            <tbody> 
                <tr v-for="ability in abilities" :key="ability.Id" class="border-b border-gray-200">
                    <td class="py-2 px-4">{{ ability.abilityName }}</td>
                    <td class="py-2 px-4">{{ ability.abilityDescription }}</td>
                </tr>
            </tbody>
        </table>
    </div>
</div>
</template>