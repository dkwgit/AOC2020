namespace AOC2020.Computer
{
    record NoOp : IInstruction
    {
        public string Name => nameof(NoOp);

        public int Amount { get; }

        public NoOp(int amount) => Amount = amount;
    }
}
