namespace AOC2020.Day22
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class PartTwoRuleVariants : IRuleVariants
    {
        private readonly Dictionary<string, bool> _history = new ();

        public GameWinInfo CheckHistoryForWinner(List<int> deckOne, List<int> deckTwo)
        {
            StringBuilder one = new (deckOne.Count + 1 + deckTwo.Count);

            foreach (var itemOne in deckOne)
            {
                one.Append(itemOne);
            }

            one.Append('-');

            foreach (var itemTwo in deckTwo)
            {
                one.Append(itemTwo);
            }

            string key = one.ToString();

            if (_history.ContainsKey(key))
            {
                return GameWinInfo.PlayerOneWinsGame;
            }
            else
            {
                _history.Add(key, true);
            }

            return GameWinInfo.NoWinYet;
        }

        public RoundWinInfo DecideRound(Game game)
        {
            if (game.PlayedOne > game.DeckOne.Count || game.PlayedTwo > game.DeckTwo.Count)
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

            List<int> newDeckOne = new (totalCards);
            newDeckOne.AddRange(game.DeckOne.GetRange(0, game.PlayedOne));

            List<int> newDeckTwo = new (totalCards);
            newDeckTwo.AddRange(game.DeckTwo.GetRange(0, game.PlayedTwo));

            Game newGame = new Game(newDeckOne, newDeckTwo, new PartTwoRuleVariants());
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
