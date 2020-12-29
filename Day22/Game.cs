namespace AOC2020.Day22
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Game
    {
        public Game(List<int> deckOne, List<int> deckTwo, IRuleVariants ruleVariants)
        {
            DeckOne.AddRange(deckOne);
            DeckTwo.AddRange(deckTwo);
            RuleVariants = ruleVariants;
        }

        public IRuleVariants RuleVariants { get; init; }

        public List<int> DeckOne { get; init; } = new ();

        public List<int> DeckTwo { get; init; } = new ();

        public int PlayedOne { get; set; } = -1;

        public int PlayedTwo { get; set; } = -1;

        public GameWinInfo WinState { get; set; } = GameWinInfo.NoWinYet;

        public long Score
        {
            get
            {
                List<int> winningDeck;
                if (WinState == GameWinInfo.PlayerOneWinsGame)
                {
                    winningDeck = DeckOne;
                }
                else if (WinState == GameWinInfo.PlayerTwoWinsGame)
                {
                    winningDeck = DeckTwo;
                }
                else
                {
                    throw new InvalidOperationException("Unexpected GameWinInfo value");
                }

                long score = 0;
                for (int i = 1; i <= winningDeck.Count; i++)
                {
                    score += winningDeck[^i] * i;
                }

                return score;
            }
        }

        public void Play()
        {
            while (DeckOne.Count >= 1 && DeckTwo.Count >= 1)
            {
                WinState = RuleVariants.CheckHistoryForWinner(DeckOne, DeckTwo);
                if (WinState != GameWinInfo.NoWinYet)
                {
                    return;
                }

                PlayRound();
            }
        }

        public void PlayRound()
        {
            PlayedOne = DeckOne[0];
            DeckOne.RemoveAt(0);
            PlayedTwo = DeckTwo[0];
            DeckTwo.RemoveAt(0);

            if (RuleVariants.DecideRound(this) == RoundWinInfo.PlayerOneWinsRound)
            {
                DeckOne.Add(PlayedOne);
                DeckOne.Add(PlayedTwo);
            }
            else
            {
                DeckTwo.Add(PlayedTwo);
                DeckTwo.Add(PlayedOne);
            }

            if (DeckOne.Count == 0)
            {
                WinState = GameWinInfo.PlayerTwoWinsGame;
            }
            else if (DeckTwo.Count == 0)
            {
                WinState = GameWinInfo.PlayerOneWinsGame;
            }
            else
            {
                WinState = GameWinInfo.NoWinYet;
            }
        }
    }
}
