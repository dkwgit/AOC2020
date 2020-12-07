namespace AOC2020.Day07
{
    using System.Collections.Generic;
    using System.Linq;

    internal record BagManager
    {
        private readonly Dictionary<Bag, int> _allBags = new ();

        public bool CheckBagExists(string bagName)
        {
            return _allBags.Keys.Any(x => x.Name == bagName);
        }

        public Bag GetOrInsertBagByName(string bagName, out bool isNew)
        {
            Bag bag;
            var result = _allBags.Keys.Where(k => k.Name == bagName).Select(b => b);
#pragma warning disable CA1827 // Do not use Count() or LongCount() when Any() can be used
            if (result.Count() != 0)
#pragma warning restore CA1827 // Do not use Count() or LongCount() when Any() can be used
            {
                isNew = false;
                bag = result.Single();
            }
            else
            {
                isNew = true;
                bag = new Bag(bagName);
                AddBag(bag);
            }

            return bag;
        }

        public void AddBag(Bag bag)
        {
            _allBags.Add(bag, 1);
        }

        public Bag GetBagByName(string name)
        {
            return _allBags.Keys.Where(k => k.Name == name).Single();
        }

        public int GetParentCount(Bag bag)
        {
            return _allBags.Keys.Count(x => x.IsProgenitor(bag));
        }
    }
}
