namespace AOC2020.Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    public record ComboGeneratorWithPositionalAlphabet<T>
    {
        public bool Dedupe { get; set; }

        public T[][] Alphabet { get; init; }

        public int LetterCount { get; init; }

        public ComboGeneratorWithPositionalAlphabet(T[][] alphabet, int letterCount, bool dedupe) => (Alphabet, LetterCount, Dedupe) = (alphabet, letterCount, dedupe);

        public IEnumerable<List<T>> Iterator()
        {
            List<List<T>> lists = GetCombo(Alphabet, LetterCount, 0);
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

        private static List<List<T>> DedupeLists(List<List<T>> lists)
        {
            List<List<T>> copy = new (lists);

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

        private static List<List<T>> GetCombo(T[][] alphabet, int n, int alphabetSlot)
        {
            List<List<T>> lists = new ();

            for (int a = 0; a < alphabet[alphabetSlot].Length; a++)
            {
                if (n > 1)
                {
                    var sublists = GetCombo(alphabet, n - 1, alphabetSlot + 1);
                    foreach (var sub in sublists)
                    {
                        // head
                        List<T> list = new () { alphabet[alphabetSlot][a] };

                        // add tail
                        list.AddRange(sub);
                        lists.Add(list);
                    }
                }
                else
                {
                    // head
                    List<T> list = new () { alphabet[alphabetSlot][a] };
                    lists.Add(list);
                }
            }

            return lists;
        }
    }
}
