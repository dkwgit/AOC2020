namespace AOC2020.Day20
{
    using System.Collections.Generic;
    using System.Drawing;

    internal interface IPicture
    {
        IPicture SetRegistry(Registry registry);

        IPicture SetLengths(int tileSideLengthWithBorder, int tileSideLengthWithoutBorder, int tilesPerSide);

        IPicture Assemble();

        IPicture StripBorders();

        IPicture FlipOnXAxis();

        IPicture RotateRight();

        void SetPatternMatchForPoint(Point p);

        int FindPatterns(Pattern pattern, int numberToFind = -1);

        int GetOccupiedPointCount();

        int GetSerpentPointCount();
    }
}
