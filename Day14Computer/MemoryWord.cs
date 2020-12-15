namespace AOC2020.Day14Computer
{
    using System;
    using System.Collections;

    public record MemoryWord : IEquatable<MemoryWord>
    {
        private BitArray _word;

        public BitArray Word => _word;

        public long Location { get; init; }

        public MemoryWord(long location) => (_word, Location) = (null, location);

        public MemoryWord(BitArray word, long location) => (_word, Location) = (word, location);

        bool IEquatable<MemoryWord>.Equals(MemoryWord other)
        {
            return Location == other.Location;
        }

        public void SetValue(BitArray value)
        {
            _word = value;
        }
    }
}
