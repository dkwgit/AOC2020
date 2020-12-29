namespace AOC2020.Day22
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class PartTwoRuleVariants : IRuleVariants
    {
        private readonly Dictionary<string, bool> _history = new ();

        public GameWinInfo CheckHistoryForWinner(List<int> deckOne, List<int> deckTwo)
        {
            StringBuilder one = new ();
            StringBuilder two = new ();

            foreach (var itemOne in deckOne)
            {
                one.Append(itemOne);
            }

            foreach (var itemTwo in deckTwo)
            {
                two.Append(itemTwo);
            }

            string key = $"{one}-{two}";

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

            Game newGame = new Game(game.DeckOne.GetRange(0, game.PlayedOne), game.DeckTwo.GetRange(0, game.PlayedTwo), new PartTwoRuleVariants());
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
