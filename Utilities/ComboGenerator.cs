namespace AOC2020.Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    public record ComboGenerator<T>
    {
        public bool Dedupe { get; set; }

        public T[] Alphabet { get; init; }

        public int LetterCount { get; init; }

        public ComboGenerator(T[] alphabet, int letterCount, bool dedupe) => (Alphabet, LetterCount, Dedupe) = (alphabet, letterCount, dedupe);

        public IEnumerable<T[]> Iterator()
        {
            List<T[]> lists = GetCombo(Alphabet, LetterCount);
            if (Dedupe)
            {
                lists = DedupeLists(lists);
            }

            for (int j = 0; j < lists.Count; j++)
            {
                yield return lists[j];
            }

            yield break;
        }

        private static List<T[]> DedupeLists(List<T[]> lists)
        {
            List<T[]> copy = new (lists);

            for (int i = 0; i < copy.Count; i++)
            {
                var item = copy[i];
                if (item != null)
                {
                    if (i + 1 >= copy.Count)
                    {
                        continue;
                    }

                    int find = find = copy.FindIndex(i + 1, x => x is not null && x.SequenceEqual(item));
                    while (find != -1)
                    {
                        copy[find] = null;
                        if (find + 1 >= copy.Count)
                        {
                            break;
                        }

                        find = copy.FindIndex(find + 1, x => x is not null && x.SequenceEqual(item));
                    }
                }
            }

            copy = copy.Where(x => x is not null).ToList();
            return copy;
        }

        private static List<T[]> GetCombo(T[] alphabet, int n)
        {
            List<T[]> lists = new ();

            for (int a = 0; a < alphabet.Length; a++)
            {
                if (n > 1)
                {
                    var sublists = GetCombo(alphabet, n - 1);
                    foreach (var sub in sublists)
                    {
                        T[] arr = new T[n];
                        arr[0] = alphabet[a];
                        for (int s = 1; s < n; s++)
                        {
                            arr[s] = sub[s - 1];
                        }

                        lists.Add(arr);
                    }
                }
                else
                {
                    T[] arr = new T[n];
                    arr[0] = alphabet[a];
                    lists.Add(arr);
                }
            }

            return lists;
        }
    }
}
