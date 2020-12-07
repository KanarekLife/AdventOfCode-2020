using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day7
{
    public class Calculator
    {
        private readonly Dictionary<string, List<(int, string)>> _connections;

        private readonly Dictionary<string, bool> _doesContainShinyGoldBag = new Dictionary<string, bool>();
        
        private Calculator(Dictionary<string, List<(int, string)>> connection)
        {
            _connections = connection;
        }

        public static Calculator Parse(IEnumerable<string> rules)
        {
            var connections = new Dictionary<string, List<(int, string)>>();
            foreach (var rule in rules)
            {
                var data = rule.Split("bags contain");
                var color = data[0].Trim();
                var connectTo = data[1]
                    .Replace(".", "")
                    .Replace("bags", "")
                    .Replace("bag", "")
                    .Split(',')
                    .Select(x=>x.Trim())
                    .Where(x=>x!="no other");
                var connectTargets = new List<(int, string)>();
                foreach (var target in connectTo)
                {
                    var count = int.Parse(new string(target.Where(char.IsDigit).ToArray()).Trim());
                    var colorName = new string(target.Where(x => char.IsLetter(x) || char.IsSeparator(x)).ToArray()).Trim();
                    connectTargets.Add((count, colorName));
                }
                connections.Add(color, connectTargets);
            }
            return new Calculator(connections);
        }

        public int CountHowManyCanContainShinyGoldBag()
        {
            return _connections.Keys.Count(CanContainShinyGoldBag);
        }

        private bool CanContainShinyGoldBag(string key)
        {
            if (_doesContainShinyGoldBag.ContainsKey(key))
            {
                return _doesContainShinyGoldBag[key];
            }
            else
            {
                var containDirectly = _connections[key].Any(x=>x.Item2 == "shiny gold");
                if (containDirectly)
                {
                    _doesContainShinyGoldBag[key] = true;
                    return true;
                }

                if (_connections[key].Select(x=>CanContainShinyGoldBag(x.Item2)).Any(x=>x==true))
                {
                    _doesContainShinyGoldBag[key] = true;
                    return true;
                }
                else
                {
                    _doesContainShinyGoldBag[key] = false;
                    return false;
                }
            }
        }

        public int CountHowManyBagsFitIntoShinyGoldBag()
        {
            var count = 0;
            var queue = new Queue<string>(new []{"shiny gold"});
            while (queue.Count > 0)
            {
                var bag = queue.Dequeue();
                foreach (var connections in _connections[bag])
                {
                    foreach (var temp in Enumerable.Repeat(connections.Item2, connections.Item1))
                    {
                        count++;
                        queue.Enqueue(temp);
                    }
                }
            }

            return count;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var task1 = Calculator.Parse(File.ReadLines("input.txt"));
            Console.WriteLine(task1.CountHowManyCanContainShinyGoldBag());
            Console.WriteLine(task1.CountHowManyBagsFitIntoShinyGoldBag());
        }
    }
}
