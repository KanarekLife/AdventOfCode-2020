using System;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        static bool Validate1(string line)
        {
            var data = line.Split(' ');
            var range = data[0].Split('-');
            var start = int.Parse(range[0]);
            var end = int.Parse(range[1]);
            var letter = data[1].First();
            var password = data[2];
            var countOfLetters = password.Count(x => x == letter);
            return countOfLetters >= start && countOfLetters <= end;
        }

        static bool Validate2(string line)
        {
            var data = line.Split(' ');
            var range = data[0].Split('-');
            var first = int.Parse(range[0]);
            var second = int.Parse(range[1]);
            var letter = data[1].First();
            var password = data[2];
            return password[first - 1] == letter ^ password[second - 1] == letter;
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine(File.ReadLines("input.txt").Where(Validate1).Count());
            Console.WriteLine(File.ReadLines("input.txt").Where(Validate2).Count());
        }
    }
}
