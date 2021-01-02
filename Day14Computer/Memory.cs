namespace AOC2020.Day14Computer
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Memory : IMemory
    {
        private readonly SortedDictionary<long, MemoryWord> _locations = new ();

        private int _wordWidth = -1;

        public Memory(int wordWidth)
        {
            SetWordWidth(wordWidth);
        }

        public MemoryWord CreateNewWord(long location)
        {
            return new MemoryWord(new BitArray(_wordWidth, false), location);
        }

        public MemoryWord GetWord(long location)
        {
            if (!_locations.ContainsKey(location))
            {
                _locations.Add(location, CreateNewWord(location));
            }

            return _locations[location];
        }

        public List<MemoryWord> GetMemory()
        {
            List<MemoryWord> words = new ();

            foreach (var key in _locations.Keys)
            {
                words.Add(_locations[key]);
            }

            return words;
        }

        public void SetWordWidth(int wordWidth)
        {
            _wordWidth = wordWidth;
        }
    }
}
