namespace AOC2020.Utilities
{
    using System.Collections.Generic;

    public record ComboGenerator<T>
    {
        public List<T> Alphabet { get; init; }

        public int LetterCount { get; init; }

        public ComboGenerator(List<T> alphabet, int letterCount) => (Alphabet, LetterCount) = (alphabet, letterCount);

        public IEnumerable<List<T>> Iterator()
        {
            List<List<T>> lists = GetCombo(Alphabet, LetterCount);

            for (int i = 0; i < lists.Count; i++)
            {
                yield return lists[i];
            }

            yield break;
        }

        private static List<List<T>> GetCombo(List<T> alphabet, int n)
        {
            List<List<T>> lists = new ();

            for (int a = 0; a < alphabet.Count; a++)
            {
                if (n > 1)
                {
                    var sublists = GetCombo(alphabet, n - 1);
                    foreach (var sub in sublists)
                    {
                        // head
                        List<T> list = new () { alphabet[a] };

                        // add tail
                        list.AddRange(sub);
                        lists.Add(list);
                    }
                }
                else
                {
                    // head
                    List<T> list = new () { alphabet[a] };
                    lists.Add(list);
                }
            }

            return lists;
        }
    }
}
