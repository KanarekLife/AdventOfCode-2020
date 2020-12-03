using System;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {
        static uint CountTrees(char[][] forest, int rightMovement, int downMovement)
        {
            var currentX = 0;
            var currentY = 0;
            uint treesCount = 0;
            do
            {
                currentY += downMovement;
                currentX += rightMovement;
                if (currentX >= forest[currentY].Length)
                {
                    currentX -= forest[currentY].Length;
                }

                if (forest[currentY][currentX] == '#')
                {
                    treesCount++;
                }
            } while (currentY < forest.Length - downMovement);

            return treesCount;
        }
        
        static void Main(string[] args)
        {
            var forest = File.ReadAllLines("input.txt").Select(x => x.ToCharArray()).ToArray();
            //Part One
            Console.WriteLine(CountTrees(forest, 3, 1));
            //Part Two
            var result = new[] {(1, 1), (3, 1), (5, 1), (7, 1), (1, 2)}
                .Select(x => CountTrees(forest, x.Item1, x.Item2))
                .Aggregate((x, y) => x * y);
            Console.WriteLine(result);
        }
    }
}
