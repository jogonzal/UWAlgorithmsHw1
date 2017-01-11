using System;
using System.Collections.Generic;
using System.Linq;

namespace ProofOfSeries
{
	public class Pokemon
	{
		public Pokemon(int battlePower, string name)
		{
			BattlePower = battlePower;
			Name = name;
		}

		public int BattlePower { get; set; }

		public string Name { get; set; }
	}

	public static class BattlePokemonExperiment
	{
		public static void PerformExperiment()
		{
			Random random = new Random();

			// Tweak the settings below as needed
			const int experimentCount = 20;
			const int experimentRepeatCount = 1000;
			const int start = 1000;

			List<double> ratios = new List<double>();
			for (int pokemonExperimentIndex = start; pokemonExperimentIndex < start + experimentCount; pokemonExperimentIndex++)
			{
				List<int> battleCountsForExperiment = new List<int>(experimentRepeatCount);
				for(int experimentRepeatIndex = 0; experimentRepeatIndex < experimentRepeatCount; experimentRepeatIndex++)
				{
					var result = ObtainSecondStrongest(GeneratePokemonListWith(pokemonExperimentIndex, random), random);
					battleCountsForExperiment.Add(result.Item1);
				}
				double battleCountAverageForExperiment = battleCountsForExperiment.Average();
				double ratio = battleCountAverageForExperiment / pokemonExperimentIndex;
				ratios.Add(ratio);
				Console.WriteLine("For {0} pokemon, battleCount was {1}. Ratio {2}.", pokemonExperimentIndex, battleCountAverageForExperiment, ratio);
			}

			double ratioAverage = ratios.Average();
			Console.WriteLine("Global ratios are {0}.", ratioAverage);

			Console.ReadLine();
		}

		private static List<Pokemon> GeneratePokemonListWith(int pokemonExperimentIndex, Random random)
		{
			List<Pokemon> pokemon = new List<Pokemon>(pokemonExperimentIndex);
			for(int i = 0;  i < pokemonExperimentIndex; i++)
			{
				pokemon.Add(new Pokemon(random.Next(), "dummy"));
			}
			return pokemon;
		}

		public static Tuple<int, Pokemon> ObtainSecondStrongest(List<Pokemon> pokemonList, Random random)
		{
			// Copy the list to avoid changes to original
			pokemonList = new List<Pokemon>(pokemonList);

			int numberOfBattles = 0;
			while(pokemonList.Count > 0)
			{
				List<Pokemon> winners = new List<Pokemon>();
				List<Pokemon> losers = new List<Pokemon>();

				Pokemon chosenOne = pokemonList[random.Next(0, pokemonList.Count)];
				pokemonList.Remove(chosenOne);
				foreach(var pokemonToBattle in pokemonList)
				{
					numberOfBattles++;
					if (chosenOne.BattlePower > pokemonToBattle.BattlePower)
					{
						losers.Add(pokemonToBattle);
					} else
					{
						winners.Add(pokemonToBattle);
					}
				}

				if (winners.Count == 1)
				{
					return new Tuple<int, Pokemon>(numberOfBattles, chosenOne);
				} else if (winners.Count == 0)
				{
					Pokemon strongestPokemon = pokemonList[0];
					foreach(var pokemon in pokemonList)
					{
						numberOfBattles += 1;
						if (pokemon.BattlePower > strongestPokemon.BattlePower)
						{
							strongestPokemon = pokemon;
						}
					}
					return new Tuple<int, Pokemon>(numberOfBattles, strongestPokemon);
				}
				else
				{
					pokemonList = winners;
					continue;
				}
			}

			throw new InvalidOperationException();
		}
	}
}
