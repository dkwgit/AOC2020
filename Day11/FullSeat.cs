namespace AOC2020.Day11
{
    internal record FullSeat : IFullSeat
    {
        public string Name { get; init; }

        public FullSeat()
        {
            Name = "Full Seat";
        }
    }
}
