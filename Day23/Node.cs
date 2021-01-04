namespace AOC2020.Day23
{
    internal class Node
    {
        public Node(int label) => Label = label;

        public Node Next { get; set; }

        public int Label { get; init; }
    }
}
