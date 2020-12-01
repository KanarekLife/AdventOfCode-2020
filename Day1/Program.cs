using System;
using System.IO;
using System.Linq;

Console.WriteLine(Day1Task1(new int[] { 1721, 979, 366, 299, 675, 1456 }) == 514579 ? "Test passed" : "Test FAILED");
Console.WriteLine(Day1Task2(new int[] { 1721, 979, 366, 299, 675, 1456 }) == 241861950 ? "Test passed" : "Test FAILED");
Console.WriteLine(Day1Task1(File.ReadAllLines("input.txt").Select(int.Parse).ToArray()));
Console.WriteLine(Day1Task2(File.ReadAllLines("input.txt").Select(int.Parse).ToArray()));

static int Day1Task1(int[] n)
{
    const int T = 2020;
    for (int i = 0; i < n.Length; i++)
    {
        for (int j = i + 1; j < n.Length; j++)
        {
            if (n[i] + n[j] == T)
            {
                return n[i] * n[j];
            }
        }
    }
    throw new SystemException();
}

static int Day1Task2(int[] n) {
    const int T = 2020;
    for (int i = 0; i < n.Length;i++) {
        for (int j = i + 1; j < n.Length;j++) {
            for (int k = j + 1; k < n.Length;k++) {
                if(n[i] + n[j] + n[k] == T) {
                    return n[i] * n[j] * n[k];
                }
            }
        }
    }
    throw new SystemException();
}