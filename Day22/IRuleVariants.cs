namespace AOC2020.Day22
{
    using System.Collections.Generic;

    internal interface IRuleVariants
    {
        GameWinInfo CheckHistoryForWinner(Hand deckOne, Hand deckTwo);

        RoundWinInfo DecideRound(Game game);
    }
}
