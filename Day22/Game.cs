namespace AOC2020.Day22
{
    using System;

    internal class Game
    {
        public Game(Hand deckOne, Hand deckTwo, IRuleVariants ruleVariants)
        {
            DeckOne = deckOne;
            DeckTwo = deckTwo;
            RuleVariants = ruleVariants;
        }

        public IRuleVariants RuleVariants { get; init; }

        public Hand DeckOne { get; init; }

        public Hand DeckTwo { get; init; }

        public int PlayedOne { get; set; } = -1;

        public int PlayedTwo { get; set; } = -1;

        public GameWinInfo WinState { get; set; } = GameWinInfo.NoWinYet;

        public long Score
        {
            get
            {
                Hand winningDeck;
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

                return winningDeck.Score();
            }
        }

        public void Play()
        {
            while (WinState == GameWinInfo.NoWinYet)
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
            PlayedOne = DeckOne.PlayCard();
            PlayedTwo = DeckTwo.PlayCard();
            if (RuleVariants.DecideRound(this) == RoundWinInfo.PlayerOneWinsRound)
            {
                DeckOne.AddAtBack(PlayedOne);
                DeckOne.AddAtBack(PlayedTwo);
            }
            else
            {
                DeckTwo.AddAtBack(PlayedTwo);
                DeckTwo.AddAtBack(PlayedOne);
            }

            if (DeckOne.CardCount == 0)
            {
                WinState = GameWinInfo.PlayerTwoWinsGame;
            }
            else if (DeckTwo.CardCount == 0)
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
