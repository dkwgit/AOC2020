namespace AOC2020.Day07
{
    using System.Collections.Generic;
    using System.Diagnostics;

    internal record Bag
    {
        private readonly Dictionary<Bag, int> _childBags = new ();
        private readonly List<Bag> _parentBags = new ();

        public string Name { get; init; }

        public Bag(string name) => Name = name;

        public void AddChild(Bag bag, int count)
        {
            Debug.Assert(!_childBags.ContainsKey(bag), "not expecting this child bag to exist");
            _childBags.Add(bag, count);
        }

        public void AddParent(Bag bag)
        {
            Debug.Assert(!_parentBags.Contains(bag), "not expecting this parent bag to exist");
            _parentBags.Add(bag);
        }

        public bool IsProgenitor(Bag bag)
        {
            if (_childBags.ContainsKey(bag))
            {
                return true;
            }

            bool retVal = false;
            foreach (var child in _childBags)
            {
                if (child.Key.IsProgenitor(bag))
                {
                    retVal = true;
                    break;
                }
            }

            return retVal;
        }

        public int CountContainedBags(int factor)
        {
            int count = 0;
            foreach (var child in _childBags)
            {
               count += child.Value * factor;
               count += child.Key.CountContainedBags(child.Value * factor);
            }

            return count;
        }
    }
}
