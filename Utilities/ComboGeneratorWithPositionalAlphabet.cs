namespace AOC2020.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

    public record ComboGeneratorWithPositionalAlphabet<T>
    {
        public T[][] Alphabet { get; init; }

        public int LetterCount { get; init; }

        public ComboGeneratorWithPositionalAlphabet(T[][] alphabet, int letterCount) => (Alphabet, LetterCount) = (alphabet, letterCount);

        public IEnumerable<T[]> Iterator()
        {
            int comboCount = 1;
            for (int a = 0; a < Alphabet.Length; a++)
            {
                comboCount *= Alphabet[a].Length;
            }

            T[][] combos = new T[comboCount][];

            if (comboCount == 1)
            {
                combos[0] = new T[LetterCount];
                for (int i = 0; i < LetterCount; i++)
                {
                    combos[0][i] = Alphabet[i][0];
                }

                yield return combos[0];
                yield break;
            }

            for (int i = 0; i < comboCount; i++)
            {
                combos[i] = new T[LetterCount];
                /*if (typeof(T) == typeof(int))
                {
                    Array.Fill<int>((combos as int[][])[i], -1);
                }*/
            }

            int currentCombo = 0;
            GetCombo(ref currentCombo, Alphabet, LetterCount, 0, combos, LetterCount);
            Debug.Assert(currentCombo == comboCount, "Expecting the final currentCombo index to match");

            for (int j = 0; j < comboCount; j++)
            {
                yield return combos[j];
            }

            yield break;
        }

        private void GetCombo(ref int currentCombo, T[][] alphabet, int n, int alphabetSlot, T[][] combos, int totalLetterCount)
        {
            int startingCombo = currentCombo;
            for (int a = 0; a < alphabet[alphabetSlot].Length; a++)
            {
                if (n > 1)
                {
                    GetCombo(ref currentCombo, alphabet, n - 1, alphabetSlot + 1, combos, totalLetterCount);
                    for (; startingCombo < currentCombo; startingCombo++)
                    {
                        combos[startingCombo][totalLetterCount - n] = alphabet[alphabetSlot][a];

                        // PrintCombos(combos);
                    }
                }
                else
                {
                    combos[currentCombo++][totalLetterCount - n] = alphabet[alphabetSlot][a];

                    // PrintCombos(combos);
                }
            }
        }

        private void PrintCombos(T[][] combos)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < combos.GetLength(0); i++)
            {
                for (int j = 0; j < combos[i].Length; j++)
                {
                    sb.AppendJoin(',', combos[i][j]);
                }

                Console.WriteLine(sb);
                sb.Clear();
            }
        }
    }
}
