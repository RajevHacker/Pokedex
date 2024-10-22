using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokeApiNet;
using Pokedex.Models;

namespace Pokedex.Services.Interface
{
    public interface IPokemonService
    {
        Task<List<string>> GetPokemonNames();
        Task<PokemonModel> GetPokemonDetails(string name);
    }
}