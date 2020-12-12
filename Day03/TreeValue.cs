namespace AOC2020.Day03
{
    internal record TreeValue : ITreeValue
    {
        public string Name { get; init; }

        public TreeValue()
        {
            Name = "Tree";
        }
    }
}
