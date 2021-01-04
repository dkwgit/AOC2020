namespace AOC2020.Day22
{
    using System.Collections.Generic;

    internal class PartOneRuleVariants : IRuleVariants
    {
        public GameWinInfo CheckHistoryForWinner(Hand deckOne, Hand deckTwo)
        {
            return GameWinInfo.NoWinYet;
        }

        public RoundWinInfo DecideRound(Game game)
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
    }
}
