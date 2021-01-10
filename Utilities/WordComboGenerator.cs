namespace AOC2020.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    // A 'word' is a fixed length of slots (of type T) combinatorially populated with letters from an 'alphabet' (of type T), that can vary by slot,
    // and where an alphabet might as well be some integers (i.e., words do not have to be sequences of char)
    //
    // This class produces all words representing all combinations of the alphabet (without repetition).
    //
    // Example: if the alphabet is 'A', 'B' for all slots in the word and the word length is 3,
    // it should produce
    // A A A
    // A A B
    // A B A
    // A B B
    // B A A
    // B A B
    // B B A
    // B B B
    public record WordComboGenerator<T>
    {
        public T[][] Alphabet { get; init; }

        public int WordLength { get; init; }

        public int WordCount { get; set; }

        public int[] Factors { get; set; }

        public WordComboGenerator(T[][] alphabet, int wordLength)
        {
            Alphabet = alphabet;
            WordLength = wordLength;
            Factors = new int[WordLength];
        }

        public static T[][] GetFixedAlphabet(T[] lettersInAlphabet, int wordLength)
        {
            T[][] alphabet = new T[wordLength][];
            for (int i = 0; i < wordLength; i++)
            {
                alphabet[i] = new T[lettersInAlphabet.Length];
                Array.Copy(lettersInAlphabet, alphabet[i], lettersInAlphabet.Length);
            }

            return alphabet;
        }

        public IEnumerable<T[]> GetWords()
        {
            WordCount = 1;
            for (int i = 0; i < WordLength; i++)
            {
                WordCount *= Alphabet[i].Length;
                Factors[i] = Alphabet[i].Length;
            }

            T[][] words = new T[WordCount][];

            if (WordCount == 1)
            {
                // A special (deformed case) in which some loop is asking for combos, but there is only 1
                // caller should not do that, but calling code may not recognize
                words[0] = new T[WordLength];
                for (int i = 0; i < WordLength; i++)
                {
                    words[0][i] = Alphabet[i][0];
                }

                yield return words[0];
                yield break;
            }

            for (int i = 0; i < WordCount; i++)
            {
                words[i] = new T[WordLength];
                if (typeof(T) == typeof(int))
                {
                    Array.Fill<int>((words as int[][])[i], -1);
                }
            }

            GetCombos(words);

            // PrintCombos(words);
            for (int j = 0; j < WordCount; j++)
            {
                yield return words[j];
            }

            yield break;
        }

        private void GetCombos(T[][] words)
        {
            for (int letter = 0; letter < WordLength; letter++)
            {
                int currentWord = 0;

                int timesToCycleOverAlphabet = Factors.Where((x, i) => i < letter).Aggregate(1, (x, y) => x * y);
                for (int cycleOverAlphabet = 0; cycleOverAlphabet < timesToCycleOverAlphabet; cycleOverAlphabet++)
                {
                    for (int a = 0; a < Alphabet[letter].Length; a++)
                    {
                        int timesToRepeatAlphabetLeter = WordCount / Factors.Where((x, i) => i <= letter).Aggregate((x, y) => x * y);
                        for (int repeat = 0; repeat < timesToRepeatAlphabetLeter; repeat++)
                        {
                            words[currentWord++][letter] = Alphabet[letter][a];

                            // PrintCombos(words);
                        }
                    }
                }
            }
        }

        private static void PrintCombos(T[][] words)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < words.GetLength(0); i++)
            {
                sb.Append($"{i}: ");
                for (int j = 0; j < words[i].Length; j++)
                {
                    sb.AppendJoin(',', words[i][j]);
                }

                Console.WriteLine(sb);
                sb.Clear();
            }
        }
    }
}
