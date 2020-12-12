namespace AOC2020.Day10
{
    using System.Collections.Generic;

    internal class Node
    {
        private Node _parent = null;

        public Node(int value) => Value = value;

        public int Value { get; init; }

        public Node Parent => _parent;

        public List<Node> Children { get; } = new ();

        public Node AddChildByValue(int value)
        {
            Node child = new Node(value);
            Children.Add(child);
            return child;
        }

        public void SetParent(Node parent)
        {
            _parent = parent;
        }

        public int CountLeaves()
        {
            int count = 0;
            foreach (var node in Children)
            {
                if (node.IsLeaf())
                {
                    count += 1;
                }
                else
                {
                    count += node.CountLeaves();
                }
            }

            return count;
        }

        public bool IsRoot()
        {
            return _parent == null;
        }

        public bool IsLeaf()
        {
            return Children.Count == 0;
        }
    }
}
