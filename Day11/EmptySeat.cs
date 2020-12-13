namespace AOC2020.Day11
{
    internal record EmptySeat : IEmptySeat
    {
        public string Name { get; init; }

        public EmptySeat()
        {
            Name = "Empty Seat";
        }
    }
}
