namespace AOC2020.Day08Computer
{
    record Accumulate : IInstruction
    {
        public string Name => nameof(Accumulate);

        public int Amount { get; }

        public Accumulate(int amount) => Amount = amount;
    }
}
