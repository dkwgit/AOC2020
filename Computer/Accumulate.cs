namespace AOC2020.Computer
{
    record Accumulate : IInstruction
    {
        public string Name => nameof(Accumulate);

        public int Amount { get; }

        public Accumulate(int amount) => Amount = amount;
    }
}
