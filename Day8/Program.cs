using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
    record Instruction
    {
        public Instruction(Command command, int value)
        {
            this.CommandType = command;
            this.Value = value;
        }
        
        public Command CommandType { get; set; }
        public int Value { get; set; }
        public enum Command
        {
            Nop,
            Jmp,
            Acc
        }

        public override string ToString()
        {
            return $"{CommandType} {(Value >= 0 ? ("+" + Value) : Value)}";
        }

        public static Instruction Parse(string instruction)
        {
            var temp = instruction.Split(' ');
            var command = temp[0] switch
            {
                "nop" => Command.Nop,
                "acc" => Command.Acc,
                "jmp" => Command.Jmp,
                _ => throw new ArgumentOutOfRangeException(nameof(instruction))
            };
            var number = int.Parse(temp[1]);
            return new Instruction(command, number);
        }
    }

    class Vm
    {
        private Instruction[] _instructions;
        private Dictionary<int, int> _executionsCounter = new Dictionary<int, int>();
        private int _accumulator = 0;
        private int _nextFunctionPointer = 0;

        public Vm(IEnumerable<Instruction> instructions)
        {
            _instructions = instructions.ToArray();
        }

        private void Next()
        {
            var currentInstruction = _instructions[_nextFunctionPointer];
            if (!_executionsCounter.ContainsKey(_nextFunctionPointer))
            {
                _executionsCounter.Add(_nextFunctionPointer, 1);
            }
            else
            {
                _executionsCounter[_nextFunctionPointer]++;
            }
            
            switch (currentInstruction.CommandType)
            {
                case Instruction.Command.Nop:
                    _nextFunctionPointer++;
                    break;
                case Instruction.Command.Jmp:
                    _nextFunctionPointer += currentInstruction.Value;
                    break;
                case Instruction.Command.Acc:
                    _accumulator += currentInstruction.Value;
                    _nextFunctionPointer++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public int ReturnAccumulator()
        {
            var lastAcc = 0;
            while (true)
            {
                lastAcc = _accumulator;
                Next();
                if (_executionsCounter.Any(x => x.Value >= 2))
                {
                    return lastAcc;
                }

                if (_nextFunctionPointer >= _instructions.Length)
                {
                    return _accumulator;
                }
            }
        }

        public void CleanUp()
        {
            _accumulator = 0;
            _nextFunctionPointer = 0;
            _executionsCounter.Clear();
        }

        public bool IsInfinite()
        {
            while (true)
            {
                Next();
                if (_executionsCounter.Any(x=>x.Value >= 2))
                {
                    return true;
                }
                if (_nextFunctionPointer == _instructions.Length)
                {
                    return false;
                }
            }
        }

        public int TryToFixVmAndReturnCorrectAccumulator()
        {
            if (IsInfinite())
            {
                for (var i = 0; i < _instructions.Length; i++)
                {
                    var arr = new Instruction[_instructions.Length];
                    _instructions.CopyTo(arr,0);
                    arr[i] = _instructions[i].CommandType switch
                    {
                        Instruction.Command.Nop => new Instruction(Instruction.Command.Jmp, _instructions[i].Value),
                        Instruction.Command.Jmp => new Instruction(Instruction.Command.Nop, _instructions[i].Value),
                        Instruction.Command.Acc => null,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    if (arr[i] == null)
                    {
                        continue;
                    }
                    var vm = new Vm(arr);
                    if (!vm.IsInfinite())
                    {
                        vm.CleanUp();
                        return vm.ReturnAccumulator();
                    }
                }

                throw new SystemException();
            }
            else
            {
                return ReturnAccumulator();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var vm = new Vm(File.ReadLines("input.txt").Select(Instruction.Parse));
            Console.WriteLine(vm.ReturnAccumulator());
            vm.CleanUp();
            Console.WriteLine(vm.TryToFixVmAndReturnCorrectAccumulator());
        }
    }
}
