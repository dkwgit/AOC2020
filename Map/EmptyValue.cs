namespace AOC2020.Map
{
    public record EmptyValue : IEmptyValue
    {
        public string Name { get; init; }

        public EmptyValue()
        {
            Name = "Empty Square";
        }
    }
}
