namespace AOC2020.Map
{
    record EmptySquareValue : IEmptyValue
    {
        public string Name { get; init; }

        public EmptySquareValue()
        {
            Name = "Empty Square";
        }
    }
}
