namespace AOC2020.Day14Computer
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IMaskingStrategy
    {
        string MaskString { get; set; }

        void SetWordWidth(int wordWidth);

        List<long> GetLocations(long location);

        BitArray GetDataToWrite(BitArray data);
    }
}
