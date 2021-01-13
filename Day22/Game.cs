namespace AOC2020.Day22
{
    using System;

    internal class Game
    {
        public Game(Hand handOne, Hand handTwo, IRuleVariants ruleVariants)
        {
            HandOne = handOne;
            HandTwo = handTwo;
            RuleVariants = ruleVariants;
        }

        public IRuleVariants RuleVariants { get; init; }

        public Hand HandOne { get; init; }

        public Hand HandTwo { get; init; }

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
                    winningDeck = HandOne;
                }
                else if (WinState == GameWinInfo.PlayerTwoWinsGame)
                {
                    winningDeck = HandTwo;
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
                WinState = RuleVariants.CheckHistoryForWinner(HandOne, HandTwo);
                if (WinState != GameWinInfo.NoWinYet)
                {
                    return;
                }

                PlayRound();
            }
        }

        public void PlayRound()
        {
            PlayedOne = HandOne.PlayCard();
            PlayedTwo = HandTwo.PlayCard();
            if (RuleVariants.DecideRound(this) == RoundWinInfo.PlayerOneWinsRound)
            {
                HandOne.AddAtBack(PlayedOne);
                HandOne.AddAtBack(PlayedTwo);
            }
            else
            {
                HandTwo.AddAtBack(PlayedTwo);
                HandTwo.AddAtBack(PlayedOne);
            }

            if (HandOne.CardCount == 0)
            {
                WinState = GameWinInfo.PlayerTwoWinsGame;
            }
            else if (HandTwo.CardCount == 0)
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
