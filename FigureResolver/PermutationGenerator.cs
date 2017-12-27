using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigureResolver
{
    public class PermutationsGenerator
    {
        private LinkedList<List<int>> _permutationList = new LinkedList<List<int>>();
        private readonly Dictionary<int, int> _indexesWithLevels = new Dictionary<int, int>();
        private readonly int _permutationLength;
        public PermutationsGenerator(int permutationLength)
        {
            _permutationLength = permutationLength;
        }
        public void InitializeData()
        {
            for (int i = 0; i < _permutationLength; i++)
            {
                _indexesWithLevels.Add(i + 1, i);
            }
        }

        public LinkedList<List<int>> FindPermutations(List<int> list, int level, int index, Stack<int> stack, int sum)
        {
            InitializeData();
            bool checker = true;
            while (checker)
            {
                index = _indexesWithLevels[level];
                bool levelUp = false;
                for (int i = index; i < list.Count; i++)
                {
                    if (level < _permutationLength)
                    {
                        MoveToUpperLevel(ref level, index, list, stack);
                        levelUp = true;
                        break;
                    }
                    Swap(list, level - 1, i);
                    stack.Push(list[level - 1]);
                    if (stack.Count == _permutationLength)
                    {
                        AddPermutation(stack, sum);
                    }
                    Swap(list, level - 1, i);
                }

                if (!levelUp)
                {
                    checker = MoveToLowerLevel(ref level, list, stack);
                }
            }

            return _permutationList;
        }

        private void MoveToUpperLevel(ref int level, int index, List<int> list, Stack<int> stack)
        {
            index = _indexesWithLevels[level];
            Swap(list, level - 1, index);
            stack.Push(list[level - 1]);
            level++;
        }

        private bool MoveToLowerLevel(ref int level, List<int> list, Stack<int> stack)
        {
            if (level != 1)
                level--;
            AdjustStackCountToCurrentLevel(stack, level);
            int index = _indexesWithLevels[level];
            Swap(list, level - 1, index);
            UpdateIndexesForTheLevels(level, index, list.Count);
            while (index >= list.Count - 1)
            {
                if (level == 1)
                {
                    AdjustStackCountToCurrentLevel(stack, level);
                    index = _indexesWithLevels[level];
                    if (index < list.Count - 1)
                    {
                        _indexesWithLevels[level] = index + 1;
                    }
                    else return false;
                }
                else
                {
                    _indexesWithLevels[level] = level - 1;
                    level--;
                    AdjustStackCountToCurrentLevel(stack, level);
                    index = _indexesWithLevels[level];
                    Swap(list, level - 1, index);
                    UpdateIndexesForTheLevels(level, index, list.Count);
                }

            }
            return true;
        }

        private void AddPermutation(Stack<int> stack, int sum)
        {
            int sumLocal = 0;
            foreach (var elem in stack)
            {
                sumLocal = sumLocal + elem;
            }
            if (sumLocal == sum)
            {
                List<int> listNew = new List<int>();
                listNew.AddRange(stack);
                _permutationList.AddLast(listNew);
            }
            stack.Pop();
        }

        private void UpdateIndexesForTheLevels(int level, int index, int listCount)
        {
            if (index <= listCount - 1)
            {
                _indexesWithLevels[level] = index + 1;
            }
        }

        private void AdjustStackCountToCurrentLevel(Stack<int> stack, int currentLevel)
        {
            while (stack.Count >= currentLevel)
            {
                if (stack.Count != 0)
                    stack.Pop();
            }
        }

        private void Swap(List<int> list, int position1, int position2)
        {
            int temp = list[position1];
            list[position1] = list[position2];
            list[position2] = temp;
        }

        public void PrintPermutations(int permutationLength)
        {
            //foreach (var permutation in _permutationList)
            //{
            //    foreach (var value in permutation)
            //        Console.Write(value);
            //    Console.WriteLine();
            //    Console.WriteLine(" --------------------------------------");
            //}
            int count = _permutationList.Where(perm => perm.Count() == permutationLength).Count();
            Console.WriteLine("The number of permutations is " + count);
        }

    }
}
