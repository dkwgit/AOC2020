namespace AOC2020.Day10
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Tree
    {
        private readonly Node _root;

        private readonly List<int> _masterRun = new ();

        public Tree(List<int> run)
        {
            _masterRun = run;
            _root = new Node(_masterRun[0]);
            InsertRun(_root, _masterRun.GetRange(1, _masterRun.Count - 1));
        }

        public int CountLeaves()
        {
            return _root.CountLeaves();
        }

        private void InsertRun(Node relativeRoot, IEnumerable<int> run)
        {
            int anchor = relativeRoot.Value;
            var candidates = run.Where(x => (x > anchor) && ((x - anchor) <= 3));
            foreach (var c in candidates)
            {
                Node child = relativeRoot.AddChildByValue(c);
                child.SetParent(relativeRoot);
                InsertRun(child, run.Where(x => x > c));
            }
        }
    }
}
