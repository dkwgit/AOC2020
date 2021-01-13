namespace AOC2020.Day22
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;

    internal class PartTwoRuleVariants : IRuleVariants
    {
        private readonly Dictionary<BigInteger, bool> _history = new ();

        public GameWinInfo CheckHistoryForWinner(Hand handOne, Hand handTwo)
        {
            BigInteger composite = (handOne.Cards << (6 * handOne.CardCount)) + handTwo.Cards;

            if (_history.ContainsKey(composite))
            {
                return GameWinInfo.PlayerOneWinsGame;
            }
            else
            {
                _history.Add(composite, true);
            }

            return GameWinInfo.NoWinYet;
        }

        public RoundWinInfo DecideRound(Game game)
        {
            if (game.PlayedOne > game.HandOne.CardCount || game.PlayedTwo > game.HandTwo.CardCount)
            {
                if (game.PlayedOne > game.PlayedTwo)
                {
                    return RoundWinInfo.PlayerOneWinsRound;
                }
                else
                {
                    return RoundWinInfo.PlayerTwoWinsRound;
                }
            }

            int totalCards = game.PlayedOne + game.PlayedTwo;

            Hand newHandOne = Hand.DealHand(Hand.GetCards(game.HandOne.Cards, game.PlayedOne));
            Hand newHandTwo = Hand.DealHand(Hand.GetCards(game.HandTwo.Cards, game.PlayedTwo));

            Game newGame = new Game(newHandOne, newHandTwo, new PartTwoRuleVariants());
            newGame.Play();

            if (newGame.WinState == GameWinInfo.PlayerOneWinsGame)
            {
                return RoundWinInfo.PlayerOneWinsRound;
            }
            else
            {
                return RoundWinInfo.PlayerTwoWinsRound;
            }

            throw new InvalidOperationException("Unexpected state after recursive game");
        }
    }
}
