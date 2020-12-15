namespace AOC2020.Day14Computer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Day14Computer<TMaskingStrategy>
        where TMaskingStrategy : IMaskingStrategy, new()
    {
        private readonly int _wordWidth = -1;

        private readonly IMemory _memory;

        public Day14Computer(int wordWidth)
        {
            _wordWidth = wordWidth;

            _memory = new Memory(wordWidth);

            MaskingStrategy = new TMaskingStrategy();
            MaskingStrategy.SetWordWidth(_wordWidth);
        }

        public IMaskingStrategy MaskingStrategy { get; init; }

        public string MaskString
        {
            get
            {
                return MaskingStrategy.MaskString;
            }

            set
            {
                string reversed = string.Join(string.Empty, value.Reverse().ToArray());
                MaskingStrategy.MaskString = reversed;
            }
        }

        public static BitArray TranslateValueToBits(long value, int wordWidth)
        {
            BitArray bitValue = new BitArray(wordWidth, false);

            long max = 1L;
            max <<= wordWidth - 1;

            if (value > max)
            {
                throw new InvalidProgramException("Unexpected {value}, which is larger than max permissible {max}");
            }

            for (int i = 0; i < wordWidth; i++)
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

        public static long GetValue(BitArray bits, int wordWidth)
        {
            long value = 0L;

            for (int i = 0; i < wordWidth; i++)
            {
                long bit = bits[i] ? 1L : 0L;
                bit <<= i;
                value |= bit;
            }

            return value;
        }

        public void WriteValue(long location, long value)
        {
            BitArray valueAsBits = MaskingStrategy.GetDataToWrite(TranslateValueToBits(value, _wordWidth));

            List<long> locations = MaskingStrategy.GetLocations(location);

            foreach (var loc in locations)
            {
                var word = _memory.GetWord(loc);

                word.SetValue(valueAsBits);
            }
        }

        public List<long> GetAllMemoryValues()
        {
            List<long> values = new ();
            foreach (var item in _memory.GetMemory())
            {
                values.Add(GetValue(item.Word, _wordWidth));
            }

            return values;
        }
    }
}
