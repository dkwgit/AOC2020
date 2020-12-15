namespace AOC2020.Day14Computer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class LocationMaskingStrategy : IMaskingStrategy
    {
        private int _wordWidth = -1;

        public string MaskString { get; set; }

        public static IEnumerable<string> GetCombinations(int length)
        {
            for (long i = 0; i < (1 << length); i++)
            {
                char[] arr = new char[length];
                long mask = 1;
                for (int j = 0; j < length; j++)
                {
                    arr[j] = (i & mask << j) >> j == 1 ? '1' : '0';
                }

                yield return string.Join(string.Empty, arr);
            }

            yield break;
        }

        public BitArray GetDataToWrite(BitArray data)
        {
            return data;
        }

        public List<long> GetLocations(long location)
        {
            BitArray addressBits = Day14Computer<LocationMaskingStrategy>.TranslateValueToBits(location, _wordWidth);

            List<(char Letter, int Index)> valuesAndIndices = MaskString.Select((x, index) => (x, index)).ToList();

            var oneIndices = valuesAndIndices.Where(x => x.Letter == '1').Select(x => x.Index);
            var xIndices = valuesAndIndices.Where(x => x.Letter == 'X').Select(x => x.Index).ToList();

            foreach (var oneIndex in oneIndices)
            {
                addressBits[oneIndex] = true;
            }

            List<long> locations = new ();

            foreach (var combo in GetCombinations(xIndices.Count))
            {
                BitArray address = addressBits.Clone() as BitArray;
                int i = 0;
                foreach (var index in xIndices)
                {
                    address[index] = combo[i] == '1';
                    i++;
                }

                locations.Add(Day14Computer<LocationMaskingStrategy>.GetValue(address, _wordWidth));
            }

            return locations;
        }

        public void SetWordWidth(int wordWidth)
        {
            _wordWidth = wordWidth;
        }
    }
}
