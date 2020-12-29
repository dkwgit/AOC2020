namespace AOC2020.Day22
{
    using System.Collections.Generic;

    internal interface IRuleVariants
    {
        GameWinInfo CheckHistoryForWinner(List<int> deckOne, List<int> deckTwo);

        RoundWinInfo DecideRound(Game game);
    }
}
