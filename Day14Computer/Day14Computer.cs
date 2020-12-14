namespace AOC2020.Day14Computer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Day14Computer
    {
        private readonly int _instructionWidth = -1;

        private BitArray _memory = new BitArray(0, false);

        private int _memorySize = 0;

        private string _maskString = string.Empty;

        private BitArray _andMask;

        private BitArray _orMask;

        public Day14Computer(int instructionWidth) => _instructionWidth = instructionWidth;

        public string MaskString
        {
            get
            {
                return _maskString;
            }

            set
            {
                _maskString = string.Join(string.Empty, value.Reverse().ToArray());

                List<(char Letter, int Index)> valuesAndIndices = _maskString.Select((x, index) => (x, index)).ToList();
                var zeroIndices = valuesAndIndices.Where(x => x.Letter == '0').Select(x => x.Index);
                var oneIndices = valuesAndIndices.Where(x => x.Letter == '1').Select(x => x.Index);

                _andMask = new BitArray(_instructionWidth, true);
                foreach (var index in zeroIndices)
                {
                    _andMask[index] = false;
                }

                _orMask = new BitArray(_instructionWidth, false);
                foreach (var index in oneIndices)
                {
                    _orMask[index] = true;
                }
            }
        }

        public void WriteValue(int location, long value)
        {
            BitArray valueAsBits = TranslateValueToBits(value);

            if (location + 1 > _memorySize)
            {
                BitArray newMemory = new BitArray((location + 1) * _instructionWidth, false);
                for (int i = 0; i < _memory.Length; i++)
                {
                    newMemory[i] = _memory[i];
                }

                _memory = newMemory;
                _memorySize = location + 1;
            }

            valueAsBits.And(_andMask);
            valueAsBits.Or(_orMask);

            for (int i = 0; i < _instructionWidth; i++)
            {
                _memory[(location * _instructionWidth) + i] = valueAsBits[i];
            }
        }

        public List<long> GetValuesInMemory()
        {
            List<long> values = new ();

            for (int i = 0; i < _memorySize; i++)
            {
                values.Add(GetValueAtLocation(i));
            }

            return values;
        }

        private BitArray TranslateValueToBits(long value)
        {
            BitArray bitValue = new BitArray(_instructionWidth, false);

            long max = 1L;
            max <<= _instructionWidth - 1;

            if (value > max)
            {
                throw new InvalidProgramException("Unexpected {value}, which is larger than max permissible {max}");
            }

            for (int i = 0; i < _instructionWidth; i++)
            {
                long checkForLargerThanValue = 1L << i;

                if (checkForLargerThanValue > (2L * value))
                {
                    // Don't need to keep setting bits
                    break;
                }

                long mask = 1L;
                long valueShifted = value >> i;
                if ((mask & valueShifted) == mask)
                {
                    bitValue[i] = true;
                }
            }

            return bitValue;
        }

        private long GetValueAtLocation(int location)
        {
            long value = 0L;

            for (int i = 0; i < _instructionWidth; i++)
            {
                long bit = _memory[(location * _instructionWidth) + i] ? 1L : 0L;
                bit <<= i;
                value |= bit;
            }

            return value;
        }
    }
}
