using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static int CountWhereAnyoneAnsweredYes(string content)
        {
            var temp = content.Split('\n');
            var results = new List<int>();
            var buffer = new Dictionary<char, bool>();
            foreach (var line in temp.Append("\n"))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    results.Add(buffer.Count(x=>x.Value));
                    buffer.Clear();
                }
                else
                {
                    foreach (var chr in line)
                    {
                        buffer[chr] = true;
                    }
                }
            }

            return results.Sum();
        }

        static int CountWhereEveryoneAnsweredYes(string content)
        {
            var temp = content.Split('\n');
            var results = new List<int>();
            var buffer = new Dictionary<char, int>();
            var n = 0;
            foreach (var line in temp.Append("\n"))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    results.Add(buffer.Count(x=>x.Value == n));
                    buffer.Clear();
                    n = 0;
                }
                else
                {
                    n++;
                    foreach (var chr in line)
                    {
                        buffer[chr] = buffer.ContainsKey(chr) ? buffer[chr]+1 : 1;
                    }
                }
            }

            return results.Sum();
        }
        
        static void Main(string[] args)
        {
            var content = File.ReadAllText("input.txt");
            Console.WriteLine(CountWhereAnyoneAnsweredYes(content));
            Console.WriteLine(CountWhereEveryoneAnsweredYes(content));
        }
    }
}
