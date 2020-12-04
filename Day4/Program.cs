using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Day4
{
    class Program
    {
        static bool VerifyPassword1(string passport)
        {
            return passport.Contains("byr")
                   && passport.Contains("iyr")
                   && passport.Contains("eyr")
                   && passport.Contains("hgt")
                   && passport.Contains("hcl")
                   && passport.Contains("ecl")
                   && passport.Contains("pid");
        }

        static bool VerifyPassword2(string passport)
        {
            if (!VerifyPassword1(passport))
            {
                return false;
            }
            foreach (var info in passport.Split(' ').Where(x=>!string.IsNullOrWhiteSpace(x)))
            {
                var temp = info.Split(':');
                var type = temp[0];
                var value = temp[1];
                if (type == "byr")
                {
                    if(!int.TryParse(value,out var val))
                    {
                        return false;
                    }
                    if (val < 1920 || val > 2002)
                    {
                        return false;
                    }
                }else if (type == "iyr")
                {
                    if(!int.TryParse(value,out var val))
                    {
                        return false;
                    }
                    if (val < 2010 || val > 2020)
                    {
                        return false;
                    }
                }else if (type == "eyr")
                {
                    if(!int.TryParse(value,out var val))
                    {
                        return false;
                    }
                    if (val < 2020 || val > 2030)
                    {
                        return false;
                    }
                }else if (type == "hgt")
                {
                    if(!int.TryParse(new string(value.SkipLast(2).ToArray()),out var val))
                    {
                        return false;
                    }
                    if (new string(value.TakeLast(2).ToArray()) == "cm")
                    {
                        if (val < 150 || val > 193)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (val < 59 || val > 76)
                        {
                            return false;
                        }
                    }
                }else if (type == "hcl")
                {
                    if (value[0] != '#' || value.Length != 7 ||
                        !value.Skip(1).All(x=>char.IsDigit(x) || char.IsLower(x)))
                    {
                        return false;
                    }
                }else if (type == "ecl")
                {
                    if (value != "amb" && value != "blu" && value != "brn" && value != "gry" && value != "grn" &&
                        value != "hzl" && value != "oth")
                    {
                        return false;
                    }
                }else if (type == "pid")
                {
                    if (value.Length != 9 || value.Any(x => !char.IsDigit(x)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        
        static void Main(string[] args)
        {
            var buffer = new StringBuilder();
            var valid1 = 0;
            var valid2 = 0;
            try
            {
                foreach (var line in File.ReadLines("input.txt"))
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        var str = buffer.ToString();
                        if (VerifyPassword1(str))
                        {
                            valid1++;
                        }

                        if (VerifyPassword2(str))
                        {
                            valid2++;
                        }
                        buffer.Clear();
                    }
                    else
                    {
                        buffer.Append(line.Trim() + " ");
                    }
                }
            }
            finally
            {
                if (buffer.Length > 0)
                {
                    var str = buffer.ToString();
                    
                    if (VerifyPassword1(str))
                    {
                        valid1++;
                    }

                    if (VerifyPassword2(str))
                    {
                        valid2++;
                    }
                }
            }
            Console.WriteLine(valid1);
            Console.WriteLine(valid2);
        }
    }
}
