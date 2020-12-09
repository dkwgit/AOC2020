namespace AOC2020.Computer
{
    using System.Linq;
    using System.Collections.Generic;

    public record Program
    {
        public readonly List<IInstruction> _program = new();

        public void AddInstruction(string line)
        {
            var lineParts = line.Split(' ');
            string instructionName = lineParts[0];
            int amount = int.Parse(lineParts[1]);

            IInstruction instruction = instructionName switch
            {
                "nop" => new NoOp(amount),
                "jmp" => new Jump(amount),
                "acc" => new Accumulate(amount),
            };
            _program.Add(instruction);
        }

        public IInstruction this[int index]
        {
            get
            {
                return _program[index];
            }

            set
            {
                _program[index] = value;
            }
        }

        public int Count()
        {
            return _program.Count;
        }

        public List<IInstruction> GetProgram()
        {
            return _program;
        }
    }
}
