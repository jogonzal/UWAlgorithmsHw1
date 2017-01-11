using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ProofOfSeries;

namespace UnitTests
{
	[TestClass]
	public class SecondStrongestPokemonUnitTests
	{
		[TestMethod]
		public void CalculateStrongestPokemon()
		{
			for(int i = 0; i < 100; i++)
			{
				List<Pokemon> pokemon = new List<Pokemon>()
				{
					new Pokemon(1, "Charmander"),
					new Pokemon(2, "Charizard"),
					new Pokemon(-1, "Pikachu"),
					new Pokemon(0, "raichu"),
					new Pokemon(-9, "bulbasaur")
				};

				var secondStrongest = BattlePokemonExperiment.ObtainSecondStrongest(pokemon, new Random());

				Assert.AreEqual("Charmander", secondStrongest.Item2.Name);
			}

			for (int i = 0; i < 100; i++)
			{
				List<Pokemon> pokemon = new List<Pokemon>()
				{
					new Pokemon(1, "Charmander"),
					new Pokemon(2, "Charizard"),
					new Pokemon(8, "Pikachu"),
					new Pokemon(0, "raichu"),
					new Pokemon(-9, "bulbasaur")
				};

				var secondStrongest = BattlePokemonExperiment.ObtainSecondStrongest(pokemon, new Random());

				Assert.AreEqual("Charizard", secondStrongest.Item2.Name);
			}
		}
	}
}
