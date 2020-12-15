namespace AOC2020.Day14Computer
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class ValueMaskingStrategy : IMaskingStrategy
    {
        private int _wordWidth = -1;

        private string _maskString;

        private BitArray _andMask;

        private BitArray _orMask;

        public string MaskString
        {
            get
            {
                return _maskString;
            }

            set
            {
                _maskString = value;
                List<(char Letter, int Index)> valuesAndIndices = _maskString.Select((x, index) => (x, index)).ToList();

                var zeroIndices = valuesAndIndices.Where(x => x.Letter == '0').Select(x => x.Index);
                var oneIndices = valuesAndIndices.Where(x => x.Letter == '1').Select(x => x.Index);

                _andMask = new BitArray(_wordWidth, true);
                foreach (var index in zeroIndices)
                {
                    _andMask[index] = false;
                }

                _orMask = new BitArray(_wordWidth, false);
                foreach (var index in oneIndices)
                {
                    _orMask[index] = true;
                }
            }
        }

        public void SetWordWidth(int wordWidth)
        {
            _wordWidth = wordWidth;
        }

        public BitArray GetDataToWrite(BitArray data)
        {
            data.And(_andMask);
            data.Or(_orMask);

            return data;
        }

        public List<long> GetLocations(long location)
        {
            return new List<long>() { location, };
        }
    }
}
