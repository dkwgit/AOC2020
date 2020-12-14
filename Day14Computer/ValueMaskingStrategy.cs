namespace AOC2020.Day14Computer
{
    using System.Collections;
    using System.Collections.Generic;

    public class ValueMaskingStrategy : IMaskingStrategy
    {
        private int _instructionWidth = -1;

        public void SetInstructionWidth(int width)
        {
            _instructionWidth = width;
        }

        public BitArray GetDataToWrite(BitArray data)
        {
            return data;
        }

        public List<int> GetLocations(int location)
        {
            return new List<int>() { location, };
        }
    }
}
