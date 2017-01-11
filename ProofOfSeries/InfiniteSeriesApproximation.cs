using System;
using System.Collections.Generic;
using System.Linq;

namespace ProofOfSeries
{
	/// <summary>
	/// This program calculates the expected number of battles
	/// </summary>
	class InfiniteSeriesApproximation
	{
		public static void PerformInfiniteSeriesCalculation()
		{
			Random random = new Random();

			List<double> nRatios = new List<double>();

			// There is some randomness with the experiments - repeat a number of itmes to obtain a meaningful average
			const int totalBattleExperiments = 5000;
			Console.WriteLine("Here it goes...");
			for (int sampleNumberOfPokemon = 50000; sampleNumberOfPokemon < 60000; sampleNumberOfPokemon++)
			{
				List<double> expectedBattles = new List<double>(totalBattleExperiments);
				for (int battleExperimentIndex = 0; battleExperimentIndex < totalBattleExperiments; battleExperimentIndex++)
				{
					double expectedBattlesIndividual = ObtainExpectedBattles(sampleNumberOfPokemon, random);
					expectedBattles.Add(expectedBattlesIndividual);
				}
				double experimentAverage = expectedBattles.Average();
				double experimentStandardDeviation = CalculateStandardDeviation(expectedBattles);
				Console.WriteLine("For \t{0}, calculated average \t{1}. standard deviation: \t{2}", sampleNumberOfPokemon, experimentAverage, experimentStandardDeviation);

				double nRatio = experimentAverage / sampleNumberOfPokemon;
				nRatios.Add(nRatio);
			}

			double nRatioAverage = nRatios.Average();
			double nRatioStandardDeviation = CalculateStandardDeviation(nRatios);
			Console.WriteLine("Did some experiments and obtained: O(n) = {0} * n with standard deviation {1}.", nRatioAverage, nRatioStandardDeviation);

			Console.ReadLine();
		}

		private static double CalculateStandardDeviation(List<double> list)
		{
			double average = list.Average();
			double sumOfSquaresOfDifferences = list.Select(val => (val - average) * (val - average)).Sum();
			double standardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / list.Count);
			return standardDeviation;
		}

		private static double ObtainExpectedBattles(int n, Random random)
		{
			double totalBattles = 0;
			int logBase2 = Convert.ToInt32(Math.Ceiling(Math.Log(10, 2)));
			for (int i = 0; i < logBase2; i++)
			{
				int nominator = (n - i - 2);
				int denominator = Convert.ToInt32(Math.Pow(2, i));

				double comparisonsInThisIteration = nominator / denominator;
				totalBattles += comparisonsInThisIteration;

				int totalPokemonRemaining = n / denominator;
				int thisPokemonIndex = random.Next(0, totalPokemonRemaining);
				// Case 1. We might have gotten the second best here, in which case we're done
				if (thisPokemonIndex == 0)
				{
					// We're done
					break;
				}
				// Case 2
				else if (thisPokemonIndex == 1)
				{
					// Still need to figure out max pokemon, that means add n-1
					totalBattles += denominator - 1;

					// After that, we're done
					break;
				}
				// Case 3
				else
				{
					// Need to continue iterating
					continue;
				}
			}
			return totalBattles;
		}
	}
}
