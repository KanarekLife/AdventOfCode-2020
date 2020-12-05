using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Day5
{
    class Program
    {
        static int GetRow(IEnumerable<char> instructions)
        {
            var start = 0;
            var end = 127;
            foreach (var chr in instructions)
            {
                var step = (int)Math.Round((end - (decimal)start)/2, MidpointRounding.AwayFromZero);
                switch (chr)
                {
                    case 'F':
                        end -= step;
                        break;
                    case 'B':
                        start += step;
                        break;
                    default:
                        throw new SystemException("There should be no other option.");
                }
            }

            return start;
        }

        static int GetColumn(IEnumerable<char> instructions)
        {
            var start = 0;
            var end = 7;
            foreach (var chr in instructions)
            {
                var step = (int)Math.Round((end - (decimal)start)/2, MidpointRounding.AwayFromZero);
                switch (chr)
                {
                    case 'L':
                        end -= step;
                        break;
                    case 'R':
                        start += step;
                        break;
                    default:
                        throw new SystemException("There should be no other option.");
                }
            }

            return start;
        }
        static int GetSeatId(string pass)
        {
            var row = GetRow(pass.Take(7));
            var column = GetColumn(pass.Skip(7).Take(3));
            return 8 * row + column;
        }

        static void Assert(Expression<Func<bool>> assertion)
        {
            Console.WriteLine(assertion.Compile().Invoke() ? "Correct" : $"ERROR! {assertion.Body}");
        }
        
        static void Main(string[] args)
        {
            Assert(() => GetSeatId("FBFBBFFRLR") == 357);
            Assert(() => GetSeatId("BFFFBBFRRR") == 567);
            Assert(() => GetSeatId("FFFBBBFRRR") == 119);
            Assert(() => GetSeatId("BBFFBBFRLL") == 820);
            var seats = File.ReadLines("input.txt").Select(GetSeatId).ToArray();
            var max = seats.Max();
            Console.WriteLine(max);
            var notFoundSeats = Enumerable
                .Range(0, max + 1)
                .Where(x => !seats.Contains(x))
                .ToArray();
            var yourSeat = notFoundSeats.FirstOrDefault(x => !notFoundSeats.Contains(x - 1) && !notFoundSeats.Contains(x + 1));
            Console.WriteLine(yourSeat);
        }
    }
}
