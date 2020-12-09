namespace AOC2020.Computer
{
    record Jump : IInstruction
    {
        public string Name => nameof(Jump);

        public int Amount { get; }

        public Jump(int amount) => Amount = amount;
    }
}
