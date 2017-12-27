using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigureResolver
{
    public class AlgorithmResolver
    {
        Dictionary<int, List<int>> positionsInFigure = new Dictionary<int, List<int>>();
        List<Dictionary<int, int>> allSolutions = new List<Dictionary<int, int>>();

        PermutationsGenerator permutationGenerator;
        int initialCount = 18;
        int givenSum = 75;
        int totalSum = 0;

        public void FindAllSolutions()
        {
            int permutationLength = 5;
            int sum = 75;
            int level = 1;
            int index = 1;

            FixFigurePositions();
            Dictionary<int, int> currentValuesForPositionsInFigure = new Dictionary<int, int>();
            currentValuesForPositionsInFigure.Add(19, 1);
            currentValuesForPositionsInFigure.Add(20, 2);
            currentValuesForPositionsInFigure.Add(21, 3);
            currentValuesForPositionsInFigure.Add(22, 4);
            currentValuesForPositionsInFigure.Add(23, 5);
            currentValuesForPositionsInFigure.Add(24, 6);

            List<int> valuesForPermutationsGenerations = new List<int>();
            for (int j = 1; j < initialCount + 1; j++)
            {
                valuesForPermutationsGenerations.Add(j);
            }
            permutationGenerator = new PermutationsGenerator(permutationLength);

            List<List<int>> permutationList = permutationGenerator.FindPermutations(
                                              valuesForPermutationsGenerations, level, index, new Stack<int>(), sum).ToList();
            int key = 1;
            foreach (var permutation in permutationList)
            {
                FindPositionsValuesWithoutFixedPositions(permutation, valuesForPermutationsGenerations, initialCount, key, currentValuesForPositionsInFigure);
            }
        }

        private void FixFigurePositions()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
            positionsInFigure.Add(1, list);
            list = new List<int>() { 4, 5, 6, 7, 8 };
            positionsInFigure.Add(2, list);
            list = new List<int>() { 7, 8, 9, 10, 11 };
            positionsInFigure.Add(3, list);
            list = new List<int>() { 10, 11, 12, 13, 14 };
            positionsInFigure.Add(4, list);
            list = new List<int>() { 13, 14, 15, 16, 17 };
            positionsInFigure.Add(5, list);
            list = new List<int>() { 16, 17, 18, 1, 2 };
            positionsInFigure.Add(6, list);
        }

        private void FindPositionsValuesWithoutFixedPositions(List<int> permutation, List<int> valuesForPermutationsGenerations, int initialCount, int key, Dictionary<int, int> currentValuesForPositionsInFigure)
        {
            Dictionary<int, int> currentValuesForPositions = new Dictionary<int, int>(currentValuesForPositionsInFigure);
            var lastRawPositions = positionsInFigure[key];
            int i = 0;
            foreach (var value in lastRawPositions)
            {
                if (!currentValuesForPositions.ContainsKey(value))
                {
                    currentValuesForPositions.Add(value, permutation[i]);
                    i++;
                }
            }

            valuesForPermutationsGenerations.Clear();
            for (int j = 1; j < 25; j++)
            {
                if (!currentValuesForPositions.ContainsValue(j))
                {
                    valuesForPermutationsGenerations.Add(j);
                }
            }
            initialCount = valuesForPermutationsGenerations.Count;
            var currentRawPositions = positionsInFigure[key + 1];
            int sum = 0;
            foreach (var currentPosition in currentRawPositions)
            {
                foreach (var lastPosition in lastRawPositions)
                {
                    if (currentPosition == lastPosition)
                    {
                        var value = currentValuesForPositions[currentPosition];
                        sum = sum + value;
                    }
                }
            }
            sum = givenSum - sum;
            if (key + 1 == 6)
            {
                CheckLastRawInFigure(valuesForPermutationsGenerations, currentValuesForPositions, currentRawPositions);
                return;
            }
            permutationGenerator = new PermutationsGenerator(3);
            List<List<int>> permutationList = permutationGenerator.FindPermutations(
                                              valuesForPermutationsGenerations, 1, 1, new Stack<int>(), sum).ToList();
            if (permutationList.Count == 0)
            {
                return;
            }
            foreach (var com in permutationList)
            {
                FindPositionsValuesWithoutFixedPositions(com, valuesForPermutationsGenerations, initialCount, key + 1, currentValuesForPositions);
            }
        }

        private void CheckLastRawInFigure(List<int> lastRawPositionsValues, Dictionary<int, int> currentValuesForPositions, List<int> lastRawPositions)
        {
            int sum = 0;
            foreach (var lastRawPosition in lastRawPositions)
            {
                if (currentValuesForPositions.ContainsKey(lastRawPosition))
                {
                    sum = sum + currentValuesForPositions[lastRawPosition];
                }
            }

            foreach (var value in lastRawPositionsValues)
            {
                sum = sum + value;
            }

            if (sum == givenSum)
            {
                totalSum++;
                var lastRawPositionValue = lastRawPositionsValues[0];
                if (!currentValuesForPositions.ContainsKey(18))
                {
                    currentValuesForPositions.Add(18, lastRawPositionValue);
                }
                else
                {
                    currentValuesForPositions[18] = lastRawPositionValue;
                }
                CalculateSumOfPositionsValuesInFigure(currentValuesForPositions);
            }
        }

        private void CalculateSumOfPositionsValuesInFigure(Dictionary<int, int> currentValuesForPositions)
        {
            int rawsCount = positionsInFigure.Count;
            Dictionary<int, List<int>> positionsInFigureWithFixedPositions = new Dictionary<int, List<int>>();
            List<int> list = new List<int>() { 17, 18, 24, 23, 22, 6, 7 };
            positionsInFigureWithFixedPositions.Add(1, list);
            list = new List<int>() { 16, 15, 20, 19, 21, 9, 8 };
            positionsInFigureWithFixedPositions.Add(2, list);
            list = new List<int>() { 11, 12, 19, 20, 24, 18, 1 };
            positionsInFigureWithFixedPositions.Add(3, list);
            list = new List<int>() { 10, 9, 21, 22, 23, 3, 2 };
            positionsInFigureWithFixedPositions.Add(4, list);
            list = new List<int>() { 14, 15, 20, 24, 23, 3, 4 };
            positionsInFigureWithFixedPositions.Add(5, list);
            list = new List<int>() { 13, 12, 19, 21, 22, 6, 5 };
            positionsInFigureWithFixedPositions.Add(6, list);
            int sum = 0;
            for (int i = 1; i < rawsCount + 1; i++)
            {
                var res = positionsInFigureWithFixedPositions[i];
                sum = 0;
                foreach (var elem in res)
                {
                    sum = sum + currentValuesForPositions[elem];
                }
                if (sum != givenSum)
                {
                    return;
                }
                if (sum == givenSum && i == 6)
                {
                    allSolutions.Add(currentValuesForPositions);
                }
            }
        }

        public List<Dictionary<int, int>> GetAllSolutions()
        {
            return allSolutions;
        }
    }
}
