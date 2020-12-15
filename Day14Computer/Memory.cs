namespace AOC2020.Day14Computer
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Memory : IMemory
    {
        private readonly SortedList<long, int> _locations = new ();

        private readonly LinkedList<MemoryWord> _wordList = new ();

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
            if (_locations.ContainsKey(location))
            {
                var node = _wordList.Find(new MemoryWord(location));
                return node.Value;
            }
            else
            {
                var word = CreateNewWord(location);

                if (_locations.Count == 0)
                {
                    _wordList.AddFirst(word);
                }
                else
                {
                    var result = _locations.Keys.Where(x => x > location).FirstOrDefault();
                    if (result == 0)
                    {
                        _wordList.AddLast(word);
                    }
                    else
                    {
                        var node = _wordList.Find(new MemoryWord(result));
                        _wordList.AddBefore(node, word);
                    }
                }

                _locations.Add(location, 1);

                return word;
            }
        }

        public List<MemoryWord> GetMemory()
        {
            List<MemoryWord> words = new ();

            foreach (var item in _wordList.AsEnumerable())
            {
                words.Add(item);
            }

            return words;
        }

        public void SetWordWidth(int wordWidth)
        {
            _wordWidth = wordWidth;
        }
    }
}
