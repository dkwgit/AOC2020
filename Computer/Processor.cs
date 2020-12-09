namespace AOC2020.Computer
{
    using System.Linq;
    using System.Collections.Generic;
    public class Processor
    { 
        private readonly Dictionary<int, int> _executionHistory = new ();

        private readonly Program _program;

        private int _accumulator = 0;

        private int _instructionPointer = 0;

        public Processor(Program program)
        {
            _program = program;
        }

        public void Reset()
        {
            _executionHistory.Clear();
            _accumulator = 0;
            _instructionPointer = 0;
        }

        public int Fix()
        {
            int accumulator = -1;

            List<int> indexesToTry = _program.GetProgram().Where(x => x.Name == "Jump" || x.Name == "NoOp").Select((x, index) => index).ToList();
            foreach(int index in indexesToTry)
            {
                Reset();
                IInstruction saveInstruction = _program[index];
                IInstruction newInstruction = saveInstruction is NoOp ? new Jump(saveInstruction.Amount) : new NoOp(saveInstruction.Amount);
                _program[index] = newInstruction;
                accumulator = Run(out bool looped);
                _program[index] = saveInstruction; //restore to original state
                if (!looped)
                {
                    break;
                }
            }

            return accumulator;
        }

        public int Run(out bool looped)
        {
            looped = false;
            while (true)
            {
                if (_instructionPointer == _program.Count())
                {
                    return _accumulator;
                }
                if (_executionHistory.ContainsKey(_instructionPointer))
                {
                    looped = true;
                    return _accumulator;
                    // _executionInfo[_instructionPointer] = _executionInfo[_instructionPointer] + 1;
                }
                else
                {
                    _executionHistory.Add(_instructionPointer, 1);
                }

                switch (_program[_instructionPointer])
                {
                    case Jump j:
                        _instructionPointer += j.Amount;
                        break;
                    case NoOp:
                        _instructionPointer++;
                        break;
                    case Accumulate a:
                        _accumulator += a.Amount;
                        _instructionPointer++;
                        break;
                    default:
                        throw new System.Exception($"Unrecognized instruction {_program[_instructionPointer]}");
                }
            }

            return -1;
        }
    }
}
