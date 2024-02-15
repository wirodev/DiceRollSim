using System;
using System.Security.Cryptography;

class Program
{
	static void Main(string[] args)
	{
		Console.Write("Enter the number of times to roll the dice: ");
		int numberOfRolls;
		
		while (!int.TryParse(Console.ReadLine(), out numberOfRolls) || numberOfRolls <= 0)
		{
			Console.WriteLine("Please enter a valid positive integer.");
			Console.Write("Enter the number of times to roll the dice: ");
		}

		// Initialize counters for each dice face for a single dice
		int[] singleDiceOutcomes = new int[6];
		// Initialize counters for the sum of two dice
		int[] twoDiceOutcomes = new int[11]; // Sums from 2 to 12

		for (int i = 0; i < numberOfRolls; i++)
		{
			int roll1 = RandomDiceRoll(1, 6);
			int roll2 = RandomDiceRoll(1, 6);
			singleDiceOutcomes[roll1 - 1]++;
			twoDiceOutcomes[(roll1 + roll2) - 2]++; // Adjust index for sum range from 2 to 12
		}

		// Display the results for a single dice
		Console.WriteLine("\nOutcome (Single Dice) | Count");
		Console.WriteLine("-----------------------|------");
		for (int i = 0; i < singleDiceOutcomes.Length; i++)
		{
			Console.WriteLine($"          {i + 1}           |   {singleDiceOutcomes[i]}");
		}

		// Display the results for the sum of two dice
		Console.WriteLine("\nOutcome (Sum of Two Dice) | Count");
		Console.WriteLine("----------------------------|------");
		for (int i = 0; i < twoDiceOutcomes.Length; i++)
		{
			Console.WriteLine($"             {i + 2}            |   {twoDiceOutcomes[i]}");
		}
		
		// Find the max count for scaling the histogram
        int maxCount = 0;
        foreach (int count in twoDiceOutcomes)
        {
            if (count > maxCount)
                maxCount = count;
        }

        Console.WriteLine("\nHistogram of the Sum of Two Dice Rolls:");
        for (int i = 0; i < twoDiceOutcomes.Length; i++)
        {
            Console.Write($"{i + 2}: ".PadRight(5));

            // Scale the bar length based on the maxCount to fit the histogram in the console window
            int barLength = (int)((double)twoDiceOutcomes[i] / maxCount * 50); // Adjust the scale factor (50) as needed

            Console.WriteLine(new string('█', barLength));
        }
	}

	

	static int RandomDiceRoll(int min, int max)
	{
		using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
		{
			byte[] randomNumber = new byte[4];
			rng.GetBytes(randomNumber);
			int value = BitConverter.ToInt32(randomNumber, 0) & int.MaxValue;
			return min + (value % (max - min + 1));
		}
	}
}
