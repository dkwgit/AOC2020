namespace AOC2020.Day14Computer
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IMaskingStrategy
    {
        List<int> GetLocations(int location);

        BitArray GetDataToWrite(BitArray data);
    }
}
